using GigaApp.Domain.Identity;
using GigaApp.Domain.Monitoring;
using MediatR;

namespace GigaApp.Domain.UseCases.SignOn
{
    public record SignOnCommand(string Login, string Password) : IRequest<IIdentity>, IMonitoredRequest
    {
        private const string CounterName = "user.signon";

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
