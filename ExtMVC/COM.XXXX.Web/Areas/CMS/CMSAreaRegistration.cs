using COM.XXXX.WebApi.Class;
using System.Web.Http;
using System.Web.Mvc;

namespace COM.XXXX.Web.Areas.CMS
{
    public class CMSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CMS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CMS_default",
                "CMS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            //注册区域WebApi路由
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                    this.AreaName + "Api",
                    "api/" + this.AreaName + "/{controller}/{action}/{id}",
                    new
                    {
                        action = RouteParameter.Optional,
                        id = RouteParameter.Optional,
                        namespaceName = new string[] { string.Format("COM.XXXX.WebApi.{0}.Controllers", this.AreaName) }
                    },
                    new { action = new StartWithConstraint() }
             );
        }
    }
}
