using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Application.Basics.Roles.Dto
{
    [AutoMapTo(typeof(Role))]
    public class CreateRoleDto : EntityDto<string>
    {
        public CreateRoleDto()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }

        
        public string Description { get; set; }

        public string[] PersIds { get; set; }


    }
}
