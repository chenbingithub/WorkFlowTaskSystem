using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Application.Sessions.Dto
{
    [AutoMapTo(typeof(User))]
    public class UserLoginInfoDto : EntityDto<string>
    {
        public string Name { get; set; }

        public string Sex { get; set; }
        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
        public string OrganizationUnitNames { get; set; }
    }
}
