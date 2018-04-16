using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.WorkFlows.Dto
{
    [AutoMap(typeof(WorkFlowInstance))]
    public class WorkFlowInstanceDto : EntityDto<string>
    {
       
        /// <summary>
        /// 当前任务id
        /// </summary>
        public string WorkTaskId { get; set; }

        /// <summary>
        /// 表单实例id
        /// </summary>
        public string FormInsId { get; set; }


        /// <summary>
        /// 流程状态
        /// </summary>
        public WorkFlowState WorkFlowStateId { get; set; }
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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 操作任务记录
        /// </summary>
        public List<WorkFlowRecord> WorkFlowRecords { get; set; }

        /// <summary>
        /// 流程定义id
        /// </summary>
        public string WorkFlowId { get; set; }
    }
    
}
