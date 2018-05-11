using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Application.Basics.PermissionInfos.Dto
{
    [AutoMap(typeof(PermissionInfo))]
    public class PermissionInfoDto : EntityDto<string>
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }

        
        public string Description { get; set; }

        public string ParentId { get; set; }

        public string ParentName { get; set; }


    }
    public class PermissionView {
        public PermissionView()
        {
            loading = false;
        }
       public string title { get; set; }
       public bool loading { get; set; }
       public PermissionInfoDto data { get; set; }

        public List<PermissionView> children { get; set; }
    }
}
