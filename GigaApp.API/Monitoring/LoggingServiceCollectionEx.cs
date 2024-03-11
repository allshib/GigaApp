using Serilog;
using Serilog.Core;
using Serilog.Filters;
using Serilog.Sinks.Grafana.Loki;

namespace GigaApp.API.Monitoring
{
    public static class LoggingServiceCollectionEx
    {

        public static IServiceCollection AddApiLogging(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var loggingLevelSwitch = new LoggingLevelSwitch();
            services.AddSingleton(loggingLevelSwitch);

            services.AddLogging(b => b.AddSerilog(new LoggerConfiguration()
                .MinimumLevel.ControlledBy(loggingLevelSwitch)
                .Enrich.WithProperty("Application", "GigaApp.API")
                .Enrich.WithProperty("Environment", environment.EnvironmentName)
                .WriteTo.Logger(lc => lc.Filter.ByExcluding(Matching.FromSource("Microsoft")).WriteTo.GrafanaLoki(
                    uri: configuration.GetConnectionString("Logs")!,
                    propertiesAsLabels: new[]
                    {
                        "level", "Environment", "Application", "SourceContext"
                    },
                    leavePropertiesIntact: true))
                .CreateLogger()));

            return services;
        }
    }
}
