using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.WorkFlows.Dto
{
    [AutoMap(typeof(WorkFlowInstance))]
    public class CreateWorkFlowInstanceDto : EntityDto<string>
    {
        public CreateWorkFlowInstanceDto()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 流程定义id
        /// </summary>
        public string WorkFlowId { get; set; }

        
        /// <summary>
        /// 当前任务id
        /// </summary>
        public string WorkTaskId { get; set; }

        /// <summary>
        /// 表单实例id
        /// </summary>
        public string FormInsId { get; set; }


        
        /// <summary>
        /// 标题名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 编码名称
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建者工号
        /// </summary>
        public string CreateUserNo { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public List<OperationUser> OperationUsers { get; set; }

    }
    
}
