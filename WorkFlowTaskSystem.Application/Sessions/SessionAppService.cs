using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Auditing;
using Abp.Extensions;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Http;
using WorkFlowTaskSystem.Application.Sessions.Dto;
using WorkFlowTaskSystem.Application.SignalR;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;

namespace WorkFlowTaskSystem.Application.Sessions
{
    public class SessionAppService : ApplicationService, ISessionAppService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        private UserManager _userManager;
        public SessionAppService(IHttpContextAccessor httpContextAccessor, UserManager userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

       
        [DisableAuditing]
        public  Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>
                    {
                        { "SignalR", SignalRFeature.IsAvailable },
                        { "SignalR.AspNetCore", SignalRFeature.IsAspNetCore }
                    }
                }
            };

            var uid=Session.GetUserId();
            if (!uid.IsNullOrEmpty())
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(GetCurrentUser());
            }
            return Task.FromResult(output);
        }
        protected virtual User GetCurrentUser()
        {
            var user = _userManager.FindById(Session.GetUserId());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }
    }
}
