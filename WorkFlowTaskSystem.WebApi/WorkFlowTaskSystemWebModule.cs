using System;
using System.Reflection;
using System.Text;
using Abp.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WorkFlowTaskSystem.Authentication.JwtBearer;
using WorkFlowTaskSystem.Web.Core;
using WorkFlowTaskSystem.Web.Core.Configuration;

namespace WorkFlowTaskSystem.WebApi
{
    [DependsOn(typeof(WorkFlowTaskSystemWebCoreModule))]
    public class WorkFlowTaskSystemWebModule: AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public WorkFlowTaskSystemWebModule(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }
        public override void PreInitialize()
        {
            //Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
            base.PreInitialize();
            ConfigureTokenAuth();
        }
        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }
        public override void Initialize()
        {
            //把当前程序集的特定类或接口注册到依赖容器中
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
