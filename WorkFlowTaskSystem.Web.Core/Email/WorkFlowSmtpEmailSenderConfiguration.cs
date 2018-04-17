using System;
using System.Collections.Generic;
using System.Text;
using Abp.Configuration;
using Abp.Net.Mail.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WorkFlowTaskSystem.Web.Core.Configuration;

namespace WorkFlowTaskSystem.Web.Core.Email
{
   public class WorkFlowSmtpEmailSenderConfiguration: SmtpEmailSenderConfiguration
    {
        private readonly IConfigurationRoot _appConfiguration;

        
        public WorkFlowSmtpEmailSenderConfiguration(ISettingManager settingManager, IHostingEnvironment env) : base(settingManager)
        {
            _appConfiguration = env.GetAppConfiguration();
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
