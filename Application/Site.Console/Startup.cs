using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EZNEW.Web.Mvc.Validation;
using EZNEW.Web.Mvc.Display;
using EZNEW.Diagnostics;
using EZNEW.Web.Mvc;
using EZNEW.Web.Security.Authentication.Cookie;
using EZNEW.Web.Security.Authorization;
using Site.Console.Util;
using Site.Console.Controllers;
using Site.Console.Filters;

namespace Site.Console
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddLogging(cfg =>
            {
                cfg.AddTraceSource("eznew", new ConsoleTraceListener());
                cfg.AddFilter("microsoft", LogLevel.Warning);
            });
            SwitchManager.EnableFrameworkTrace();
            services.AddMvc(options =>
            {
                options.ModelValidatorProviders.Add(new CustomDataAnnotationsModelValidatorProvider());
                options.ModelMetadataDetailsProviders.Add(new CustomModelDisplayProvider());
                options.Filters.Add<ExtendAuthorizeFilter>();
                options.Filters.Add<ConsoleExceptionFilter>();
            })
            .AddViewOptions(vo =>
            {
                vo.ClientModelValidatorProviders.Add(new CustomDataAnnotationsClientModelValidatorProvider());
            })
            .AddRazorOptions(ro =>
            {
                ro.ViewLocationExpanders.Add(new FolderLevelViewLocationExpander(typeof(WebBaseController)));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookieAuthentication(option =>
            {
                option.ForceValidatePrincipal = true;
                option.ValidatePrincipalAsync = IdentityManager.ValidatePrincipalAsync;
                option.StorageModel = CookieStorageModel.InMemory;
                option.CookieConfiguration = options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/accessdenied";
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "accessdenied",
                    pattern: "accessdenied",
                    defaults: new { controller = "account", action = "AccessDenied" });
                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "login",
                    defaults: new { controller = "account", action = "login" });
                endpoints.MapControllerRoute(
                    name: "loginout",
                    pattern: "loginout",
                    defaults: new { controller = "account", action = "LoginOut" });
                endpoints.MapControllerRoute(
                    name: "noauth",
                    pattern: "noauth",
                    defaults: new { controller = "home", action = "authentication" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            SiteConfig.Init();
        }
    }
}
