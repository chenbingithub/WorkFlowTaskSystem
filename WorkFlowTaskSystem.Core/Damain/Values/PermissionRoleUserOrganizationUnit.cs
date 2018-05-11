using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WorkFlowTaskSystem.Core.Damain.Values
{
   public class PermissionRoleUserOrganizationUnit : CreationAuditedEntity<string>
    {
        public PermissionRoleUserOrganizationUnit()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string PermissionId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public string OrganizationUnitId { get; set; }
    }
}
