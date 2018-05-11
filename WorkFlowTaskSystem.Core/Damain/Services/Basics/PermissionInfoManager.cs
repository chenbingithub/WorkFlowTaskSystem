using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Services;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Services.Basics
{
    public class PermissionInfoManager : DomainService
    {
        private IPermissionInfoRepository _permissionInfoRepository;
        public PermissionInfoManager(IPermissionInfoRepository permissionInfoRepository)
        {
            _permissionInfoRepository = permissionInfoRepository;
        }

        public List<PermissionInfo> GetPermissionByParentId(string parentId) {
           return _permissionInfoRepository.GetAll().Where(u => u.ParentId == parentId).ToList();
        }
    } 
}
