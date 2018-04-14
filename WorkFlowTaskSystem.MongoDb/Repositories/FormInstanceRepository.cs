using System;
using System.Collections.Generic;
using System.Text;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using MongoDB.Bson;
using WorkFlowTaskSystem.Core.Entitys.Forms;

namespace WorkFlowTaskSystem.MongoDb.Repositories
{
   public class FormInstanceRepository : WorkFlowTaskRepositoryBase<FormInstance, string>
    {
        public FormInstanceRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }
    }
}
