
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Documents.BridgeConstants.Dto
{
    [AutoMap(typeof(BridgeConstant))]
    public class BridgeConstantDto : EntityDto<string>
    {
    /// <summary>
      /// �żܱ���
      /// </summary>
      public string BridgeCode { get; set; }
      /// <summary>
      /// ͨ������
      /// </summary>
      public string PassageType { get; set; }
      /// <summary>
      /// �żܽ����
      /// </summary>
      public string SectionalArea { get; set; }
      /// <summary>
      /// �ݻ�����ֵ
      /// </summary>
      public string PlotRatioLimit { get; set; }
      /// <summary>
      /// ������ֵ
      /// </summary>
      public string WeightLimit { get; set; }
    public string Description { get; set; }

    }
}
