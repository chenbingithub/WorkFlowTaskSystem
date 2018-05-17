using Abp.Configuration.Startup;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WorkFlowTaskSystem.Core.Session
{
    public class AbpSessionExtension : ClaimsAbpSession, IAbpSessionExtension
    {
        public AbpSessionExtension(IPrincipalAccessor principalAccessor,
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider):base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {
        }
        public string Email {
            get {
                var claimsPrincipal = PrincipalAccessor.Principal;

                var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (string.IsNullOrEmpty(claim?.Value))
                    return null;

                return claim.Value;
            }
        }

        string IAbpSessionExtension.UserId {
            get
            {
                
                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.UserId);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }
                return userIdClaim.Value;
            }
        }

       
    }
}
