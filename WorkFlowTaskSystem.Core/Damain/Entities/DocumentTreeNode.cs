using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
   public class DocumentTreeNode: BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }
        public string PathName { get; set; }
        public bool IsLeaf { get; set; }
        public int DateOrderby { get; set; }
        public DefineType DataDefine { get; set; }

        public Urls Url { get; set; }

    }
    /// <summary>
    /// 定义文件类型以及所属项目
    /// </summary>
    public class DefineType
    {
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 文件类型编码
        /// </summary>
        public string FileTypeCode { get; set; }
        /// <summary>
        /// 文件类型名称
        /// </summary>
        public string FileTypeName { get; set; }
    }

    public class Urls
    {
        public string Curl { get; set; }
        public string Aurl { get; set; }
        public string Burl { get; set; }
    }
}
