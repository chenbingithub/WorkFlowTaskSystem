using System;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WorkFlowTaskSystem.Application.Basics.Roles;
using WorkFlowTaskSystem.Application.Forms;
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
            var thisAssembly = typeof(WorkFlowTaskSystemApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
           
        }
    }
}
