using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.Basics.OrganizationUnits.Dto;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;

namespace WorkFlowTaskSystem.Application.Basics.OrganizationUnits
{
    public class OrganizationUnitAppService : WorkFlowTaskSystemAppServiceBase<OrganizationUnit, OrganizationUnitDto, CreateOrganizationUnitDto>, IOrganizationUnitAppService
    {
        private OrganizationUnitManager _organizationUnitManager;
        public OrganizationUnitAppService(IOrganizationUnitRepository repository, OrganizationUnitManager organizationUnitManager) : base(repository)
        {
            _organizationUnitManager = organizationUnitManager;
        }

        public override Task<OrganizationUnitDto> Create(CreateOrganizationUnitDto input)
        {
            var parent=Repository.Get(input.ParentId ?? "-2");
            var entity = MapToEntity(input);
            if (parent != null)
            {
                entity.Path = parent.Path + parent.Id + ",";
                entity.PathName = parent.PathName + "," + entity.Name;
            }
            else
            {
                entity.PathName =entity.Name;
            }
            Repository.Insert(entity);
            _organizationUnitManager.SetRole(input.Id, input.RoleIds);
            _organizationUnitManager.SetPermission(input.Id, input.PersIds);
            return Task.FromResult(MapToEntityDto(entity));
        }

        public override Task<OrganizationUnitDto> Update(OrganizationUnitDto input)
        {
            var parent = Repository.Get(input.ParentId ?? "-2");
            var entity =Repository.Get(input.Id);
            MapToEntity(input, entity);
            if (parent != null)
            {
                entity.Path = parent.Path + "," + parent.Id;
                entity.PathName = parent.PathName + "," + parent.Name;
            }
            Repository.Update(entity);
            _organizationUnitManager.SetRole(input.Id, input.RoleIds);
            _organizationUnitManager.SetPermission(input.Id, input.PersIds);
            return Task.FromResult(MapToEntityDto(entity));
        }
        public object GetRolePers(string organzitionId)
        {

            var roles = _organizationUnitManager.GetRoleTree(organzitionId);
            var permissions = _organizationUnitManager.GetPermissionTree(organzitionId);
            var data = new { roles, permissions };
            return data;
        }

        public Task<List<IviewTree>> GetAllTree()
        {
            List<OrganizationUnitDto> all = Repository.GetAll().AsEnumerable().Select(MapToEntityDto).ToList();
            List<IviewTree> data = IviewTree.RecursiveQueries(all);
            return Task.FromResult(data);
        }
       

    }
}
