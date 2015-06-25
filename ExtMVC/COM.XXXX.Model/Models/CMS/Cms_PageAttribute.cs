using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using COM.XXXX.Models.Admin;

namespace COM.XXXX.Models.CMS
{
    [Serializable]
    [DataContract]
    public class Cms_PageAttribute : IModel
    {
        /// <summary>
        /// 是否顶置
        /// </summary>
        [DataMember]
        public bool isTop { get; set; }
        /// <summary>
        /// 是否幻灯片
        /// </summary>
        [DataMember]
        public bool? isPic { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        [DataMember]
        public bool isHot { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        [DataMember]
        public bool isRecommend { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        [DataMember]
        public bool isComment { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public int SortIndex { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        [DataMember]
        public int ReadCount { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        [DataMember]
        public Guid? CreateUserID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 更新人ID
        /// </summary>
        [DataMember]
        public Guid? UpdateUserID { get; set; }
        /// <summary>
        /// 页面类型ID
        /// </summary>
        [DataMember]
        public Guid? PageTypeID { get; set; }


        public virtual User CreateUser { get; set; }

        public virtual User UpdateUser { get; set; }

        public virtual Cms_PageType PageType { get; set; }


    }
}
