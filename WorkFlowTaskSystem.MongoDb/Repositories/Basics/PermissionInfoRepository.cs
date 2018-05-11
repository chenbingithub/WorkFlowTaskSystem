using Abp.MongoDb;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;

namespace WorkFlowTaskSystem.MongoDb.Repositories.Basics
{
   public class PermissionInfoRepository : WorkFlowTaskRepositoryBase<PermissionInfo, string>, IPermissionInfoRepository
    {
        public PermissionInfoRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }
        
    }
}
