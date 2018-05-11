using WorkFlowTaskSystem.Application.Basics.Users.Dto;

namespace WorkFlowTaskSystem.Application.Basics.Users
{
    
    public interface IUserAppService : IWorkFlowTaskSystemAppServiceBase<UserDto, CreateUserDto>
    {
    }
}
