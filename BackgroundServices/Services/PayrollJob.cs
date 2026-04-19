using Application.Abstraction.Services;
using Timer = System.Threading.Timer;

namespace BackgroundServices.Services
{
    public class PayrollJob : BackgroundService
    {
        //private readonly ILogger<PayrollJob> _logger;
        private readonly Serilog.ILogger _logger;
        private readonly IBackgroundServices _backgroundServices;
        private Timer _timer;

        public PayrollJob(Serilog.ILogger logger, IBackgroundServices backgroundServices)
        {
            _logger = logger;
            _backgroundServices = backgroundServices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information($"PayrollJob started at {DateTime.Now}.");
            DateTime now = DateTime.Now;
            DateTime nextRun = now.Date.AddHours(23).AddMinutes(40); // 11:40 PM

            if (now > nextRun)
            {
                nextRun = nextRun.AddDays(1);
            }

            TimeSpan initialDelay = nextRun - now;

            _timer = new Timer(async _ => await AddPayroll(), null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);

            await Task.Delay(initialDelay); // Wait for the initial delay

            await AddPayroll(); // Run the first time

            _timer.Change(TimeSpan.FromDays(1), TimeSpan.FromDays(1));
            //_timer = new Timer(async _ => await AddPayroll(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private async Task AddPayroll()
        {
            try
            {
                _logger.Information($"PayrollJob executetion start at {DateTime.Now}.");
                await _backgroundServices.AddPayroll();

                _logger.Information($"PayrollJob executed successfully at {DateTime.Now}.");
            }
            catch (Exception ex)
            {
                _logger.Fatal($"Error in PayrollJob: {ex.Message}");
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
