using Abp;
using Abp.Application.Features;
using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Localization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Core;

namespace WorkFlowTaskSystem.Application
{
    public class MyAuthorizationHelper : AuthorizationHelper
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        private readonly IAuthorizationConfiguration _authConfiguration;
        public MyAuthorizationHelper(IHttpContextAccessor httpContextAccessor, IFeatureChecker featureChecker, IAuthorizationConfiguration authConfiguration) :base(featureChecker, authConfiguration)
        {
            _authConfiguration = authConfiguration;
            PermissionChecker = NullPermissionChecker.Instance;
            _httpContextAccessor = httpContextAccessor;
        }
        public override Task AuthorizeAsync(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes)
        {
            if (!_authConfiguration.IsEnabled)
            {
                return Task.CompletedTask;
            }
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(WorkFlowTaskAbpConsts.CookiesUserId,
                out var cookiesId);
            var userid=Session.GetUserId() ?? Session.SetUserId(cookiesId);
            if (userid.IsNullOrEmpty())
            {
                throw new AbpAuthorizationException(
                    LocalizationManager.GetString(AbpConsts.LocalizationSourceName, "CurrentUserDidNotLoginToTheApplication")
                    );
            }
           

            foreach (var authorizeAttribute in authorizeAttributes)
            {
                 PermissionChecker.Authorize(authorizeAttribute.RequireAllPermissions, authorizeAttribute.Permissions);
            }
            return Task.CompletedTask;
        }

        
    }
}
