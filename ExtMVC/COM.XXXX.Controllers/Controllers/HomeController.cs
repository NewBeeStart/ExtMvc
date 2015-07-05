using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COM.XXXX.Common;
using COM.XXXX.Models.CMS;
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

        public JObject UploadWater()
        {
            string text = "Eric测试";
            var c = Request.Files[0];
            JObject job = new JObject();
            string destination = Server.MapPath("~/UploadFile/");
            string filename = string.Empty;
            if (c != null && c.ContentLength > 0)
            {
                filename = Guid.NewGuid().ToString() + c.FileName;
                c.SaveAs(destination + c.FileName);
                COM.XXXX.Common.WaterMark.AddImageSignText("~/UploadFile/" + c.FileName, "~/UploadFile/" + filename, text, 9, 100, "", 50);
                job.Add("path", "~/UploadFile/" + filename);
            }
            job.Add("success", true);
            return job;
        }
        private string GetWebSitePage(string path)
        {
            return Com.XXXX.Common.ConstHelper.WebSitePath + path;
        }

        ///// <summary>
        ///// 生成网站页面
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public string CreateWebSite(string id)
        //{
        //    //Cms_WebSite website = new CmsWebSiteApiController().Repository.Query(u => u.ID.ToString() == id).First();
        //    //FileWriter.Write(GetWebSitePage(website.IndexPage), website.PageContent);
        //    //List<Cms_Channel> cmschanels = new CmsChannelApiController().Repository.Query(u => u.WebSiteID.ToString() == id).ToList();
        //    //foreach (Cms_Channel cmsChannel in cmschanels)
        //    //{
        //    //    FileWriter.Write(GetWebSitePage(cmsChannel.ChannelCode), website.PageContent);

        //    //    List<Cms_Classify> cmsClassifies =
        //    //        new CmsClassifyApiController().Repository.Query(u => u.ChannelID == cmsChannel.ID).ToList();
        //    //    foreach (Cms_Classify cmsClassify in cmsClassifies)
        //    //    {


        //    //    }
        //    //}
        //    //return "生成页面成功";
        //}
       
    }
}
