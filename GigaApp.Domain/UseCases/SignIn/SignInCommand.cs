using GigaApp.Domain.Identity;
using GigaApp.Domain.Monitoring;
using MediatR;

namespace GigaApp.Domain.UseCases.SignIn
{
    public record SignInCommand(string Login, string Password) : IRequest<(IIdentity identity, string token)>, IMonitoredRequest
    {
        private const string CounterName = "user.signin";

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
