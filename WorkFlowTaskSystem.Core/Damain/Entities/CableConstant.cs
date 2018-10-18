using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
    /// <summary>
    /// 电缆常量基本信息
    /// </summary>
     public class CableConstant : BaseEntity
    {
      /// <summary>
      /// 型号
      /// </summary>
      public string Version { get; set; }
      /// <summary>
      /// 规格
      /// </summary>
      public string Specification { get; set; }
      /// <summary>
      /// 外径
      /// </summary>
      public string Diameter { get; set; }
      /// <summary>
      /// 重量限值
      /// </summary>
      public string WeightLimit { get; set; }
  }
}
