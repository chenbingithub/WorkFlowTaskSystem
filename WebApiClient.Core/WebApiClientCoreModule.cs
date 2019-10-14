using System;
using System.Reflection;
using Abp.Dependency;
using Abp.Modules;

namespace WebApiClient.Core
{
    public class WebApiClientCoreModule:AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IHttpApiFactory<IEmailApi>, HttpApiFactory<IEmailApi>>();
           

            base.PreInitialize();
        }
        public override void Initialize()
        {
            //把当前程序集的特定类或接口注册到依赖容器中
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

        }
    }
}
