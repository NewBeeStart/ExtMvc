/*----------------------------------------------------------------
// Copyright (C) 2014 郑州华粮科技股份有限公司
// 版权所有。 
//
// 文件名：Role
// 文件功能描述：
//
// 创建标识：xycui 2014/12/11 9:11:30
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace COM.XXXX.Models.Admin
{
    [Serializable]
    [DataContract]
    public class Role : IModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Desc { get; set; }

    }
}
