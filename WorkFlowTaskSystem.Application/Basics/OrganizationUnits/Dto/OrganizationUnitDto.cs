using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Application.Basics.OrganizationUnits.Dto
{
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : EntityDto<string>
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }

        
        public string Description { get; set; }

        public bool IsActive { get; set; }

    }
}
