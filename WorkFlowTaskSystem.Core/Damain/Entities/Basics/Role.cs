namespace WorkFlowTaskSystem.Core.Damain.Entities.Basics
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role: BaseEntity
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
