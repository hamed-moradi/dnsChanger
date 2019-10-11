using Microsoft.Extensions.DependencyInjection;
using shared.utility.infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace shared.utility._app {
    public class ModuleInjector {
        public static void Init(IServiceCollection services) {
            services.AddTransient<IRandomGenerator, RandomGenerator>();
            services.AddTransient<ISMSService, Candoo>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddSingleton(new ServiceLocator(services));
        }
    }
}
