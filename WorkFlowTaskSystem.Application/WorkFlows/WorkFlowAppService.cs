using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Application.WorkFlows.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;

namespace WorkFlowTaskSystem.Application.WorkFlows
{
    public class WorkFlowAppService : WorkFlowTaskSystemAppServiceBase<WorkFlow, WorkFlowDto,CreateWorkFlowDto>, IWorkFlowAppService
    {
        public WorkFlowAppService(IWorkFlowRepository repository) : base(repository)
        {
        }

        

        
    }
}
