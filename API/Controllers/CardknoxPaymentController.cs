using Application.Handler.CardKnoxPayment.Command.AddCardknoxPaymentMethod;
using Application.Handler.CardKnoxPayment.Command.AddTransactionByCustomerId;
using Application.Handler.CardKnoxPayment.Command.DeleteCardknoxPayment;
using Application.Handler.CardKnoxPayment.Command.RunMonthlyTask;
using Application.Handler.CardKnoxPayment.Queries.GetCardknoxPaymentMethodByFamilyId;
using Application.Handler.CardKnoxPayment.Queries.GetPaymentMethod;
using Application.Handler.CardKnoxPayment.Queries.GetPaymentMethodListByFamilyId;
using Application.Handler.CardKnoxPayment.Queries.GetTransactionByCustomerId;
using Application.Handler.Drivers.Queries;
using Application.Handler.Routes.Queries.GetRoutes;
using Application.Settings;
using CardknoxApi;
using DTO.Request.CardknoxPaymentMethod;
using DTO.Response;
using DTO.Response.CardknoxPaymentMethod;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardknoxPaymentController : BaseController
    {
        private const string ContentType = "application/json";

        private readonly Serilog.ILogger _logger;
        private readonly CardKnoxsettings _settings;
        private readonly HttpClient _recurringHttpClient;
        private readonly HttpClient _transactionHttpClient;

        public CardknoxPaymentController(
            Serilog.ILogger logger,
            IOptions<CardKnoxsettings> settings,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _settings = settings.Value;

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

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetCardknoxPaymentMethodByFamilyId")]
        public async Task<IActionResult> GetCardknoxPaymentMethodByFamilyId([FromBody] GetCardknoxPaymentMethodByFamilyIdRequestDto getCardknoxPaymentMethodByFamilyIdRequestDto)
        {
            var result = await Mediator.Send(new GetCardknoxPaymentMethodByFamilyIdQuery
            {
                CommonRequest = getCardknoxPaymentMethodByFamilyIdRequestDto.CommonRequest,
                FamilyId = getCardknoxPaymentMethodByFamilyIdRequestDto.FamilyId
            });
            return Ok(result);
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("GetTransactionByCustomerId")]
        public async Task<IActionResult> GetTransactionByCustomerId([FromBody] GetTransactionByCustomerIdRequestDto getTransactionByCustomerIdRequestDto)
        {
            var result = await Mediator.Send(new GetTransactionByCustomerIdQuery
            {
                CommonRequest = getTransactionByCustomerIdRequestDto.CommonRequest,
                CustomerId = getTransactionByCustomerIdRequestDto.CustomerId
            });
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AddCardknoxPaymentMethod")]
        public async Task<IActionResult> AddCardknoxPaymentMethod([FromBody] AddCardknoxPaymentMethodDto request)
        {
            try
            {
                _logger.Information("Adding payment method for customer {customerId}", request.CustomerId);

                // ── Validate request ──
                if (string.IsNullOrEmpty(request.CustomerId))
                    return BadRequest(new { Error = "CustomerId is required" });

                var tokenType = request.TokenType ?? "cc";

                if (tokenType != "cc" && tokenType != "ach")
                    return BadRequest(new { Error = "TokenType must be 'cc' or 'ach'" });

                var last4 = request.Last4
                    ?? request.CardNumber?.Substring(request.CardNumber.Length - 4);

                string token;
                string cardType = "";

                // ── STEP 1: Tokenize raw card data via CardKnox Gateway ──
                if (!string.IsNullOrEmpty(request.CardNumber))
                {
                    var tokenizePayload = new Dictionary<string, string>
                    {
                        { "xKey",             _settings.XKey },
                        { "xVersion",         "4.5.9" },
                        { "xSoftwareName",    _settings.xSoftwareName },
                        { "xSoftwareVersion", _settings.xSoftwareVersion },
                        { "xCommand",         "cc:save" },
                        { "xCardNum",         request.CardNumber },
                        { "xCVV",             request.SecurityCode },
                        { "xExp",             request.ExpDate },
                        { "xName",            request.CardHolderName },
                        { "xBillStreet",      request.BillingAddress },
                        { "xBillZip",         request.Zipcode },
                    };

                    _logger.Information("Tokenizing card for customer {customerId}", request.CustomerId);

                    var tokenizeResponse = await _transactionHttpClient.PostAsync(
                        "https://x1.cardknox.com/gateway",
                        new FormUrlEncodedContent(tokenizePayload));

                    var tokenizeContent = await tokenizeResponse.Content.ReadAsStringAsync();
                    _logger.Information("Tokenize response: {content}", tokenizeContent);

                    var tokenizeResult = tokenizeContent
                        .Split('&')
                        .Select(p => p.Split('='))
                        .Where(p => p.Length == 2)
                        .ToDictionary(p => p[0], p => Uri.UnescapeDataString(p[1]));

                    if (!tokenizeResult.TryGetValue("xResult", out var xResult) || xResult != "A")
                    {
                        var errorMsg = tokenizeResult.GetValueOrDefault("xError", "Tokenization failed");
                        _logger.Error("Tokenization failed for customer {customerId}: {error}",
                            request.CustomerId, errorMsg);
                        return BadRequest(new { Error = errorMsg });
                    }

                    token = tokenizeResult["xToken"];
                    cardType = tokenizeResult.GetValueOrDefault("xCardType", "");
                    _logger.Information("Card tokenized successfully for customer {customerId}", request.CustomerId);
                }
                else if (!string.IsNullOrEmpty(request.Token))
                {
                    token = request.Token;
                }
                else
                {
                    return BadRequest(new { Error = "Either CardNumber or Token is required" });
                }

                // ── STEP 2: Register token as payment method in CardKnox Recurring ──
                var payload = new
                {
                    SoftwareName = _settings.xSoftwareName,
                    SoftwareVersion = _settings.xSoftwareVersion,
                    CustomerId = request.CustomerId,
                    Token = token,
                    TokenType = tokenType,
                    TokenAlias = request.TokenAlias ?? $"{tokenType.ToUpper()} ending in {last4}",
                    Exp = request.ExpDate,
                    Name = request.CardHolderName,
                    Street = request.BillingAddress,
                    Zip = request.Zipcode,
                    SetAsDefault = request.IsDefault
                };

                _logger.Information("Calling CreatePaymentMethod for customer {customerId}", request.CustomerId);

                var response = await _recurringHttpClient.PostAsync(
                    "CreatePaymentMethod",
                    new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, ContentType));

                var content = await response.Content.ReadAsStringAsync();
                _logger.Information("CreatePaymentMethod response: {content}", content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.Error("CreatePaymentMethod failed: {content}", content);
                    return StatusCode((int)response.StatusCode, new { Error = content });
                }

                var result = JsonConvert.DeserializeObject<CreatePaymentMethodResponse>(content);

                if (result?.Result == "S")
                {
                    // ── STEP 3: Set as default in CardKnox if requested ──
                    // ── STEP 3: Set as default in CardKnox if requested ──
                    if (request.IsDefault)
                    {
                        // ── GET payment method to retrieve Revision ──
                        var getPayload = new
                        {
                            SoftwareName = _settings.xSoftwareName,
                            SoftwareVersion = _settings.xSoftwareVersion,
                            PaymentMethodId = result.PaymentMethodId
                        };

                        _logger.Information("GetPaymentMethod request: {payload}",
                            JsonConvert.SerializeObject(getPayload));

                        var getResponse = await _recurringHttpClient.PostAsync(
                            "GetPaymentMethod",
                            new StringContent(JsonConvert.SerializeObject(getPayload), Encoding.UTF8, ContentType));

                        var getContent = await getResponse.Content.ReadAsStringAsync();
                        _logger.Information("GetPaymentMethod raw response: {content}", getContent);

                        var getResult = JsonConvert.DeserializeObject<GetPaymentMethodResponse>(getContent);

                        _logger.Information("GetPaymentMethod parsed - Result:{result} Revision:{revision} Error:{error}",
                            getResult?.Result, getResult?.Revision, getResult?.Error);

                        if (getResult?.Result != "S")
                        {
                            _logger.Warning("GetPaymentMethod failed: {error}", getResult?.Error);
                        }
                        else
                        {
                            // ── UPDATE with Revision ──
                            var updatePayload = new
                            {
                                SoftwareName = _settings.xSoftwareName,
                                SoftwareVersion = _settings.xSoftwareVersion,
                                PaymentMethodId = result.PaymentMethodId,
                                Revision = getResult.Revision,
                                SetAsDefault = true
                            };

                            _logger.Information("UpdatePaymentMethod request: {payload}",
                                JsonConvert.SerializeObject(updatePayload));

                            var updateResponse = await _recurringHttpClient.PostAsync(
                                "UpdatePaymentMethod",
                                new StringContent(JsonConvert.SerializeObject(updatePayload), Encoding.UTF8, ContentType));

                            var updateContent = await updateResponse.Content.ReadAsStringAsync();
                            _logger.Information("UpdatePaymentMethod raw response: {content}", updateContent);

                            var updateResult = JsonConvert.DeserializeObject<UpdatePaymentMethodResponse>(updateContent);

                            _logger.Information("UpdatePaymentMethod parsed - Result:{result} Error:{error}",
                                updateResult?.Result, updateResult?.Error);

                            if (updateResult?.Result != "S")
                            {
                                _logger.Warning("Failed to set as default: {error}", updateResult?.Error);
                            }
                        }
                    }

                    // ── STEP 4: Save to database via MediatR ──
                    var command = request.Adapt<AddCardknoxPaymentMethodCommand>();
                    command.Token = token;
                    command.Last4 = last4;
                    command.TokenType = tokenType;
                    command.CardType = cardType;
                    command.PaymentMethodId = result.PaymentMethodId;

                    var savedId = await Mediator.Send(command);

                    _logger.Information(
                        "Successfully added payment method for customer {customerId}, DB Id: {id}",
                        request.CustomerId, savedId);

                    return Ok(new
                    {
                        Success = true,
                        PaymentMethodId = result.PaymentMethodId,
                        DatabaseId = savedId,
                        Message = "Payment method added successfully"
                    });
                }
                else
                {
                    _logger.Error("CreatePaymentMethod error: {error}", result?.Error);
                    return BadRequest(new { Error = result?.Error ?? "Unknown error from CardKnox" });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception adding payment method for customer {customerId}", request.CustomerId);
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("DeleteCardknoxPayment")]
        public async Task<IActionResult> DeleteCardknoxPayment([FromQuery] DeleteCardknoxPaymentRequestDto deleteCardknoxPaymentRequestDto)
        {

            try
            {
                _logger.Information("Deleting payment method {paymentMethodId}", deleteCardknoxPaymentRequestDto.PaymentMethodId);

                var request = new
                {
                    SoftwareName = _settings.xSoftwareName,
                    SoftwareVersion = _settings.xSoftwareVersion,
                    PaymentMethodId = deleteCardknoxPaymentRequestDto.PaymentMethodId
                };

                var response = await _recurringHttpClient.PostAsync(
                    "DeletePaymentMethod",
                    new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, ContentType));

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.Error("DeletePaymentMethod failed: {content}", content);
                    return StatusCode((int)response.StatusCode, new { Error = content });
                }

                var result = JsonConvert.DeserializeObject<DeletePaymentMethodResponse>(content);

                if (result.Result == "E")
                {
                    _logger.Error("DeletePaymentMethod error: {error}", result.Error);
                    return BadRequest(new { Error = result.Error });
                } 

                _logger.Information("Successfully deleted payment method {paymentMethodId}", deleteCardknoxPaymentRequestDto.PaymentMethodId);

                return Ok(await Mediator.Send(deleteCardknoxPaymentRequestDto.Adapt<DeleteCardknoxPaymentCommand>()));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception deleting payment method {paymentMethodId}", deleteCardknoxPaymentRequestDto.PaymentMethodId);
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }

        }

        [HttpGet("customer/{customerId}/payment-methods")]
        public async Task<IActionResult> GetCustomerPaymentMethods(string customerId)
        {
            try
            {
                _logger.Information("Getting payment methods for customer {customerId}", customerId);

                var request = new
                {
                    SoftwareName = _settings.xSoftwareName,
                    SoftwareVersion = _settings.xSoftwareVersion,
                    CustomerId = customerId
                };

                var response = await _recurringHttpClient.PostAsync(
                    "GetCustomer",
                    new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, ContentType));

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.Error("GetCustomer failed for {customerId}: {content}", customerId, content);
                    return StatusCode((int)response.StatusCode, new { Error = content });
                }

                var result = JsonConvert.DeserializeObject<GetCustomerResponse>(content);

                if (result.Result == "E")
                {
                    _logger.Error("GetCustomer error: {error}", result.Error);
                    return BadRequest(new { Error = result.Error });
                }

                return Ok(new
                {
                    Success = true,
                    CustomerId = customerId,
                    PaymentMethods = result.PaymentMethods ?? new List<PaymentMethodDto>()
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception getting payment methods for customer {customerId}", customerId);
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        [HttpDelete("payment-method/{paymentMethodId}")]
        public async Task<IActionResult> DeletePaymentMethod(string paymentMethodId)
        {
            try
            {
                _logger.Information("Deleting payment method {paymentMethodId}", paymentMethodId);

                var request = new
                {
                    SoftwareName = _settings.xSoftwareName,
                    SoftwareVersion = _settings.xSoftwareVersion,
                    PaymentMethodId = paymentMethodId
                };

                var response = await _recurringHttpClient.PostAsync(
                    "DeletePaymentMethod",
                    new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, ContentType));

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.Error("DeletePaymentMethod failed: {content}", content);
                    return StatusCode((int)response.StatusCode, new { Error = content });
                }

                var result = JsonConvert.DeserializeObject<DeletePaymentMethodResponse>(content);

                if (result.Result == "E")
                {
                    _logger.Error("DeletePaymentMethod error: {error}", result.Error);
                    return BadRequest(new { Error = result.Error });
                }

                _logger.Information("Successfully deleted payment method {paymentMethodId}", paymentMethodId);

                return Ok(new
                {
                    Success = true,
                    Message = "Payment method deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception deleting payment method {paymentMethodId}", paymentMethodId);
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("AddTransactionByCustomerId")]
        public async Task<IActionResult> AddTransactionByCustomerId([FromBody] AddTransactionByCustomerIdRequestDto addTransactionByCustomerIdRequestDto)
        {
            var result = await Mediator.Send(new AddTransactionByCustomerIdQuery
            {
                CustomerId = addTransactionByCustomerIdRequestDto.CustomerId,
                Amount = addTransactionByCustomerIdRequestDto.Amount,
                Description = addTransactionByCustomerIdRequestDto.Description,
                IsManual = addTransactionByCustomerIdRequestDto.IsManual,
                PaymentMethodId = addTransactionByCustomerIdRequestDto.PaymentMethodId,
                CheckNumber = addTransactionByCustomerIdRequestDto.CheckNumber,
                CheckDate = addTransactionByCustomerIdRequestDto.CheckDate,
                ChargeId = addTransactionByCustomerIdRequestDto.ChargeId

            });
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RunMonthlyTask")]
        public async Task<IActionResult> RunMonthlyTask()
        {
            var result = await Mediator.Send(new RunMonthlyTaskCommand());
            return Ok(result);
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetPaymentMethod")]
        public async Task<IActionResult> GetPaymentMethod([FromQuery] string customerId)
        {
            var result = await Mediator.Send(new GetPaymentMethodQuery
            {
                CusomerId = customerId
            });
            return Ok(result);
        }

        [HttpGet]
        //[AllowAnonymous]
        [Route("GetPaymentMethodListByFamilyId")]
        public async Task<IActionResult> GetPaymentMethodListByFamilyId([FromQuery] int familyId)
        {
            var result = await Mediator.Send(new GetPaymentMethodListByFamilyIdQuery
            {
                FamilyId = familyId
            });
            return Ok(result);
        }


    }
}