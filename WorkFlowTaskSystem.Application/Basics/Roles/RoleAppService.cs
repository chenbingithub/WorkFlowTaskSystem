using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Extensions;
using WorkFlowTaskSystem.Application.Basics.Roles.Dto;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;

namespace WorkFlowTaskSystem.Application.Basics.Roles
{
    public class RoleAppService : WorkFlowTaskSystemAppServiceBase<Role, RoleDto, CreateRoleDto>, IRoleAppService
    {
        private RoleManager _roleManager;
        public RoleAppService(IRoleRepository repository, RoleManager roleManager) : base(repository)
        {
            CreatePermissionName = PermissionNames.Pages_Roles_Create;
            UpdatePermissionName = PermissionNames.Pages_Roles_Update;
            DeletePermissionName = PermissionNames.Pages_Roles_Delete;
            GetAllPermissionName = PermissionNames.Pages_Roles_GetAll;
            GetPermissionName = PermissionNames.Pages_Roles_GetAll;
            _roleManager = roleManager;
        }

        public override Task<RoleDto> Create(CreateRoleDto input)
        {
            if (IsGranted(PermissionNames.Pages_Roles_SetPers)) {
                _roleManager.SetPermission(input.Id, input.PersIds);
            }
            CheckCreatePermission();
            return base.Create(input);
        }

        public override Task<RoleDto> Update(RoleDto input)
        {
            if (IsGranted(PermissionNames.Pages_Roles_SetPers))
            {
                _roleManager.SetPermission(input.Id, input.PersIds);
            }
            CheckUpdatePermission();
            return base.Update(input);
        }
        public object GetPers(string roleId)
        {
            List<IviewTree> permissions = new List<IviewTree>();
            if (IsGranted(PermissionNames.Pages_Roles_SetPers))
            {
                permissions = _roleManager.GetPermissionTree(roleId);
            }
            var data = new { permissions };
            return data;
        }
    }
}
