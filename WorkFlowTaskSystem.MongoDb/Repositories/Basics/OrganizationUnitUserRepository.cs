using Abp.MongoDb;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Values;

namespace WorkFlowTaskSystem.MongoDb.Repositories.Basics
{
   public class OrganizationUnitUserRepository : WorkFlowTaskRepositoryBase<OrganizationUnitUser, string>, IOrganizationUnitUserRepository
    {
        public OrganizationUnitUserRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }

        
    }
}
