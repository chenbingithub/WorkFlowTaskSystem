using System.Threading.Tasks;
using Abp.Extensions;
using WorkFlowTaskSystem.Application.Basics.Roles.Dto;
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
            _roleManager = roleManager;
        }

        public override Task<RoleDto> Create(CreateRoleDto input)
        {
            _roleManager.SetPermission(input.Id,input.PersIds);
            return base.Create(input);
        }

        public override Task<RoleDto> Update(RoleDto input)
        {
            _roleManager.SetPermission(input.Id, input.PersIds);
            return base.Update(input);
        }
        public object GetPers(string roleId)
        {

            var permissions = _roleManager.GetPermissionTree(roleId);
            var data = new { permissions };
            return data;
        }
    }
}
