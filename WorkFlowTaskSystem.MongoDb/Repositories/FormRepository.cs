using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using MongoDB.Bson;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.IRepositories;

namespace WorkFlowTaskSystem.MongoDb.Repositories
{
   public class FormRepository : WorkFlowTaskRepositoryBase<Form, string>,IFormRepository
    {
        public FormRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }
        
    }
}
