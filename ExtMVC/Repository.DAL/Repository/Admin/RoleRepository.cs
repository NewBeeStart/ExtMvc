﻿/*----------------------------------------------------------------
// Copyright (C) 2014 NewBee工作室
// 版权所有。 
//
// 文件名：RoleRepository
// 文件功能描述：
//
// 创建标识：xycui 2014/12/15 10:25:33
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COM.XXXX.Models;
using COM.XXXX.Models.Admin;
using Repository.Domain.Infrastructure;
using System.Transactions;

namespace Repository.DAL.Repository
{
    public class RoleRepository:Repository<Role>
    {
    }
}
