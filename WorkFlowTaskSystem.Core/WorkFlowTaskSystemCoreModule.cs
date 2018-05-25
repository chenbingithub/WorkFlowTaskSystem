using Abp.Modules;
using System;
using System.Reflection;
using Abp.Reflection.Extensions;
using WorkFlowTaskSystem.Localization;
using Abp.Localization;
using WorkFlowTaskSystem.Core.Localization;

namespace WorkFlowTaskSystem.Core
{
    public class WorkFlowTaskSystemCoreModule: AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", isDefault: true,isDisabled:true));
            WorkFlowTaskLocalizationConfigurer.Configure(Configuration.Localization);
            Configuration.Settings.Providers.Add<AppSettingProvider>();
            base.PreInitialize();
        }
        public override void Initialize()
        {
            //把当前程序集的特定类或接口注册到依赖容器中
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            
        }
    }
    
}
