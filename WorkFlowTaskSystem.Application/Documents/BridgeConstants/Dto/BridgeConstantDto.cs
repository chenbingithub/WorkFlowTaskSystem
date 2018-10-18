
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Documents.BridgeConstants.Dto
{
    [AutoMap(typeof(BridgeConstant))]
    public class BridgeConstantDto : EntityDto<string>
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
    public string Description { get; set; }

    }
}
