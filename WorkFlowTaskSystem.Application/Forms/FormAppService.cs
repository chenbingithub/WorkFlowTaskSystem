using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using MongoDB.Bson;
using WorkFlowTaskSystem.Application.Forms.Dto;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Entitys.Forms;

namespace WorkFlowTaskSystem.Application.Forms
{
    public class FormAppService : AsyncCrudAppService<Form, FormDto, string, PagedResultRequestDto, CreateFormDto, FormDto>, IFormAppService
    {
        public FormAppService(IRepository<Form, string> repository) : base(repository)
        {
            
        }
       
    }
}
