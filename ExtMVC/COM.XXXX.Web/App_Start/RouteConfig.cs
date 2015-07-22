using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace COM.XXXX.Web
{
    public class RouteConfig
    {

        public class DomainWebSiteConstraint : IRouteConstraint
        {
            public bool Match(HttpContextBase httpContext, Route route,
                string parameterName, RouteValueDictionary values,
                RouteDirection routeDirection)
            {
                return httpContext.Request.RawUrl.ToLowerInvariant() == "/" || httpContext.Request.RawUrl.ToLowerInvariant().Contains("website") || httpContext.Request.RawUrl.ToLowerInvariant()==string.Empty;
            }
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.RouteExistingFiles = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("", new { DomainConstraint = new DomainWebSiteConstraint() });
            
            //默认的无区域路由
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "WebSite", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "COM.XXXX.Controllers" }
                );



        } 
    }
}