using domain.repository._app;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using shared.resource;

namespace test.common._app {
    [TestClass]
    public class Startup {
        [AssemblyInitialize]
        public static void Init(TestContext testContext) {
            var services = new ServiceCollection();
            services.AddLocalization(options => options.ResourcesPath = "shared.resource.resources");
            services.AddSingleton(new MongoDBContext());
            shared.utility._app.ModuleInjector.Init(services);
            domain.application._app.ModuleInjector.Init(services);
            services.AddSingleton(new shared.utility._app.ServiceLocator(services));
            services.Configure<RequestLocalizationOptions>(options => {
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = SupportedCultures.List;
                options.SupportedUICultures = SupportedCultures.List;
            });
        }
    }

    [TestClass]
    public class Cleanup {
        [AssemblyCleanup]
        public static void Init() {
        }
    }
}