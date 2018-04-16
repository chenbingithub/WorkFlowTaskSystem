using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Core.IRepositories
{
    public interface IFormRepository : IRepository<Form, string>
    {
        
    }
}