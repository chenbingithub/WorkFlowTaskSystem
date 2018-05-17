using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Runtime.Security;
using Abp.UI;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFlowTaskSystem.Application.Basics.Users;
using WorkFlowTaskSystem.Controllers;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Authorization;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Services;
using WorkFlowTaskSystem.Web.Core.Models.TokenAuth;

namespace WorkFlowTaskSystem.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : WorkFlowTaskSystemControllerBase
    {
        private LoginManager _loginManager;
        private LoginResultTypeHelper _loginResultTypeHelper;
 
        public TokenAuthController(LoginManager loginManager, LoginResultTypeHelper loginResultTypeHelper)
        {
            _loginManager = loginManager;
            _loginResultTypeHelper = loginResultTypeHelper;
        }


        [HttpPost]
        public  Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        {
           var loginResult = GetLoginResult(model.UserNameOrEmailAddress, model.Password);
            
            HttpContext.Session.SetString(WorkFlowTaskAbpConsts.UserId, loginResult.User?.Id);
           

            return Task.FromResult(new AuthenticateResultModel
            {
                AccessToken = AppConsts.DefaultPassPhrase,
                EncryptedAccessToken = loginResult.Identity.Claims,
                ExpireInSeconds = 5200,
                UserId = loginResult.User?.Id
            });
        }
        private LoginResult<User> GetLoginResult(string usernameOrEmailAddress, string password)
        {
            var loginResult = _loginManager.Login(usernameOrEmailAddress, password);
           
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _loginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress);
            }
        }



        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, WorkFlowTaskAbpConsts.DefaultPassPhrase);
        }
    }
}
