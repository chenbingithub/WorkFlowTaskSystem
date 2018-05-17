using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Abp.Runtime.Security;
using WorkFlowTaskSystem.Application.Basics.OrganizationUnits.Dto;
using WorkFlowTaskSystem.Application.Basics.Users.Dto;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;

namespace WorkFlowTaskSystem.Application.Basics.Users
{
    public class UserAppService : WorkFlowTaskSystemAppServiceBase<User, UserDto, CreateUserDto>, IUserAppService
    {
        private OrganizationUnitManager _organizationUnitManager;
        private UserManager _userManager;
        private RoleManager _roleManager;
        private PermissionInfoManager _permissionInfoManager;
        public UserAppService(IUserRepository repository, OrganizationUnitManager organizationUnitManager, RoleManager roleManager, PermissionInfoManager permissionInfoManager, UserManager userManager) : base(repository)
        {
            _organizationUnitManager = organizationUnitManager;
            _roleManager = roleManager;
            _permissionInfoManager = permissionInfoManager;
            _userManager = userManager;
        }

        public override Task<UserDto> Create(CreateUserDto input)
        {
            input.EName=PinYinUtil.GetAllPinYin(input.Name);
            input.SName=PinYinUtil.GetSimplePinYin(input.Name);
            input.Password = GetEncrpyedAccessToken(input.Password);
            _userManager.SetOrganizationUnit(input.Id, input.OrganIds);
            _userManager.SetRole(input.Id, input.RoleIds);
            _userManager.SetPermission(input.Id, input.PersIds);
            return base.Create(input);
        }

       

        public override  Task<UserDto> Update(UserDto input)
        {
            input.EName = PinYinUtil.GetAllPinYin(input.Name);
            input.SName = PinYinUtil.GetSimplePinYin(input.Name);
            _userManager.SetOrganizationUnit(input.Id, input.OrganIds);
            _userManager.SetRole(input.Id, input.RoleIds);
            _userManager.SetPermission(input.Id, input.PersIds);
            return base.Update(input);
        }

        public object GetRolePersOrgan(string userId)
        {
           
            var roles=_userManager.GetRoleTree(userId);
            var permissions=_userManager.GetPermissionTree(userId);
            var organzitions=_userManager.GetOrganizationTree(userId);
            var data = new { roles, permissions, organzitions };
            return data;
        }

        public Task<PagedResultDto<UserDto>> GetAllOrSeach(string seachKey, string organzitionId, PagedAndSortedResultRequestDto input)
        {


            if (!string.IsNullOrEmpty(seachKey))
            {
                var result = Repository.GetAll()
                    .Where(u => u.UserName.Contains(seachKey) || u.Name.Contains(seachKey) ||
                                u.EName.Contains(seachKey) || u.SName.Contains(seachKey));
                var totalCount = result.Count();
                var query = result.OrderByDescending(u => u.CreationTime).PageBy(input);
                var data = new PagedResultDto<UserDto>(
                    totalCount,
                    query.AsEnumerable().Select(MapToEntityDto).ToList()
                );
                return Task.FromResult(data);
            }
            else
            {
                if (!string.IsNullOrEmpty(organzitionId))
                {
                    var result = _organizationUnitManager.GetChildrenUsers(organzitionId);
                    var totalCount = result.Count();
                    var query = result.OrderByDescending(u => u.CreationTime).PageBy(input);
                    var data = new PagedResultDto<UserDto>(
                        totalCount,
                        query.AsEnumerable().Select(MapToEntityDto).ToList()
                    );
                    return Task.FromResult(data);

                }

            }
            return base.GetAll(input);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private string GetEncrpyedAccessToken(string accessToken)
        {
            
            return SimpleStringCipher.Instance.Encrypt(accessToken, WorkFlowTaskAbpConsts.DefaultPassPhrase);
        }
    }
}
