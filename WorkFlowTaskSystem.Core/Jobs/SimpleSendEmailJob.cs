using System;
using System.Collections.Generic;
using System.Text;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Jobs
{
    public class SimpleSendEmailJob : BackgroundJob<SimpleSendEmailJobArgs>, ITransientDependency
    {
        private ISmtpEmailSender _emailSender;

        public SimpleSendEmailJob(ISmtpEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public override void Execute(SimpleSendEmailJobArgs args)
        {

            _emailSender.Send(args.TargetUserId, args.Subject, args.Body);
        }
    }
}
