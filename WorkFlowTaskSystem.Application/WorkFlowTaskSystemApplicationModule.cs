using System;
using System.Reflection;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WorkFlowTaskSystem.Application.Basics.Roles;
using WorkFlowTaskSystem.Application.Forms;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.RabbitMqPublisher;

namespace WorkFlowTaskSystem.Application
{
    [DependsOn(typeof(AbpAutoMapperModule), typeof(WorkFlowTaskSystemCoreModule), typeof(WorkFlowTaskSystemRabbitMqPublisherModule))]
    public class WorkFlowTaskSystemApplicationModule:AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IPermissionChecker, MyPermissionChecker>();
            IocManager.Register<IAuthorizationHelper, MyAuthorizationHelper>();
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
