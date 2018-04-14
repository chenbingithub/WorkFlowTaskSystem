using System;
using System.Collections.Generic;

namespace WorkFlowTaskSystem.Core.Entitys.WorkFlows
{
    /// <summary>
    /// 流程实例
    /// </summary>
    public class WorkFlowInstance: BaseEntity
    {
       
        /// <summary>
        /// 当前实例id
        /// </summary>
        public string WorkFlowInsId { get; set; }
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
        /// 定义的流程
        /// </summary>
        public WorkFlow WorkFlowIns { get; set; }
    }

    public class WorkFlowRecord {

        public WorkTask WorkTaskIns { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public ActivityState ActivityStateId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string ApplyContent { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string ApplyUserName { get; set; }
        /// <summary>
        /// 审批人工号
        /// </summary>
        public string ApplyUserNo { get; set; }
    }

    public enum ActivityState {
        /// <summary>
        /// 准备状态
        /// </summary>
        Ready,
        /// <summary>
        /// 运行状态
        /// </summary>
        Running,
        /// <summary>
        /// 完成状态
        /// </summary>
        Completed,
        /// <summary>
        /// 退回状态
        /// </summary>
        SendBacked,
        /// <summary>
        /// 结束状态
        /// </summary>
        End
    }

    public enum WorkFlowState {
        /// <summary>
        /// 运行状态
        /// </summary>
        Running,
        /// <summary>
        /// 完成状态
        /// </summary>
        Completed,
        /// <summary>
        /// 终止
        /// </summary>
        Suspended,
        /// <summary>
        /// 撤销
        /// </summary>
        Withdrawed
    }
}
