using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
    /// <summary>
    /// 桥架常量基本信息
    /// </summary>
    public class BridgeConstant : BaseEntity
    {
        public string Number { get; set; }
      /// <summary>
      /// 桥架编码
      /// </summary>
      public string BridgeCode { get; set; }
        /// <summary>
        /// 系列
        /// </summary>
        public string Series { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Types { get; set; }
        
        /// <summary>
        /// 型号
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// 长
        /// </summary>
      public decimal Lenght { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
      public decimal Weight { get; set; }
     /// <summary>
     /// 高
     /// </summary>
      public decimal Height { get; set; }

        /// <summary>
        /// 通道类型
        /// </summary>
        public string PassageType { get; set; }
        /// <summary>
        /// 桥架截面积
        /// </summary>
        public string SectionalArea { get; set; }
        /// <summary>
        /// 容积率限值
        /// </summary>
        public string PlotRatioLimit { get; set; }
        /// <summary>
        /// 重量限值
        /// </summary>
        public string WeightLimit { get; set; }

    }
}
