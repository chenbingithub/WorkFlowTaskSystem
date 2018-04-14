using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WorkFlowTaskSystem.Application;
using WorkFlowTaskSystem.Web.Core;


namespace WorkFlowTaskSystem.Web.Host
{
    [DependsOn(typeof(WorkFlowTaskSystemWebCoreModule))]
    public class WorkFlowTaskSystemWebModule: AbpModule
    {
       

        public override void Initialize()
        {
            //把当前程序集的特定类或接口注册到依赖容器中
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
