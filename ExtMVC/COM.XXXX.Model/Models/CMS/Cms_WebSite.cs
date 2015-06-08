using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace COM.XXXX.Models.CMS
{
    [Serializable]
    [DataContract]
    public class Cms_WebSite : IModel
    {
        /// <summary>
        /// 网站所属公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 网站所属公司地址
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 网站所属公司类型
        /// </summary>
        public string CompanyType { get; set; }
        /// <summary>
        /// 公司联系电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 网站管理Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 主机地址（域名）
        /// </summary>
        public string HostAddress { get; set; }
        /// <summary>
        /// 网站标题
        /// </summary>
        public string SiteTitle { get; set; }
        /// <summary>
        /// 网站主页名称
        /// </summary>
        public string IndexPage { get; set; }
        /// <summary>
        /// 网站模板ID
        /// </summary>
        public Guid? LayOutTemplateID { get; set; }

        public virtual Cms_PageTemplate LayOutTemplate { get; set; }

    }
}

