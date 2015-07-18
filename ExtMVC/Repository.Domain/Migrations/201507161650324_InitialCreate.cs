namespace Repository.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buttons",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        BtnName = c.String(),
                        BtnNo = c.String(),
                        BtnClass = c.String(),
                        BtnIcon = c.String(),
                        BtnStatus = c.String(),
                        MenuID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menus", t => t.MenuID)
                .Index(t => t.MenuID);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Width = c.Int(),
                        Height = c.Int(),
                        SortKey = c.Int(),
                        Controller = c.String(),
                        Action = c.String(),
                        iconCls = c.String(),
                        IsLeaf = c.Boolean(nullable: false),
                        OpenModel = c.Int(),
                        PMenuID = c.Guid(),
                        ModuleID = c.Guid(),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Modules", t => t.ModuleID)
                .ForeignKey("dbo.Menus", t => t.PMenuID)
                .Index(t => t.PMenuID)
                .Index(t => t.ModuleID);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Desc = c.String(),
                        iconCls = c.String(),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cms_Channel",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        ChannelName = c.String(),
                        ChannelCode = c.String(),
                        ChannelRemark = c.String(),
                        WebSiteID = c.Guid(),
                        PageContent = c.String(),
                        SortIndex = c.Int(nullable: false),
                        InUse = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cms_WebSite", t => t.WebSiteID)
                .Index(t => t.WebSiteID);
            
            CreateTable(
                "dbo.Cms_WebSite",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        CompanyName = c.String(),
                        CompanyAddress = c.String(),
                        CompanyType = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        HostAddress = c.String(),
                        SiteTitle = c.String(),
                        IndexPage = c.String(),
                        PageContent = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cms_Classify",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Remark = c.String(),
                        ChannelID = c.Guid(),
                        SortIndex = c.Int(),
                        PageContent = c.String(),
                        InUse = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cms_Channel", t => t.ChannelID)
                .Index(t => t.ChannelID);
            
            CreateTable(
                "dbo.Cms_PageAttribute",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        isTop = c.Boolean(nullable: false),
                        isPic = c.Boolean(nullable: false),
                        isHot = c.Boolean(nullable: false),
                        isRecommend = c.Boolean(nullable: false),
                        isComment = c.Boolean(nullable: false),
                        SortIndex = c.Int(nullable: false),
                        ReadCount = c.Int(nullable: false),
                        CreateUserID = c.Guid(),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUserID = c.Guid(),
                        PageID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.CreateUserID)
                .ForeignKey("dbo.Cms_Page", t => t.PageID)
                .ForeignKey("dbo.Users", t => t.UpdateUserID)
                .Index(t => t.CreateUserID)
                .Index(t => t.UpdateUserID)
                .Index(t => t.PageID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        RealName = c.String(),
                        UserName = c.String(),
                        PassWord = c.String(),
                        OfferTime = c.String(),
                        Education = c.String(),
                        Professional = c.String(),
                        IsMarry = c.Boolean(nullable: false),
                        BirthDay = c.String(),
                        InUse = c.Boolean(nullable: false),
                        Remark = c.String(),
                        OrganizationID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        POrganizationID = c.Guid(),
                        OrgType = c.String(),
                        Sort = c.Int(nullable: false),
                        ChargeLeaderIDs = c.String(),
                        ChargeLeaderNames = c.String(),
                        LeaderIDs = c.String(),
                        LeaderNames = c.String(),
                        Remark = c.String(),
                        InUse = c.Boolean(nullable: false),
                        iconCls = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.POrganizationID)
                .Index(t => t.POrganizationID);
            
            CreateTable(
                "dbo.Cms_Page",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        PageTypeID = c.Guid(),
                        PicPath = c.String(),
                        PageName = c.String(),
                        PageCode = c.String(),
                        Remark = c.String(),
                        InUse = c.Boolean(),
                        SortIndex = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cms_Page", t => t.PageTypeID)
                .Index(t => t.PageTypeID);
            
            CreateTable(
                "dbo.Cms_PageContent",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        PageContent = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                        PageID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cms_Page", t => t.PageID)
                .Index(t => t.PageID);
            
            CreateTable(
                "dbo.Cms_PagePicture",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        PicPath = c.String(),
                        Remark = c.String(),
                        PageID = c.Guid(),
                        SortIndex = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cms_PageType", t => t.PageID)
                .Index(t => t.PageID);
            
            CreateTable(
                "dbo.Cms_PageType",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        ClassifyID = c.Guid(),
                        TypeName = c.String(),
                        TypeCode = c.String(),
                        hasTitle = c.Boolean(nullable: false),
                        hasArticle = c.Boolean(nullable: false),
                        hasPictrue = c.Boolean(nullable: false),
                        hasForm = c.Boolean(nullable: false),
                        InUse = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cms_Classify", t => t.ClassifyID)
                .Index(t => t.ClassifyID);
            
            CreateTable(
                "dbo.Cms_PageTitle",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        TitleContent = c.String(),
                        Remark = c.String(),
                        PageID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cms_Page", t => t.PageID)
                .Index(t => t.PageID);
            
            CreateTable(
                "dbo.Cms_Privilege",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Master = c.String(),
                        MasterValue = c.String(),
                        Access = c.String(),
                        AccessValue = c.String(),
                        Operation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Dictionaries",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        PDictionaryID = c.Guid(),
                        Title = c.String(),
                        Code = c.String(),
                        Value = c.String(),
                        Sort = c.Int(nullable: false),
                        Desc = c.String(),
                        iconCls = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dictionaries", t => t.PDictionaryID)
                .Index(t => t.PDictionaryID);
            
            CreateTable(
                "dbo.Privileges",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Master = c.String(),
                        MasterValue = c.String(),
                        Access = c.String(),
                        AccessValue = c.String(),
                        Operation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleRights",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        RoleID = c.Guid(),
                        UserID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.RoleID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleRights", "UserID", "dbo.Users");
            DropForeignKey("dbo.RoleRights", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Dictionaries", "PDictionaryID", "dbo.Dictionaries");
            DropForeignKey("dbo.Cms_PageTitle", "PageID", "dbo.Cms_Page");
            DropForeignKey("dbo.Cms_PagePicture", "PageID", "dbo.Cms_PageType");
            DropForeignKey("dbo.Cms_PageType", "ClassifyID", "dbo.Cms_Classify");
            DropForeignKey("dbo.Cms_PageContent", "PageID", "dbo.Cms_Page");
            DropForeignKey("dbo.Cms_PageAttribute", "UpdateUserID", "dbo.Users");
            DropForeignKey("dbo.Cms_PageAttribute", "PageID", "dbo.Cms_Page");
            DropForeignKey("dbo.Cms_Page", "PageTypeID", "dbo.Cms_Page");
            DropForeignKey("dbo.Cms_PageAttribute", "CreateUserID", "dbo.Users");
            DropForeignKey("dbo.Users", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "POrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Cms_Classify", "ChannelID", "dbo.Cms_Channel");
            DropForeignKey("dbo.Cms_Channel", "WebSiteID", "dbo.Cms_WebSite");
            DropForeignKey("dbo.Buttons", "MenuID", "dbo.Menus");
            DropForeignKey("dbo.Menus", "PMenuID", "dbo.Menus");
            DropForeignKey("dbo.Menus", "ModuleID", "dbo.Modules");
            DropIndex("dbo.RoleRights", new[] { "UserID" });
            DropIndex("dbo.RoleRights", new[] { "RoleID" });
            DropIndex("dbo.Dictionaries", new[] { "PDictionaryID" });
            DropIndex("dbo.Cms_PageTitle", new[] { "PageID" });
            DropIndex("dbo.Cms_PageType", new[] { "ClassifyID" });
            DropIndex("dbo.Cms_PagePicture", new[] { "PageID" });
            DropIndex("dbo.Cms_PageContent", new[] { "PageID" });
            DropIndex("dbo.Cms_Page", new[] { "PageTypeID" });
            DropIndex("dbo.Organizations", new[] { "POrganizationID" });
            DropIndex("dbo.Users", new[] { "OrganizationID" });
            DropIndex("dbo.Cms_PageAttribute", new[] { "PageID" });
            DropIndex("dbo.Cms_PageAttribute", new[] { "UpdateUserID" });
            DropIndex("dbo.Cms_PageAttribute", new[] { "CreateUserID" });
            DropIndex("dbo.Cms_Classify", new[] { "ChannelID" });
            DropIndex("dbo.Cms_Channel", new[] { "WebSiteID" });
            DropIndex("dbo.Menus", new[] { "ModuleID" });
            DropIndex("dbo.Menus", new[] { "PMenuID" });
            DropIndex("dbo.Buttons", new[] { "MenuID" });
            DropTable("dbo.Roles");
            DropTable("dbo.RoleRights");
            DropTable("dbo.Privileges");
            DropTable("dbo.Dictionaries");
            DropTable("dbo.Cms_Privilege");
            DropTable("dbo.Cms_PageTitle");
            DropTable("dbo.Cms_PageType");
            DropTable("dbo.Cms_PagePicture");
            DropTable("dbo.Cms_PageContent");
            DropTable("dbo.Cms_Page");
            DropTable("dbo.Organizations");
            DropTable("dbo.Users");
            DropTable("dbo.Cms_PageAttribute");
            DropTable("dbo.Cms_Classify");
            DropTable("dbo.Cms_WebSite");
            DropTable("dbo.Cms_Channel");
            DropTable("dbo.Modules");
            DropTable("dbo.Menus");
            DropTable("dbo.Buttons");
        }
    }
}
