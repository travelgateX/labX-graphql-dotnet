using System;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = WebHost.CreateDefaultBuilder(args)
                 .UseContentRoot(System.IO.Directory.GetCurrentDirectory())
                 .UseUrls("http://*:8080") // -> Must match the Kestrel configuration.
                 .UseStartup<Startup>()
                 .UseLibuv(options =>
                 {
                     options.ThreadCount = 32;
                 })
                 .UseKestrel(options =>
                 {
                     options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(60);

                     options.ApplicationSchedulingMode = Microsoft.AspNetCore.Server.Kestrel.Transport.Abstractions.Internal.SchedulingMode.ThreadPool;
                     options.Limits.MinRequestBodyDataRate =
                         new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                     options.Limits.MinResponseDataRate =
                         new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                     options.Limits.MaxConcurrentConnections = null;             // No limit. Default: null
                     options.Limits.MaxConcurrentUpgradedConnections = null;     // No limit. Default: null
                                                                                 //--- Kestrel endpoints -----------------
                     options.Listen(IPAddress.Any, 8080, listenOptions =>
                     {
                         listenOptions.NoDelay = true;
                     });
                 })
                 .Build();

            host.Run();
        }
    }
}
