/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2015/6/25 21:25:22                           */
/*==============================================================*/


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

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Privileges')
            and   type = 'U')
   drop table dbo.Privileges
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.__MigrationHistory')
            and   type = 'U')
   drop table dbo.__MigrationHistory
go

alter table dbo.Cms_Classify
   add PID uniqueidentifier null
go

if exists (select 1
from sysproperties
where TableID = object_id('dbo.Cms_Classify')
and ColName = 'PID' AND PropName='MS_Description')
begin
execute sp_dropextendedproperty 'MS_Description',
'user', 'dbo', 'table', 'Cms_Classify', 'column', 'PID'


end

execute sp_addextendedproperty 'MS_Description',
'父分类ID',
'user', 'dbo', 'table', 'Cms_Classify', 'column', 'PID'
go

if exists (select 1
from sysproperties
where TableID = object_id('dbo.Cms_Classify')
and ColName = 'SortIndex' AND PropName='MS_Description')
begin
execute sp_dropextendedproperty 'MS_Description',
'user', 'dbo', 'table', 'Cms_Classify', 'column', 'SortIndex'


end

execute sp_addextendedproperty 'MS_Description',
'排序',
'user', 'dbo', 'table', 'Cms_Classify', 'column', 'SortIndex'
go

if exists (select 1
from sysproperties
where TableID = object_id('dbo.Cms_Classify')
and ColName = 'PageContent' AND PropName='MS_Description')
begin
execute sp_dropextendedproperty 'MS_Description',
'user', 'dbo', 'table', 'Cms_Classify', 'column', 'PageContent'


end

execute sp_addextendedproperty 'MS_Description',
'页面内容',
'user', 'dbo', 'table', 'Cms_Classify', 'column', 'PageContent'
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
   PageTypeID           uniqueidentifier     null,
   constraint PK_CMS_PAGEATTRIBUTE primary key (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '页面属性',
   'user', @CurrentUser, 'table', 'Cms_PageAttribute'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'isTop' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isTop'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'是否顶置',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isTop'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'isPic' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isPic'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'是否幻灯片',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isPic'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'isHot' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isHot'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'是否热门',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isHot'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'isRecommend' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isRecommend'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'是否推荐',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isRecommend'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'isComment' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isComment'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'是否推荐',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'isComment'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'SortIndex' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'SortIndex'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'排序',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'SortIndex'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'ReadCount' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'ReadCount'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'浏览次数',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'ReadCount'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'CreateUserID' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'CreateUserID'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'创建人ID',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'CreateUserID'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'UpdateTime' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'UpdateTime'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'更新时间',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'UpdateTime'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'UpdateUserID' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'UpdateUserID'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'更新人ID',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'UpdateUserID'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageAttribute')
and ColName = 'PageTypeID' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'PageTypeID'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'页面类型ID',
'user', @CurrentUser, 'table', 'Cms_PageAttribute', 'column', 'PageTypeID'
go

/*==============================================================*/
/* Table: Cms_PageContent                                       */
/*==============================================================*/
create table Cms_PageContent (
   ID                   uniqueidentifier     not null,
   PageContent          text                 null,
   CreateTime           datetime             null,
   Remark               nvarchar(Max)        null,
   PageTypeID           uniqueidentifier     null,
   constraint PK_CMS_PAGECONTENT primary key (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '页面内容',
   'user', @CurrentUser, 'table', 'Cms_PageContent'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageContent')
and ColName = 'ID' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'ID'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'标识列',
'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'ID'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageContent')
and ColName = 'PageContent' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'PageContent'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'页面内容',
'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'PageContent'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageContent')
and ColName = 'CreateTime' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'CreateTime'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'创建时间',
'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'CreateTime'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageContent')
and ColName = 'Remark' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'Remark'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'备注信息',
'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'Remark'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageContent')
and ColName = 'PageTypeID' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'PageTypeID'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'页面类型ID',
'user', @CurrentUser, 'table', 'Cms_PageContent', 'column', 'PageTypeID'
go

/*==============================================================*/
/* Table: Cms_PageTitle                                         */
/*==============================================================*/
create table Cms_PageTitle (
   ID                   uniqueidentifier     not null,
   TitleContent         nvarchar(max)        null,
   Remark               nvarchar(Max)        null,
   PageTypeID           uniqueidentifier     null,
   constraint PK_CMS_PAGETITLE primary key (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '页面标题',
   'user', @CurrentUser, 'table', 'Cms_PageTitle'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageTitle')
and ColName = 'ID' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'ID'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'标识列',
'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'ID'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageTitle')
and ColName = 'TitleContent' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'TitleContent'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'标题内容',
'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'TitleContent'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageTitle')
and ColName = 'Remark' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'Remark'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'备注信息',
'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'Remark'
go

if exists (select 1
from sysproperties
where TableID = object_id('Cms_PageTitle')
and ColName = 'PageTypeID' AND PropName='MS_Description')
begin
declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'PageTypeID'


end

select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
'创建类型ID',
'user', @CurrentUser, 'table', 'Cms_PageTitle', 'column', 'PageTypeID'
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
   hasForm              bit                  null,
   constraint "PK_dbo.Cms_PageTemplate" primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

if exists (select 1
from sysproperties
where TableID = object_id('dbo.Cms_PageType')
and ColName = 'ID' AND PropName='MS_Description')
begin
execute sp_dropextendedproperty 'MS_Description',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'ID'


end

execute sp_addextendedproperty 'MS_Description',
'标识列',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'ID'
go

if exists (select 1
from sysproperties
where TableID = object_id('dbo.Cms_PageType')
and ColName = 'Remark' AND PropName='MS_Description')
begin
execute sp_dropextendedproperty 'MS_Description',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'Remark'


end

execute sp_addextendedproperty 'MS_Description',
'备注信息',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'Remark'
go

if exists (select 1
from sysproperties
where TableID = object_id('dbo.Cms_PageType')
and ColName = 'hasTitle' AND PropName='MS_Description')
begin
execute sp_dropextendedproperty 'MS_Description',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasTitle'


end

execute sp_addextendedproperty 'MS_Description',
'是否有标题',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasTitle'
go

if exists (select 1
from sysproperties
where TableID = object_id('dbo.Cms_PageType')
and ColName = 'hasArticle' AND PropName='MS_Description')
begin
execute sp_dropextendedproperty 'MS_Description',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasArticle'


end

execute sp_addextendedproperty 'MS_Description',
'是否有文章',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasArticle'
go

if exists (select 1
from sysproperties
where TableID = object_id('dbo.Cms_PageType')
and ColName = 'hasPictrue' AND PropName='MS_Description')
begin
execute sp_dropextendedproperty 'MS_Description',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasPictrue'


end

execute sp_addextendedproperty 'MS_Description',
'是否有图片',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasPictrue'
go

if exists (select 1
from sysproperties
where TableID = object_id('dbo.Cms_PageType')
and ColName = 'hasForm' AND PropName='MS_Description')
begin
execute sp_dropextendedproperty 'MS_Description',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasForm'


end

execute sp_addextendedproperty 'MS_Description',
'是否有表单',
'user', 'dbo', 'table', 'Cms_PageType', 'column', 'hasForm'
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

