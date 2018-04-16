using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
    public class BaseEntity : AuditedEntity<string>, IDeletionAudited, IPassivable
    {
        /// <summary>
        /// 操作人id
        /// </summary>
        public long? DeleterUserId { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletionTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get ; set ; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsActive { get; set; }
    }
}
