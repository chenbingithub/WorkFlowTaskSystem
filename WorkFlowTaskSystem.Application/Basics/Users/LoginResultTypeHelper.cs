using System;
using Abp;
using Abp.Dependency;
using Abp.UI;
using WorkFlowTaskSystem.Core;

namespace WorkFlowTaskSystem.Application.Basics.Users
{
    public class LoginResultTypeHelper : AbpServiceBase, ITransientDependency
    {
        public LoginResultTypeHelper()
        {
            LocalizationSourceName = WorkFlowTaskAbpConsts.LocalizationSourceName;
        }

        public Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName="")
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new Exception("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                    return new UserFriendlyException("登陆失败", "用户名或密码错误");
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("登陆失败"), L("用户名或密码错误"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("登陆失败"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("登陆失败"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("登陆失败"), L("该用户未启用不允许登陆", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("登陆失败"), L("UserEmailIsNotConfirmedAndCanNotLogin"));
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(L("登陆失败"), L("UserLockedOutMessage"));
                default: // Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("登陆失败"));
            }
        }

        public string CreateLocalizedMessageForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName="")
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    throw new Exception("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return L("InvalidUserNameOrPassword");
                case AbpLoginResultType.InvalidTenancyName:
                    return L("ThereIsNoTenantDefinedWithName{0}", tenancyName);
                case AbpLoginResultType.TenantIsNotActive:
                    return L("TenantIsNotActive", tenancyName);
                case AbpLoginResultType.UserIsNotActive:
                    return L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return L("UserEmailIsNotConfirmedAndCanNotLogin");
                default: // Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return L("登陆失败");
            }
        }
    }

    
}
