using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Presentation.WebApi {
    public class Program {
        public static void Main(string[] args) {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((ctx, config) => {
                    config.ReadFrom.Configuration(ctx.Configuration);
                    Serilog.Debugging.SelfLog.Enable(Console.Error);
                })
            .Build();
    }
}
