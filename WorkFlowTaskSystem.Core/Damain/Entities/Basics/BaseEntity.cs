using System;
using Abp.Domain.Entities.Auditing;

namespace WorkFlowTaskSystem.Core.Damain.Entities.Basics
{
    public class BaseEntity : AuditedEntity<string>, IDeletionAudited
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
        
    }
}
