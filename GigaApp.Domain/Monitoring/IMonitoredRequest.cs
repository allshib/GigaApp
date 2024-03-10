using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Monitoring
{
    internal interface IMonitoredRequest
    {
        void MonitorSuccess(DomainMetrics metrics);
        void MonitorFailure(DomainMetrics metrics);
    }
}
