﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COM.XXXX.Models.CMS;
using Repository.DAL.Repository;

namespace COM.XXXX.WebApi.Cms.Controllers
{
    public class PageTemplateApiController : ApiControllerBase<PageTemplateRepository, Cms_PageTemplate>
    {
        public PageTemplateApiController()
        {
            base.SetRepository();
        }
    }
}