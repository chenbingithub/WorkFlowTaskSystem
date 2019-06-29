using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.Core
{
    public enum AbpLoginResultType : byte
    {
        Success = 1,
        InvalidUserNameOrEmailAddress = 2,
        InvalidPassword = 3,
        UserIsNotActive = 4,
        InvalidTenancyName = 5,
        TenantIsNotActive = 6,
        UserEmailIsNotConfirmed = 7,
        UnknownExternalLogin = 8,
        LockedOut = 9,
        UserPhoneNumberIsNotConfirmed = 10
    }
}
