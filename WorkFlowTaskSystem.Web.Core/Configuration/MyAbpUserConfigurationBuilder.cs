using Abp.Application.Features;
using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Web.Configuration;
using Abp.Web.Models.AbpUserConfiguration;
using Abp.Web.Security.AntiForgery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using WorkFlowTaskSystem.Application;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;

namespace WorkFlowTaskSystem.Web.Core.Configuration
{
    public class MyAbpUserConfigurationBuilder : AbpUserConfigurationBuilder
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        private UserManager _userManager;
        private PermissionInfoManager _permissionInfoManager;
        public MyAbpUserConfigurationBuilder(IMultiTenancyConfig multiTenancyConfig, ILanguageManager languageManager, ILocalizationManager localizationManager, IFeatureManager featureManager, IFeatureChecker featureChecker, IPermissionManager permissionManager, IUserNavigationManager userNavigationManager, ISettingDefinitionManager settingDefinitionManager, ISettingManager settingManager, IAbpAntiForgeryConfiguration abpAntiForgeryConfiguration, IAbpSession abpSession, IPermissionChecker permissionChecker, IIocResolver iocResolver, IAbpStartupConfiguration startupConfiguration, IHttpContextAccessor httpContextAccessor, UserManager userManager, PermissionInfoManager permissionInfoManager) : base(multiTenancyConfig, languageManager, localizationManager, featureManager, featureChecker, permissionManager, userNavigationManager, settingDefinitionManager, settingManager, abpAntiForgeryConfiguration, abpSession, permissionChecker, iocResolver, startupConfiguration)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _permissionInfoManager = permissionInfoManager;
        }
        //public MyAbpUserConfigurationBuilder(IHttpContextAccessor httpContextAccessor, UserManager userManager, PermissionInfoManager permissionInfoManager,IMultiTenancyConfig multiTenancyConfig, ILanguageManager languageManager, ILocalizationManager localizationManager, IFeatureManager featureManager, IFeatureChecker featureChecker, IPermissionManager permissionManager, IUserNavigationManager userNavigationManager, ISettingDefinitionManager settingDefinitionManager, ISettingManager settingManager, IAbpAntiForgeryConfiguration abpAntiForgeryConfiguration, IAbpSession abpSession, IPermissionChecker permissionChecker) 
        //    : base(multiTenancyConfig, languageManager, localizationManager, featureManager, featureChecker, permissionManager, userNavigationManager, settingDefinitionManager, settingManager, abpAntiForgeryConfiguration, abpSession, permissionChecker)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    _userManager = userManager;
        //    _permissionInfoManager = permissionInfoManager;
        //}
        protected override Task<AbpUserAuthConfigDto> GetUserAuthConfig()
        {
            var config = new AbpUserAuthConfigDto();

            var allPermissionNames = _permissionInfoManager.GetAll().Select(p => p.Code).ToList();
            var grantedPermissionNames = new List<string>();
            var userId = Session.GetUserId();
            if (!userId.IsNullOrEmpty())
            {
                var pers = _userManager.GetAllPermissions(userId).Select(u => u.Code).ToList();
                if (pers.Count > 0)
                {
                    grantedPermissionNames.AddRange(pers);
                }
            }

            config.AllPermissions = allPermissionNames.ToDictionary(permissionName => permissionName, permissionName => "true");
            config.GrantedPermissions = grantedPermissionNames.ToDictionary(permissionName => permissionName, permissionName => "true");

            return Task.FromResult(config);
        }

       
    }
}
