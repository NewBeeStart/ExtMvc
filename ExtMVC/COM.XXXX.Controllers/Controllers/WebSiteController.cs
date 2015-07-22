using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Com.XXXX.Class;
using Com.XXXX.Common;
using COM.XXXX.Models.Admin;
using Repository.DAL.Repository;
using Repository.Domain;
using System.Net;

namespace COM.XXXX.Controllers
{
    public class WebSiteController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "尉氏县鑫瑞专业种植合作社-网站首页";
            return View();
        }

        public ActionResult News()
        {
            ViewBag.Title = "尉氏县鑫瑞专业种植合作社-新闻动态";
            return View();
        }
        public ActionResult Products()
        {
            ViewBag.Title = "尉氏县鑫瑞专业种植合作社-产品详情";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Title = "尉氏县鑫瑞专业种植合作社-联系方式";
            return View();
        }
        public ActionResult CompanyInfo()
        {
            ViewBag.Title = "尉氏县鑫瑞专业种植合作社-公司简介";
            return View();
        }
        public ActionResult BaseShow() 
        {
            ViewBag.Title = "尉氏县鑫瑞专业种植合作社-基地展示";
            return View();
        }
    }
}
