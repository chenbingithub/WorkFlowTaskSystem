using Abp.MongoDb;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Values;

namespace WorkFlowTaskSystem.MongoDb.Repositories.Basics
{
   public class OrganizationUnitRoleRepository : WorkFlowTaskRepositoryBase<OrganizationUnitRole, string>, IOrganizationUnitRoleRepository
    {
        public OrganizationUnitRoleRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }

        
    }
}
