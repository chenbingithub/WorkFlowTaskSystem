using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WorkFlowTaskSystem.Application.Roles.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;

namespace WorkFlowTaskSystem.Application.Roles
{
    public class RoleAppService : WorkFlowTaskSystemAppServiceBase<Role, RoleDto, CreateRoleDto>, IRoleAppService
    {
        public RoleAppService(IRoleRepository repository) : base(repository)
        {
        }

        
    }
}
