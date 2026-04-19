using API;
using API.Adapter;
using API.BackgroundServices;
using Application.Adapter;
using Application.Settings;
using BackgroundServices.Services;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Middlewares;
using Persistence;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Configuration;
using System.Text;
using TTS_API.Services;


var builder = WebApplication.CreateBuilder(args);
var myCors = "_myAllowSpecificOrigins";

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

ApiMapsterMappings.Configure();
ApplicationMapsterMappings.Configure();

//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCors,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200","https://localhost:4200", "https://darkaiyosher.datavanced.com", "http://darkaiyosher.datavanced.com", "http://betadarkaiyosher.datavanced.com", "https://betadarkaiyosher.datavanced.com",
                              "https://betadarkaiyosherapi.datavanced.com", "http://betadarkaiyosherapi.datavanced.com", "https://portal.darkeiyosher.org", "http://portal.darkeiyosher.org", "*").AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithExposedHeaders("Content-Disposition"); ;
                      });
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(myCors, policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Application"));
builder.Services.AddInfrastructure();
builder.Services.AddPersistence();
builder.Services.AddBackgroundServiceDI();
builder.Services.AddScoped<PayrollJob>();
builder.Services.AddHostedService<PayrollJob>();
builder.Services.AddScoped<BusLocationJob>();
builder.Services.AddHostedService<BusLocationJob>();
builder.Services.AddScoped<CreateRouteOrMissingEndDateService>();
builder.Services.AddHostedService<CreateRouteOrMissingEndDateService>();
builder.Services.AddScoped<RemoveExpiredTempBusDriversService>();
builder.Services.AddHostedService<RemoveExpiredTempBusDriversService>();
builder.Services.AddScoped<CardknoxService>();
builder.Services.AddHttpClient("Cardknox");
builder.Services.AddHttpClient("Cardknox", client =>
{
    client.BaseAddress = new Uri("https://api.cardknox.com/v2/");
    //client.DefaultRequestHeaders.Add("Authorization", "vancedsoliutdevc0e8bbba0dbc4381862aeffe4aa43d");
    //client.DefaultRequestHeaders.Add("X-Recurring-Api-Version", "2.1");
});
builder.Services.AddHttpClient("CardknoxTransaction", client =>
{

    client.BaseAddress = new Uri("https://x1.cardknox.com/gateway");
});
builder.Services.AddHostedService<CardknoxService>();
//builder.Services.AddScoped<CardknoxTransactionsService>();
//builder.Services.AddHostedService<CardknoxTransactionsService>();

builder.Services.AddSingleton<ITtsService, TtsService>();
builder.Services.AddSingleton<IAzureTtsService, AzureTtsService>();
//builder.Services.AddScoped<MarkStopPassedForRoutesService>();
//builder.Services.AddHostedService<MarkStopPassedForRoutesService>();
//builder.Services.AddSingleton<IBackgroundServices, BackgroundsServices>();
//builder.Services.AddSingleton<IBackgroundRepository, BackgroundRepository>();
//builder.Services.AddHostedService<CardknoxService>();
//builder.Services.AddSingleton(Log.Logger);
//builder.Services.AddScoped<PayrollService>();
//builder.Services.AddHostedService<PayrollService>();

builder.Services.Configure<ConnectionStringSettings>(builder.Configuration.GetSection("ConnectionStrings"));
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
var cardKnoxSection = builder.Configuration.GetSection("CardKnox");
var twilioSettingsSection = builder.Configuration.GetSection("Twilio");
var ttsSettingsSection = builder.Configuration.GetSection("TTSSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
builder.Services.Configure<CardKnoxsettings>(cardKnoxSection);
builder.Services.Configure<TwilioSettings>(twilioSettingsSection);
builder.Services.Configure<TTSSetiings>(ttsSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
//        options => builder.Configuration.Bind("JwtSettings", options))
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
//        options => builder.Configuration.Bind("CookieSettings", options));
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(i => i.FullName);
    c.OperationFilter<AddRequiredHeaderParameter>();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "School Transportation System API",
        Version = "v1"

    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      Array.Empty<string>()
                    }
                  });
});
builder.Services.AddAuthentication(cfg => {
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes(appSettings.Secret)
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

//builder.Services.AddSwaggerGen();
var mailSettingsSection = builder.Configuration.GetSection("Mail");
builder.Services.Configure<Mailsetting>(mailSettingsSection);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Transportation System API V1");
    });
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors(myCors);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public class AddRequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();
    }
}