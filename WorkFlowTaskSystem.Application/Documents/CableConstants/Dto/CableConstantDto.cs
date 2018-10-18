using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Documents.CableConstants.Dto
{
    [AutoMap(typeof(CableConstant))]
    public class CableConstantDto : EntityDto<string>
    {
    /// <summary>
      /// �ͺ�
      /// </summary>
      public string Version { get; set; }
      /// <summary>
      /// ���
      /// </summary>
      public string Specification { get; set; }
      /// <summary>
      /// �⾶
      /// </summary>
      public string Diameter { get; set; }
      /// <summary>
      /// ������ֵ
      /// </summary>
      public string WeightLimit { get; set; }
    public string Description { get; set; }

    }
}
