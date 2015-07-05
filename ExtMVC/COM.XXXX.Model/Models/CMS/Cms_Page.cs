using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace COM.XXXX.Models.CMS
{
    [Serializable]
    [DataContract]
    public class Cms_Page : IModel
    {
    
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public Guid? PageTypeID { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        [DataMember]
        public string PicPath { get; set; }

        /// <summary>
        /// 页面名称
        /// </summary>
        [DataMember]
        public string PageName { get; set; }

        /// <summary>
        /// 页面编号
        /// </summary> 
        [DataMember]
        public string PageCode { get; set; }

       /// <summary>
       /// 备注
       /// </summary>
        [DataMember]
        public string Remark { get; set; }



        public virtual Cms_Page PageType { get; set; }
    }
}
