using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.WorkFlows.Dto
{
    [AutoMap(typeof(WorkFlow))]
    public class WorkFlowDto : EntityDto<string>
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 流程编码
        /// </summary>
        public string Code { get; set; }
       
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
