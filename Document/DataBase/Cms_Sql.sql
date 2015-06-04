create table Cms_WebSite 
(
   ID                   int                            not null identity,
   CompanyName          varchar(300)                   null,
   CompanyAddress       varchar(300)                   null,
   CompanyType          varchar(300)                   null,
   Telephone            varchar(300)                   null,
   Email                varchar(300)                   null,
   HostAddress          varchar(300)                   null,
   SiteTitle            varchar(300)                   null,
   IndexPage            varchar(60)                    null,
   LayOutTemplateID     int                            null,
   constraint PK_CMS_WEBSITE primary key clustered (ID) 
);
create table Cms_Classify 
(
   ID                   int                            not null identity,
   Name                 varchar(200)                   null,
   Code                 varchar(200)                   null,
   Remark               varchar(200)                   null,
   ChannelID            int                            null,
   LayOutTemplateID     int                            null,
   constraint PK_CMS_CLASSIFY primary key clustered (ID)
);
create table Cms_Channel 
(
   ID                   int                            not null identity,
   ChannelName          varchar(100)                   null,
   ChannelCode          varchar(200)                   null,
   ChannelRemark        varchar(2000)                  null,
   WebSiteID            int                            null,
   LayOutTemplateID     int                            null,
   constraint PK_CMS_CHANNEL primary key clustered (ID)
);

create table Cms_PageTemplate 
(
   ID                   int                            not null identity,
   Path                 varchar(200)                   null,
   PageContent          text                           null,
   constraint PK_CMS_PAGETEMPLATE primary key clustered (ID)
);

create table Cms_Module 
(
   ID                   int                            not null identity,
   TagName              varchar(60)                    null,
   TagCode              varchar(60)                    null,
   TagRemark            varchar(200)                   null,
  constraint PK_CMS_Module primary key clustered (ID)
);

create table Cms_Privilege 
(
   ID                   int                            not null identity,
   Master               varchar(300)                   null,
   MasterValue          varchar(300)                   null,
   Access               varchar(300)                   null,
   AccessValue          varchar(300)                   null,
   Operation            varchar(300)                   null,
   constraint PK_CMS_PRIVILEGE primary key clustered (ID)
);
