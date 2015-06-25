/*----------------------------------------------------------------
// Copyright (C) 2014 NewBee工作室
// 版权所有。 
//
// 文件名：XinGangDbContext
// 文件功能描述：
//
// 创建标识：xycui 2014/8/26 14:28:02
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COM.XXXX.Models.Admin;
using COM.XXXX.Models.CMS;

namespace Repository.Domain
{
  

    public class TestDbContext:DbContext
    {
        public TestDbContext()
            : base("DefaultConnection")
       {
           Database.SetInitializer<TestDbContext>(null);
       }

        public DbSet<User> Users { get; set; }
        public DbSet<Button> Buttons { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleRight> RoleRights { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        //public DbSet<DictionaryGroup> DictionaryGroups { get; set; }
        public DbSet<Dictionary> Dictionarys { get; set; }

        public DbSet<Cms_Channel> Cms_Channels { get; set; }
        public DbSet<Cms_PageType> Cms_PageTypes { get; set; }
        public DbSet<Cms_Classify> Cms_Classifies { get; set; }
        public DbSet<Cms_PageAttribute> Cms_PageAttributes { get; set; }
        public DbSet<Cms_PageTitle> Cms_PageTitles { get; set; }
        public DbSet<Cms_PageContent> Cms_PageContents { get; set; }
        public DbSet<Cms_Privilege> Cms_Privileges { get; set; }
        public DbSet<Cms_WebSite> Cms_WebSites { get; set; }   
        //public DbSet<Page> Pages { get; set; } 
            

    } 
}
