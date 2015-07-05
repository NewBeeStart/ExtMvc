using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using COM.XXXX.Models.CMS;
using COM.XXXX.WebApi;
using Repository.DAL.Repository;
using Repository.DAL.Repository.Cms;

namespace COM.XXXX.WebApi.Cms.Controllers
{

    public class CmsPageContentApiController : ApiControllerBase<CmsPageContentRepository, Cms_PageContent>
    {
        public CmsPageContentApiController()
        {
            base.SetRepository();
        }

        [HttpGet]
        public dynamic GetFormByPageId(Guid id)
        {
            var result = base.Repository.Query(u => u.PageID == id).ToList();
            if (result.Count > 0)
            {
                return base.GetForm(result.First().ID);
            }
            return new { success = false, message = "数据获取失败！" };
        }
    }
}
