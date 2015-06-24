using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using COM.XXXX.Models.CMS;
using Repository.DAL.Repository;
using Repository.DAL.Repository.Cms;

namespace COM.XXXX.WebApi.Cms.Controllers
{
    public class CmsModuleApiController : ApiControllerBase<CmsModuleRepository, Cms_Module>
    {
        public CmsModuleApiController()
        {
            base.SetRepository();
        }
    }
}
