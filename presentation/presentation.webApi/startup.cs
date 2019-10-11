using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using domain.repository._app;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using presentation.webApi.helpers;
using presentation.webApi.middlewares;
using shared.resource;
using Swashbuckle.AspNetCore.Swagger;

namespace Presentation.WebApi {
    public class Startup {
        #region Constructor
        //private static readonly string apiVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
        private static readonly string apiVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        #endregion

        public void ConfigureServices(IServiceCollection services) {
            services.AddLocalization(options => options.ResourcesPath = "shared.resource.resources");
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options => {
                    //options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResource));
                });
            services.AddSingleton(new MongoDBContext());
            services.AddSingleton(new MapperConfig().Init().CreateMapper());
            domain.application._app.ModuleInjector.Init(services);
            services.AddSwaggerGen(c => {
                c.SwaggerDoc(apiVersion, new Info { Title = shared.utility._app.AppSettings.MyTitle, Version = apiVersion });
            });
            services.Configure<RequestLocalizationOptions>(options => {
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = SupportedCultures.List;
                options.SupportedUICultures = SupportedCultures.List;
            });
            shared.utility._app.ModuleInjector.Init(services); // ServiceLocator
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseGateway();
            app.UseSwagger();
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint($"swagger/{apiVersion}/swagger.json", $"biavoo {apiVersion}");
                    c.RoutePrefix = string.Empty;
                });
            }
            app.UseRequestLocalization(new RequestLocalizationOptions {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = SupportedCultures.List,
                // UI strings that we have localized.
                SupportedUICultures = SupportedCultures.List
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization();
            app.UseMvcWithDefaultRoute();
        }
    }
}