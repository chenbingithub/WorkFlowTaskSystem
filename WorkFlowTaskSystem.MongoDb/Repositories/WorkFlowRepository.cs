using System;
using System.Collections.Generic;
using System.Text;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using MongoDB.Bson;
using WorkFlowTaskSystem.Core.Entitys.WorkFlows;

namespace WorkFlowTaskSystem.MongoDb.Repositories
{
   public class WorkFlowRepository: WorkFlowTaskRepositoryBase<WorkFlow, string>
    {
        public WorkFlowRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }
    }
}
