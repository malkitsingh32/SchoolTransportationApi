using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Infrastructure.Implementation.Repositories;
using Infrastructure.Implementation.Services;
using Serilog;

namespace API
{
    public static class DependencyInjection
    {
        public static void AddBackgroundServiceDI(this IServiceCollection services)
        {
            services.AddSingleton(Log.Logger);
            services.AddSingleton<IBackgroundServices, BackgroundsServices>();
            services.AddSingleton<IBackgroundRepository, BackgroundRepository>();
        }
    }
}
