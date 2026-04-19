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

        //Target: Saturday at 11:40 PM
            DayOfWeek targetDay = DayOfWeek.Saturday;
            TimeSpan targetTime = new TimeSpan(23, 40, 0);

            ////To test
            //DayOfWeek targetDay = now.DayOfWeek;
            //TimeSpan targetTime = new TimeSpan(16, 33, 0); // 1:26 PM

            // Calculate the next occurrence of Saturday at 11:40 PM
            int daysUntilNextRun = ((int)targetDay - (int)now.DayOfWeek + 7) % 7;
            DateTime nextRun = now.Date.AddDays(daysUntilNextRun).Add(targetTime);

            if (now > nextRun)
            {
                nextRun = nextRun.AddDays(7);
            }

            TimeSpan initialDelay = nextRun - now;

            _logger.Information($"Next payroll run scheduled for: {nextRun}");

            _timer = new Timer(async _ => await AddPayroll(), null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);

            await Task.Delay(initialDelay, stoppingToken); // Wait until first run

            await AddPayroll(); // Run once on schedule

            // Repeat every 7 days (weekly)
            _timer.Change(TimeSpan.FromDays(7), TimeSpan.FromDays(7));
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
