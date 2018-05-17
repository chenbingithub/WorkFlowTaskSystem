namespace WorkFlowTaskSystem.Core.Damain.Entities.Basics
{
    /// <summary>
    /// 组织单位/组织架构
    /// </summary>
   public class OrganizationUnit: BaseEntity,ITree
    {
        public string No { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public string Code { get; set; }

        public string ParentId { get; set; }


        public string ParentName { get; set; }
        /// <summary>
        /// 部门领导人
        /// </summary>
        public string Leader { get; set; }
        /// <summary>
        /// 最高领导人
        /// </summary>
        public string Header { get; set; }
        public string RoleNames { get; set; }
        public string Path { get; set; }
        public string PathName { get; set; }
    }
}
