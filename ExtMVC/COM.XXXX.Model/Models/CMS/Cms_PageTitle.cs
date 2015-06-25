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
        /// 创建类型ID
        /// </summary>
        [DataMember]
        public Guid? PageTypeID { get; set; }


        public Cms_PageType PageType { get; set; }
    }
}
