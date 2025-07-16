using GigaApp.Domain.Models;
using GigaApp.Domain.Monitoring;
using MediatR;

namespace GigaApp.Domain.UseCases.GetForums
{
    public record GetForumsQuery() : IRequest<IEnumerable<Forum>>, IMonitoredRequest
    {
        private const string CounterName = "forums.fetched";

        public void MonitorFailure(DomainMetrics metrics)
        {
            metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
        }

        public void MonitorSuccess(DomainMetrics metrics)
        {
            metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));
        }
    }
}
