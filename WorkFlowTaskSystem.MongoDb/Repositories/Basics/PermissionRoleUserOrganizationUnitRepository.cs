using Abp.MongoDb;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Values;

namespace WorkFlowTaskSystem.MongoDb.Repositories.Basics
{
   public class PermissionRoleUserOrganizationUnitRepository : WorkFlowTaskRepositoryBase<PermissionRoleUserOrganizationUnit, string>, IPermissionRoleUserOrganizationUnitRepository
    {
        public PermissionRoleUserOrganizationUnitRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }

        
    }
}
