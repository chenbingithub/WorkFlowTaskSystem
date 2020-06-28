using System;
using System.Collections.Generic;
using System.Text;
using Elasticsearch.Net;
using Nest;

namespace Abp.ElasticSearch
{
   public class AbpConnectionSettings: ConnectionSettings, IAbpConnectionSettings
    {
        public AbpConnectionSettings(IAbpConnectionPool pool):base(pool)
        {
            this.DefaultIndex("themepark_ota");
        }
    }
}
