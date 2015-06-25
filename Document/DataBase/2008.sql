/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2015/6/25 13:16:35                           */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Buttons') and o.name = 'FK_dbo.Buttons_dbo.Menus_MenuID')
alter table dbo.Buttons
   drop constraint "FK_dbo.Buttons_dbo.Menus_MenuID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Cms_Channel') and o.name = 'FK_dbo.Cms_Channel_dbo.Cms_PageTemplate_LayOutTemplateID')
alter table dbo.Cms_Channel
   drop constraint "FK_dbo.Cms_Channel_dbo.Cms_PageTemplate_LayOutTemplateID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Cms_Channel') and o.name = 'FK_dbo.Cms_Channel_dbo.Cms_WebSite_WebSiteID')
alter table dbo.Cms_Channel
   drop constraint "FK_dbo.Cms_Channel_dbo.Cms_WebSite_WebSiteID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Cms_Classify') and o.name = 'FK_dbo.Cms_Classify_dbo.Cms_Channel_ChannelID')
alter table dbo.Cms_Classify
   drop constraint "FK_dbo.Cms_Classify_dbo.Cms_Channel_ChannelID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Cms_Classify') and o.name = 'FK_dbo.Cms_Classify_dbo.Cms_PageTemplate_LayOutTemplateID')
alter table dbo.Cms_Classify
   drop constraint "FK_dbo.Cms_Classify_dbo.Cms_PageTemplate_LayOutTemplateID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Dictionaries') and o.name = 'FK_dbo.Dictionaries_dbo.Dictionaries_PDictionaryID')
alter table dbo.Dictionaries
   drop constraint "FK_dbo.Dictionaries_dbo.Dictionaries_PDictionaryID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Menus') and o.name = 'FK_dbo.Menus_dbo.Menus_PMenuID')
alter table dbo.Menus
   drop constraint "FK_dbo.Menus_dbo.Menus_PMenuID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Menus') and o.name = 'FK_dbo.Menus_dbo.Modules_ModuleID')
alter table dbo.Menus
   drop constraint "FK_dbo.Menus_dbo.Modules_ModuleID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Organizations') and o.name = 'FK_dbo.Organizations_dbo.Organizations_POrganizationID')
alter table dbo.Organizations
   drop constraint "FK_dbo.Organizations_dbo.Organizations_POrganizationID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.RoleRights') and o.name = 'FK_dbo.RoleRights_dbo.Roles_RoleID')
alter table dbo.RoleRights
   drop constraint "FK_dbo.RoleRights_dbo.Roles_RoleID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.RoleRights') and o.name = 'FK_dbo.RoleRights_dbo.Users_UserID')
alter table dbo.RoleRights
   drop constraint "FK_dbo.RoleRights_dbo.Users_UserID"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Users') and o.name = 'FK_dbo.Users_dbo.Organizations_OrganizationID')
alter table dbo.Users
   drop constraint "FK_dbo.Users_dbo.Organizations_OrganizationID"
go

alter table dbo.Buttons
   drop constraint "PK_dbo.Buttons"
go

alter table dbo.Buttons 
   drop constraint DF__Buttons__ID__2CA8951C
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Buttons')
            and   type = 'U')
   drop table dbo.tmp_Buttons
go

execute sp_rename Buttons, tmp_Buttons
go

alter table dbo.Cms_Channel
   drop constraint "PK_dbo.Cms_Channel"
go

alter table dbo.Cms_Channel 
   drop constraint DF__Cms_Channel__ID__0B129727
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Cms_Channel')
            and   type = 'U')
   drop table dbo.tmp_Cms_Channel
go

execute sp_rename Cms_Channel, tmp_Cms_Channel
go

alter table dbo.Cms_Classify
   drop constraint "PK_dbo.Cms_Classify"
go

alter table dbo.Cms_Classify 
   drop constraint DF__Cms_Classify__ID__13A7DD28
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Cms_Classify')
            and   type = 'U')
   drop table dbo.tmp_Cms_Classify
