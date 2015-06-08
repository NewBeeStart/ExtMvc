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
    /// CMS配置表
    /// </summary>
    public class Cms_Privilege : IModel
    {
        /// <summary>
        /// 配置项：Template
        /// </summary>
        public string Master { get; set; }
        /// <summary>
        /// 配置值：TemplateID
        /// </summary>
        public string MasterValue { get; set; }
        /// <summary>
        /// Tag，module
        /// </summary>
        public string Access { get; set; }
        /// <summary>
        /// tagID，moduleID
        /// </summary>
        public string AccessValue { get; set; }
        /// <summary>
        /// enable，disable
        /// </summary>
        public string Operation { get; set; }
    }
}

