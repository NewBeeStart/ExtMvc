﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace COM.XXXX.Controllers.Areas.Cms.Controllers
{
    public class CmsPageController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageList()
        {
            return View();
        }
    }
}
