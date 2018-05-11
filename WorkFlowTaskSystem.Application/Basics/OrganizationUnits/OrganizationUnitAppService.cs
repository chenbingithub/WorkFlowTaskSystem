using WorkFlowTaskSystem.Application.Basics.OrganizationUnits.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;

namespace WorkFlowTaskSystem.Application.Basics.OrganizationUnits
{
    public class OrganizationUnitAppService : WorkFlowTaskSystemAppServiceBase<OrganizationUnit, OrganizationUnitDto, CreateOrganizationUnitDto>, IOrganizationUnitAppService
    {
        public OrganizationUnitAppService(IOrganizationUnitRepository repository) : base(repository)
        {
        }

        
    }
}
