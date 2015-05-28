using Com.XXXX.Common;
/*----------------------------------------------------------------
// Copyright (C) 2014 NewBee工作室
// 版权所有。 
//
// 文件名：MvcHandleErrorAttribute
// 文件功能描述：
//
// 创建标识：xycui 2014/12/15 16:18:53
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Com.XXXX.Common;
using Newtonsoft.Json;


namespace COM.XXXX.Controllers
{
    public class MvcHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            StringBuilder log = new StringBuilder();
            log.Append("错误类型:UI操作"); 
            log.Append(string.Format("<br />请求URL:{0}", filterContext.RequestContext.HttpContext.Request.Url));
            log.Append(string.Format("<br />请求参数:{0}", JsonConvert.SerializeObject(filterContext.RequestContext.HttpContext.Request.QueryString)));
            log.Append(string.Format("<br />客户端主机:{0};客户端IP:{1}", filterContext.RequestContext.HttpContext.Request.UserHostName,filterContext.RequestContext.HttpContext.Request.UserHostAddress));
            log.Append(string.Format("<br />客户浏览器:{0}",filterContext.RequestContext.HttpContext.Request.Browser.Browser));
            log.Append(string.Format("<br />请求方法:{0}", filterContext.RequestContext.HttpContext.Request.HttpMethod));

            Log4NetHelper.CreateInstance()
                .WriteLog(log.ToString(), filterContext.Exception);

            base.OnException(filterContext);
        } 
    }
}
