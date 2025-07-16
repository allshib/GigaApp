using GigaApp.Domain.Monitoring;
using MediatR;

namespace GigaApp.Domain.UseCases.SignOut
{
    public record SignOutCommand : IRequest, IMonitoredRequest
    {
        private const string CounterName = "user.signout";

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
