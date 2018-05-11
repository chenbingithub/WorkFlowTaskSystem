using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WorkFlowTaskSystem.Core.Damain.Values
{
    public class OrganizationUnitUser: CreationAuditedEntity<string>
    {
        public OrganizationUnitUser()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string OrganizationUnitId { get; set; }
        public string UserId { get; set; }
    }
}
