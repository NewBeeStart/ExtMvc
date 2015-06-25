using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
