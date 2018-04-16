using System.Collections.Generic;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
     public class FormInstance: BaseEntity
    {
        public string WorkFlowInsId { get; set; }

        /// <summary>
        /// 表单
        /// </summary>
        public Form Forms { get; set; }

        public dynamic PageData { get; set; }

        public List<Attachment> Attachments { get; set; }
    }

    public class Attachment {
        /// <summary>
        /// 附件id
        /// </summary>
        public string AttachmentId { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        public string AttachmentType { get; set; }
    }
}
