using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WorkFlowTaskSystem.Application.Forms.Dto;

namespace WorkFlowTaskSystem.Application.Forms
{
    public interface IFormInstanceAppService : IAsyncCrudAppService<FormInstanceDto, string, PagedResultRequestDto, CreateFormInstanceDto, FormInstanceDto>
    {
    }
}
