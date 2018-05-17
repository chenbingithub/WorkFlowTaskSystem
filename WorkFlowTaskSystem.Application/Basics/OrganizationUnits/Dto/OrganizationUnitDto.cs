using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Application.Basics.OrganizationUnits.Dto
{
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : EntityDto<string>, ITree
    {
        [Required]
        public string No { get; set; }
        [Required]
        public string Name { get; set; }
        
        
        public string Code { get; set; }

        
        public string Description { get; set; }

        /// <summary>
        /// 部门领导人
        /// </summary>
        public string Leader { get; set; }
        /// <summary>
        /// 最高领导人
        /// </summary>
        public string Header { get; set; }

        public string ParentId { get; set; }
        public string RoleNames { get; set; }

        public string ParentName { get; set; }
        public string[] RoleIds { get; set; }
        public string[] PersIds { get; set; }

    }
}
