using System;

namespace OverwatchStats.Web.OverwatchApi
{
    internal static class OverwatchApiConfigurationExtension
    {
        public static bool IsInvalid(this OverwatchApiConfiguration configuration)
            => configuration.HostUri == null;
    }

    public class OverwatchApiConfiguration
    {
        public Uri HostUri { get; set; }
    }
}
