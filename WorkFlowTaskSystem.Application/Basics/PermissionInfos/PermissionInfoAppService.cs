using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.Basics.PermissionInfos.Dto;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Services;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;

namespace WorkFlowTaskSystem.Application.Basics.PermissionInfos
{
    public class PermissionInfoAppService : WorkFlowTaskSystemAppServiceBase<PermissionInfo, PermissionInfoDto, CreatePermissionInfoDto>, IPermissionInfoAppService
    {
        private PermissionInfoManager _permissionManager;
        public PermissionInfoAppService(IPermissionInfoRepository repository, PermissionInfoManager permissionManager) : base(repository)
        {
            _permissionManager = permissionManager;
        }

        /// <summary>
        /// 获取当前节点下的子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public Task<List<IviewTree>> GetPermissionByParentId(string parentId) {
            var list= Repository.GetAll().Where(u => u.ParentId == (parentId ?? "-1")).AsEnumerable().Select(MapToEntityDto).ToList();
            List<IviewTree> data = IviewTree.RecursiveQueries(list, parentId);
            return Task.FromResult(data);
        }

        public Task<List<PermissionInfoDto>> GetAllList()
        {
            List<PermissionInfoDto> all = Repository.GetAll().AsEnumerable().Select(MapToEntityDto).ToList();
            return Task.FromResult(all);
        }

        public Task<List<IviewTree>> GetAllTree()
        {
            List<PermissionInfoDto> all =Repository.GetAll().AsEnumerable().Select(MapToEntityDto).ToList();
            List<IviewTree> data = IviewTree.RecursiveQueries(all);
            return Task.FromResult(data);
        }
       

    }
}
