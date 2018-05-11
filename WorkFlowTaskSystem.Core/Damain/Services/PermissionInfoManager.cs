using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Domain.Services;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;

namespace WorkFlowTaskSystem.Core.Damain.Services
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
