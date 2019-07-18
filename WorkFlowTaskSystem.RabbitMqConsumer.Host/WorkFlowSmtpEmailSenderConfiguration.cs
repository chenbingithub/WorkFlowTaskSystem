using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Abp.Configuration;
using Abp.Net.Mail.Smtp;
using Microsoft.Extensions.Configuration;

namespace WorkFlowTaskSystem.RabbitMqConsumer.Host
{
   public class MySmtpEmailSenderConfiguration: SmtpEmailSenderConfiguration
    {
        IConfigurationRoot _appConfiguration { get; set; }
        public MySmtpEmailSenderConfiguration(ISettingManager settingManager) : base(settingManager)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
            _appConfiguration = builder.Build();
        }

        public override string Host => _appConfiguration["Abp:Email:Smtp:Host"];
        public override int Port => int.Parse(_appConfiguration["Abp:Email:Smtp:Port"]);
        public override string DefaultFromAddress => _appConfiguration["Abp:Email:Smtp:UserName"];
        public override string DefaultFromDisplayName => _appConfiguration["Abp:Email:Smtp:DefaultFromDisplayName"];

        public override string Domain => _appConfiguration["Abp:Email:Smtp:Domain"];

        public override bool EnableSsl => bool.Parse(_appConfiguration["Abp:Email:Smtp:EnableSsl"]);
        public override string UserName => _appConfiguration["Abp:Email:Smtp:UserName"];
        public override string Password => _appConfiguration["Abp:Email:Smtp:Password"];
        public override bool UseDefaultCredentials=> bool.Parse(_appConfiguration["Abp:Email:Smtp:UseDefaultCredentials"]);

       
    }
}
