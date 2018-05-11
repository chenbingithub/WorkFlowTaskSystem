using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.Basics.PermissionInfos.Dto;

namespace WorkFlowTaskSystem.Application.Basics.PermissionInfos
{
    
    public interface IPermissionInfoAppService : IWorkFlowTaskSystemAppServiceBase<PermissionInfoDto, CreatePermissionInfoDto>
    {
        Task<List<PermissionView>> GetPermissionByParentId(string parentId);
        Task<List<PermissionView>> GetAllTree();
    }
}
