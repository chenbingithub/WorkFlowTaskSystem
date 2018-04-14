using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MongoDB.Bson;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Entitys.Forms;

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
        /// ����
        /// </summary>
        public List<FormItem> FormItems { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public string Name { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
        
    }
}
