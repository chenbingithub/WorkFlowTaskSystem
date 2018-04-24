using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Net.Mail.Smtp;
using Abp.Runtime.Caching;
using WorkFlowTaskSystem.Application.Forms.Dto;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;

namespace WorkFlowTaskSystem.Application.Forms
{
    public class FormAppService : WorkFlowTaskSystemAppServiceBase<Form, FormDto,  CreateFormDto>, IFormAppService
    {
       
        private ICacheManager _cacheManager;
        public FormAppService(IFormRepository repository, ICacheManager cacheManager) : base(repository)
        {
            _cacheManager = cacheManager;
           
        }

        /// <summary>
        /// 使用redis缓存
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<FormDto> Get(EntityDto<string> input)
        {
           
            return _cacheManager.GetCache("forms").Get(input.Id, () => base.Get(input));
        }
    }
}
