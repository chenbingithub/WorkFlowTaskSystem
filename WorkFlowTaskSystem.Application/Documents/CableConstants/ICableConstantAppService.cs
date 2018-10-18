using WorkFlowTaskSystem.Application.Documents.CableConstants.Dto;
using WorkFlowTaskSystem.Core.ViewModel;

namespace WorkFlowTaskSystem.Application.Documents.CableConstants
{
    
    public interface ICableConstantAppService : IWorkFlowTaskSystemAppServiceBase<CableConstantDto, CableConstantDto>
    {
      void Insert(ConstantView enView);
    }
}
