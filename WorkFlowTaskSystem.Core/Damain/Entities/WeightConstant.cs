using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
    public class WeightConstant : BaseEntity
    {
        /// <summary>
        /// 通道类型
        /// </summary>
        public string PassageTypes { get; set; }
        public string PassageTypeName { get; set; }
        public decimal WeightDecimal { get; set; }
        /// <summary>
        /// 重量限值
        /// </summary>
        public string WeightLimit { get; set; }
    }
}