go

execute sp_rename Cms_Classify, tmp_Cms_Classify
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Cms_Module')
            and   type = 'U')
   drop table dbo.Cms_Module
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Cms_PageTemplate')
            and   type = 'U')
   drop table dbo.Cms_PageTemplate
go

alter table dbo.Cms_Privilege
   drop constraint "PK_dbo.Cms_Privilege"
go

alter table dbo.Cms_Privilege 
   drop constraint DF__Cms_Privileg__ID__1960B67E
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Cms_Privilege')
            and   type = 'U')
   drop table dbo.tmp_Cms_Privilege
go

execute sp_rename Cms_Privilege, tmp_Cms_Privilege
go

alter table dbo.Cms_WebSite
   drop constraint "PK_dbo.Cms_WebSite"
go

alter table dbo.Cms_WebSite 
   drop constraint DF__Cms_WebSite__ID__10CB707D
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Cms_WebSite')
            and   type = 'U')
   drop table dbo.tmp_Cms_WebSite
go

execute sp_rename Cms_WebSite, tmp_Cms_WebSite
go

alter table dbo.Dictionaries
   drop constraint "PK_dbo.Dictionaries"
go

alter table dbo.Dictionaries 
   drop constraint DF__Dictionaries__ID__353DDB1D
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Dictionaries')
            and   type = 'U')
   drop table dbo.tmp_Dictionaries
go

execute sp_rename Dictionaries, tmp_Dictionaries
go

alter table dbo.Menus
   drop constraint "PK_dbo.Menus"
go

alter table dbo.Menus 
   drop constraint DF__Menus__ID__2F8501C7
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Menus')
            and   type = 'U')
   drop table dbo.tmp_Menus
go

execute sp_rename Menus, tmp_Menus
go

alter table dbo.Modules
   drop constraint "PK_dbo.Modules"
go

alter table dbo.Modules 
   drop constraint DF__Modules__ID__32616E72
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Modules')
            and   type = 'U')
   drop table dbo.tmp_Modules
go

execute sp_rename Modules, tmp_Modules
go

alter table dbo.Organizations
   drop constraint "PK_dbo.Organizations"
go

alter table dbo.Organizations 
   drop constraint DF__Organization__ID__381A47C8
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Organizations')
            and   type = 'U')
   drop table dbo.tmp_Organizations
go

execute sp_rename Organizations, tmp_Organizations
go

alter table dbo.Privileges
   drop constraint "PK_dbo.Privileges"
go

alter table dbo.Privileges 
   drop constraint DF__Privileges__ID__3AF6B473
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Privileges')
            and   type = 'U')
   drop table dbo.tmp_Privileges
go

execute sp_rename Privileges, tmp_Privileges
go

alter table dbo.RoleRights
   drop constraint "PK_dbo.RoleRights"
go

alter table dbo.RoleRights 
   drop constraint DF__RoleRights__ID__3DD3211E
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_RoleRights')
            and   type = 'U')
   drop table dbo.tmp_RoleRights
go

execute sp_rename RoleRights, tmp_RoleRights
go

alter table dbo.Roles
   drop constraint "PK_dbo.Roles"
go

alter table dbo.Roles 
   drop constraint DF__Roles__ID__40AF8DC9
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Roles')
            and   type = 'U')
   drop table dbo.tmp_Roles
go

execute sp_rename Roles, tmp_Roles
go

alter table dbo.Users
   drop constraint "PK_dbo.Users"
go

alter table dbo.Users 
   drop constraint DF__Users__ID__438BFA74
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_Users')
            and   type = 'U')
   drop table dbo.tmp_Users
go

execute sp_rename Users, tmp_Users
go

alter table dbo.__MigrationHistory
   drop constraint "PK_dbo.__MigrationHistory"
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp___MigrationHistory')
            and   type = 'U')
   drop table dbo.tmp___MigrationHistory
go

