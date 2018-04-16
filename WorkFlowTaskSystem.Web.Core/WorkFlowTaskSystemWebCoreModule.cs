using System;
using System.Reflection;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WorkFlowTaskSystem.Application;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.MongoDb;
using WorkFlowTaskSystem.Web.Core.Configuration;

namespace WorkFlowTaskSystem.Web.Core
{
   [DependsOn(typeof(WorkFlowTaskSystemApplicationModule),typeof(AbpAspNetCoreModule))]
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
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(WorkFlowTaskSystemApplicationModule).GetAssembly()
                );
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
