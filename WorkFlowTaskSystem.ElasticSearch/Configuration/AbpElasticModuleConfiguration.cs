using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.ElasticSearch.Configuration
{
   public class AbpElasticModuleConfiguration : IAbpElasticModuleConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
