using Abp.Net.Mail.Smtp;
using Castle.Core.Logging;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowTaskSystem.MqMessage;

namespace WorkFlowTaskSystem.RabbitMqConsumer.Host.Handlers
{
    public class EmailHandler : IHandleMessages<EmailMessage>
    {
        private ISmtpEmailSender _emailSender;
        public ILogger Logger { get; set; }
        public EmailHandler(ISmtpEmailSender emailSender)
        {
            Logger = NullLogger.Instance;
            _emailSender = emailSender;
        }
        public async Task Handle(EmailMessage message)
        {
            var msg = $"{Logger.GetType()}:{message.Body},{DateTime.Now}";
            Logger.Debug(msg);
           await _emailSender.SendAsync(message.TargetUserId, message.Subject, message.Body);
        }
    }
}
