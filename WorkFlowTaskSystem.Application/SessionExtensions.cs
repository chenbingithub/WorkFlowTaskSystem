using System;
using System.Collections.Generic;
using System.Text;
using Abp.Extensions;
using Abp.Runtime.Security;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WorkFlowTaskSystem.Core;

namespace WorkFlowTaskSystem.Application
{
   public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static string GetUserId(this ISession session)
        {
           return session.GetString(WorkFlowTaskAbpConsts.UserId);
        }

        public static string SetUserId(this ISession session,string userId)
        {
            if (userId.IsNullOrEmpty()) return "";
            var duserid=SimpleStringCipher.Instance.Decrypt(userId, AppConsts.DefaultPassPhrase);
            session.SetString(WorkFlowTaskAbpConsts.UserId, duserid);
            return duserid;
        }

    }
}
