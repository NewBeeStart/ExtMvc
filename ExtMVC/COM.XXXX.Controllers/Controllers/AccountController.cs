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
    public class AccountController : Controller
    {
        private UserRepository userRepository;
        public AccountController()
        {
            userRepository = new UserRepository(new TestDbContext());
        }
        /// <summary>
        /// 登陆视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                var user = new CookieManage().ReadFromCookie(ConstHelper.UserCookie) as User;

                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        VolidateImage image = new VolidateImage(25, 60, ColorTranslator.FromHtml("#EEEEEE"), Color.Teal, "", 12, 0, 0);
        /// <summary>
        /// 验证码图片
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidationImage()
        {
            var randCode = image.CreateShowString();
            new CookieManage().WriteCookie(ConstHelper.UserValidateCode, randCode, 1);
            MemoryStream ms = new MemoryStream();
            image.CreateImage(randCode).Save(ms, ImageFormat.Jpeg);
            return File(ms.ToArray(), "image/jpep");
        }
       
        /// <summary>
        /// 登陆方法，用于验证用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult OnLogin(string username, string password, string randCode,string rememberme)
        {
            User user = new User() { UserName = username, PassWord = password };

            if (new CookieManage().ReadCookie(ConstHelper.UserValidateCode) != randCode)
            {
                return Content("警告：验证码错误！");
            }

            if (user.UserName.IsNullOrEmpty() || user.PassWord.IsNullOrEmpty())
            {
                return Content("警告：用户名密码不能为空！");
            }

            var current=userRepository.GetUserByUserName(user.UserName, user.PassWord);
            if (null != current)
            {
                if (rememberme == "remember")
                    new CookieManage().WriteObject2Cookie(ConstHelper.UserCookie, current, 365);
                else
                    new CookieManage().WriteObject2Cookie(ConstHelper.UserCookie, current, 1);
                return Content("{success:true}");
            }
            return Content("错误：账号或密码不匹配！");
        }

        /// <summary>
        /// 注销登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Logout()
        {
            new CookieManage().WriteCookie(ConstHelper.UserCookie, "", 0);
            return Content("OK");
        }

    }
}
