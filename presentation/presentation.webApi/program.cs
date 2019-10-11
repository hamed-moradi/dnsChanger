using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Presentation.WebApi {
    public class Program {
        public static void Main(string[] args) {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //.UseKestrel(o => { o.Listen(IPAddress.Any, 5001); o.Limits.MaxRequestBodySize = null; })
                //.UseDefaultServiceProvider(options => options.ValidateScopes = false)
                //.UseSetting(WebHostDefaults.SuppressStatusMessagesKey, "True")
                //.UseUrls("http://*.5001")
                .UseSerilog((ctx, config) => {
                    config.ReadFrom.Configuration(ctx.Configuration);
                    //Serilog.Debugging.SelfLog.Enable(Console.Error);
                })
            .Build();
    }
}
