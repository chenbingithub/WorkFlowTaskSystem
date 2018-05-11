namespace WorkFlowTaskSystem.Core.Damain.Entities.Basics
{
    /// <summary>
    /// 组织单位/组织架构
    /// </summary>
   public class OrganizationUnit: BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public string Code { get; set; }

        public string ParentId { get; set; }

        public string ParentName { get; set; }
    }
}
