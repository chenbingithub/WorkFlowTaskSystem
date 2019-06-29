using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.Core.ViewModel
{
   public class ReportView
    {
        public string Number { get; set; }
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
        public double SectionalArea { get; set; }
        /// <summary>
        /// 容积率限值
        /// </summary>
        public double PlotRatioLimit { get; set; }
        /// <summary>
        /// 重量限值
        /// </summary>
        public double WeightLimit { get; set; }
        /// <summary>
        /// 容积率
        /// </summary>
        public double PlotRatio { get; set; }
        /// <summary>
        /// 电缆根数
        /// </summary>
        public int CableNumber { get; set; }
        public double CableWeight { get; set; }
        /// <summary>
        /// 电缆编码
        /// </summary>
        public string  CableCodes{ get; set; }
        /// <summary>
        /// 电缆截面积
        /// </summary>
        public double CableArea { get; set; }
    }
}
