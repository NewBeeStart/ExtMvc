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
    /// 页面模板
    /// </summary>
    public class Cms_PageTemplate : IModel
    {
       
        /// <summary>
        /// 模板路径：生成模板的路径
        /// </summary>
        [DataMember]
        public string Path { get; set; }
        /// <summary>
        /// 模板内容：html标签及JS，Razor的内容
        /// </summary>
        [DataMember]
        public string PageContent { get; set; }

        [DataMember] 
        public string Remark { get; set; }
    }
}

