using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yarp.Proxy;

namespace Yarp.ServiceFabric.Service
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

            //services.AddSingleton<IMonotonicTimer, MonotonicTimer>();
            //services.AddSingleton<IOperationLogger, TextOperationLogger>();
            //services.AddSingleton<IMetricCreator, NullMetricCreator>();

            services.AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"));
            //services.AddSingleton<IRemoteConfigClientFactory, RemoteConfigClientFactory>();

            //services.AddSingleton<ICertificateLoader, CertificateLoader>();
            //services.AddSingleton<ISniServerCertificateSelector, SniServerCertificateSelector>();
            //services.AddHostedService<SniServerCertificateUpdater>();
            services.TryAddSingleton<ShutdownStateManager>();

            //services.AddHostedService<TelemetryManager>();
            //services.Configure<RemoteConfigDiscoveryOptions>(this.configuration.GetSection("RemoteConfigDiscovery"));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapReverseProxy();
            });
        }
    }
}