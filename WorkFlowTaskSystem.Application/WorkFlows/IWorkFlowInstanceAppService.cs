using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WorkFlowTaskSystem.Application.WorkFlows.Dto;
using WorkFlowTaskSystem.Core.Entitys.WorkFlows;

namespace WorkFlowTaskSystem.Application.WorkFlows
{
    public interface IWorkFlowInstanceAppService : IAsyncCrudAppService<WorkFlowInstanceDto, string, PagedResultRequestDto, CreateWorkFlowInstanceDto, WorkFlowInstanceDto>
    {
        void Start();
        void TaskNext(string workflowInsId,string worktaskId,List<OperationUser>  operationUsers=null);
        void TaskBack();
    }
}
