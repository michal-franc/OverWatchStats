using System;
using Microsoft.AspNetCore.Hosting;

namespace OverWatchStats.Web.IntegrationTests.OverWatchFakedApi
{
    public class FakedApiServer : IDisposable
    {
        private IWebHost _host;

        public FakedApiServer()
        {
            _host = new WebHostBuilder()
                .UseUrls("http://localhost:2222/")
                .UseIISIntegration()
                .UseKestrel()
                .UseStartup<OverWatchFakeNancy>()
                .Build();

            _host.Start();
        }

        public void Dispose()
        {
            _host.Dispose();
        }
    }
}
