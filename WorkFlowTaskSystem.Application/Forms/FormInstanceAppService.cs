using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Application.Forms.Dto;
using WorkFlowTaskSystem.Core.Entitys.Forms;

namespace WorkFlowTaskSystem.Application.Forms
{
    public class FormInstanceAppService : AsyncCrudAppService<FormInstance, FormInstanceDto, string, PagedResultRequestDto, CreateFormInstanceDto, FormInstanceDto>, IFormInstanceAppService
    {
        public FormInstanceAppService(IRepository<FormInstance, string> repository) : base(repository)
        {
        }
    }
}
