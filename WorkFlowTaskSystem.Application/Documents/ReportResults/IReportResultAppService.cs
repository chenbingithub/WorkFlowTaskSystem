
using WorkFlowTaskSystem.Application.Documents.ReportResults.Dto;
using WorkFlowTaskSystem.Core.ViewModel;


namespace WorkFlowTaskSystem.Application.Documents.ReportResults
{
    
    public interface IReportResultAppService : IWorkFlowTaskSystemAppServiceBase<ReportResultDto, ReportResultDto>
    {

      void RealDeleteAll();
    }
    
}
