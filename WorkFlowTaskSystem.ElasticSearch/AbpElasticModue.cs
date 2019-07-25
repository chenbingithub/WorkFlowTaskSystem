using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Abp;
using Abp.Modules;
using Elasticsearch.Net;
using Nest;
using WorkFlowTaskSystem.ElasticSearch.Configuration;

namespace WorkFlowTaskSystem.ElasticSearch
{
    [DependsOn(typeof(AbpKernelModule))]
    public  class AbpElasticModue : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAbpElasticModuleConfiguration, AbpElasticModuleConfiguration>();
            IocManager.Register<IAbpConnectionSettings, AbpConnectionSettings>();
            IocManager.Register<IAbpConnectionPool, AbpConnectionPool>();
            IocManager.Register<IAbpElasticClient, AbpElasticClient>();

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

        }
    }
}
