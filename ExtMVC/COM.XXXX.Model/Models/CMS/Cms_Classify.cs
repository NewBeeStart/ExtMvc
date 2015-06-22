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
    /// 分类信息
    /// </summary>
    public class Cms_Classify : IModel
    {
       
        /// <summary>
        /// 类型名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 类型编号
        /// </summary>
        [DataMember]
        public string Code { get; set; }
        /// <summary>
        /// 类型编号
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 所属频道ID
        /// </summary>
        [DataMember]
        public Guid? ChannelID { get; set; }

        [DataMember]
        public int? SortIndex { get; set; }
        /// <summary>
        /// 布局模板ID
        /// </summary>
        [DataMember]
        public Guid? LayOutTemplateID { get; set; }

        public virtual Cms_Channel Channel { get; set; }
        public virtual Cms_PageTemplate LayOutTemplate { get; set; }
    }
}

