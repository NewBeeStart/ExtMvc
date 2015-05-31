using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.XXXX.Models.CMS
{
    public  class Cms_SiteInfo:IModel
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 网站标题
        /// </summary>
        public string SiteTitle
        {
            get;
            set;
        }

        /// <summary>
        /// 网站简介
        /// </summary>
        public string Summary
        {
            get;
            set;
        }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string Telephone
        {
            get;
            set;
        }

        /// <summary>
        /// 公司Email
        /// </summary>
        public string Email
        {
            get;
            set;
        }
      
    }
}
