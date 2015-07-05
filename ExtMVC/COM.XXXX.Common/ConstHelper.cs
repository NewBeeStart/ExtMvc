/*----------------------------------------------------------------
// Copyright (C) 2014 NewBee工作室
// 版权所有。 
//
// 文件名：ConstHelper
// 文件功能描述：
//
// 创建标识：xycui 2014/8/26 15:42:54
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.XXXX.Common
{ 
    public static class ConstHelper
    {
        public static string UserCookie = "user";

        public static string UserValidateCode = "validatecode";

        public static string MenuXmlPath = "~/Menu.xml";

        public static string IconPath = "~/Scripts/icons/";

        /// <summary>
        /// 只能在Area页面中使用
        /// </summary>
        public static string ExtIconPath = "../../Scripts/Ext/Icons/fam/";

        /// <summary>
        /// website生成的路径
        /// </summary>
        public static string WebSitePath = "~/WebSite/";
    }
}