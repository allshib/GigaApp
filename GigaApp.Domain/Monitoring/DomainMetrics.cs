using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace GigaApp.Domain.Monitoring
{
    internal class DomainMetrics(IMeterFactory meterFactory)
    {
        private readonly Meter meter = meterFactory.Create("GigaApp.Domain");
        private readonly ConcurrentDictionary<string, Counter<int>> counters = new();


        public void ForumsFetched(bool success)
        {
            IncrementCount("forums.fetched", 1, new Dictionary<string, object?>
            {
                ["success"] = success
            });
        }
        private void IncrementCount(string counterName, int value, IDictionary<string, object?> additionalTags = null)
        {
            var counter = counters.GetOrAdd(counterName, _ => meter.CreateCounter<int>(counterName));
            counter.Add(value, additionalTags?.ToArray()?? ReadOnlySpan<KeyValuePair<string, object?>>.Empty);
        }

    }
}
