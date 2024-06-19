using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Yarp.ServiceFabric.Service;

namespace Yarp.Proxy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SFYarpStandalone.Run();
        }
    }

    internal static class SFYarpStandalone
    {
        public static void Run()
        {
            // TODO: Wire-up with SigTerm (`Console.CancelKeyPress`)
            var shutdownStateManager = new ShutdownStateManager();

            // TODO: Support configurable endpoints to listen to, e.g. by reading command line config.
            var host = SFYarpService.CreateWebHost(
                shutdownStateManager: shutdownStateManager,
                urls: new[] { "http://+:280", "https://+:2443" },
                //serviceContext: null,
                configureAppConfigurationAction: _ => { });
            //var host = WebHost.CreateDefaultBuilder()
            //    .Build();
            host.Run();
        }
    }
}
