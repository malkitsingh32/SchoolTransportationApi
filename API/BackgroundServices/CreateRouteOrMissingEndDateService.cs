
using Application.Abstraction.Services;
using Timer = System.Threading.Timer;
namespace API.BackgroundServices
{
    public class CreateRouteOrMissingEndDateService : BackgroundService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IBackgroundServices _backgroundServices;
        private Timer _timer;

        public CreateRouteOrMissingEndDateService(Serilog.ILogger logger, IBackgroundServices backgroundServices)
        {
            _logger = logger;
            _backgroundServices = backgroundServices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information($"Service started at {DateTime.Now}.");

            //await ScheduleDailyRun(stoppingToken);
            _timer = new Timer(async _ => await GetRouteOrMissingEndDate(), null, TimeSpan.Zero, TimeSpan.FromMinutes(59));
        }
        private async Task GetRouteOrMissingEndDate()
        {
            try
            {
                await _backgroundServices.InsertNextDayRouteDetails();
            }
            catch (Exception ex)
            {
                _logger.Fatal($"Error in Create Route Or Missing EndDate Service: {ex.Message}");
            }
        }
    }
}
