using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WorkFlowTaskSystem.Application.WorkFlows.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.WorkFlows
{
    public interface IWorkFlowAppService : IAsyncCrudAppService<WorkFlowDto, string, PagedResultRequestDto, CreateWorkFlowDto, WorkFlowDto>
    {
    }
}
