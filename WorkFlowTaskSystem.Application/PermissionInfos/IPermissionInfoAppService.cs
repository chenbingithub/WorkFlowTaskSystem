using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.PermissionInfos.Dto;

namespace WorkFlowTaskSystem.Application.PermissionInfos
{
    
    public interface IPermissionInfoAppService : IWorkFlowTaskSystemAppServiceBase<PermissionInfoDto, CreatePermissionInfoDto>
    {
        Task<List<PermissionView>> GetPermissionByParentId(string parentId);
        Task<List<PermissionView>> GetAllTree();
    }
}
