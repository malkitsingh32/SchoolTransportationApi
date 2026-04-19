using Application.Abstraction.Services;
using Application.Settings;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response.CardknoxPaymentMethod;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
        private readonly HttpClient _httpClient;
        private readonly CardKnoxsettings _settings;
        private readonly Serilog.ILogger _logger;
        private readonly HttpClient _recurringHttpClient;
        private readonly HttpClient _transactionHttpClient;
        public PaymentTransactionService(
            IHttpClientFactory httpClientFactory,
            IOptions<CardKnoxsettings> settings,
            Serilog.ILogger logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _settings = settings.Value;
            _logger = logger;

            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.Token);
            _httpClient.DefaultRequestHeaders.Add("X-Recurring-Api-Version", "2.1");

            // Recurring API client
            _recurringHttpClient = httpClientFactory.CreateClient("CardknoxRecurring");
            _recurringHttpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _recurringHttpClient.DefaultRequestHeaders.Add("Authorization", _settings.Token);
            _recurringHttpClient.DefaultRequestHeaders.Add("X-Recurring-Api-Version", "2.1");
            _recurringHttpClient.DefaultRequestHeaders.Add("X-Ifields-Key", _settings.Ifields);
            // Transaction API client (for cc:save to convert SUT tokens)
            _transactionHttpClient = httpClientFactory.CreateClient("CardknoxTransaction");
            _transactionHttpClient.BaseAddress = new Uri(_settings.PaymentGatewayApiUrl);
        }

        public async Task<PaymentResultDto> ProcessPaymentAsync(string customerId, decimal amount, string? description, string paymentMethodId)
        {
            var payload = new Dictionary<string, object?>
            {
                ["SoftwareName"] = _settings.xSoftwareName,
                ["SoftwareVersion"] = _settings.xSoftwareVersion,
                ["Amount"] = amount,
            };

            if (!string.IsNullOrWhiteSpace(paymentMethodId))
            {
                // Use PaymentMethodId only — do NOT send CustomerId together
                payload["PaymentMethodId"] = paymentMethodId;
            }
            else
            {
                // No PaymentMethodId, fall back to CustomerId
                payload["CustomerId"] = customerId;
            }

            if (!string.IsNullOrWhiteSpace(description))
                payload["Description"] = description;

            var response = await _httpClient.PostAsync(
                "ProcessTransaction",
                new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(content);

            return new PaymentResultDto
            {
                IsSuccess = result?.GatewayStatus == "Approved",
                GatewayRefNum = result?.GatewayRefNum,
                Status = result?.GatewayStatus,
                Error = result?.GatewayErrorMessage ?? result?.Error,
                FullResponse = content
            };
        }

        public async Task<PaymentResultDto> ProcessPaymentWithCardAsync(
    string customerId, decimal amount, string? description,
    string cardNumber, string expiryDate, string cvv, string cardHolderName)
        {
            var tokenizePayload = new Dictionary<string, string>
            {
                { "xKey",             _settings.XKey                },
                { "xVersion",         "4.5.9"                       },
                { "xSoftwareName",    _settings.xSoftwareName       },
                { "xSoftwareVersion", _settings.xSoftwareVersion    },
                { "xCommand",         "cc:sale"                     }, // charge immediately
                { "xCardNum",         cardNumber                    },
                { "xCVV",             cvv                           },
                { "xExp",             expiryDate                    },
                { "xName",            cardHolderName                },
                { "xAmount",          amount.ToString("F2")         }, // amount must be here
            };

            if (!string.IsNullOrWhiteSpace(description))
                tokenizePayload["xDescription"] = description;

            if (!string.IsNullOrWhiteSpace(customerId))
                tokenizePayload["xCustom01"] = customerId;

            var response = await _transactionHttpClient.PostAsync(
                "https://x1.cardknox.com/gateway",
                new FormUrlEncodedContent(tokenizePayload));  // must be FormUrlEncoded

            var content = await response.Content.ReadAsStringAsync();

            // Gateway returns key=value pairs, NOT JSON
            var result = content
                .Split('&')
                .Select(p => p.Split('='))
                .Where(p => p.Length == 2)
                .ToDictionary(p => p[0], p => Uri.UnescapeDataString(p[1]));

            var xResult = result.GetValueOrDefault("xResult", "");

            return new PaymentResultDto
            {
                IsSuccess = xResult == "A",
                GatewayRefNum = result.GetValueOrDefault("xRefNum", ""),
                Status = result.GetValueOrDefault("xStatus", ""),
                Error = xResult != "A"
                                    ? result.GetValueOrDefault("xError", "Transaction failed")
                                    : null,
                FullResponse = content
            };
        }
    }

}
