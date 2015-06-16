using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using COM.XXXX.Models.CMS;
using Repository.DAL.Repository;
using System.Web.Http;

namespace COM.XXXX.WebApi.Cms.Controllers
{
    public class WebSiteApiController : ApiControllerBase<WebSiteRepository, Cms_WebSite>
    {
        public WebSiteApiController()
        {
            base.SetRepository();
        }
      
        [HttpPost]
        [HttpGet]
        public HttpResponseMessage GetWebSiteCombox()
        {
            var result = this.Get();
            return toJson(result);
        }
    }
}
