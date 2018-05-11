using Abp.MongoDb;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Values;

namespace WorkFlowTaskSystem.MongoDb.Repositories.Basics
{
   public class UserRoleRepository : WorkFlowTaskRepositoryBase<UserRole, string>, IUserRoleRepository
    {
        public UserRoleRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }

        
    }
}
