using System;
using System.Collections.Generic;
using System.Text;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using MongoDB.Bson;
using WorkFlowTaskSystem.Core.Entitys.WorkFlows;

namespace WorkFlowTaskSystem.MongoDb.Repositories
{
   public class WorkFlowInstanceRepository : WorkFlowTaskRepositoryBase<WorkFlowInstance, string>
    {
        public WorkFlowInstanceRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }
    }
}
