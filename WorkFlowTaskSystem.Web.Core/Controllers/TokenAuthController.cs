using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Runtime.Security;
using Abp.UI;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFlowTaskSystem.Application;
using WorkFlowTaskSystem.Application.Basics.Users;
using WorkFlowTaskSystem.Authentication.JwtBearer;
using WorkFlowTaskSystem.Controllers;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Authorization;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;
using WorkFlowTaskSystem.Core.Damain.Services;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;
using WorkFlowTaskSystem.Web.Core.Models.TokenAuth;

namespace WorkFlowTaskSystem.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : WorkFlowTaskSystemControllerBase
    {
        private LoginManager _loginManager;
        private LoginResultTypeHelper _loginResultTypeHelper;
        private TokenAuthConfiguration _configuration;
        private UserManager _userManager;

        public TokenAuthController(LoginManager loginManager, LoginResultTypeHelper loginResultTypeHelper, TokenAuthConfiguration tokenAuthConfiguration, UserManager userManager)
        {
            _loginManager = loginManager;
            _loginResultTypeHelper = loginResultTypeHelper;
            _configuration = tokenAuthConfiguration;
            _userManager = userManager;
        }


        [HttpPost]
        public  AuthenticateResultModel Authenticate([FromBody] AuthenticateModel model)
        {
           var loginResult = GetLoginResult(model.UserNameOrEmailAddress, model.Password);
            //设置session
            HttpContext.Session.SetString(WorkFlowTaskAbpConsts.UserId, loginResult.User?.Id);
            HttpContext.Response.Cookies.Append(WorkFlowTaskAbpConsts.CookiesUserId, GetEncrpyedAccessToken(loginResult.User?.Id??""));
            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                UserId = loginResult.User.Id
            };
           
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
        [HttpPost]
        public void Changepwd([FromBody]ChangePwdModel changePwd)
        {
            HttpContext.Request.Cookies.TryGetValue(WorkFlowTaskAbpConsts.CookiesUserId,
                out var cookiesId);
            var uid = HttpContext.Session.GetUserId() ?? HttpContext.Session.SetUserId(cookiesId);
            if (uid.IsNullOrEmpty())
            {
                throw new UserFriendlyException("更改失败", "登陆失效，请重新登陆");
            }
            if (changePwd.OldPass.IsNullOrEmpty() || changePwd.NewPass.IsNullOrEmpty())
            {
                throw new UserFriendlyException("更改失败", "旧密码或新密码不能为空！");
            }
            var user=_userManager.FindById(uid);
            if (!user.Password.Equals(WorkFlowTaskAbpConsts.GetEncrpyedAccessToken(changePwd.OldPass)))
            {
                throw new UserFriendlyException("更改失败", "旧密码不正确");
            }
            user.Password = WorkFlowTaskAbpConsts.GetEncrpyedAccessToken(changePwd.NewPass);
            _userManager.Update(user);
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }
    }
}
