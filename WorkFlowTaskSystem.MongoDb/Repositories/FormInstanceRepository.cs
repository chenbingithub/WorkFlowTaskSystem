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
   public class FormInstanceRepository : WorkFlowTaskRepositoryBase<FormInstance, string>,IFormInstanceRepository
    {
        public FormInstanceRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }
    }
}
