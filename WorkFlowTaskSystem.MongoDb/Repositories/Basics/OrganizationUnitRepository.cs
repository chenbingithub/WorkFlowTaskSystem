using Abp.MongoDb;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;

namespace WorkFlowTaskSystem.MongoDb.Repositories.Basics
{
   public class OrganizationUnitRepository : WorkFlowTaskRepositoryBase<OrganizationUnit, string>, IOrganizationUnitRepository
    {
        public OrganizationUnitRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }

        
    }
}
