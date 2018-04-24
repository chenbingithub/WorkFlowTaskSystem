using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Net.Mail.Smtp;
using WorkFlowTaskSystem.Application.WorkFlows.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;

namespace WorkFlowTaskSystem.Application.WorkFlows
{
    public class WorkFlowInstanceAppService : WorkFlowTaskSystemAppServiceBase<WorkFlowInstance, WorkFlowInstanceDto,CreateWorkFlowInstanceDto>, IWorkFlowInstanceAppService
    {
        private ISmtpEmailSender _emailSender;
        private IWorkFlowRepository _workFlowRepository;
        public WorkFlowInstanceAppService(IWorkFlowInstanceRepository repository, IWorkFlowRepository workFlowRepository, ISmtpEmailSender emailSender) : base(repository)
        {
            _workFlowRepository = workFlowRepository;
            _emailSender = emailSender;
        }
        /// <summary>
        /// 发起流程
        /// </summary>
        public void Start(CreateWorkFlowInstanceDto entity)
        {
            WorkFlowInstance workFlowInstance = MapToEntity(entity);
            WorkFlow workFlow = _workFlowRepository.Get(entity.WorkFlowId);
            workFlowInstance.StartTask(workFlow, entity.OperationUsers);
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
            currentFlow.BackTask(backTask.BackWorkTaskId, backTask.CurrentUser, backTask.ApplyContent);
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
            currentFlow.NextTask(nextTask.CurrentUser, nextTask.ApplyContent, nextTask.NextWorkTaskId,
                nextTask.OperationUsers);
                Repository.Update(currentFlow);
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
