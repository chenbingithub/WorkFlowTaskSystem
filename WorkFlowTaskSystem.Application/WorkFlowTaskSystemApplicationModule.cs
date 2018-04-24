using System;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WorkFlowTaskSystem.Application.Forms;
using WorkFlowTaskSystem.Application.Roles;
using WorkFlowTaskSystem.Core;

namespace WorkFlowTaskSystem.Application
{
    [DependsOn(typeof(AbpAutoMapperModule), typeof(WorkFlowTaskSystemCoreModule))]
    public class WorkFlowTaskSystemApplicationModule:AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IRoleAppService, RoleAppService>();
            base.PreInitialize();
        }

        public override void Initialize()
        {

            //把当前程序集的特定类或接口注册到依赖容器中
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
