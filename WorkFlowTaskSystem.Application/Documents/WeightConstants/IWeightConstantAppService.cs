
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Application.Documents.WeightConstants.Dto;
using WorkFlowTaskSystem.Core.ViewModel;


namespace WorkFlowTaskSystem.Application.Documents.WeightConstants
{
    
    public interface IWeightConstantAppService : IWorkFlowTaskSystemAppServiceBase<WeightConstantDto, WeightConstantDto>
    {
      void RealDeleteAll();
    }
    
}
