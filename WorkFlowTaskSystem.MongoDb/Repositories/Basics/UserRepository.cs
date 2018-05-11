using Abp.MongoDb;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;

namespace WorkFlowTaskSystem.MongoDb.Repositories.Basics
{
   public class UserRepository : WorkFlowTaskRepositoryBase<User, string>, IUserRepository
    {
        public UserRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }

        
    }
}
