using System;
using System.Collections.Generic;
using System.Text;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Abp.Net.Mail.Smtp;
using WorkFlowTaskSystem.Core.Jobs;

namespace WorkFlowTaskSystem.Core.Events
{
    public class SendEmailEvent:IEventHandler<SendEmailEventData>,ITransientDependency
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public SendEmailEvent( IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }

        public void HandleEvent(SendEmailEventData eventData)
        {
             _backgroundJobManager.Enqueue<SimpleSendEmailJob, SimpleSendEmailJobArgs>(
                new SimpleSendEmailJobArgs
                {
                    Subject = eventData.Subject,
                    Body = eventData.Body,
                    SenderUserId = eventData.SenderUserId,
                    TargetUserId = eventData.TargetUserId
                });
        }
    }
}
