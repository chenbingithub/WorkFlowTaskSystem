﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
    public class Role: BaseEntity
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
