using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using WeiXinBase;

namespace COM.XXXX.Controllers.Areas.WeiXin.Controllers
{
    public class HomeController : Controller
    {
        //AppID：wx999e8c0058cc0750
        //AppSecret:585e415d15fa11b36c31a5d408614798 
        //URL:http://anbylau2130.gicp.net/WeiXin/Home
        //Token:WeiXinApp
        //EncodingAESKey:a61A1b0Nlbjz7YoP621XbjE6U8S6yVlf4WsYactx6fc
       [HttpPost]
        public ActionResult Index()
        {
           // new TokenValidater("WeiXinApp",base.HttpContext).Validate();
            string postStr = "";
            Stream s = HttpContext.Request.InputStream;//此方法是对System.Web.HttpContext.Current.Request.InputStream的封装，可直接代码
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            postStr = Encoding.UTF8.GetString(b);
            return View();
        }

        [HttpPost]
        public void RecMsg(string xmlmsg) 
        {
          
        }



      
    }
}
