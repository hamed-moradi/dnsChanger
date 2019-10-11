using domain.application.services;
using domain.repository._app;
using Microsoft.Extensions.DependencyInjection;

namespace domain.application._app {
    public class ModuleInjector {
        public static void Init(IServiceCollection services) {
            services.AddSingleton(new ConnectionKeeper());
            services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton(typeof(IStoreProcedure<,>), typeof(StoreProcedure<,>));
            services.AddTransient<IHttpLog_Service, HttpLogService>();
            services.AddTransient<IException_Service, ExceptionService>();
            services.AddTransient<IUser_Service, UserService>();
            services.AddTransient<ICustomer_Service, Customer_Service>();
        }
    }
}
