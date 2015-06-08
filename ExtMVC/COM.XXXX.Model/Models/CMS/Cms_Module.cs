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
        public string TagName { get; set; }
        /// <summary>
        /// 标签编号
        /// </summary>
        public string TagCode { get; set; }
        /// <summary>
        /// 标签备注
        /// </summary>
        public string TagRemark { get; set; }
    }
}

