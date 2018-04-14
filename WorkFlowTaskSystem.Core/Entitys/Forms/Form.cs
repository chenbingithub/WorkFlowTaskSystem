﻿using System.Collections.Generic;

namespace WorkFlowTaskSystem.Core.Entitys.Forms
{
    public class Form: BaseEntity
    {
        /// <summary>
        /// 表单项
        /// </summary>
        public List<FormItem> FormItems { get; set; }
        /// <summary>
        /// 表单名称
        /// </summary>
        public string Name { get; set; }

        public string Code { get; set; }
    }
    public class FormItem
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType { get; set; }
    }
}
