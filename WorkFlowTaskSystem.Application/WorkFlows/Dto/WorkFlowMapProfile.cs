using AutoMapper;
using MongoDB.Bson;

namespace WorkFlowTaskSystem.Application.WorkFlows.Dto
{
    public class WorkFlowMapProfile : Profile
    {
        public WorkFlowMapProfile()
        {
            // Role and permission

            //CreateMap<WorkFlow, CreateWorkFlowDto>().WorkFlowember(x => x.Id, opt => opt.MapFrom(e => e.Id.ToString()));
            //CreateMap<WorkFlowDto, WorkFlow>().WorkFlowember(x => x.Id, opt => opt.MapFrom(e => new ObjectId(e.Id)));
        }
    }
}
