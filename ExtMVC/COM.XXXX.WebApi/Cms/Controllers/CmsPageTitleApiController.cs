using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using COM.XXXX.Models.CMS;
using Repository.DAL.Repository;
using Repository.DAL.Repository.Cms;

namespace COM.XXXX.WebApi.Cms.Controllers
{
    public class CmsPageTitleApiController : ApiControllerBase<CmsPageTitleRepository, Cms_PageTitle>
    {
        public CmsPageTitleApiController()
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
