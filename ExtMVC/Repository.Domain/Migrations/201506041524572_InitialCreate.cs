namespace Repository.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
          
            
            CreateTable(
                "dbo.Cms_Channel",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        ChannelName = c.String(),
                        ChannelCode = c.String(),
                        ChannelRemark = c.String(),
                        WebSiteID = c.Guid(),
                        LayOutTemplateID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cms_PageTemplate", t => t.LayOutTemplateID)
                .ForeignKey("dbo.Cms_WebSite", t => t.WebSiteID)
                .Index(t => t.WebSiteID)
                .Index(t => t.LayOutTemplateID);
            
            CreateTable(
                "dbo.Cms_PageTemplate",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Path = c.String(),
                        PageContent = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        LayOutTemplateID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cms_PageTemplate", t => t.LayOutTemplateID)
                .Index(t => t.LayOutTemplateID);
            
            CreateTable(
                "dbo.Cms_Classify",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Remark = c.String(),
                        ChannelID = c.Guid(),
                        LayOutTemplateID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cms_Channel", t => t.ChannelID)
                .ForeignKey("dbo.Cms_PageTemplate", t => t.LayOutTemplateID)
                .Index(t => t.ChannelID)
                .Index(t => t.LayOutTemplateID);
            
            CreateTable(
                "dbo.Cms_Module",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        TagName = c.String(),
                        TagCode = c.String(),
                        TagRemark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
           
            
        }
        
        public override void Down()
        {
          
            DropForeignKey("dbo.Cms_Classify", "LayOutTemplateID", "dbo.Cms_PageTemplate");
            DropForeignKey("dbo.Cms_Classify", "ChannelID", "dbo.Cms_Channel");
            DropForeignKey("dbo.Cms_Channel", "WebSiteID", "dbo.Cms_WebSite");
            DropForeignKey("dbo.Cms_WebSite", "LayOutTemplateID", "dbo.Cms_PageTemplate");
            DropForeignKey("dbo.Cms_Channel", "LayOutTemplateID", "dbo.Cms_PageTemplate");
         
            DropIndex("dbo.Cms_Classify", new[] { "LayOutTemplateID" });
            DropIndex("dbo.Cms_Classify", new[] { "ChannelID" });
            DropIndex("dbo.Cms_WebSite", new[] { "LayOutTemplateID" });
            DropIndex("dbo.Cms_Channel", new[] { "LayOutTemplateID" });
            DropIndex("dbo.Cms_Channel", new[] { "WebSiteID" });
          
            DropTable("dbo.Cms_Privilege");
            DropTable("dbo.Cms_Module");
            DropTable("dbo.Cms_Classify");
            DropTable("dbo.Cms_WebSite");
            DropTable("dbo.Cms_PageTemplate");
            DropTable("dbo.Cms_Channel");
           
        }
    }
}
