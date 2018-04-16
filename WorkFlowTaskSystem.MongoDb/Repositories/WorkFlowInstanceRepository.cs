using System;
using System.Collections.Generic;
using System.Text;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using MongoDB.Bson;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.IRepositories;

namespace WorkFlowTaskSystem.MongoDb.Repositories
{
   public class WorkFlowInstanceRepository : WorkFlowTaskRepositoryBase<WorkFlowInstance, string>,IWorkFlowInstanceRepository
    {
        public WorkFlowInstanceRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }
    }
}
