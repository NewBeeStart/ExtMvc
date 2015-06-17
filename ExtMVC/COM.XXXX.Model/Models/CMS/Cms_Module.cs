using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace COM.XXXX.Models.CMS
{
    [Serializable]
    [DataContract]
    public class Cms_Module : IModel
    {
       
        /// <summary>
        /// 标签名称
        /// </summary>
        [DataMember]
        public string TagName { get; set; }
        /// <summary>
        /// 标签编号
        /// </summary>
        [DataMember]
        public string TagCode { get; set; }
        /// <summary>
        /// 标签备注
        /// </summary>
        [DataMember]
        public string TagRemark { get; set; }
    }
}

