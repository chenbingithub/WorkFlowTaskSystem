using Abp.Authorization;
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
    [AbpAuthorize(PermissionNames.Pages_OrganizationUnits)]
    public class OrganizationUnitAppService : WorkFlowTaskSystemAppServiceBase<OrganizationUnit, OrganizationUnitDto, CreateOrganizationUnitDto>, IOrganizationUnitAppService
    {
        private OrganizationUnitManager _organizationUnitManager;
        public OrganizationUnitAppService(IOrganizationUnitRepository repository, OrganizationUnitManager organizationUnitManager) : base(repository)
        {
            CreatePermissionName = PermissionNames.Pages_OrganizationUnits_Create;
            UpdatePermissionName = PermissionNames.Pages_OrganizationUnits_Update;
            DeletePermissionName = PermissionNames.Pages_OrganizationUnits_Delete;
            GetAllPermissionName = PermissionNames.Pages_OrganizationUnits_GetAll;
            GetPermissionName= PermissionNames.Pages_OrganizationUnits_GetAll;
            _organizationUnitManager = organizationUnitManager;
        }

        public override Task<OrganizationUnitDto> Create(CreateOrganizationUnitDto input)
        {
            CheckCreatePermission();
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
            if (IsGranted(PermissionNames.Pages_OrganizationUnits_SetRole)) {
                _organizationUnitManager.SetRole(input.Id, input.RoleIds);
            }
            if (IsGranted(PermissionNames.Pages_OrganizationUnits_SetPers)) {
                _organizationUnitManager.SetPermission(input.Id, input.PersIds);
            }
            
            return Task.FromResult(MapToEntityDto(entity));
        }

        public override Task<OrganizationUnitDto> Update(OrganizationUnitDto input)
        {
            CheckUpdatePermission();
            var parent = Repository.Get(input.ParentId ?? "-2");
            var entity =Repository.Get(input.Id);
            MapToEntity(input, entity);
            if (parent != null)
            {
                entity.Path = parent.Path + "," + parent.Id;
                entity.PathName = parent.PathName + "," + parent.Name;
            }
            Repository.Update(entity);
            if (IsGranted(PermissionNames.Pages_OrganizationUnits_SetRole))
            {
                _organizationUnitManager.SetRole(input.Id, input.RoleIds);
            }
            if (IsGranted(PermissionNames.Pages_OrganizationUnits_SetPers))
            {
                _organizationUnitManager.SetPermission(input.Id, input.PersIds);
            }
            return Task.FromResult(MapToEntityDto(entity));
        }
        public object GetRolePers(string organzitionId)
        {
            List<IviewTree> roles = new List<IviewTree>();
            List<IviewTree> permissions = new List<IviewTree>();
            if (IsGranted(PermissionNames.Pages_OrganizationUnits_SetRole))
            {
                 roles = _organizationUnitManager.GetRoleTree(organzitionId);
            }
            if (IsGranted(PermissionNames.Pages_OrganizationUnits_SetPers))
            {
                 permissions = _organizationUnitManager.GetPermissionTree(organzitionId);
            }
            
            
            var data = new { roles, permissions };
            return data;
        }

        public Task<List<IviewTree>> GetAllTree()
        {
            CheckGetAllPermission();
            List<OrganizationUnitDto> all = Repository.GetAll().AsEnumerable().Select(MapToEntityDto).ToList();
            List<IviewTree> data = IviewTree.RecursiveQueries(all);
            return Task.FromResult(data);
        }
       

    }
}
