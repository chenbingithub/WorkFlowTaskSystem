using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Net.Mail.Smtp;
using Hangfire;

namespace WorkFlowTaskSystem.Core.Jobs
{
    [Queue("email")]
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
