using System;
using System.Collections.Generic;
using System.Text;
using Abp.Configuration.Startup;

namespace Abp.ElasticSearch.Configuration.Startup
{
   public static class ElasticConfigurationExtensions
    {
        public static IAbpElasticModuleConfiguration ElasticSearch(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.Get<IAbpElasticModuleConfiguration>();
        }
    }
}
