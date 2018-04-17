using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Forms.Dto
{
    [AutoMapTo(typeof(Form))]
    public class CreateFormDto 
    {
        public CreateFormDto()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string Id { get; set; }
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
