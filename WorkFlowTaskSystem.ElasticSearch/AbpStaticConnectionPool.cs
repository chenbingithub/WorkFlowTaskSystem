using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using WorkFlowTaskSystem.ElasticSearch.Configuration;

namespace WorkFlowTaskSystem.ElasticSearch
{
   public class AbpConnectionPool: StaticConnectionPool, IAbpConnectionPool
    {
        public AbpConnectionPool(IAbpElasticModuleConfiguration options) : base(options.ConnectionString.Split(',').Select(uri => new Uri(uri)))
        {
        }
    }
}
