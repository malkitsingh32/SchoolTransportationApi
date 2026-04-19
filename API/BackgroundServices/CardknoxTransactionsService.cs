using Application.Abstraction.Services;
using Application.Settings;
using DTO.Response.CardknoxCustomers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Timer = System.Threading.Timer;

namespace API.BackgroundServices
{
    public class CardknoxTransactionsService : BackgroundService
    {
        private const string ContentType = "application/json-patch+json";
        private readonly Serilog.ILogger _logger;
        private readonly IBackgroundServices _backgroundServices;
        private readonly CardKnoxsettings _settings;
        private readonly HttpClient _httpClient;
        private Timer _timer;
        public CardknoxTransactionsService(
            Serilog.ILogger logger,
            IBackgroundServices backgroundServices,
            IOptions<CardKnoxsettings> settings,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _backgroundServices = backgroundServices;
            _settings = settings.Value;
            _httpClient = httpClientFactory.CreateClient("Cardknox");

            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.Token);
            _httpClient.DefaultRequestHeaders.Add("X-Recurring-Api-Version", "2.1");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information("Cardknox Service Started at {time}", DateTime.Now);
            _timer = new Timer(async _ => await SyncTransactions(stoppingToken), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private async Task SyncTransactions(CancellationToken cancellationToken)
        {
            string nextToken = null;
            int totalTransactions = 0;

            _logger.Information("Starting transaction sync");

            do
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

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
                        "ListTransactions",
                        new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, ContentType),
                        cancellationToken);

                    var content = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.Error("ListTransactions failed with status {status}: {content}",
                            response.StatusCode, content);
                        throw new HttpRequestException($"ListTransactions failed: {response.StatusCode}");
                    }

                    var result = JsonConvert.DeserializeObject<TransactionsResponseDto>(content);

                    if (result?.Transactions != null && result.Transactions.Any())
                    {
                        await _backgroundServices.AddTransaction(result.Transactions);
                        totalTransactions += result.Transactions.Count();
                        _logger.Information("Synced {count} transactions (total: {total})",
                            result.Transactions.Count(), totalTransactions);
                    }

                    nextToken = result?.NextToken;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error during transaction sync at page with token {token}", nextToken);
                    throw;
                }

            } while (!string.IsNullOrEmpty(nextToken));

            _logger.Information("Transaction sync completed. Total transactions synced: {total}", totalTransactions);
        }
    }
}