execute sp_rename __MigrationHistory, tmp___MigrationHistory
go

/*==============================================================*/
/* Table: Buttons                                               */
/*==============================================================*/
create table dbo.Buttons (
   ID                   uniqueidentifier     not null constraint DF__Buttons__ID__2CA8951C default newsequentialid(),
   BtnName              nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   BtnNo                nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   BtnClass             nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   BtnIcon              nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   BtnStatus            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   MenuID               uniqueidentifier     null
)
on "PRIMARY"
go

insert into dbo.Buttons (ID, BtnName, BtnNo, BtnClass, BtnIcon, BtnStatus, MenuID)
select ID, BtnName, BtnNo, BtnClass, BtnIcon, BtnStatus, MenuID
from dbo.tmp_Buttons
go

alter table dbo.Buttons
   add constraint "PK_dbo.Buttons" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_MenuID                                             */
/*==============================================================*/
create index IX_MenuID on dbo.Buttons (
MenuID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: Cms_Channel                                           */
/*==============================================================*/
create table dbo.Cms_Channel (
   ID                   uniqueidentifier     not null constraint DF__Cms_Channel__ID__0B129727 default newsequentialid(),
   ChannelName          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ChannelCode          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ChannelRemark        nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   WebSiteID            uniqueidentifier     null,
   SortIndex            int                  null,
   PageContent          text                 collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', 'dbo', 'table', 'Cms_Channel', 'column', 'ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '频道名称',
   'user', 'dbo', 'table', 'Cms_Channel', 'column', 'ChannelName'
go

execute sp_addextendedproperty 'MS_Description', 
   '频道编号',
   'user', 'dbo', 'table', 'Cms_Channel', 'column', 'ChannelCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '频道备注',
   'user', 'dbo', 'table', 'Cms_Channel', 'column', 'ChannelRemark'
go

execute sp_addextendedproperty 'MS_Description', 
   '网站ID',
   'user', 'dbo', 'table', 'Cms_Channel', 'column', 'WebSiteID'
go

insert into dbo.Cms_Channel (ID, ChannelName, ChannelCode, ChannelRemark, WebSiteID, SortIndex)
select ID, ChannelName, ChannelCode, ChannelRemark, WebSiteID, SortIndex
from dbo.tmp_Cms_Channel
go

alter table dbo.Cms_Channel
   add constraint "PK_dbo.Cms_Channel" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_WebSiteID                                          */
/*==============================================================*/
create index IX_WebSiteID on dbo.Cms_Channel (
WebSiteID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: Cms_Classify                                          */
/*==============================================================*/
create table dbo.Cms_Classify (
   ID                   uniqueidentifier     not null constraint DF__Cms_Classify__ID__13A7DD28 default newsequentialid(),
   Name                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Code                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Remark               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ChannelID            uniqueidentifier     null,
   SortIndex            int                  null,
   PageListContent      text                 collate Chinese_PRC_CI_AS null,
   PageDetailContent    text                 null
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', 'dbo', 'table', 'Cms_Classify', 'column', 'ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '分类名称',
   'user', 'dbo', 'table', 'Cms_Classify', 'column', 'Name'
go

execute sp_addextendedproperty 'MS_Description', 
   '分类编号',
   'user', 'dbo', 'table', 'Cms_Classify', 'column', 'Code'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'Cms_Classify', 'column', 'Remark'
go

execute sp_addextendedproperty 'MS_Description', 
   '频道ID',
   'user', 'dbo', 'table', 'Cms_Classify', 'column', 'ChannelID'
go

execute sp_addextendedproperty 'MS_Description', 
   '排序',
   'user', 'dbo', 'table', 'Cms_Classify', 'column', 'SortIndex'
go

execute sp_addextendedproperty 'MS_Description', 
   '列表页模板',
   'user', 'dbo', 'table', 'Cms_Classify', 'column', 'PageListContent'
go

execute sp_addextendedproperty 'MS_Description', 
   '详细页模板',
   'user', 'dbo', 'table', 'Cms_Classify', 'column', 'PageDetailContent'
go

insert into dbo.Cms_Classify (ID, Name, Code, Remark, ChannelID, SortIndex)
select ID, Name, Code, Remark, ChannelID, SortIndex
from dbo.tmp_Cms_Classify
go

alter table dbo.Cms_Classify
   add constraint "PK_dbo.Cms_Classify" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ChannelID                                          */
/*==============================================================*/
create index IX_ChannelID on dbo.Cms_Classify (
ChannelID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: Cms_PageAttribute                                     */
/*==============================================================*/
create table Cms_PageAttribute (
   ID                   uniqueidentifier     not null,
   isTop                bit                  null,
   isPic                bit                  null,
   isHot                bit                  null,
   isRecommend          bit                  null,
   isComment            bit                  null,
   SortIndex            int                  null,
   ReadCount            int                  null,
   CreateUserID         uniqueidentifier     null,
   UpdateTime           datetime             null,
   UpdateUserID         uniqueidentifier     null,
   PageTypeID           uniqueidentifier     null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '页面属性',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否顶置',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isTop'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否幻灯片',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isPic'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否热门',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isHot'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否推荐',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isRecommend'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否推荐',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isComment'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'SortIndex'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '浏览次数',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'ReadCount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'CreateUserID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'UpdateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新人ID',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'UpdateUserID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '页面类型ID',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'PageTypeID'
go

alter table Cms_PageAttribute
   add constraint PK_CMS_PAGEATTRIBUTE primary key (ID)
go

/*==============================================================*/
/* Table: Cms_PageContent                                       */
/*==============================================================*/
create table Cms_PageContent (
   ID                   uniqueidentifier     not null,
   PageContent          text                 null,
   CreateTime           datetime             null,
   Remark               nvarchar(Max)        null,
   PageTypeID           uniqueidentifier     null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '页面内容',
   'user', @CurrentUser, 'table', 'Cms_PageContent'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识列',
   'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '页面内容',
   'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'PageContent'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注信息',
   'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'Remark'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '页面类型ID',
   'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'PageTypeID'
go

alter table Cms_PageContent
   add constraint PK_CMS_PAGECONTENT primary key (ID)
go

/*==============================================================*/
/* Table: Cms_PageTitle                                         */
/*==============================================================*/
create table Cms_PageTitle (
   ID                   uniqueidentifier     not null,
   TitleContent         nvarchar(max)        null,
   Remark               nvarchar(Max)        null,
   PageTypeID           uniqueidentifier     null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '页面标题',
   'user', @CurrentUser, 'table', 'Cms_PageTitle'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识列',
   'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标题内容',
   'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'TitleContent'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注信息',
   'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'Remark'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建类型ID',
   'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'PageTypeID'
go

alter table Cms_PageTitle
   add constraint PK_CMS_PAGETITLE primary key (ID)
go

/*==============================================================*/
/* Table: Cms_PageType                                          */
/*==============================================================*/
create table dbo.Cms_PageType (
   ID                   uniqueidentifier     not null constraint DF__Cms_PageTemp__ID__0DEF03D2 default newsequentialid(),
   ClassifyID           uniqueidentifier     null,
   Remark               varchar(max)         collate Chinese_PRC_CI_AS null,
   hasTitle             bit                  null,
   hasArticle           bit                  null,
   hasPictrue           bit                  null,
   hasForm              bit                  null
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '标识列',
   'user', 'dbo', 'table', 'Cms_PageType', 'column', 'ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注信息',
   'user', 'dbo', 'table', 'Cms_PageType', 'column', 'Remark'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否有标题',
   'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasTitle'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否有文章',
   'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasArticle'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否有图片',
   'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasPictrue'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否有表单',
   'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasForm'
go

alter table dbo.Cms_PageType
   add constraint "PK_dbo.Cms_PageTemplate" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Cms_Privilege                                         */
/*==============================================================*/
create table dbo.Cms_Privilege (
   ID                   uniqueidentifier     not null constraint DF__Cms_Privileg__ID__1960B67E default newsequentialid(),
   Master               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   MasterValue          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Access               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   AccessValue          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Operation            nvarchar(Max)        collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

insert into dbo.Cms_Privilege (ID, Master, MasterValue, Access, AccessValue, Operation)
select ID, Master, MasterValue, Access, AccessValue, Operation
from dbo.tmp_Cms_Privilege
go

alter table dbo.Cms_Privilege
   add constraint "PK_dbo.Cms_Privilege" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Cms_WebSite                                           */
/*==============================================================*/
create table dbo.Cms_WebSite (
   ID                   uniqueidentifier     not null constraint DF__Cms_WebSite__ID__10CB707D default newsequentialid(),
   CompanyName          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   CompanyAddress       nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   CompanyType          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Telephone            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Email                nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   HostAddress          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   SiteTitle            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   IndexPage            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   PageContent          text                 collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

insert into dbo.Cms_WebSite (ID, CompanyName, CompanyAddress, CompanyType, Telephone, Email, HostAddress, SiteTitle, IndexPage, PageContent)
select ID, CompanyName, CompanyAddress, CompanyType, Telephone, Email, HostAddress, SiteTitle, IndexPage, PageContent
from dbo.tmp_Cms_WebSite
go

alter table dbo.Cms_WebSite
   add constraint "PK_dbo.Cms_WebSite" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Dictionaries                                          */
/*==============================================================*/
create table dbo.Dictionaries (
   ID                   uniqueidentifier     not null constraint DF__Dictionaries__ID__353DDB1D default newsequentialid(),
   PDictionaryID        uniqueidentifier     null,
   Title                nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Code                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Value                nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Sort                 int                  not null,
   "Desc"               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   iconCls              nvarchar(Max)        collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

insert into dbo.Dictionaries (ID, PDictionaryID, Title, Code, Value, Sort, "Desc", iconCls)
select ID, PDictionaryID, Title, Code, Value, Sort, "Desc", iconCls
from dbo.tmp_Dictionaries
go

alter table dbo.Dictionaries
   add constraint "PK_dbo.Dictionaries" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_PDictionaryID                                      */
/*==============================================================*/
create index IX_PDictionaryID on dbo.Dictionaries (
PDictionaryID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: Menus                                                 */
/*==============================================================*/
create table dbo.Menus (
   ID                   uniqueidentifier     not null constraint DF__Menus__ID__2F8501C7 default newsequentialid(),
   DisplayName          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Width                int                  null,
   Height               int                  null,
   SortKey              int                  null,
   Controller           nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Action               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   iconCls              nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   IsLeaf               bit                  not null,
   OpenModel            int                  null,
   PMenuID              uniqueidentifier     null,
   ModuleID             uniqueidentifier     null,
   "Desc"               nvarchar(Max)        collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

insert into dbo.Menus (ID, DisplayName, Width, Height, SortKey, Controller, Action, iconCls, IsLeaf, OpenModel, PMenuID, ModuleID, "Desc")
select ID, DisplayName, Width, Height, SortKey, Controller, Action, iconCls, IsLeaf, OpenModel, PMenuID, ModuleID, "Desc"
from dbo.tmp_Menus
go

alter table dbo.Menus
   add constraint "PK_dbo.Menus" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_ModuleID                                           */
/*==============================================================*/
create index IX_ModuleID on dbo.Menus (
ModuleID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_PMenuID                                            */
/*==============================================================*/
create index IX_PMenuID on dbo.Menus (
PMenuID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: Modules                                               */
/*==============================================================*/
create table dbo.Modules (
   ID                   uniqueidentifier     not null constraint DF__Modules__ID__32616E72 default newsequentialid(),
   Code                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Name                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   "Desc"               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   iconCls              nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Sort                 int                  not null
)
on "PRIMARY"
go

insert into dbo.Modules (ID, Code, Name, "Desc", iconCls, Sort)
select ID, Code, Name, "Desc", iconCls, Sort
from dbo.tmp_Modules
go

alter table dbo.Modules
   add constraint "PK_dbo.Modules" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Organizations                                         */
/*==============================================================*/
create table dbo.Organizations (
   ID                   uniqueidentifier     not null constraint DF__Organization__ID__381A47C8 default newsequentialid(),
   Name                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   POrganizationID      uniqueidentifier     null,
   OrgType              nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Sort                 int                  not null,
   ChargeLeaderIDs      nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ChargeLeaderNames    nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   LeaderIDs            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   LeaderNames          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Remark               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   InUse                bit                  not null,
   iconCls              nvarchar(Max)        collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

insert into dbo.Organizations (ID, Name, POrganizationID, OrgType, Sort, ChargeLeaderIDs, ChargeLeaderNames, LeaderIDs, LeaderNames, Remark, InUse, iconCls)
select ID, Name, POrganizationID, OrgType, Sort, ChargeLeaderIDs, ChargeLeaderNames, LeaderIDs, LeaderNames, Remark, InUse, iconCls
from dbo.tmp_Organizations
go

alter table dbo.Organizations
   add constraint "PK_dbo.Organizations" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_POrganizationID                                    */
/*==============================================================*/
create index IX_POrganizationID on dbo.Organizations (
POrganizationID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: Privileges                                            */
/*==============================================================*/
create table dbo.Privileges (
   ID                   uniqueidentifier     not null constraint DF__Privileges__ID__3AF6B473 default newsequentialid(),
   Master               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   MasterValue          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Access               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   AccessValue          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Operation            nvarchar(Max)        collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

insert into dbo.Privileges (ID, Master, MasterValue, Access, AccessValue, Operation)
select ID, Master, MasterValue, Access, AccessValue, Operation
from dbo.tmp_Privileges
go

alter table dbo.Privileges
   add constraint "PK_dbo.Privileges" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: RoleRights                                            */
/*==============================================================*/
create table dbo.RoleRights (
   ID                   uniqueidentifier     not null constraint DF__RoleRights__ID__3DD3211E default newsequentialid(),
   RoleID               uniqueidentifier     null,
   UserID               uniqueidentifier     null
)
on "PRIMARY"
go

insert into dbo.RoleRights (ID, RoleID, UserID)
select ID, RoleID, UserID
from dbo.tmp_RoleRights
go

alter table dbo.RoleRights
   add constraint "PK_dbo.RoleRights" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_RoleID                                             */
/*==============================================================*/
create index IX_RoleID on dbo.RoleRights (
RoleID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_UserID                                             */
/*==============================================================*/
create index IX_UserID on dbo.RoleRights (
UserID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: Roles                                                 */
/*==============================================================*/
create table dbo.Roles (
   ID                   uniqueidentifier     not null constraint DF__Roles__ID__40AF8DC9 default newsequentialid(),
   Name                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   "Desc"               nvarchar(Max)        collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

insert into dbo.Roles (ID, Name, "Desc")
select ID, Name, "Desc"
from dbo.tmp_Roles
go

alter table dbo.Roles
   add constraint "PK_dbo.Roles" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Table: Users                                                 */
/*==============================================================*/
create table dbo.Users (
   ID                   uniqueidentifier     not null constraint DF__Users__ID__438BFA74 default newsequentialid(),
   RealName             nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   UserName             nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   PassWord             nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   OfferTime            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Education            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Professional         nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   IsMarry              bit                  not null,
   BirthDay             nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   InUse                bit                  not null,
   Remark               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   OrganizationID       uniqueidentifier     null
)
on "PRIMARY"
go

insert into dbo.Users (ID, RealName, UserName, PassWord, OfferTime, Education, Professional, IsMarry, BirthDay, InUse, Remark, OrganizationID)
select ID, RealName, UserName, PassWord, OfferTime, Education, Professional, IsMarry, BirthDay, InUse, Remark, OrganizationID
from dbo.tmp_Users
go

alter table dbo.Users
   add constraint "PK_dbo.Users" primary key (ID)
      on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_OrganizationID                                     */
/*==============================================================*/
create index IX_OrganizationID on dbo.Users (
OrganizationID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: __MigrationHistory                                    */
/*==============================================================*/
create table dbo.__MigrationHistory (
   MigrationId          nvarchar(150)        collate Chinese_PRC_CI_AS not null,
   ContextKey           nvarchar(300)        collate Chinese_PRC_CI_AS not null,
   Model                varbinary(Max)       not null,
   ProductVersion       nvarchar(32)         collate Chinese_PRC_CI_AS not null
)
on "PRIMARY"
go

insert into dbo.__MigrationHistory (MigrationId, ContextKey, Model, ProductVersion)
select MigrationId, ContextKey, Model, ProductVersion
from dbo.tmp___MigrationHistory
go

alter table dbo.__MigrationHistory
   add constraint "PK_dbo.__MigrationHistory" primary key (MigrationId, ContextKey)
      on "PRIMARY"
go

alter table dbo.Buttons
   add constraint "FK_dbo.Buttons_dbo.Menus_MenuID" foreign key (MenuID)
      references dbo.Menus (ID)
go

alter table dbo.Cms_Channel
   add constraint "FK_dbo.Cms_Channel_dbo.Cms_WebSite_WebSiteID" foreign key (WebSiteID)
      references dbo.Cms_WebSite (ID)
go

alter table dbo.Cms_Classify
   add constraint "FK_dbo.Cms_Classify_dbo.Cms_Channel_ChannelID" foreign key (ChannelID)
      references dbo.Cms_Channel (ID)
go

alter table Cms_PageAttribute
   add constraint FK_CMS_PAGE_REFERENCE_CMS_PAGE2 foreign key (PageTypeID)
      references dbo.Cms_PageType (ID)
go

alter table Cms_PageContent
   add constraint FK_CMS_PAGE_REFERENCE_CMS_PAGE3 foreign key (PageTypeID)
      references dbo.Cms_PageType (ID)
go

alter table Cms_PageTitle
   add constraint FK_CMS_PAGE_REFERENCE_CMS_PAGE1 foreign key (PageTypeID)
      references dbo.Cms_PageType (ID)
go

alter table dbo.Cms_PageType
   add constraint FK_CMS_PAGE_REFERENCE_CMS_CLAS foreign key (ClassifyID)
      references dbo.Cms_Classify (ID)
go

alter table dbo.Dictionaries
   add constraint "FK_dbo.Dictionaries_dbo.Dictionaries_PDictionaryID" foreign key (PDictionaryID)
      references dbo.Dictionaries (ID)
go

alter table dbo.Menus
   add constraint "FK_dbo.Menus_dbo.Menus_PMenuID" foreign key (PMenuID)
      references dbo.Menus (ID)
go

alter table dbo.Menus
   add constraint "FK_dbo.Menus_dbo.Modules_ModuleID" foreign key (ModuleID)
      references dbo.Modules (ID)
go

alter table dbo.Organizations
   add constraint "FK_dbo.Organizations_dbo.Organizations_POrganizationID" foreign key (POrganizationID)
      references dbo.Organizations (ID)
go

alter table dbo.RoleRights
   add constraint "FK_dbo.RoleRights_dbo.Roles_RoleID" foreign key (RoleID)
      references dbo.Roles (ID)
go

alter table dbo.RoleRights
   add constraint "FK_dbo.RoleRights_dbo.Users_UserID" foreign key (UserID)
      references dbo.Users (ID)
go

alter table dbo.Users
   add constraint "FK_dbo.Users_dbo.Organizations_OrganizationID" foreign key (OrganizationID)
      references dbo.Organizations (ID)
go

