/*----------------------------------------------------------------
// Copyright (C) 2014 NewBee工作室
// 版权所有。 
//
// 文件名：RoleRight
// 文件功能描述：
//
// 创建标识：xycui 2014/12/11 9:19:22
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
    public class RoleRight : IModel
    {
        [DataMember]
        public Guid? RoleID { get; set; }
        [DataMember]
        public Guid? UserID { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
