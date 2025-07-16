using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace GigaApp.Domain.Monitoring
{
    public class DomainMetrics(IMeterFactory meterFactory)
    {
        private readonly Meter meter = meterFactory.Create("GigaApp.Domain");
        private readonly ConcurrentDictionary<string, Counter<int>> counters = new();
        public void IncrementCount(string counterName, int value, IDictionary<string, object?> additionalTags = null)
        {
            var counter = counters.GetOrAdd(counterName, _ => meter.CreateCounter<int>(counterName));
            counter.Add(value, additionalTags?.ToArray()?? ReadOnlySpan<KeyValuePair<string, object?>>.Empty);
        }

        public static IDictionary<string, object?> ResultTags(bool success)
            => new Dictionary<string, object?>
            {
                ["success"] = success
            };

    }
}
