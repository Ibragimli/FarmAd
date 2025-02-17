using Serilog.Core;
using Serilog.Events;

namespace FarmAd.API.Configurations
{
    public class CustomUserNameColumnWriter : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var (username, value) = logEvent.Properties.FirstOrDefault(x => x.Key == "Username");
            if (value?.ToString() != null)
            {
                var getValue = propertyFactory.CreateProperty(username, value.ToString());
                logEvent.AddPropertyIfAbsent(getValue);
            }
        }
    }
}
