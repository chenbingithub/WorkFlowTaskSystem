using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Cors;
using WorkFlowTaskSystem.Application.Roles.Dto;

namespace WorkFlowTaskSystem.Application.Roles
{
    
    public interface IRoleAppService : IWorkFlowTaskSystemAppServiceBase<RoleDto, CreateRoleDto>
    {
    }
}
