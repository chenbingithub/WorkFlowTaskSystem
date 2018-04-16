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
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.IRepositories;

namespace WorkFlowTaskSystem.Application.Forms
{
    public class FormAppService : AsyncCrudAppService<Form, FormDto, string, PagedResultRequestDto, CreateFormDto, FormDto>, IFormAppService
    {
        public FormAppService(IFormRepository repository) : base(repository)
        {
            
        }
       
    }
}
