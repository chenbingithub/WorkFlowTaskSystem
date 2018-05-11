using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Runtime.Security;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using WorkFlowTaskSystem.Controllers;
using WorkFlowTaskSystem.Web.Core.Models.TokenAuth;

namespace WorkFlowTaskSystem.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : WorkFlowTaskSystemControllerBase
    {
       

        
        //[HttpPost]
        //public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        //{
        //    var loginResult = await GetLoginResultAsync(
        //        model.UserNameOrEmailAddress,
        //        model.Password
        //    );

        //    var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

        //    return new AuthenticateResultModel
        //    {
        //        AccessToken = accessToken,
        //        EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
        //        ExpireInSeconds =5200,
        //        UserId = loginResult.User.Id
        //    };
        //}

        

        


       

        //private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password)
        //{
        //    var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

        //    switch (loginResult.Result)
        //    {
        //        case AbpLoginResultType.Success:
        //            return loginResult;
        //        default:
        //            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
        //    }
        //}

        //private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        //{
        //    var now = DateTime.UtcNow;

        //    var jwtSecurityToken = new JwtSecurityToken(
        //        issuer: _configuration.Issuer,
        //        audience: _configuration.Audience,
        //        claims: claims,
        //        notBefore: now,
        //        expires: now.Add(expiration ?? _configuration.Expiration),
        //        signingCredentials: _configuration.SigningCredentials
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        //}

        //private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        //{
        //    var claims = identity.Claims.ToList();
        //    var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

        //    // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
        //    claims.AddRange(new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        //    });

        //    return claims;
        //}

        //private string GetEncrpyedAccessToken(string accessToken)
        //{
        //    return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        //}
    }
}
