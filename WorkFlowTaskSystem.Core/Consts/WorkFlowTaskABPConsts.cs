using System;
using System.Collections.Generic;
using System.Text;
using Abp.Runtime.Security;

namespace WorkFlowTaskSystem.Core
{
    /// <summary>
    /// 配置常量
    /// </summary>
    public class WorkFlowTaskAbpConsts
    {
        public const string LocalizationSourceName = "WorkFlowTaskSystem-zh-CN";
        /// <summary>
        /// 数据库连接字符串key
        /// </summary>
        public const string ConnectionStringName = "mongodb";
        /// <summary>
        /// 数据库名称key
        /// </summary>
        public const string DatatabaseName = "database";
        /// <summary>
        /// 加密key
        /// </summary>
        public const string DefaultPassPhrase = "workflowtoken";

        /// <summary>
        /// 登陆id
        /// </summary>
        public const string UserId = "userId";
        /// <summary>
        /// 验证码
        /// </summary>
        public const string VerificationCode = "verificationCode";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string GetEncrpyedAccessToken(string accessToken)
        {

            return SimpleStringCipher.Instance.Encrypt(accessToken, WorkFlowTaskAbpConsts.DefaultPassPhrase);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string GetDecryptAccessToken(string accessToken)
        {

            return SimpleStringCipher.Instance.Decrypt(accessToken, WorkFlowTaskAbpConsts.DefaultPassPhrase);
        }
    }

    public class AppConsts
    {
        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public const string DefaultPassPhrase = "gsKxGZ012HLL3MI5";
    }
}
