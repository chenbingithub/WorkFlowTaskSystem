using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient.Attributes;
using WorkFlowTaskSystem.Application.EmailJobs.Dto;

namespace WebApiClient.Core
{
   public interface IEmailApi:IHttpApi
    {
        // GET api/user?account=laojiu
        // Return json或xml内容
        [HttpPost("api/user")]
        ITask<bool> SendEmail(SendEmailInput sendEmailInput);
    }
}
