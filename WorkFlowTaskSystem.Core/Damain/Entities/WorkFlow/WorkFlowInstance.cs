using System;
using System.Collections.Generic;
using System.Linq;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
    /// <summary>
    /// 流程实例
    /// </summary>
    public class WorkFlowInstance : BaseEntity
    {



        /// <summary>
        /// 工作任务id
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
        /// 流程id
        /// </summary>
        public string WorkFlowId { get; set; }

        public WorkFlow DefinitionWorkFlow { get; set; }

        /// <summary>
        /// 退回任务
        /// </summary>
        public bool BackTask(string backWorkTaskId, OperationUser currentUser, string applyContent)
        {
            //找到定义的流程
            WorkTask currenTask = DefinitionWorkFlow.WorkTasks.Find(u => u.WorkTaskId == this.WorkTaskId);
            foreach (var record in WorkFlowRecords)
            {
                if (string.Equals(record.ApplyUserNo, currentUser.UserNo,
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    record.ActivityStateId = ActivityState.SendBacked;
                    record.EndTime = DateTime.Now;
                    record.ApplyContent = applyContent ?? "不同意";
                }
                else
                {
                    if (currenTask.OperationTypeId == OperationType.All ||
                        currenTask.OperationTypeId == OperationType.ChocieOne)
                    {
                        record.ActivityStateId = ActivityState.End;
                        record.EndTime = DateTime.Now;
                        record.ApplyContent = "流程已结束";
                    }
                }
            }
            List<WorkFlowRecord> wrs = WorkFlowRecords.Where(u => u.WorkTaskIns.WorkTaskId == backWorkTaskId).ToList();
            foreach (var record in wrs)
            {
                WorkFlowRecord r = record;
                r.ActivityStateId = ActivityState.Running;
                r.CreateTime = DateTime.Now;
                r.EndTime = null;
                r.ApplyContent = "";
                WorkFlowRecords.Add(r);

            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workFlow"></param>
        /// <param name="operationUsers"></param>
        /// <returns></returns>
        public WorkFlowInstance StartTask(WorkFlow workFlow, List<OperationUser> operationUsers)
        {
            CreateTime = DateTime.Now;
            WorkFlowStateId = WorkFlowState.Running;
            WorkFlowRecords = new List<WorkFlowRecord>();
            //找到定义的流程
            DefinitionWorkFlow = workFlow;
            //处理当前任务
            WorkTask currentTask = workFlow.WorkTasks.FirstOrDefault(u => u.WorkTaskId == WorkTaskId);
            WorkFlowRecord currentRecord = new WorkFlowRecord()
            {
                WorkTaskIns = currentTask,
                ActivityName = currentTask.Name,
                ActivityStateId = ActivityState.Completed,
                ApplyContent = "编辑",
                ApplyUserName = CreateUserName,
                ApplyUserNo = CreateUserNo,
                CreateTime = DateTime.Now,
                EndTime = DateTime.Now

            };

            WorkFlowRecords.Add(currentRecord);
            //处理下一步任务
            WorkTask nxetTask = workFlow.WorkTasks.FirstOrDefault(u => u.WorkTaskId == currentTask.NextWorkTaskId);

            if (operationUsers != null && operationUsers.Count > 0)
            {
                foreach (var user in operationUsers)
                {
                    WorkFlowRecord nxetRecord = new WorkFlowRecord()
                    {
                        WorkTaskIns = nxetTask,
                        ActivityName = nxetTask.Name,
                        ActivityStateId = ActivityState.Running,
                        ApplyUserName = user.UserName,
                        ApplyUserNo = user.UserName,
                        CreateTime = DateTime.Now,
                    };
                    WorkFlowRecords.Add(nxetRecord);
                }

            }
            else
            {
                foreach (var user in nxetTask.OperationUsers ?? new List<OperationUser>())
                {
                    WorkFlowRecord nxetRecord = new WorkFlowRecord()
                    {
                        WorkTaskIns = nxetTask,
                        ActivityName = nxetTask.Name,
                        ActivityStateId = ActivityState.Running,
                        ApplyUserName = user.UserName,
                        ApplyUserNo = user.UserName,
                        CreateTime = DateTime.Now,
                    };
                    WorkFlowRecords.Add(nxetRecord);
                }

            }
            //当前未处理的任务
            WorkTaskId = nxetTask.WorkTaskId;
            return this;
        }

        public WorkFlowInstance NextTask(OperationUser currentUser, string applyContent, string nextWorkTaskId,
            List<OperationUser> operationUsers)
        {
            List<WorkFlowRecord> workFlowRecords =
                WorkFlowRecords.Where(u => u.WorkTaskIns.WorkTaskId == WorkTaskId).ToList();

            WorkTask currenTask = DefinitionWorkFlow.WorkTasks.Find(u => u.WorkTaskId == WorkTaskId);


            foreach (var record in workFlowRecords)
            {
                if (string.Equals(record.ApplyUserNo, currentUser.UserNo,
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    record.ActivityStateId = ActivityState.Completed;
                    record.EndTime = DateTime.Now;
                    record.ApplyContent = applyContent ?? "同意";
                }
                else
                {
                    if (currenTask.OperationTypeId == OperationType.ChocieOne)
                    {
                        record.ActivityStateId = ActivityState.End;
                        record.EndTime = DateTime.Now;
                        record.ApplyContent = "流程已结束";
                    }
                }
            }
            if (currenTask.OperationTypeId == OperationType.All && workFlowRecords.Count !=
                workFlowRecords.Count(u => u.ActivityStateId == ActivityState.Completed))
            {
                return this;
            }
            string nextTaskid = string.IsNullOrEmpty(nextWorkTaskId) ? currenTask.NextWorkTaskId : nextWorkTaskId;
            WorkTaskId = nextTaskid;
            WorkTask nextworkTask = DefinitionWorkFlow.WorkTasks.Find(u => u.WorkTaskId == nextTaskid);
            if (operationUsers != null && operationUsers.Count > 0)
            {
                foreach (var user in operationUsers)
                {
                    WorkFlowRecord nxetRecord = new WorkFlowRecord()
                    {
                        WorkTaskIns = nextworkTask,
                        ActivityName = nextworkTask.Name,
                        ActivityStateId = ActivityState.Running,
                        ApplyUserName = user.UserName,
                        ApplyUserNo = user.UserName,
                        CreateTime = DateTime.Now,
                    };
                    WorkFlowRecords.Add(nxetRecord);
                }
            }
            else
            {

                foreach (var user in nextworkTask.OperationUsers ?? new List<OperationUser>())
                {
                    WorkFlowRecord nxetRecord = new WorkFlowRecord()
                    {
                        WorkTaskIns = nextworkTask,
                        ActivityName = nextworkTask.Name,
                        ActivityStateId = ActivityState.Running,
                        ApplyUserName = user.UserName,
                        ApplyUserNo = user.UserName,
                        CreateTime = DateTime.Now,
                    };
                    WorkFlowRecords.Add(nxetRecord);
                }
                if (nextworkTask.TaskTypeId == TaskType.End)
                {
                    WorkFlowRecord nxetRecord = new WorkFlowRecord()
                    {
                        WorkTaskIns = nextworkTask,
                        ActivityName = nextworkTask.Name,
                        ActivityStateId = ActivityState.End,
                        ApplyContent = "流程结束",
                        CreateTime = DateTime.Now,
                        EndTime = DateTime.Now
                    };
                    WorkFlowRecords.Add(nxetRecord);
                    WorkFlowStateId = WorkFlowState.Completed;
                }
            }
            return this;
        }
    }
    public class WorkFlowRecord
        {

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

        public enum ActivityState
        {
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

        public enum WorkFlowState
        {
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
