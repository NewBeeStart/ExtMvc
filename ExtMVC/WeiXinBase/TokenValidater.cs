using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace WeiXinBase
{
    //微信令牌验证类
    public class TokenValidater
    {
        public string Token { get; set; }
        public HttpContextBase Context  { get; set; } 

        public TokenValidater(string tokenstr, HttpContextBase Context)
        {
            this.Token = tokenstr;
            this.Context = Context;
        } 

        public void Validate()
        {
            string url = Context.Request.RawUrl;
            if (Context.Request.HttpMethod != "POST")
            {
                if(ValidUrl(Token));
            }
        }

        /// <summary>
        /// 验证url权限， 接入服务器
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool ValidUrl(string token)
        {
            string echoStr = Context.Request["echoStr"];
            if (CheckSignature(token))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    ResponseWrite(echoStr);
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature(string token)
        {
            string signature = Context.Request["signature"];
            string timestamp = Context.Request["timestamp"];
            string nonce = Context.Request["nonce"];
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string HashPasswordForStoringInConfigFile(string str, string type)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, type);
        }

        private void ResponseWrite(string str)
        {
            Context.Response.Write(str);
            Context.Response.End();
        }
    }
}
