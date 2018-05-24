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
            CreatePermissionName = PermissionNames.Pages_Permissions_Create;
            UpdatePermissionName = PermissionNames.Pages_Permissions_Update;
            DeletePermissionName = PermissionNames.Pages_Permissions_Delete;
            GetAllPermissionName = PermissionNames.Pages_Permissions_GetAll;
            GetPermissionName = PermissionNames.Pages_Permissions_GetAll;
            _permissionManager = permissionManager;
        }

        public override Task<PermissionInfoDto> Create(CreatePermissionInfoDto input)
        {
            CheckCreatePermission();
            var parent = Repository.Get(input.ParentId ?? "-2");
            var entity = MapToEntity(input);
            if (parent != null)
            {
                entity.Path = parent.Path + parent.Id + ",";
                entity.PathName = parent.PathName + "," + entity.Name;
            }
            else
            {
                entity.PathName = entity.Name;
            }
            input.ParentId = input.ParentId ?? "-1";
            Repository.Insert(entity);
           

            return Task.FromResult(MapToEntityDto(entity));

        }
        public override Task<PermissionInfoDto> Update(PermissionInfoDto input)
        {
            CheckUpdatePermission();
            var parent = Repository.Get(input.ParentId ?? "-2");
            var entity = Repository.Get(input.Id);
            MapToEntity(input, entity);
            if (parent != null)
            {
                entity.Path = parent.Path + "," + parent.Id;
                entity.PathName = parent.PathName + "," + parent.Name;
            }
            Repository.Update(entity);
            
            return Task.FromResult(MapToEntityDto(entity));
        }
        /// <summary>
        /// 区分大小写查询
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public PermissionInfo GetPermission(string code) {
            CheckGetAllPermission();
            return Repository.GetAll().FirstOrDefault(u => u.Code == code);
        }
        /// <summary>
        /// 获取当前节点下的子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public Task<List<IviewTree>> GetPermissionByParentId(string parentId) {
            CheckGetAllPermission();
            var list= Repository.GetAll().Where(u => u.ParentId == (parentId ?? "-1")).AsEnumerable().Select(MapToEntityDto).ToList();
            List<IviewTree> data = IviewTree.RecursiveQueries(list, parentId);
            return Task.FromResult(data);
        }

        public Task<List<PermissionInfoDto>> GetAllList()
        {
            CheckGetAllPermission();
            List<PermissionInfoDto> all = Repository.GetAll().AsEnumerable().Select(MapToEntityDto).ToList();
            return Task.FromResult(all);
        }

        public Task<List<IviewTree>> GetAllTree()
        {
            CheckGetAllPermission();
            List<PermissionInfoDto> all =Repository.GetAll().AsEnumerable().Select(MapToEntityDto).ToList();
            List<IviewTree> data = IviewTree.RecursiveQueries(all);
            return Task.FromResult(data);
        }
       

    }
}
