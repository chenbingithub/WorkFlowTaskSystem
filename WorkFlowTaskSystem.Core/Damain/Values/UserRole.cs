using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WorkFlowTaskSystem.Core.Damain.Values
{
    public class UserRole : CreationAuditedEntity<string>
    {
        public UserRole()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
