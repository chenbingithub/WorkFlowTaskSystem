using System;
using Abp.Domain.Entities;

namespace WorkFlowTaskSystem.Core.Damain.Entities.Basics
{
    /// <summary>
    /// 用户
    /// </summary>
   public class User: BaseEntity, IPassivable
    {
        
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 是否启用 true启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
