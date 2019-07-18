using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.MqMessage
{
   public class EmailMessage
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
