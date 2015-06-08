using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace COM.XXXX.Models.CMS
{
    [Serializable]
    [DataContract]
    /// <summary>
    /// 频道信息
    /// </summary>
    public class Cms_Channel : IModel
    {
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember]
        public string ChannelName { get; set; }
        /// <summary>
        /// 频道编号
        /// </summary>
        [DataMember]
        public string ChannelCode { get; set; }
        /// <summary>
        /// 频道备注
        /// </summary>
        [DataMember]
        public string ChannelRemark { get; set; }
        /// <summary>
        /// 网站ID
        /// </summary>
        [DataMember]
        public Guid? WebSiteID { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        [DataMember]
        public Guid? LayOutTemplateID { get; set; }

        public virtual Cms_WebSite WebSite { get; set; }

        public virtual Cms_PageTemplate LayOutTemplate{ get; set; }
    }
}

