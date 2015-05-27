using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using RouteDebug;
using Com.XXXX.Common;
using COM.XXXX.Models.Admin;

namespace COM.XXXX.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //日志
            Log4NetHelper.CreateInstance().SetConfig();
            Log4NetHelper.CreateInstance().WriteLog("Begin log!");
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AuthConfig.RegisterAuth();
            // 默认情况下对 Entity Framework 使用 LocalDB
            //Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //在出现未处理的错误时运行的代码
            Exception objExp = HttpContext.Current.Server.GetLastError();
            User user=new CookieManage().ReadFromCookie(ConstHelper.UserCookie) as User;
            if (user != null)
            {
                Log4NetHelper.CreateInstance().WriteLog("\r\n用户ID:" + user.ID + "\r\n用户名:" + user.UserName + "\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n异常信息:" + Server.GetLastError().Message, objExp);
            }
            Log4NetHelper.CreateInstance().WriteLog("\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n异常信息:" + Server.GetLastError().Message, objExp);
        } 
    }
}