using System;
using System.Reflection;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.MailKit;
using Abp.Modules;
using Abp.Net.Mail.Smtp;
using Abp.Reflection.Extensions;
using Abp.Runtime.Caching.Redis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WorkFlowTaskSystem.Application;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.MongoDb;
using WorkFlowTaskSystem.Web.Core.Configuration;
using WorkFlowTaskSystem.Web.Core.Email;

namespace WorkFlowTaskSystem.Web.Core
{
  //,typeof(AbpRedisCacheModule)
  [DependsOn(typeof(WorkFlowTaskSystemApplicationModule),typeof(AbpAspNetCoreModule),typeof(WorkFlowTaskSystemMongoDbModule), typeof(AbpRedisCacheModule), typeof(AbpHangfireAspNetCoreModule), typeof(AbpMailKitModule)
#if FEATURE_SIGNALR
        ,typeof(AbpWebSignalRModule)
#elif FEATURE_SIGNALR_ASPNETCORE
        ,typeof(AbpAspNetCoreSignalRModule)
#endif
        )]
    public class WorkFlowTaskSystemWebCoreModule:AbpModule
    {

        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public WorkFlowTaskSystemWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            IocManager.Register<ISmtpEmailSenderConfiguration, WorkFlowSmtpEmailSenderConfiguration>();
            //Configuration.ReplaceService<IMailKitSmtpBuilder, MyMailKitSmtpBuilder>();
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(WorkFlowTaskSystemApplicationModule).GetAssembly()
                );
            Configuration.BackgroundJobs.UseHangfire();
            //配置跨域

            //GlobalConfiguration.Configuration.EnableCors(new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*"));

            //配置输出json时不使用骆驼式命名法，按对象属性原名输出

            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();


            ////设置所有缓存的默认过期时间
            //Configuration.Caching.ConfigureAll(cache =>
            //{
            //    cache.DefaultAbsoluteExpireTime = TimeSpan.FromMinutes(2);
            //});
            ////设置某个缓存的默认过期时间 根据 "CacheName" 来区分
            //Configuration.Caching.Configure("CacheName", cache =>
            //{
            //    cache.DefaultAbsoluteExpireTime = TimeSpan.FromMinutes(2);
            //});
            //使用redis数据库缓存
            Configuration.Caching.UseRedis(option =>
            {
                option.ConnectionString = _appConfiguration["Abp:RedisCache:ConnectionStrings"];
                option.DatabaseId = int.Parse(_appConfiguration["Abp:RedisCache:DatabaseId"]);
            });



        }

        public override void Initialize()
        {
            //mongodb数据库连接地址
            Configuration.Modules.AbpMongoDb().ConnectionString = _appConfiguration.GetConnectionString(WorkFlowTaskAbpConsts.ConnectionStringName );
            Configuration.Modules.AbpMongoDb().DatatabaseName = _appConfiguration.GetConnectionString(WorkFlowTaskAbpConsts.DatatabaseName);

            //把当前程序集的特定类或接口注册到依赖容器中
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

        }
    }
}
