using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace COM.XXXX.Models.CMS
{
    [Serializable]
    [DataContract]
    public class Cms_PageType : IModel
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        [DataMember]
        public Guid? ClassifyID { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [DataMember]
        public string TypeName { get; set; }
        /// <summary>
        /// 类型编号
        /// </summary>
        [DataMember]
        public string TypeCode { get; set; }
        /// <summary>
        /// 是否有标题
        /// </summary>
        [DataMember]
        public bool hasTitle { get; set; }
        /// <summary>
        /// 是否有文章
        /// </summary>
        [DataMember]
        public bool hasArticle { get; set; }
        /// <summary>
        /// 是否有图片
        /// </summary>
        [DataMember]
        public bool hasPictrue { get; set; }
        /// <summary>
        /// 是否有表单
        /// </summary>
        [DataMember]
        public bool hasForm { get; set; }

      

        public virtual Cms_Classify Classify { get; set; }

        //public virtual List<Cms_Page> Pages { get; set; }
    }
}
