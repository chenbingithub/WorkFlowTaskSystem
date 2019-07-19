using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.MqMessage
{
    /// <summary>
    /// 验证码消息
    /// </summary>
    public class PhoneVerificationCodeMessage
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string TelPhone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}
