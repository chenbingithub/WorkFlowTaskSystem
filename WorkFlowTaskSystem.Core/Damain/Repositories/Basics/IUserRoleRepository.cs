using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Values;

namespace WorkFlowTaskSystem.Core.Damain.Repositories.Basics
{
    public interface IUserRoleRepository : IRepository<UserRole, string>
    {
        
    }
}