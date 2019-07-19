using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rebus.Handlers;
using WorkFlowTaskSystem.MqMessage;

namespace DotNetCoreConsumerHost.Handlers
{
    public class PhoneVerificationCodeHandler : IHandleMessages<PhoneVerificationCodeMessage>
    {
        public async Task Handle(PhoneVerificationCodeMessage message)
        {
           await Task.Run(() => { UtaisTelPhone.ReturnCode(message.TelPhone, message.Code); });
        }
    }
}
