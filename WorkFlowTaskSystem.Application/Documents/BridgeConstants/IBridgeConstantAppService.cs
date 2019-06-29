
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.Documents.BridgeConstants.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.ViewModel;


namespace WorkFlowTaskSystem.Application.Documents.BridgeConstants
{
    
    public interface IBridgeConstantAppService : IWorkFlowTaskSystemAppServiceBase<BridgeConstantDto, BridgeConstantDto>
    {
      void Insert(ConstantView enView);
      void RealDeleteAll();
        List<BridgeConstant> GetListToNotIn(List<string> codes);
    }
    
}
