using AutoMapper;
using MongoDB.Bson;

namespace WorkFlowTaskSystem.Application.Forms.Dto
{
    public class FormMapProfile : Profile
    {
        public FormMapProfile()
        {
            // Role and permission

            //CreateMap<Form, CreateFormDto>().ForMember(x => x.Id, opt => opt.MapFrom(e => e.Id.ToString()));
            //CreateMap<FormDto, Form>().ForMember(x => x.Id, opt => opt.MapFrom(e => new ObjectId(e.Id)));
        }
    }
}
