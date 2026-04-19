using Application.Abstraction.Services;
using DTO.Response.BusStatus;

namespace API.BackgroundServices
{
    public class MarkStopPassedForRoutesService : BackgroundService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IBackgroundServices _backgroundServices;
        private readonly IBusDetailsService _busDetailsService;
        private System.Threading.Timer _timer;

        public MarkStopPassedForRoutesService(Serilog.ILogger logger, IBackgroundServices backgroundServices, IBusDetailsService busDetailsService)
        {
            _logger = logger;
            _backgroundServices = backgroundServices;
            _busDetailsService = busDetailsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information($"Service started at {DateTime.Now}.");

            await ScheduleDailyRun(stoppingToken);
        }

        private async Task ScheduleDailyRun(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                //var nextRun = now.Date.AddHours(23); // 11:00 PM production
                var nextRun = now.AddSeconds(10); //every 10 sec testing

                // If time already passed today, schedule for tomorrow
                if (now > nextRun)
                    nextRun = nextRun.AddDays(1);

                var delay = nextRun - now;

                _logger.Information($"Next run scheduled at {nextRun}");

                // wait until next run
                await Task.Delay(delay, stoppingToken);

                // execute job
                await MarkStopPassedForRoutes();

                _logger.Information("Executed MarkStopPassedForRoutes");
            }
        }


        private async Task MarkStopPassedForRoutes()
        {
            try
            {
                var todayRoutesRes = await _backgroundServices.GetTodayUpcomingRoutes();
                if (todayRoutesRes?.Any() == true)
                    {
                    foreach (var route in todayRoutesRes)
                    {
                        List<BusPosition> recentPositions;
                        try
                        {
                            var recentPositionsResult = _busDetailsService.GetRecentBusPositions(route.TodaysBusName, route.RouteID);
                            recentPositions = recentPositionsResult.Result?.Data?.ToList() ?? new List<BusPosition>();
                        }
                        catch
                        {
                            recentPositions = new List<BusPosition>();
                        }
                        List<(double lat, double lng)> routePathList = new List<(double lat, double lng)>();
                        try
                        {
                            routePathList = route.BusStopLatLong
                                .Split('|', StringSplitOptions.RemoveEmptyEntries)
                                .Select(c =>
                                {
                                    var parts = c.Split(',');
                                    return (lat: double.Parse(parts[0]), lng: double.Parse(parts[1]));
                                }).ToList();
                        }
                        catch
                        {
                            _logger.Information("Error finding route path");
                        }

                        int lastPassedStop = GetLastPassedStop(routePathList, recentPositions);

                        if (lastPassedStop >= 0)
                        {
                            await _backgroundServices.InsertStopPassedByBusOnRoute(route.RouteID,route.RouteDate, lastPassedStop + 1);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Fatal($"Error in Create Route Or Missing EndDate Service: {ex.Message}");
            }
        }

        private int GetLastPassedStop(List<(double lat, double lng)> route, List<BusPosition> history)
        {
            int lastPassedStop = -1;

            // 1. Check historical positions to find stops the bus has definitely passed
            if (history != null && history.Count > 0)
            {
                history.Reverse();
                foreach (var pos in history)
                {
                    int nextStop = lastPassedStop + 1;
                    // If lastPassedStop = -1, then nextStop = 0 (first stop)
                    // If lastPassedStop = 0, then nextStop = 1 (second stop)
                    // If lastPassedStop = 1, then nextStop = 2 (third stop), etc.

                    if (nextStop < route.Count)
                    {
                        double distance = HaversineDistance(route[nextStop], (pos.Latitude, pos.Longitude));

                        if (distance < 50)
                        {
                            lastPassedStop = nextStop;
                        }
                    }
                }
            }

            return lastPassedStop;
        }

        private double HaversineDistance((double lat, double lng) p1, (double lat, double lng) p2)
        {
            const double R = 6371000; // Earth radius in meters
            double lat1Rad = p1.lat * Math.PI / 180.0;
            double lat2Rad = p2.lat * Math.PI / 180.0;
            double dLat = (p2.lat - p1.lat) * Math.PI / 180.0;
            double dLon = (p2.lng - p1.lng) * Math.PI / 180.0;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // distance in meters
        }
    }
}
