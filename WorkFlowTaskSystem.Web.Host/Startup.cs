using System;
using System.Linq;
using System.Reflection;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.Mvc.Extensions;
using Abp.AspNetCore.OData.Configuration;
using Abp.Castle.Logging.Log4Net;
using Abp.Extensions;
using Castle.Facilities.Logging;
using Hangfire;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Web.Core.Configuration;


namespace WorkFlowTaskSystem.Web.Host
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSession();
            services.AddOData();

            // Workaround: https://github.com/OData/WebApi/issues/1177
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            // Configure CORS for angular2 UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            _appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        //.AllowAnyMethod()
                        .WithMethods(_appConfiguration["App:CorsMethods"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()).AllowCredentials()
                )
            );
            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "workflowVueABP API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });
            services.AddHangfire(config =>
                {
                    //config.UseRedisStorage("127.0.0.1:6379");
                    config.UseRedisStorage(_appConfiguration["Abp:RedisCache:ConnectionStrings"]);
                });
            return services.AddAbp<WorkFlowTaskSystemWebModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseBrowserLink();
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseSession();
            //初始化abp框架
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; });
            //设置跨域处理的 代理
            app.UseCors(_defaultCorsPolicyName); // Enable CORS!

            app.UseOData(builder =>
            {
                builder.EntitySet<WeightConstant>("Products").EntityType.Expand().Filter().OrderBy().Page().Select();
            });

            app.UseStaticFiles();
            app.UseAbpRequestLocalization();
            //app.UseAuthentication();
            //使用hangfire
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                //options.InjectOnCompleteJavaScript("/swagger/ui/abp.js");
                //options.InjectOnCompleteJavaScript("/swagger/ui/on-complete.js");
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkFlowService API V1");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("WorkFlowTaskSystem.Web.Host.wwwroot.swagger.ui.index.html");
            }); // URL: /swagger

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "defaultWithArea",
                   template: "{area}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                app.ApplicationServices.GetRequiredService<IAbpAspNetCoreConfiguration>().RouteConfiguration.ConfigureAll(routes);
                routes.MapODataServiceRoute(app);
            });
        }
    }
}
