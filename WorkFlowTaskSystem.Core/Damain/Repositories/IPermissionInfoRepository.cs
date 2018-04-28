using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Core.Damain.Repositories
{
    public interface IPermissionInfoRepository : IRepository<PermissionInfo, string>
    {
        
    }
}