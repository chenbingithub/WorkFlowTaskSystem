using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Modules;
using WorkFlowTaskSystem.Web.Core;

namespace WorkFlowTaskSystem.WebApp.Host
{
    [DependsOn(typeof(WorkFlowTaskSystemWebCoreModule))]
    public class WorkFlowTaskSystemWebModule: AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
            base.PreInitialize();
        }

        public override void Initialize()
        {
            //把当前程序集的特定类或接口注册到依赖容器中
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
