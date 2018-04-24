using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Cors;
using WorkFlowTaskSystem.Application.Roles.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;

namespace WorkFlowTaskSystem.Application.Roles
{
    [EnableCors("any")] //设置跨域处理的 代理
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, string, PagedResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        public RoleAppService(IRoleRepository repository) : base(repository)
        {
        }

        public override Task<PagedResultDto<RoleDto>> GetAll(PagedResultRequestDto input)
        {
            Console.WriteLine();
            return base.GetAll(input);
        }
    }
}
