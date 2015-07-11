using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace COM.XXXX.Models.CMS
{
    [Serializable]
    [DataContract]
    public class Cms_PagePicture : IModel
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        [DataMember]
        public string PicPath { get; set; }
        /// <summary>
        /// 图片说明
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 页面标识
        /// </summary>
        [DataMember]
        public Guid? PageID { get; set; }
        /// <summary>
        /// 图片排序
        /// </summary>
        [DataMember]
        public int? SortIndex { get; set; }

        public  virtual Cms_PageType Page { get; set; }
    }
}
