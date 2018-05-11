using AutoMapper;

namespace WorkFlowTaskSystem.Application.Basics.PermissionInfos.Dto
{
    public class PermissionInfoMapProfile : Profile
    {
        public PermissionInfoMapProfile()
        {
            
            //CreateMap<PermissionInfoDto, PermissionView>().
            //    ForMember(x => x, opt => opt.MapFrom(e=>new PermissionView {
            //        title=e.Name,
            //        data=e
            //} ));

        }
    }
}
