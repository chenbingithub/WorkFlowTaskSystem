using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.Application.EmailJobs.Dto
{
    public class SendEmailInput
    {
        public string SenderUserId { get; set; }

        public string TargetUserId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
