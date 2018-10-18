﻿using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.MongoDb;
using System;
using System.Reflection;
using Abp.Domain.Repositories;
using Abp.Reflection.Extensions;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.MongoDb.Repositories;

namespace WorkFlowTaskSystem.MongoDb
{
    [DependsOn(typeof(WorkFlowTaskSystemCoreModule), typeof(AbpMongoDbModule))]
    public class WorkFlowTaskSystemMongoDbModule: AbpModule
    {
        public override void PreInitialize()
        {

            //IocManager.Register<IRepository<Form,string>, FormRepository>();
            //IocManager.Register<IRepository<DocumentTreeNode, string>, WorkFlowTaskRepositoryBase<DocumentTreeNode, string>>();
            IocManager.Register<IRepository<WorkFlowInstance, string>, WorkFlowInstanceRepository>();
            IocManager.Register<IRepository<WorkFlow, string>, WorkFlowRepository>();
            IocManager.Register<IRepository<CableLayingDetails, string>, WorkFlowTaskRepositoryBase<CableLayingDetails, string>>();
            IocManager.Register<IRepository<CableConstant, string>, WorkFlowTaskRepositoryBase<CableConstant, string>>();
            IocManager.Register<IRepository<BridgeConstant, string>, WorkFlowTaskRepositoryBase<BridgeConstant, string>>(); 
            IocManager.Register<IRepository<BridgeInstances, string>, WorkFlowTaskRepositoryBase<BridgeInstances, string>>();
            IocManager.Register<IRepository<CableSummarizedBill, string>, WorkFlowTaskRepositoryBase<CableSummarizedBill, string>>();
    }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            
        }
    }
}
