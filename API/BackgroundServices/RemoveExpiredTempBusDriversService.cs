using Application.Abstraction.Services;
using Timer = System.Threading.Timer;

namespace API.BackgroundServices
{
    public class RemoveExpiredTempBusDriversService : BackgroundService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IBackgroundServices _backgroundServices;
        private Timer _timer;

        public RemoveExpiredTempBusDriversService(Serilog.ILogger logger, IBackgroundServices backgroundServices)
        {
            _logger = logger;
            _backgroundServices = backgroundServices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information($"Service started at {DateTime.Now}.");

            _timer = new Timer(async _ => await RemoveExpiredTempBusDrivers(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private async Task RemoveExpiredTempBusDrivers()
        {
            try
            {
                await _backgroundServices.RemoveExpiredTempBusDrivers();
            }
            catch (Exception ex)
            {
                _logger.Fatal($"Error in Create Route Or Missing EndDate Service: {ex.Message}");
            }
        }
    }
}


