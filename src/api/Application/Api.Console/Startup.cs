using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EZNEW.Web.Mvc.Validation;
using EZNEW.Web.Mvc.Display;
using EZNEW.Diagnostics;
using EZNEW.Web.Security.Authorization;
using EZNEW.Web.Mvc.Formatters;
using Api.Console.Util;
using Api.Console.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EZNEW.Web.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using EZNEW.Serialization.Json.Converter;
using NSwag.Generation.AspNetCore.Processors;
using NSwag.Generation.AspNetCore;
using EZNEW.Model;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api.Console
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            //Logging
            services.AddLogging();
            SwitchManager.EnableFrameworkTrace();

            //Api
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<ApiExceptionFilter>();
                options.Filters.Add<ApiActionFilter>();
                options.InputFormatters.Insert(0, new TextPlainInputFormatter());
                options.UseGlobalRoutePrefix(new RouteAttribute("api"));
            });

            //Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                    ValidIssuer = "http://localhost:5000",
                    ValidAudience = "api",
                    IssuerSigningKey = Constants.Token.SecurityKey,
                };
            });
            services.AddAuthorization();

            //Cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services?.ConfigureJson();

            //Swagger
            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = doc =>
                {
                    doc.Info.Title = "EZNEW";
                };
                config.UseControllerSummaryAsTagDescription = true;
                config.AddOperationFilter(context =>
                {
                    if (context is AspNetCoreOperationProcessorContext aspnetContext)
                    {
                        foreach (var apiResponse in aspnetContext.ApiDescription.SupportedResponseTypes)
                        {
                            var returnType = apiResponse.Type;
                            if (returnType != null && !typeof(IResult).IsAssignableFrom(returnType))
                            {
                                apiResponse.Type = typeof(Result<>).MakeGenericType(returnType);
                            }
                        }
                    }
                    return true;
                });
                config.GenerateEnumMappingDescription = true;
                config.AllowReferencesWithProperties = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            ApiConfig.Init();
        }
    }
}
