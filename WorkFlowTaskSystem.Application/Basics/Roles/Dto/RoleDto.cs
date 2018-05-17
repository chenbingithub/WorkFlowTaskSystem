using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Application.Basics.Roles.Dto
{
    [AutoMap(typeof(Role))]
    public class RoleDto : EntityDto<string>, ILinear
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }

        
        public string Description { get; set; }
        public string[] PersIds { get; set; }



    }
}
