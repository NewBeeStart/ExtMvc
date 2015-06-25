using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
