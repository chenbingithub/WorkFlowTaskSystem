using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Documents.CableConstants.Dto
{
    [AutoMap(typeof(CableConstant))]
    public class CableConstantDto : EntityDto<string>
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
    public string Description { get; set; }

    }
}
