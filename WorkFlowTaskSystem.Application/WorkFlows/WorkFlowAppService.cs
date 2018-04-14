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
    public class WorkFlowAppService : AsyncCrudAppService<WorkFlow, WorkFlowDto, string, PagedResultRequestDto, CreateWorkFlowDto, WorkFlowDto>, IWorkFlowAppService
    {
        public WorkFlowAppService(IRepository<WorkFlow, string> repository) : base(repository)
        {
        }
    }
}
