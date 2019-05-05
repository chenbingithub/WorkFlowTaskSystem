using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.BackgroundJobs;
using Abp.Runtime.Session;
using WorkFlowTaskSystem.Application.EmailJobs.Dto;
using WorkFlowTaskSystem.Core.Jobs;

namespace WorkFlowTaskSystem.Application.EmailJobs
{
    public class EmailAppService : ApplicationService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public EmailAppService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
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

        public async Task TestEmail(string subject="测试", string body="这是一个测试!!")
        {
            await SendEmail(new SendEmailInput()
                {Subject = subject, Body = body, TargetUserId = "hqchenbin@hytch.com"});
        }
    }
}
