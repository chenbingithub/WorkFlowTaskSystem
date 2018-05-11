using WorkFlowTaskSystem.Application.Basics.Roles.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;

namespace WorkFlowTaskSystem.Application.Basics.Roles
{
    public class RoleAppService : WorkFlowTaskSystemAppServiceBase<Role, RoleDto, CreateRoleDto>, IRoleAppService
    {
        public RoleAppService(IRoleRepository repository) : base(repository)
        {
        }

        
    }
}
