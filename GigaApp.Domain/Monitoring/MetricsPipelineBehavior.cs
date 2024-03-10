using MediatR;
using Microsoft.Extensions.Logging;

namespace GigaApp.Domain.Monitoring;

internal class MetricsPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly DomainMetrics metrics;
    private readonly ILogger<MetricsPipelineBehavior<TRequest, TResponse>> logger;

    public MetricsPipelineBehavior(
        DomainMetrics metrics,
        ILogger<MetricsPipelineBehavior<TRequest, TResponse>> logger)
    {
        this.metrics = metrics;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not IMonitoredRequest monitoredRequest)
            return await next();


        try
        {
            var result = await next();
            monitoredRequest.MonitorSuccess(metrics);
            return result;
        }
        catch
        {
            monitoredRequest.MonitorFailure(metrics);
            logger.LogError("An error occurred while processing the request {Request}", request);
            throw;
        }
    }


}