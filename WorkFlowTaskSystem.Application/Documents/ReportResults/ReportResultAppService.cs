using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;
using WorkFlowTaskSystem.Application.Documents.ReportResults.Dto;

using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.ViewModel;
using WorkFlowTaskSystem.MongoDb;

namespace WorkFlowTaskSystem.Application.Documents.ReportResults
{
    public class ReportResultAppService : WorkFlowTaskSystemAppServiceBase<ReportResult, ReportResultDto, ReportResultDto>, IReportResultAppService
    {
      private IHostingEnvironment _hostingEnvironment;
      public ReportResultAppService(IRepository<ReportResult,string> repository, IHostingEnvironment hostingEnvironment) : base(repository)
      {
        _hostingEnvironment = hostingEnvironment;
      }

    

      public override Task Delete(EntityDto<string> input)
      {
        Repository.RealDelete(input.Id);
        return Task.CompletedTask;
      }

      public void RealDeleteAll()
      {
        Repository.RealDeleteAll();
      }
    }
}
