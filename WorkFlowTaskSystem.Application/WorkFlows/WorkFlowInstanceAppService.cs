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

        public void Back()
        {
            throw new NotImplementedException();
        }
        ///
        public void Next(NextTask nextTask)
        {
            //找到当前流程
            WorkFlowInstance currentFlow= Repository.FirstOrDefault(nextTask.WorkflowInsId);
           List<WorkFlowRecord> workFlowRecords= currentFlow.WorkFlowRecords.Where(u => u.WorkTaskIns.WorkTaskId == nextTask.WorktaskId).ToList();
            //找到定义的流程
            WorkFlow workFlow = _workFlowRepository.FirstOrDefault(currentFlow.WorkFlowId);
            WorkTask currenTask = workFlow.WorkTasks.Find(u=>u.WorkTaskId==nextTask.WorktaskId);
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
        public string WorkflowInsId { get; set; }
        public string WorktaskId { get; set; }

        /// <summary>
        /// 为空时，走默认定义的
        /// </summary>
        public string NextWorkTaskId { get; set; }

        public OperationUser CurrentUser { get; set; }
        public List<OperationUser> OperationUsers { get; set; }
        public string ApplyContent { get; set; }
    }
}
