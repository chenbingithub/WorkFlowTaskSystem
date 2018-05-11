using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WorkFlowTaskSystem.Core.Damain.Values
{
    public class OrganizationUnitRole : CreationAuditedEntity<string>
    {
        public OrganizationUnitRole()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string OrganizationUnitId { get; set; }
        public string RoleId { get; set; }
    }
}
