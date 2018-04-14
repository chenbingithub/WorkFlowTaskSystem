using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Application.WorkFlows.Dto;
using WorkFlowTaskSystem.Core.Entitys.WorkFlows;

namespace WorkFlowTaskSystem.Application.WorkFlows
{
    public class WorkFlowInstanceAppService : AsyncCrudAppService<WorkFlowInstance, WorkFlowInstanceDto, string, PagedResultRequestDto, CreateWorkFlowInstanceDto, WorkFlowInstanceDto>, IWorkFlowInstanceAppService
    {
        public WorkFlowInstanceAppService(IRepository<WorkFlowInstance, string> repository) : base(repository)
        {
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void TaskBack()
        {
            throw new NotImplementedException();
        }

        public void TaskNext(string workflowInsId, string worktaskId, List<OperationUser> operationUsers = null)
        {
            throw new NotImplementedException();
        }
    }
}
