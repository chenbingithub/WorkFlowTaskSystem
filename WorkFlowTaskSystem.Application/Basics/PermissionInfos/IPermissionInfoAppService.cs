using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.Basics.PermissionInfos.Dto;
using WorkFlowTaskSystem.Core;

namespace WorkFlowTaskSystem.Application.Basics.PermissionInfos
{
    
    public interface IPermissionInfoAppService : IWorkFlowTaskSystemAppServiceBase<PermissionInfoDto, CreatePermissionInfoDto>
    {
        Task<List<IviewTree>> GetPermissionByParentId(string parentId);
        Task<List<IviewTree>> GetAllTree();
    }
}
