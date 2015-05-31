using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.XXXX.Models.CMS
{
    public class Cms_SiteMenu:IModel
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            get;
            set;
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int MenuSort
        {
            get;
            set;
        }

       /// <summary>
       /// 摘要
       /// </summary>
        public string Summary
        {
            get;
            set;
        }

        /// <summary>
        /// 是否dingzhi
        /// </summary>
        public bool IsTop
        {
            get;
            set;
        }
        

    }
}
