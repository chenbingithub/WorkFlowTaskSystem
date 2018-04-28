using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using MongoDB.Bson;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;

namespace WorkFlowTaskSystem.MongoDb.Repositories
{
   public class PermissionInfoRepository : WorkFlowTaskRepositoryBase<PermissionInfo, string>, IPermissionInfoRepository
    {
        public PermissionInfoRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }
        
    }
}
