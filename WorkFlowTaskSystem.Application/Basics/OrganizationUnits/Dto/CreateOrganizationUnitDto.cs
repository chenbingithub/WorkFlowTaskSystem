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
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }

        
        public string Description { get; set; }
        

        public string ParentId { get; set; }

        public string ParentName { get; set; }

    }
}
