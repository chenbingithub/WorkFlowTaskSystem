using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Core.IRepositories
{
    public interface IWorkFlowInstanceRepository : IRepository<WorkFlowInstance, string>
    {
        
    }
}