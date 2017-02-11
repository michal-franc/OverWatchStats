using System;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OverwatchStats.Web.Api;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore;
using SimpleInjector.Integration.AspNetCore.Mvc;
using OverwatchStats.Web.OverwatchApi;
using RestEase;

namespace OverwatchStats.Web
{
    public class Startup
    {
        private readonly Container _container = new Container();

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            services.AddMvc();

            // Both had to be installed as a middlewares. Fastest Gzip compression should be 'Good Enough'
            // Not sure why this is not in the basic template.
            services.AddResponseCaching();
            services.AddResponseCompression(o =>
            {
                o.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<GzipCompressionProviderOptions>(o =>
            {
                o.Level = CompressionLevel.Fastest;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeContainer(_container, app, env);

            app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();
            app.UseResponseCompression();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/");
            });
        }

        private void InitializeContainer(Container container, IApplicationBuilder app, IHostingEnvironment env)
        {
            // We should crash the app at the startup as app is unusable without it.
            // Later on we could move this to RegisterSingleton scope and crash only on request to specific feature.
            // This would still make it possible that user can use other features not dependant on JustEatApi
            var config = LoadAndCheckOverWatchApiConfiguration(env);

            container.Options.DefaultScopedLifestyle = new AspNetRequestLifestyle();

            container.RegisterMvcControllers(app);

            container.RegisterSingleton(() =>
            {
                // One problem with RestEase library is that for each api we need to create separate client.
                // If this would be a problem, I would switch to RestSharp and use IRestClient.
                // One IRestClient could be registered and shared.
                // With RestSharp, I would use RestRequest object to generate requests.
                var apiClient = RestClient.For<IOverwatchApiClientAsync>(config.HostUri.AbsoluteUri);
                return apiClient;
            });

            container.RegisterSingleton<IProfileStatsProviderAsync, OverwatchApiClientAsync>();

            container.Verify();
        }

        // Microsoft introduced IOption
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration#using-options-and-configuration-objects
        // https://github.com/aspnet/Options
        // Which breaks Dependancy Inversion Principle by forcing the leak of IOption<T> - to JustEatApiAsync in separate project
        // This function is a 'hack' way to avoid it
        private OverwatchApiConfiguration LoadAndCheckOverWatchApiConfiguration(IHostingEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json");

            var config = configBuilder.Build();

            var overWatchApConfig = new OverwatchApiConfiguration();
            config.GetSection("OverWatchApi").Bind(overWatchApConfig);

            if (overWatchApConfig.IsInvalid())
            {
                throw new ArgumentException("OverWatchApi Configuration Error");
            }

            return overWatchApConfig;
        }
    }
}
