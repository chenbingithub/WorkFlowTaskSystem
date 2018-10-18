using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{   
    /// <summary>
    /// 桥架实例
    /// </summary>
    public class BridgeInstances:BaseEntity
    {
    /// <summary>
      /// 桥架编码
      /// </summary>
      public string BridgeCode { get; set; }
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
