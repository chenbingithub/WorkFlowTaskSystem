using System.Collections.Generic;

namespace WorkFlowTaskSystem.Core.Entitys.WorkFlows
{
    /// <summary>
    /// 定义工作流类
    /// </summary>
    public class WorkFlow : BaseEntity
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
    }
    /// <summary>
    /// 工作任务类
    /// </summary>
    public class WorkTask {
        public string Name { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// 当前任务id
        /// </summary>
        public string WorkTaskId { get; set; }
        /// <summary>
        /// 下一个任务id
        /// </summary>
        public string NextWorkTaskId { get; set; }
        /// <summary>
        /// 任务类型 
        /// </summary>
        public TaskType TaskTypeId { get; set; }
        /// <summary>
        /// 当前任务处理类型
        /// </summary>
        public OperationType OperationTypeId { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public List<OperationUser> OperationUsers { get; set; }
    }
    public class OperationUser{
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string UserNo { get; set; }
    }

    /// <summary>
    /// 任务类型
    /// </summary>
    public enum TaskType {

        /// <summary>
        /// 开始
        /// </summary>
        Start,
        /// <summary>
        /// 结束
        /// </summary>
        End,
        /// <summary>
        /// 进行中
        /// </summary>
        Underway
    }
    /// <summary>
    /// 处理类型
    /// </summary>
    public enum OperationType {
        /// <summary>
        /// 多个人共同处理
        /// </summary>
        All,
        /// <summary>
        /// 多个人之中其中一个处理即可
        /// </summary>
        ChocieOne,
        /// <summary>
        /// 仅仅是一个人处理
        /// </summary>
        OnlyOne
    }

    
}
