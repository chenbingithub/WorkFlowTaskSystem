using System;
using System.Collections.Generic;
using System.Text;
using Abp.Events.Bus;

namespace WorkFlowTaskSystem.Core.Events
{
    public class SendEmailEventData:EventData
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string SenderUserId { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string TargetUserId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
    }
}
