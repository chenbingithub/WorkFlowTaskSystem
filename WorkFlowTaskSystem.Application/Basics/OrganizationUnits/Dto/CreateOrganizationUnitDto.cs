using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Application.Basics.OrganizationUnits.Dto
{
    [AutoMapTo(typeof(OrganizationUnit))]
    public class CreateOrganizationUnitDto : EntityDto<string>
    {
        public CreateOrganizationUnitDto()
        {
            Id = Guid.NewGuid().ToString("N");
            ParentId = "-1";
        }

        public string No { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Code { get; set; }

        
        public string Description { get; set; }
        

        public string ParentId { get; set; }
        public string RoleNames { get; set; }
        /// <summary>
        /// 部门领导人
        /// </summary>
        public string Leader { get; set; }
        /// <summary>
        /// 最高领导人
        /// </summary>
        public string Header { get; set; }
        public string ParentName { get; set; }
        public string[] RoleIds { get; set; }
        public string[] PersIds { get; set; }

    }
}
