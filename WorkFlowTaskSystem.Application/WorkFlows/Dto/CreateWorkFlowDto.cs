using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Entitys.WorkFlows;

namespace WorkFlowTaskSystem.Application.WorkFlows.Dto
{
    [AutoMapTo(typeof(WorkFlow))]
    public class CreateWorkFlowDto : EntityDto<string>
    {
        public CreateWorkFlowDto()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 流程编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 流程id
        /// </summary>
        public string WorkFlowId { get; set; }
        /// <summary>
        /// 活动节点 或 任务节点
        /// </summary>
        public List<WorkTask> WorkTasks { get; set; }
        /// <summary>
        /// 表单id
        /// </summary>
        public string Formid { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        
    }
}
