using Application.Abstraction.Services;
using Application.Settings;
using Domain.Entities;
using DTO.Response.CardknoxCustomers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Implementation.Services
{
    public class CardknoxMonthlyTaskService : ICardknoxMonthlyTaskService
    {
        private const string ContentType = "application/json-patch+json";
        private readonly Serilog.ILogger _logger;
        private readonly IBackgroundServices _backgroundServices;
        private readonly CardKnoxsettings _settings;
        private readonly HttpClient _httpClient;
        private readonly IBillingService _billingService;
        private readonly IPaymentTransactionService _paymentService;
        private readonly IPaymentHistoryService _paymentHistoryService;
        private readonly ICardknoxService _cardknoxService;

        public CardknoxMonthlyTaskService(
            Serilog.ILogger logger,
            IBackgroundServices backgroundServices,
            IOptions<CardKnoxsettings> settings,
            IHttpClientFactory httpClientFactory,
            IBillingService billingService,
            IPaymentTransactionService paymentService,
            IPaymentHistoryService paymentHistoryService,
            ICardknoxService cardknoxService)
        {
            _logger = logger;
            _backgroundServices = backgroundServices;
            _settings = settings.Value;
            _billingService = billingService;
            _paymentService = paymentService;
            _paymentHistoryService = paymentHistoryService;
            _cardknoxService = cardknoxService;
            _httpClient = httpClientFactory.CreateClient("Cardknox");
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.Token);
            _httpClient.DefaultRequestHeaders.Add("X-Recurring-Api-Version", "2.1");
        }

        public async Task RunMonthlyTask(CancellationToken cancellationToken)
        {
            try
            {
                await SyncCustomers();
                _logger.Information("Running transaction-based billing job at {time}", DateTime.Now);

                var customers = await _billingService.GetCustomersToCharge();

                foreach (var charge in customers)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    if (await _paymentHistoryService.AlreadyChargedThisMonth(charge.CustomerId))
                    {
                        _logger.Information("Skipping {customerId} - already charged this month", charge.CustomerId);
                        continue;
                    }

                    if (await _paymentHistoryService.HasPendingFailure(charge.CustomerId))
                    {
                        _logger.Information("Skipping {customerId} - has pending failed retry", charge.CustomerId);
                        continue;
                    }

                    try
                    {
                        if (string.IsNullOrEmpty(charge.CustomerId))
                        {
                            _logger.Warning("Skipping - CustomerId is null");
                            continue;
                        }

                        if (charge.TotalCharge <= 0)
                        {
                            _logger.Information("Skipping {customerId} - zero amount", charge.CustomerId);
                            continue;
                        }

                        _logger.Information("Charging customer {customerId} amount {amount}",
                            charge.CustomerId, charge.TotalCharge);

                        var response = await _cardknoxService.GetPaymentMethod(charge.CustomerId);
                        var cards = response?.Data?
                                    .OrderByDescending(c => c.IsDefault)
                                    .ToList();

                        if (cards == null || !cards.Any())
                        {
                            await _paymentHistoryService.SaveFailure(
                                charge.CustomerId,
                                charge.TotalCharge,
                                "No card available for this customer",
                                null,
                                "Monthly Charges",
                                false,
                                1,
                                null,
                                null,false, charge.ChargeId, "Declined");

                            continue;
                        }

                        foreach (var card in cards)
                        {
                            var result = await _paymentService.ProcessPaymentAsync(
                                charge.CustomerId,
                                charge.TotalCharge,
                                "Monthly Charges",
                                card.PaymentMethodId);

                            if (result.IsSuccess)
                            {
                                await _paymentHistoryService.SaveSuccess(
                                    charge.CustomerId,
                                    charge.TotalCharge,
                                    result.GatewayRefNum,
                                    result.FullResponse,
                                    "Monthly Charges",
                                    false,
                                    null,
                                    null,
                                    card.PaymentMethodId,
                                null, false,charge.ChargeId, result.Status);
                                break;
                            }
                            else
                            {
                                await _paymentHistoryService.SaveFailure(
                                    charge.CustomerId,
                                    charge.TotalCharge,
                                    result.Error,
                                    result.FullResponse,
                                    "Monthly Charges",
                                    false,
                                    1,
                                    card.PaymentMethodId,
                                null, false,  charge.ChargeId, result.Status);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Error processing customer {customerId}", charge.CustomerId);
                    }
                }

                var failedPayments = await _paymentHistoryService.GetFailedPaymentsToRetry();

                foreach (var item in failedPayments)
                {
                    try
                    {
                        if (await _paymentHistoryService.AlreadyChargedThisMonth(item.CustomerId))
                        {
                            _logger.Information("Skipping retry for {customerId} - already charged", item.CustomerId);
                            continue;
                        }

                        _logger.Information("Retrying payment for {customerId}", item.CustomerId);
                        var response = await _cardknoxService.GetPaymentMethod(item.CustomerId);
                        var cards = response?.Data?
                                    .OrderByDescending(c => c.IsDefault)
                                    .ToList();

                        if (cards == null || !cards.Any())
                        {
                            await _paymentHistoryService.IncrementRetry(item.Id);
                            continue;
                        }

                        bool isSuccess = false;
                        foreach (var card in cards)
                        {
                            var result = await _paymentService.ProcessPaymentAsync(item.CustomerId, item.Amount, "Monthly Charges", card.PaymentMethodId);

                            if (result.IsSuccess)
                            {
                                await _paymentHistoryService.SaveSuccess(
                                    item.CustomerId,
                                    item.Amount,
                                    result.GatewayRefNum,
                                    result.FullResponse,
                                    "Monthly Charges",
                                    false,
                                    null,
                                    null,
                                    card.PaymentMethodId,
                                null, false, item.ChargeId, result.Status);
                                isSuccess = true;
                                break;
                            }
                            else
                            {
                                await _paymentHistoryService.SaveFailure(
                                    item.CustomerId,
                                    item.Amount,
                                    result.Error,
                                    result.FullResponse,
                                    "Monthly Charges",
                                    false,
                                    item.AttemptCount,
                                    card.PaymentMethodId,
                                null, false, item.ChargeId, result.Status);
                            }
                        }

                        if (!isSuccess)
                        {
                            await _paymentHistoryService.IncrementRetry(item.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Retry failed for {customerId}", item.CustomerId);
                    }
                }

                _logger.Information("Billing job completed at {time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Billing job failed");
                throw;
            }
        }

        private async Task SyncCustomers()
        {
            string nextToken = null;
            int totalCustomers = 0;

            _logger.Information("Starting customer sync");

            do
            {
                try
                {
                    var request = new
                    {
                        SoftwareName = _settings.xSoftwareName,
                        SoftwareVersion = _settings.xSoftwareVersion,
                        PageSize = 50,
                        NextToken = nextToken
                    };

                    var response = await _httpClient.PostAsync(
                        "ListCustomers",
                        new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, ContentType));

                    var content = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.Error("ListCustomers failed with status {status}: {content}",
                            response.StatusCode, content);
                        throw new HttpRequestException($"ListCustomers failed: {response.StatusCode}");
                    }

                    var result = JsonConvert.DeserializeObject<CustomerResponseDto>(content);

                    if (result?.Customers != null && result.Customers.Any())
                    {
                        await _backgroundServices.AddCardknoxCustomers(result.Customers);
                        totalCustomers += result.Customers.Count();
                        _logger.Information("Synced {count} customers (total: {total})",
                            result.Customers.Count(), totalCustomers);
                    }

                    nextToken = result?.NextToken;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error during customer sync at page with token {token}", nextToken);
                    throw;
                }

            } while (!string.IsNullOrEmpty(nextToken));

            _logger.Information("Customer sync completed. Total customers synced: {total}", totalCustomers);
        }
    }
}
