using Abp;
using Abp.Authorization;
using Abp.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Core.Damain.Repositories.Basics;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;
using WorkFlowTaskSystem.Core.Session;

namespace WorkFlowTaskSystem.Application
{
    public class MyPermissionChecker : IPermissionChecker
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        private UserManager _userManager;
        public MyPermissionChecker(IHttpContextAccessor httpContextAccessor, UserManager userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public Task<bool> IsGrantedAsync(string permissionName)
        {
            if (permissionName.IsNullOrEmpty())
            {
                return Task.FromResult(true);
            }

            if (Session.GetUserId().IsNullOrEmpty()) {
                return Task.FromResult(false);
            }
            var allpers=_userManager.GetAllPermissions(Session.GetUserId());
            if (allpers.Exists(u => u.Code.ToLower() == permissionName.ToLower())) {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            return Task.FromResult(false);
        }
    }
}
