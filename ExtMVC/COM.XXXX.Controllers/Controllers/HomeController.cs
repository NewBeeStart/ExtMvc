using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COM.XXXX.Controllers
{ 
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            this.ViewBag.User = CurrentUser;
            return View();
        }
    }
}
