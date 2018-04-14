using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using MongoDB.Bson;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Entitys.Forms;

namespace WorkFlowTaskSystem.MongoDb.Repositories
{
   public class FormRepository : WorkFlowTaskRepositoryBase<Form, string>
    {
        public FormRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }
        public override IQueryable<Form> GetAll()
        {
            return base.GetAll();
        }
    }
}
