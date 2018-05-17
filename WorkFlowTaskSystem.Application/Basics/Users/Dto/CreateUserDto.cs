using System;
using System.Collections.Generic;
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
            Password = "123456";
        }        
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 拼音全拼
        /// </summary>
        public string EName { get; set; }
        /// <summary>
        /// 拼音简拼
        /// </summary>
        public string SName { get; set; }

        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 是否启用 true启用
        /// </summary>
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string RoleNames { get; set; }
        public string OrganizationUnitNames { get; set; }
        public string[] OrganIds { get; set; }
        public string[] RoleIds { get; set; }
        public string[] PersIds { get; set; }


    }
}
