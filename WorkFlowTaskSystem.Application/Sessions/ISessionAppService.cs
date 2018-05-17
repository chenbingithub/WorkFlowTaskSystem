using System.Threading.Tasks;
using Abp.Application.Services;
using WorkFlowTaskSystem.Application.Sessions.Dto;

namespace WorkFlowTaskSystem.Application.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
