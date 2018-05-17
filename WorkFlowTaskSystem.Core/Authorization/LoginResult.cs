using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Authorization
{
   public class LoginResult<TUser>  where TUser:BaseEntity
    {
        public LoginResult(AbpLoginResultType result, TUser user=null)
        {
            this.Result = result;
            this.User = user;
        }

        public LoginResult(TUser user, ClaimsIdentity identity) : this(AbpLoginResultType.Success,user)
        {
            this.User = user;
            this.Identity = identity;
        }

        public AbpLoginResultType Result { get; private set; }
        public TUser User { get; private set; }
        public ClaimsIdentity Identity { get; set; }
    }
}
