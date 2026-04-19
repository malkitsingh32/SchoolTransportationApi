using Application.Abstraction.Services;
using DTO.Request.BackgroundServices;
using Timer = System.Threading.Timer;

namespace BackgroundServices.Services
{
    public class HistoryJob : BackgroundService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IBackgroundServices _backgroundServices;
        private Timer _timer;

        public HistoryJob(Serilog.ILogger logger, IBackgroundServices backgroundServices)
        {
            _logger = logger;
            _backgroundServices = backgroundServices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information($"History job started at {DateTime.Now}.");
            DateTime now = DateTime.Now;
            DateTime nextRun = now.Date.AddHours(23).AddMinutes(30);


            if (now > nextRun)
            {
                nextRun = nextRun.AddDays(1);
            }

            TimeSpan initialDelay = nextRun - now;

            _timer = new Timer(async _ => await AddHistory(), null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);


            await Task.Delay(initialDelay, stoppingToken);


            await AddHistory();
            _timer.Change(TimeSpan.FromDays(1), TimeSpan.FromDays(1));
            //_timer = new Timer(async _ => await AddHistory(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private async Task AddHistory()
        {
            try
            {
                _logger.Information($"History job executetion start at {DateTime.Now}.");
                var history = await _backgroundServices.GetHistory();
                var addHistoryRequestDto = new AddHistoryRequestDto { History = history };
                await _backgroundServices.AddHistory(addHistoryRequestDto);

                _logger.Information($"History job executed successfully at {DateTime.Now}.");
            }
            catch (Exception ex)
            {
                _logger.Fatal($"Error in HistoryJob: {ex.Message}");
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
