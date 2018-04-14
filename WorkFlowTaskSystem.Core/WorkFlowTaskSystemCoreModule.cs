using Abp.Modules;
using System;
using System.Reflection;
using Abp.Reflection.Extensions;

namespace WorkFlowTaskSystem.Core
{
    public class WorkFlowTaskSystemCoreModule: AbpModule
    {
        public override void Initialize()
        {
            //把当前程序集的特定类或接口注册到依赖容器中
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            
        }
    }
}
