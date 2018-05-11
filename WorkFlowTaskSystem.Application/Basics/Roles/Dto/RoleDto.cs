using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Application.Basics.Roles.Dto
{
    [AutoMap(typeof(Role))]
    public class RoleDto : EntityDto<string>
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }

        
        public string Description { get; set; }


    }
}
