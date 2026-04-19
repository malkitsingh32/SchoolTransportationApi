using Application.Abstraction.DataBase;
using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BackgroundServices.Services;
using Infrastructure.Implementation.Repositories;
using Infrastructure.Implementation.Services;
using Persistence.DataBase;
using Serilog;
using Serilog.Events;

string logDirectory = Path.Combine(AppContext.BaseDirectory, "..", "..", "logs"); 
Directory.CreateDirectory(logDirectory);
string logFilePath = Path.Combine(logDirectory, "log.txt");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(logDirectory, rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Logger.Information("Logging Started");
var builder = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .UseSerilog()
    .ConfigureServices((hostContext, services) =>
    {
        //services.AddHostedService<HistoryJob>();
        //services.AddHostedService<PayrollJob>();
        services.AddHostedService<BusLocationJob>();
    })
    .ConfigureContainer<ContainerBuilder>((hostContext, containerBuilder) =>
    {
        containerBuilder.RegisterInstance(Log.Logger)
                       .As<Serilog.ILogger>()
                       .SingleInstance();
        containerBuilder.RegisterType<DbContext>().As<IDbContext>().InstancePerDependency(); 
        containerBuilder.RegisterType<ParameterManager>().As<IParameterManager>().InstancePerDependency();

        containerBuilder.RegisterType<BackgroundRepository>()
                        .As<IBackgroundRepository>()
                        .InstancePerDependency();

        containerBuilder.RegisterType<BackgroundsServices>()
                        .As<IBackgroundServices>()
                        .InstancePerDependency();
    });

var host = builder.Build();
host.Run();
