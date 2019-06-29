using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace WorkFlowTaskSystem.Core.Localization
{
    public static class WorkFlowTaskLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(WorkFlowTaskAbpConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(WorkFlowTaskLocalizationConfigurer).GetAssembly(),
                        "WorkFlowTaskSystem.Core.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
