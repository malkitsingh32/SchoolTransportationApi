using Application.Abstraction.Services;
using DTO.Request.BackgroundServices;
using System.Net.Http.Headers;
using System.Text.Json;
using Timer = System.Threading.Timer;

namespace BackgroundServices.Services
{
    public class BusLocationJob : BackgroundService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IBackgroundServices _backgroundServices;
        private Timer _timer;

        public BusLocationJob(Serilog.ILogger logger, IBackgroundServices backgroundServices)
        {
            _logger = logger;
            _backgroundServices = backgroundServices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information($"  started at {DateTime.Now}.");
            _timer = new Timer(async _ => await GetBusesLocationFromSamSara(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private async Task GetBusesLocationFromSamSara()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "samsara_api_y7jWodH0N0o9cihkewr3VCTgjO932V");

                    string apiUrl = "https://api.samsara.com/fleet/vehicles/stats?types=gps";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var busData = JsonSerializer.Deserialize<BusLocationResponseDto>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        await _backgroundServices.AddBuLocationData(busData);

                    }
                    else
                    {
                        _logger.Error($"Failed to fetch bus locations. Status Code: {response.StatusCode}");
                    }
                }

                _logger.Information($"Bus Location job executed successfully at {DateTime.Now}.");
            }
            catch (Exception ex)
            {
                _logger.Fatal($"Error in Bus Location Job: {ex.Message}");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
