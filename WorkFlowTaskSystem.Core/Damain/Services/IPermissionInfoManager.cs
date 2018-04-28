using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Services;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Core.Damain.Services
{
   public interface IPermissionInfoManager : IDomainService
    {
        List<PermissionInfo> GetPermissionByParentId(string parentId);
    }
}
