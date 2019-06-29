using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace WorkFlowTaskSystem.Application.EmailJobs
{
   public interface IEmailAppService : IApplicationService { 
       Task TestEmail(string subject, string body);
        Task TestEmailEvent(string subject, string body);
   }
}
