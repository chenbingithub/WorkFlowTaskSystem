using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Application.Forms.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;

namespace WorkFlowTaskSystem.Application.Forms
{
    public class FormInstanceAppService : WorkFlowTaskSystemAppServiceBase<FormInstance, FormInstanceDto,CreateFormInstanceDto>, IFormInstanceAppService
    {
        public FormInstanceAppService(IFormInstanceRepository repository) : base(repository)
        {
        }
    }
}
