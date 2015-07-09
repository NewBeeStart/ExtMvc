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
        public string PageContent { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public int SortIndex { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DataMember]
        public bool? InUse { get; set; }
        public virtual Cms_WebSite WebSite { get; set; }
        //public virtual List<Cms_Classify> Classifies { get; set; }
    }
}

