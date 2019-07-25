using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.ElasticSearch.Configuration
{
   public class AbpElasticModuleConfiguration : IAbpElasticModuleConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
