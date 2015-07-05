using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using COM.XXXX.Common;
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

        private string GetWebSitePage(string path)
        {
            return   Com.XXXX.Common.ConstHelper.WebSitePath + path;
        }

        /// <summary>
        /// 生成网站页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage GetWebSite(string  id)
        { 
            Cms_WebSite website = this.Repository.Query(u => u.ID.ToString() == id).First();
            FileWriter.Write(GetWebSitePage(website.IndexPage), website.PageContent);
            List<Cms_Channel> cmschanels = new CmsChannelApiController().Repository.Query(u => u.WebSiteID.ToString() == id).ToList();
            foreach (Cms_Channel cmsChannel in cmschanels)
            {
                FileWriter.Write(GetWebSitePage(cmsChannel.ChannelCode), website.PageContent);
                
                List<Cms_Classify> cmsClassifies =
                    new CmsClassifyApiController().Repository.Query(u => u.ChannelID == cmsChannel.ID).ToList();
                foreach (Cms_Classify cmsClassify in cmsClassifies)
                {
                    
                     
                }
            }
            return toJson(new {success = true, message = "页面生成成功"});
        }
    }
}
