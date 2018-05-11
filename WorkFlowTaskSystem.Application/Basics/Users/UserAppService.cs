using WorkFlowTaskSystem.Application.Basics.Users.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;

namespace WorkFlowTaskSystem.Application.Basics.Users
{
    public class UserAppService : WorkFlowTaskSystemAppServiceBase<User, UserDto, CreateUserDto>, IUserAppService
    {
        public UserAppService(IUserRepository repository) : base(repository)
        {
        }

        
    }
}
