using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.Core.ViewModel
{
    public class ConstantView
    {
    /// <summary>
      /// 文件名
      /// </summary>
      public string Path { get; set; }

      /// <summary>
      /// 根据当前时间和随机数生成流水号,由前端生成
      /// </summary>
      public string NumberNo { get; set; }
  }
}
