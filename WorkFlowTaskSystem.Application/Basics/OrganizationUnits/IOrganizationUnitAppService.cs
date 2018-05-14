using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.Basics.OrganizationUnits.Dto;
using WorkFlowTaskSystem.Core;

namespace WorkFlowTaskSystem.Application.Basics.OrganizationUnits
{
    
    public interface IOrganizationUnitAppService : IWorkFlowTaskSystemAppServiceBase<OrganizationUnitDto, CreateOrganizationUnitDto>
    {
        Task<List<IviewTree>> GetAllTree();
    }
}
