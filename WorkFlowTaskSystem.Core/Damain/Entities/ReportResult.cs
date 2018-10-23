using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
   public class ReportResult : BaseEntity
    {
      public string Name { get; set; }
      public string Url { get; set; }

    }
}
