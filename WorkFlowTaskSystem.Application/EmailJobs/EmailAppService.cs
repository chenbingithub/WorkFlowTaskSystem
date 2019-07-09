using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.BackgroundJobs;
using Abp.Events.Bus;
using Abp.Runtime.Session;
using WorkFlowTaskSystem.Application.EmailJobs.Dto;
using WorkFlowTaskSystem.Core.Events;
using WorkFlowTaskSystem.Core.Jobs;

namespace WorkFlowTaskSystem.Application.EmailJobs
{
    public class EmailAppService : ApplicationService, IEmailAppService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;
        private IEventBus _eventBus;
        public EmailAppService(IBackgroundJobManager backgroundJobManager, IEventBus eventBus)
        {
            _backgroundJobManager = backgroundJobManager;
            _eventBus = eventBus;
        }

        public async Task SendEmail(SendEmailInput input)
        {
            await _backgroundJobManager.EnqueueAsync<SimpleSendEmailJob, SimpleSendEmailJobArgs>(
                new SimpleSendEmailJobArgs
                {
                    Subject = input.Subject,
                    Body = input.Body,
                    SenderUserId = input.SenderUserId,
                    TargetUserId = input.TargetUserId
                });


        }
        public async Task TimingEmail(string subject = "定时邮件", string body = "这是一个定时邮件!!")
        {
    
            await _backgroundJobManager.EnqueueAsync<SimpleSendEmailJob, SimpleSendEmailJobArgs>(
                new SimpleSendEmailJobArgs
                {
                    Subject = subject,
                    Body = body,
                    TargetUserId = "hqchenbin@hytch.com"
                },delay:DateTime.Now.AddMinutes(1).TimeOfDay);
        }
        public async Task TestEmail(string subject="测试", string body="这是一个测试!!")
        {
            await SendEmail(new SendEmailInput()
                {Subject = subject, Body = body, TargetUserId = "hqchenbin@hytch.com"});
        }

        public async Task TestEmailEvent(string subject = "测试", string body = "这是一个测试!!")
        {
           await _eventBus.TriggerAsync(new SendEmailEventData
                {Subject = subject, Body = body, TargetUserId = "hqchenbin@hytch.com"});

        }
    }
}
