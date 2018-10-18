
using WorkFlowTaskSystem.Application.Documents.BridgeConstants.Dto;
using WorkFlowTaskSystem.Core.ViewModel;


namespace WorkFlowTaskSystem.Application.Documents.BridgeConstants
{
    
    public interface IBridgeConstantAppService : IWorkFlowTaskSystemAppServiceBase<BridgeConstantDto, BridgeConstantDto>
    {
      void Insert(ConstantView enView);
    }
    
}
