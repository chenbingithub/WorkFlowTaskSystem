using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
    public class PlotRatio: BaseEntity
    {
        /// <summary>
        /// 通道类型
        /// </summary>
        public string PassageType { get; set; }
        /// <summary>
        /// 容积率限值
        /// </summary>
        public string PlotRatioLimit { get; set; }
    }
}
