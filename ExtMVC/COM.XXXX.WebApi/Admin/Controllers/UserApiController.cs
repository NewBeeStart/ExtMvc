using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using COM.XXXX.Models;
using COM.XXXX.Models.Admin;
using Repository.DAL.Repository;

namespace COM.XXXX.WebApi.Admin.Controllers
{
    public class UserApiController : ApiControllerBase<UserRepository, User>
    {

        public UserApiController()
        {
            base.SetRepository();
        }

        [HttpPost]
        public dynamic GetAllUsers()
        {
            var list = base.Get();
            return new
                {
                    total = list.Count(),
                    rows = list
                };
        }
        //[HttpPost]
        //public dynamic GetGridPager()
        //{
        //    string start=string.Empty;
        //    string limit=string.Empty;
        //    var dicparams=this.ControllerContext.Request.GetQueryNameValuePairs();
        //    foreach (var item in dicparams)
        //    {
        //        if(item.Key=="start")
        //        {
        //            start=item.Value;
        //        }
        //        if(item.Key=="limit")
        //        {
        //            limit=item.Value;
        //        }
        //    }
        //    return base.PostGridPager(start, limit);
        //}
      
        public dynamic PutInitPassword(Guid? id)
        {
            try
            {
                User user = base.Get(Guid.Parse(id.ToString()));

                user.PassWord = System.Configuration.ConfigurationSettings.AppSettings["InitPassword"].ToString();

                return  base.Put(Guid.Parse(id.ToString()), user);
               
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }





        public dynamic PutMoveUser(Guid? id, Guid? orgid)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                User user = base.Get(Guid.Parse(id.ToString()));

                user.OrganizationID = orgid;

                return base.Put(Guid.Parse(id.ToString()), user);
            }
            return "(。﹏。*)移动失败！";
        }
    }
}