using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Application.WorkFlows.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.IRepositories;

namespace WorkFlowTaskSystem.Application.WorkFlows
{
    public class WorkFlowInstanceAppService : AsyncCrudAppService<WorkFlowInstance, WorkFlowInstanceDto, string, PagedResultRequestDto, CreateWorkFlowInstanceDto, WorkFlowInstanceDto>, IWorkFlowInstanceAppService
    {
        private IWorkFlowRepository _workFlowRepository;
        public WorkFlowInstanceAppService(IWorkFlowInstanceRepository repository, IWorkFlowRepository workFlowRepository) : base(repository)
        {
            _workFlowRepository = workFlowRepository;
        }
        /// <summary>
        /// 发起流程
        /// </summary>
        public void Start(CreateWorkFlowInstanceDto entity)
        {
            WorkFlowInstance workFlowInstance = MapToEntity(entity);
            workFlowInstance.CreateTime=DateTime.Now;
            workFlowInstance.WorkFlowStateId = WorkFlowState.Running;
            workFlowInstance.WorkFlowRecords = new List<WorkFlowRecord>();
            //找到定义的流程
            WorkFlow workFlow = _workFlowRepository.FirstOrDefault(workFlowInstance.WorkFlowId);

            //处理当前任务
            WorkTask currentTask = workFlow.WorkTasks.FirstOrDefault(u => u.WorkTaskId == workFlowInstance.WorkTaskId);
            WorkFlowRecord currentRecord = new WorkFlowRecord()
            {
               WorkTaskIns = currentTask,
               ActivityName = currentTask.Name,
               ActivityStateId = ActivityState.Completed,
               ApplyContent = "同意",
               ApplyUserName = entity.CreateUserName,
               ApplyUserNo = entity.CreateUserNo,
               CreateTime = DateTime.Now,
               EndTime = DateTime.Now

            };
            
            workFlowInstance.WorkFlowRecords.Add(currentRecord);
            //处理下一步任务
            WorkTask nxetTask = workFlow.WorkTasks.FirstOrDefault(u => u.WorkTaskId == currentTask.NextWorkTaskId);
           
            if (entity.OperationUsers != null && entity.OperationUsers.Count > 0)
            {
                foreach (var user in entity.OperationUsers)
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
                    workFlowInstance.WorkFlowRecords.Add(nxetRecord);
                }

            }
            else
            {
                foreach (var user in nxetTask.OperationUsers??new List<OperationUser>())
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
                    workFlowInstance.WorkFlowRecords.Add(nxetRecord);
                }
                
            }
            //当前未处理的任务
            workFlowInstance.WorkTaskId = nxetTask.WorkTaskId;
            Repository.Insert(workFlowInstance);
            return;
        }
        /// <summary>
        /// 退回流程
        /// </summary>
        /// <param name="backTask"></param>
        public void Back(BackTask backTask)
        {
            //找到当前流程
            WorkFlowInstance currentFlow = Repository.FirstOrDefault(backTask.WorkflowInsId);
            List<WorkFlowRecord> workFlowRecords = currentFlow.WorkFlowRecords.Where(u => u.WorkTaskIns.WorkTaskId == currentFlow.WorkTaskId).ToList();
            //找到定义的流程
            WorkFlow workFlow = _workFlowRepository.FirstOrDefault(currentFlow.WorkFlowId);
            WorkTask currenTask = workFlow.WorkTasks.Find(u => u.WorkTaskId == currentFlow.WorkTaskId);
            foreach (var record in workFlowRecords)
            {
                if (string.Equals(record.ApplyUserNo, backTask.CurrentUser.UserNo,
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    record.ActivityStateId = ActivityState.SendBacked;
                    record.EndTime = DateTime.Now;
                    record.ApplyContent = backTask.ApplyContent ?? "不同意";
                }
                else
                {
                    if (currenTask.OperationTypeId == OperationType.All || currenTask.OperationTypeId == OperationType.ChocieOne)
                    {
                        record.ActivityStateId = ActivityState.End;
                        record.EndTime = DateTime.Now;
                        record.ApplyContent = "流程已结束";
                    }
                }
            }
            string backtaskId = backTask.BackWorkTaskId ;
            List<WorkFlowRecord> wrs= currentFlow.WorkFlowRecords.Where(u => u.WorkTaskIns.WorkTaskId == backtaskId).ToList();
            foreach (var record in wrs)
            {
                WorkFlowRecord r = record;
                r.ActivityStateId = ActivityState.Running;
                r.CreateTime=DateTime.Now;
                r.EndTime = null;
                r.ApplyContent = "";
                currentFlow.WorkFlowRecords.Add(r);

            }
            Repository.Update(currentFlow);
        }
        /// <summary>
        ///处理当前任务，提交至下一步
        /// </summary>
        /// <param name="nextTask"></param>
        public void Next(NextTask nextTask)
        {
            //找到当前流程
            WorkFlowInstance currentFlow= Repository.FirstOrDefault(nextTask.WorkflowInsId);
           List<WorkFlowRecord> workFlowRecords= currentFlow.WorkFlowRecords.Where(u => u.WorkTaskIns.WorkTaskId == currentFlow.WorkTaskId).ToList();
            //找到定义的流程
            WorkFlow workFlow = _workFlowRepository.FirstOrDefault(currentFlow.WorkFlowId);
            WorkTask currenTask = workFlow.WorkTasks.Find(u => u.WorkTaskId == currentFlow.WorkTaskId);
           
            
            foreach (var record in workFlowRecords)
            {
                if (string.Equals(record.ApplyUserNo, nextTask.CurrentUser.UserNo,
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    record.ActivityStateId = ActivityState.Completed;
                    record.EndTime = DateTime.Now;
                    record.ApplyContent = nextTask.ApplyContent ?? "同意";
                }
                else
                {
                    if (currenTask.OperationTypeId == OperationType.ChocieOne)
                    {
                        record.ActivityStateId = ActivityState.End;
                        record.EndTime = DateTime.Now;
                        record.ApplyContent ="流程已结束";
                    }   
                }
            }
            if (currenTask.OperationTypeId == OperationType.All&& workFlowRecords.Count!= workFlowRecords.Count(u=>u.ActivityStateId== ActivityState.Completed))
            {
                Repository.Update(currentFlow);
            }
            else
            {
                
               string nextTaskid = string.IsNullOrEmpty(nextTask.NextWorkTaskId) ?  currenTask.NextWorkTaskId:nextTask.NextWorkTaskId;
                currentFlow.WorkTaskId = nextTaskid;
                WorkTask nextworkTask = workFlow.WorkTasks.Find(u => u.WorkTaskId == nextTaskid);
                if (nextTask.OperationUsers != null && nextTask.OperationUsers.Count > 0)
                {
                    foreach (var user in nextTask.OperationUsers)
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
                        currentFlow.WorkFlowRecords.Add(nxetRecord);
                    }
                }
                else
                {

                    foreach (var user in nextworkTask.OperationUsers??new List<OperationUser>() )
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
                        currentFlow.WorkFlowRecords.Add(nxetRecord);
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
                        currentFlow.WorkFlowRecords.Add(nxetRecord);
                        currentFlow.WorkFlowStateId = WorkFlowState.Completed;
                    }
                }

                Repository.Update(currentFlow);
            }
            
          
        }


    }

    public class NextTask
    {
        /// <summary>
        /// 流程实例id
        /// </summary>
        public string WorkflowInsId { get; set; }
      

        /// <summary>
        /// 下一步任务,为空时执行默认任务
        /// </summary>
        public string NextWorkTaskId { get; set; }
        /// <summary>
        /// 当前操作人
        /// </summary>
        public OperationUser CurrentUser { get; set; }
        /// <summary>
        /// 下一步任务操作人
        /// </summary>
        public List<OperationUser> OperationUsers { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string ApplyContent { get; set; }
    }

    public class BackTask
    {
        /// <summary>
        /// 流程实例id
        /// </summary>
        public string WorkflowInsId { get; set; }

        /// <summary>
        /// 退回任务
        /// </summary>
        public string BackWorkTaskId { get; set; }
        /// <summary>
        /// 当前操作人
        /// </summary>
        public OperationUser CurrentUser { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string ApplyContent { get; set; }
    }
}
