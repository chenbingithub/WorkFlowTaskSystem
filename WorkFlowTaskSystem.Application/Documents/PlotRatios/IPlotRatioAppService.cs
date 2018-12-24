
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.Documents.PlotRatios.Dto;
using WorkFlowTaskSystem.Core.ViewModel;


namespace WorkFlowTaskSystem.Application.Documents.PlotRatios
{
    
    public interface IPlotRatioAppService : IWorkFlowTaskSystemAppServiceBase<PlotRatioDto, PlotRatioDto>
    {
      void RealDeleteAll();
    }
    
}
