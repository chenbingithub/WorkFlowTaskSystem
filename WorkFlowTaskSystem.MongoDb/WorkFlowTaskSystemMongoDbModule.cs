using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MongoDb;
using System;
using System.Reflection;
using Abp.Domain.Repositories;
using Abp.Reflection.Extensions;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.MongoDb.Repositories;
using Castle.MicroKernel.Registration;

namespace WorkFlowTaskSystem.MongoDb
{
    [DependsOn(typeof(WorkFlowTaskSystemCoreModule), typeof(AbpMongoDbModule))]
    public class WorkFlowTaskSystemMongoDbModule: AbpModule
    {
        public override void PreInitialize()
        {

            //IocManager.Register<IRepository<Form,string>, FormRepository>();
            //IocManager.Register<IRepository<DocumentTreeNode, string>, WorkFlowTaskRepositoryBase<DocumentTreeNode, string>>();
            //IocManager.Register<IRepository<WorkFlowInstance, string>, WorkFlowInstanceRepository>();
            //IocManager.Register<IRepository<WorkFlow, string>, WorkFlowRepository>();
            //IocManager.Register<IRepository<CableLayingDetails, string>, WorkFlowTaskRepositoryBase<CableLayingDetails, string>>();
            //IocManager.Register<IRepository<CableConstant, string>, WorkFlowTaskRepositoryBase<CableConstant, string>>();
            //IocManager.Register<IRepository<BridgeConstant, string>, WorkFlowTaskRepositoryBase<BridgeConstant, string>>(); 
            //IocManager.Register<IRepository<BridgeInstances, string>, WorkFlowTaskRepositoryBase<BridgeInstances, string>>();
            //IocManager.Register<IRepository<CableSummarizedBill, string>, WorkFlowTaskRepositoryBase<CableSummarizedBill, string>>();
            //IocManager.Register<IRepository<ReportResult, string>, WorkFlowTaskRepositoryBase<ReportResult, string>>();
            //IocManager.Register<IRepository<WeightConstant, string>, WorkFlowTaskRepositoryBase<WeightConstant, string>>();
            //IocManager.Register<IRepository<PlotRatio, string>, WorkFlowTaskRepositoryBase<PlotRatio, string>>();
            
            IocManager.IocContainer.Register(
                Component.For(typeof(WorkFlowTaskRepositoryBase<,>))
                    .ImplementedBy(typeof(IRepository<,>))
                    .LifestyleTransient()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            
        }
    }
}
