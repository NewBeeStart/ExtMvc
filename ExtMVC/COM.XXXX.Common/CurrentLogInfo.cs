using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COM.XXXX.Models.Admin;
using Com.XXXX.Common;

namespace COM.XXXX.Common
{
    public class CurrentLogInfo
    {
        public static User CurrentLog {
            get
            {
                 return new CookieManage().ReadFromCookie(ConstHelper.UserCookie) as User;
            }
        }
    }
}
