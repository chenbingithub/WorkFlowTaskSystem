using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
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
            CreatePermissionName = PermissionNames.Pages_Users_Create;
            UpdatePermissionName = PermissionNames.Pages_Users_Update;
            DeletePermissionName = PermissionNames.Pages_Users_Delete;
            GetAllPermissionName = PermissionNames.Pages_Users_GetAll;
            GetPermissionName = PermissionNames.Pages_Users_GetAll;
            _organizationUnitManager = organizationUnitManager;
            _roleManager = roleManager;
            _permissionInfoManager = permissionInfoManager;
            _userManager = userManager;
        }

        public override Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();
            input.EName=PinYinUtil.GetAllPinYin(input.Name);
            input.SName=PinYinUtil.GetSimplePinYin(input.Name);
            input.Password = GetEncrpyedAccessToken(input.Password);
            if (IsGranted(PermissionNames.Pages_Users_SetOrgan)) {
                _userManager.SetOrganizationUnit(input.Id, input.OrganIds);
            }
            if (IsGranted(PermissionNames.Pages_Users_SetRole))
            {
                _userManager.SetRole(input.Id, input.RoleIds);
            }
            if (IsGranted(PermissionNames.Pages_Users_SetPers))
            {
                _userManager.SetPermission(input.Id, input.PersIds);
            }
            return base.Create(input);
        }

       

        public override  Task<UserDto> Update(UserDto input)
        {
            CheckUpdatePermission();
            input.EName = PinYinUtil.GetAllPinYin(input.Name);
            input.SName = PinYinUtil.GetSimplePinYin(input.Name);
            if (IsGranted(PermissionNames.Pages_Users_SetOrgan))
            {
                _userManager.SetOrganizationUnit(input.Id, input.OrganIds);
            }
            if (IsGranted(PermissionNames.Pages_Users_SetRole))
            {
                _userManager.SetRole(input.Id, input.RoleIds);
            }
            if (IsGranted(PermissionNames.Pages_Users_SetPers))
            {
                _userManager.SetPermission(input.Id, input.PersIds);
            }

            return base.Update(input);
        }

        public object GetRolePersOrgan(string userId)
        {
            List<IviewTree> roles = new List<IviewTree>();
            List<IviewTree> permissions = new List<IviewTree>();
            List<IviewTree> organzitions = new List<IviewTree>();
            if (IsGranted(PermissionNames.Pages_Users_SetOrgan))
            {
                 organzitions = _userManager.GetOrganizationTree(userId);
            }
            if (IsGranted(PermissionNames.Pages_Users_SetRole))
            {
                 roles = _userManager.GetRoleTree(userId);
            }
            if (IsGranted(PermissionNames.Pages_Users_SetPers))
            {
                 permissions = _userManager.GetPermissionTree(userId);
            }
            
           
           
            var data = new { roles, permissions, organzitions };
            return data;
        }

        public Task<PagedResultDto<UserDto>> GetAllOrSeach(string seachKey, string organzitionId, PagedAndSortedResultRequestDto input)
        {
            CheckGetAllPermission();

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
        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            if (AbpSession.TenantId != null)
                await SettingManager.ChangeSettingForTenantAsync(
                    AbpSession.TenantId.Value,
                    LocalizationSettingNames.DefaultLanguage,
                    input.LanguageName
                );
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
