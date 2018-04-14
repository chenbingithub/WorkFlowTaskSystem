using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MongoDb;
using System;
using System.Reflection;
using Abp.Domain.Repositories;
using Abp.Reflection.Extensions;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Entitys.Forms;
using WorkFlowTaskSystem.Core.Entitys.WorkFlows;
using WorkFlowTaskSystem.MongoDb.Repositories;

namespace WorkFlowTaskSystem.MongoDb
{
    [DependsOn(typeof(WorkFlowTaskSystemCoreModule), typeof(AbpMongoDbModule))]
    public class WorkFlowTaskSystemMongoDbModule: AbpModule
    {
        public override void PreInitialize()
        {
            
            IocManager.Register<IRepository<Form,string>, FormRepository>();
            IocManager.Register<IRepository<FormInstance, string>, FormInstanceRepository>();
            IocManager.Register<IRepository<WorkFlowInstance, string>, WorkFlowInstanceRepository>();
            IocManager.Register<IRepository<WorkFlow, string>, WorkFlowRepository>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            
        }
    }
}
