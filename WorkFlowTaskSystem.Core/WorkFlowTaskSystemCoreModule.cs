using Abp.Modules;
using System;
using System.Reflection;
using Abp.Reflection.Extensions;
using Abp.Localization;
using WorkFlowTaskSystem.Core.Localization;

namespace WorkFlowTaskSystem.Core
{
    public class WorkFlowTaskSystemCoreModule: AbpModule
    {
        public override void PreInitialize()
        {

            WorkFlowTaskLocalizationConfigurer.Configure(Configuration.Localization);
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文",icon: "famfamfam-flags cn", isDefault: true,isDisabled: true));
            Configuration.Settings.Providers.Add<AppSettingProvider>();
            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = false;
            
            base.PreInitialize();
        }
        public override void Initialize()
        {
            //把当前程序集的特定类或接口注册到依赖容器中
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            
        }
    }
    
}
