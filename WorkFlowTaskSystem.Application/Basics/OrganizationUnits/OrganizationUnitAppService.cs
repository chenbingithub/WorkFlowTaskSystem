using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.Basics.OrganizationUnits.Dto;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;

namespace WorkFlowTaskSystem.Application.Basics.OrganizationUnits
{
    public class OrganizationUnitAppService : WorkFlowTaskSystemAppServiceBase<OrganizationUnit, OrganizationUnitDto, CreateOrganizationUnitDto>, IOrganizationUnitAppService
    {
        public OrganizationUnitAppService(IOrganizationUnitRepository repository) : base(repository)
        {
        }
        public Task<List<IviewTree>> GetAllTree()
        {
            List<OrganizationUnitDto> all = Repository.GetAll().AsEnumerable().Select(MapToEntityDto).ToList();
            List<IviewTree> data = IviewTree.RecursiveQueries(all);
            return Task.FromResult(data);
        }
       

    }
}
