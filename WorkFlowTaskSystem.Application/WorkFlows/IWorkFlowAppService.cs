using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WorkFlowTaskSystem.Application.WorkFlows.Dto;

namespace WorkFlowTaskSystem.Application.WorkFlows
{
    public interface IWorkFlowAppService : IAsyncCrudAppService<WorkFlowDto, string, PagedResultRequestDto, CreateWorkFlowDto, WorkFlowDto>
    {
    }
}
