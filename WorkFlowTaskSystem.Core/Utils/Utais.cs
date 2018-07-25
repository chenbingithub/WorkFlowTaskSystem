using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
    /// <summary>
    /// http://www.ucpaas.com/
    /// 云之讯·开放平台
    /// </summary>
    public class UtaisTelPhone
    {
 
        /// <summary>
        /// 短信提醒
        /// </summary>
        /// <param name="telphone">手机号</param>
        /// <param name="code">验证码</param>
        public static void ReturnCode(string telphone,string code)
        {
            string serverIp = "api.ucpaas.com";
            string serverPort = "443";
            string account = "00abc316448190ebe4e6898bbdada959";    //用户sid
            string token = "78d323f3759677c457a2ffb39606b480";      //用户sid对应的token
            string appId = "12d44796f9a443cba0b781518daa640b";      //对应的应用id，非测试应用需上线使用
            string clientNum = "60000000000001";
            string clientpwd = "";
            string friendName = "";
            string clientType = "0";
            string charge = "0";
            string phone = "";
            string date = "day";
            uint start = 0;
            uint limit = 100;
            string toPhone = telphone;                                    //发送短信手机号码，群发逗号区分
            string templatedId = "36099";                               //短信模板id，需通过审核
            string param = code;                                     //短信参数
            string verifyCode = "1234";
            string fromSerNum = "4000000000";
            string toSerNum = "4000000000";
            string maxallowtime = "60";

           UCSRestRequest api = new UCSRestRequest();

            api.init(serverIp, serverPort);
            api.setAccount(account, token);
            api.enabeLog(true);
            api.setAppId(appId);
            api.enabeLog(true);

            //查询主账号
            //api.QueryAccountInfo();

            //申请client账号
            //api.CreateClient(friendName, clientType, charge, phone);

            //查询账号信息(账号)
            //api.QueryClientNumber(clientNum);

            //查询账号信息(电话号码)
            //api.QueryClientMobile(phone);

            //查询账号列表
            //api.GetClient(start, limit);

            //删除一个账号
            //api.DropClient(clientNum);

            //查询应用话单
            //api.GetBillList(date);

            //查询账号话单
            //api.GetClientBillList(clientNum, date);

            //账号充值
            //api.ChargeClient(clientNum, clientType, charge);

            //回拨
            //api.CallBack(clientNum, toPhone, fromSerNum, toSerNum, maxallowtime);

            //短信
            api.SendSMS(toPhone, templatedId, param);

            //语音验证码
            //api.VoiceCode(toPhone, "1234");
        }
    }
}