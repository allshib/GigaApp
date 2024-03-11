using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace GigaApp.API.Monitoring
{
    internal static class OpenTelemetryCollectionExtensions
    {
        public static IServiceCollection AddMetrics(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenTelemetry()
                .WithMetrics(builder => builder
                    .AddAspNetCoreInstrumentation()
                    .AddMeter("GigaApp.Domain")
                    //.AddConsoleExporter()
                    //.AddView("http.server.request.duration", new ExplicitBucketHistogramConfiguration
                    //{
                    //    Boundaries = new[] { 0, 0.05, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 10 }

                    //})
                    .AddPrometheusExporter()
                    //.AddView("http.server.request.duration", new ExplicitBucketHistogramConfiguration
                    //{
                    //    Boundaries = new[] { 0, 0.05, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 10 }

                    //})
                    )
                .WithTracing(builder=> builder
                    .ConfigureResource(r=>r.AddService("Giga"))
                    .AddAspNetCoreInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation(cfg=>cfg.SetDbStatementForText = true)
                    .AddSource("Giga.Domain")
                    .AddJaegerExporter(options=>options.Endpoint = new Uri(configuration.GetConnectionString("Tracing")!)));


            return services;
        }
    }
}
