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
        [DataMember]
        public string CompanyName { get; set; }
        /// <summary>
        /// 网站所属公司地址
        /// </summary>
        [DataMember]
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 网站所属公司类型
        /// </summary>
        [DataMember]
        public string CompanyType { get; set; }
        /// <summary>
        /// 公司联系电话
        /// </summary>
        [DataMember]
        public string Telephone { get; set; }
        /// <summary>
        /// 网站管理Email
        /// </summary>
        [DataMember]
        public string Email { get; set; }
        /// <summary>
        /// 主机地址（域名）
        /// </summary>
        [DataMember]
        public string HostAddress { get; set; }
        /// <summary>
        /// 网站标题
        /// </summary>
        [DataMember]
        public string SiteTitle { get; set; }
        /// <summary>
        /// 网站主页名称
        /// </summary>
        [DataMember]
        public string IndexPage { get; set; }
        /// <summary>
        /// 网站页面内容
        /// </summary>
        [DataMember]
        public string PageContent { get; set; }

        //public virtual List<Cms_Channel> Channels { get; set; }

    }
}

