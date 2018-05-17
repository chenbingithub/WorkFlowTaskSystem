using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using WorkFlowTaskSystem.Core.Authorization;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Services
{
    public class LoginManager: DomainService
    {
        private IUserRepository _userRepository;
        private UserManager _userManager;

        public LoginManager(IUserRepository userRepository, UserManager userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public LoginResult<User> Login(string usernameOrEmailAddress,string password)
        {
            
            return LoginAsyncInternal(usernameOrEmailAddress, password); ;
        }

        protected  LoginResult<User> LoginAsyncInternal(string userNameOrEmailAddress, string plainPassword, bool shouldLockout=false)
        {
            if (userNameOrEmailAddress.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(userNameOrEmailAddress));
            }

            if (plainPassword.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }



            //TryLoginFromExternalAuthenticationSources method may create the user, that's why we are calling it before AbpStore.FindByNameOrEmailAsync

            var user = _userRepository.GetAll().FirstOrDefault(u => (u.UserName == userNameOrEmailAddress || u.EmailAddress == userNameOrEmailAddress));
            if (user == null)
            {
                return new LoginResult<User>(AbpLoginResultType.InvalidUserNameOrEmailAddress);
            }

            if (!user.IsActive)
            {
                return new LoginResult<User>(AbpLoginResultType.LockedOut, user);
            }

            if (!CheckPassword(user, plainPassword))
            {
                if (shouldLockout)
                {
                    if (TryLockOut(user.Id))
                    {
                        return new LoginResult<User>(AbpLoginResultType.LockedOut, user);
                    }
                }

                return new LoginResult<User>(AbpLoginResultType.InvalidPassword, user);
            }

            return CreateLoginResult(user);
        }

        private  LoginResult<User> CreateLoginResult(User user)
        {
            if (!user.IsActive)
            {
                return new LoginResult<User>(AbpLoginResultType.UserIsNotActive);
            }

            user.LastLoginTime = Clock.Now;

            _userRepository.Update(user);

            var pers = _userManager.GetAllPermissions(user.Id).Select(u => new Claim("pers", u.Code)).ToList();

            var principal = new ClaimsIdentity(pers);
            return new LoginResult<User>(
                user,
                principal
            );
        }

        private bool CheckPassword(User user,string plainPassword)
        {
            return  WorkFlowTaskAbpConsts.GetEncrpyedAccessToken(plainPassword) == user.Password;
        }

        private bool TryLockOut(string userId)
        {
           var user= _userRepository.Get(userId);
            if (user == null) return false;
            user.IsActive = false;
            _userRepository.Update(user);

            return true;

        }
    }
}
