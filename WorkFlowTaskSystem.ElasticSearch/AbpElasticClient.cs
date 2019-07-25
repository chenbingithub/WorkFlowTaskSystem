using System;
using System.Collections.Generic;
using System.Text;
using Elasticsearch.Net;
using Nest;

namespace WorkFlowTaskSystem.ElasticSearch
{
   public class AbpElasticClient : ElasticClient, IAbpElasticClient
    {
        public AbpElasticClient(IAbpConnectionSettings settings) : base(settings)
        {
        }
    }
}
