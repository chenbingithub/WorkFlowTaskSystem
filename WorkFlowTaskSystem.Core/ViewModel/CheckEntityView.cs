namespace WorkFlowTaskSystem.Core.ViewModel
{
    public class CheckEntityView 
    {
     
        /// <summary>
        /// 实际敷设表
        /// </summary>
        public string RealityTable { get; set; }
        
        /// <summary>
        /// 设计敷设表
        /// </summary>
        public string[] DesignTables { get; set; }
      /// <summary>
      /// 根据当前时间和随机数生成流水号,由前端生成
      /// </summary>
      public string NumberNo { get; set; }
    }
}
