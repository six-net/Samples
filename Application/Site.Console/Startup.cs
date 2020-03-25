using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.Mvc.CustomModelDisplayName;
using EZNEW.Mvc.DataAnnotationsModelValidatorConfig;
using EZNEW.Web.Mvc;
using EZNEW.Web.Security.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Site.Console.Config;
using Site.Console.Controllers;
using Site.Console.Filters;
using Site.Console.Util;

namespace Site.Console
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMvc(options =>
            {
                options.ModelValidatorProviders.Add(new CustomDataAnnotationsModelValidatorProvider());
                options.ModelMetadataDetailsProviders.Add(new MvcCustomModelDisplayProvider());
                options.Filters.Add<OperationAuthorizeFilter>();
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
            AppConfig.Init();
            CacheDataManager.InitData();
        }
    }
}
