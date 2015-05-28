using Com.XXXX.Common;
using Newtonsoft.Json;
/*----------------------------------------------------------------
// Copyright (C) 2014 NewBee工作室
// 版权所有。 
//
// 文件名：WebApiExceptionFilter
// 文件功能描述：
//
// 创建标识：xycui 2014/12/15 15:54:55
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;


namespace COM.XXXX.WebApi
{
    
        public class WebApiExceptionFilter : ExceptionFilterAttribute
        {
            public override void OnException(HttpActionExecutedContext context)
            {
               
                StringBuilder log = new StringBuilder();
                log.Append("错误类型:WebApi");
                log.Append(string.Format("<br />请求URL:{0}", HttpContext.Current.Request.Url));
                log.Append(string.Format("<br />客户端主机:{0};客户端IP:{1}", HttpContext.Current.Request.UserHostName, HttpContext.Current.Request.UserHostAddress));
                log.Append(string.Format("<br />请求参数:{0}",JsonConvert.SerializeObject(HttpContext.Current.Request.QueryString)));
                log.Append(string.Format("<br />请求form:{0}", HttpContext.Current.Request.Form));
                log.Append(string.Format("<br />请求controller:{0}", HttpContext.Current.Request.RequestContext.RouteData.Values["controller"]));
                log.Append(string.Format("<br />请求action:{0}", HttpContext.Current.Request.RequestContext.RouteData.Values["action"]));
                log.Append(string.Format("<br />客户浏览器:{0}", HttpContext.Current.Request.Browser.Browser));
                log.Append(string.Format("<br />请求方法:{0}", HttpContext.Current.Request.HttpMethod));
                Log4NetHelper.CreateInstance().WriteLog(log.ToString(),context.Exception);
                var message = context.Exception.Message;
                if (context.Exception.InnerException != null)
                    message = context.Exception.InnerException.Message;

                context.Response = new HttpResponseMessage() { Content = new StringContent(message) };

                base.OnException(context);
            }
      
    }
}
