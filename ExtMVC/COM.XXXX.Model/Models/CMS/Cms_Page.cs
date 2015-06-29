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
        public string Pics { get; set; }

        public virtual Cms_Page PageType { get; set; }
    }
}
