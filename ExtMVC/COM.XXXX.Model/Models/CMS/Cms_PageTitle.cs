using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace COM.XXXX.Models.CMS
{
    [Serializable]
    [DataContract]
    public class Cms_PageTitle : IModel
    {
        /// <summary>
        /// 标题内容
        /// </summary>
        [DataMember]
        public string TitleContent { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 页面ID
        /// </summary>
        [DataMember]
        public Guid? PageID { get; set; }

        public virtual Cms_Page Page { get; set; }
    }
}
