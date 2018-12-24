using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WorkFlowTaskSystem.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
          services.AddMvc();
          services.AddSession();
          services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
          app.UseSession();
          //初始化abp框架
          app.UseAbp(options1 => { options1.UseAbpRequestLocalization = false; });
          //设置跨域处理的 代理
         // app.UseCors(_defaultCorsPolicyName); // Enable CORS!
          app.UseAbpRequestLocalization();
          DefaultFilesOptions options = new DefaultFilesOptions();
          options.DefaultFileNames.Clear();
          options.DefaultFileNames.Add("index.html");
          app.UseDefaultFiles(options);
            app.UseAuthentication();
          app.UseStaticFiles();
          app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
              routes.MapRoute(
                name: "defaultWithArea",
                template: "{area}/{controller=Home}/{action=Index}/{id?}");
              routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
