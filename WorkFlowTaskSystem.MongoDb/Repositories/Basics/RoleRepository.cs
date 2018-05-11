using Abp.MongoDb;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;

namespace WorkFlowTaskSystem.MongoDb.Repositories.Basics
{
   public class RoleRepository : WorkFlowTaskRepositoryBase<Role, string>, IRoleRepository
    {
        public RoleRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }

        
    }
}
