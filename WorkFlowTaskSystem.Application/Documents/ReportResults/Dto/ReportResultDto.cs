
using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Documents.ReportResults.Dto
{
    [AutoMap(typeof(ReportResult))]
    public class ReportResultDto : EntityDto<string>
    {
      public ReportResultDto()
      {
        Id = Guid.NewGuid().ToString("N");
      }
      public string Name { get; set; }
      
      public string Url { get; set; }
    
      public string Description { get; set; }

    }
}
