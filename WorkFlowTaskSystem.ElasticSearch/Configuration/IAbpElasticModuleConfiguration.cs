using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.ElasticSearch.Configuration
{
   public interface IAbpElasticModuleConfiguration
    {
        /// <summary>
        /// 连接字符串，多个节点以逗号分隔
        /// </summary>
        string ConnectionString { get; set; }
    }
}
