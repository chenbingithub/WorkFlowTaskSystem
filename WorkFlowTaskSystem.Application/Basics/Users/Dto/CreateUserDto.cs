using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Application.Basics.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class CreateUserDto : EntityDto<string>
    {
        public CreateUserDto()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }

        
        public string Description { get; set; }

        public bool IsActive { get; set; }

    }
}
