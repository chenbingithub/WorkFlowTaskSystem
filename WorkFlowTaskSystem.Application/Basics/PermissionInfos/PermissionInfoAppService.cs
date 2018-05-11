using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.Basics.PermissionInfos.Dto;
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


        public Task<List<PermissionView>> GetPermissionByParentId(string parentId) {
            var list= Repository.GetAll().Where(u => u.ParentId == (parentId ?? "-1")).AsEnumerable().Select(MapToEntityDto).ToList();
            List<PermissionView> data = list.Select(e => new PermissionView(){title = e.Name,data = e}).ToList();
            return Task.FromResult(data);
        }

        public Task<List<PermissionView>> GetAllTree()
        {
            List<PermissionInfoDto> all =Repository.GetAll().AsEnumerable().Select(MapToEntityDto).ToList();
            List<PermissionView> data = RecursiveQueries(all);
            return Task.FromResult(data);
        }
        /// <summary>
        /// 递归查询组成树形结构
        /// </summary>
        /// <param name="all">源</param>
        /// <param name="currentId"></param>
        /// <returns></returns>
        private List<PermissionView> RecursiveQueries(List<PermissionInfoDto> all, string currentId=null)
        {
            if (string.IsNullOrEmpty(currentId))
            {
                currentId = "-1";
            }
           List<PermissionView> data= all.Where(u => u.ParentId == currentId).Select(e => new PermissionView() { title = e.Name, data = e }).ToList();
            if (data == null || data.Count <= 0)
            {
                return  new List<PermissionView>();
            }
            foreach (var dto in data)
            {
                dto.children=new List<PermissionView>();
                dto.children.AddRange(RecursiveQueries(all, dto.data.Id));               
            }
            return data;
        }

    }
}
