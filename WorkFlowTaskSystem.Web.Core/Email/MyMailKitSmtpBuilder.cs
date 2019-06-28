using Abp.MailKit;
using Abp.Net.Mail.Smtp;
using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;

namespace Abp.Configuration.Startup
{
    public class MyMailKitSmtpBuilder : DefaultMailKitSmtpBuilder
    {
       

        protected override void ConfigureClient(SmtpClient client)
        {
            client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            base.ConfigureClient(client);
        }

        public MyMailKitSmtpBuilder(ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration, IAbpMailKitConfiguration abpMailKitConfiguration) : base(smtpEmailSenderConfiguration, abpMailKitConfiguration)
        {
        }
    }
}
