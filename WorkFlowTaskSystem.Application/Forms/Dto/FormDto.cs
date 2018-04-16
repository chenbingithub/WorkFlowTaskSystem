using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MongoDB.Bson;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Forms.Dto
{
    [AutoMap(typeof(Form))]
    public class FormDto : EntityDto<string>
    {
        /// <summary>
        /// 表单项
        /// </summary>
        public List<FormItem> FormItems { get; set; }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string Name { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
       
    }
    
}
