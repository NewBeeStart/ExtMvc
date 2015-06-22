using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace COM.XXXX.Controllers
{ 
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            this.ViewBag.User = CurrentUser;
            return View();
        }

        public JObject Upload()
        { 
            var c = Request.Files[0];
            JObject job=new JObject();
            if (c != null && c.ContentLength > 0)
            {
                string filename = Guid.NewGuid().ToString() +c.FileName;
                string destination = Server.MapPath("/UploadFile/");
                c.SaveAs(destination + filename);
                job.Add("path", filename);
            }
          job.Add("success",true);
          job.Add("data",  "上传成功");
          return job;
        }
    }
}
