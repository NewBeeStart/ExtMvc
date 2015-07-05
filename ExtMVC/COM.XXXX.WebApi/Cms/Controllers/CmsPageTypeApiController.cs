using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using COM.XXXX.EasyUIModels;
using COM.XXXX.Models.Admin;
using COM.XXXX.Models.CMS;
using Repository.DAL.Repository;
using Repository.DAL.Repository.Cms;

namespace COM.XXXX.WebApi.Cms.Controllers
{
    public class CmsPageTypeApiController : ApiControllerBase<CmsPageTypeRepository, Cms_PageType>
    {
        public CmsPageTypeApiController()
        {
            base.SetRepository();
        }

        /// <summary>
        /// 获取组织机构Tree
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<ExtTree> GetWebSiteTree()
        {
            var websiteapi=new WebSiteApiController();
            var cmschannel=new CmsChannelApiController();
            var cmsclassify=new CmsClassifyApiController();
            var cmspagetype=new CmsPageTypeApiController(); 
            var cmspage=new CmsPageApiController(); 
            List<ExtTree> treelst = new List<ExtTree>();

            var website = websiteapi.Repository.List();
            foreach (Cms_WebSite cmsWebSite in website)
            {
                ExtTree tree = new ExtTree()
                {
                    id = cmsWebSite.ID.ToString(),
                    text = cmsWebSite.SiteTitle,
                    iconCls="icon-1753",
                    attributes = new { type = "WebSite" }
                };
                var channels = cmschannel.Repository.Query(item => item.WebSiteID == cmsWebSite.ID).OrderBy(u=>u.SortIndex);
                tree.leaf = !channels.Any();
                foreach (Cms_Channel cmsChannel in channels)
                {
                    ExtTree cmsChanneltree = new ExtTree()
                    {
                        id = cmsChannel.ID.ToString(),
                        text = cmsChannel.ChannelName,
                        iconCls = "icon-1007",
                        attributes = new{type="CmsChannel"}
                    };
                    var classifies = cmsclassify.Repository.Query(item => item.ChannelID == cmsChannel.ID).OrderBy(u=>u.SortIndex);
                    cmsChanneltree.leaf = !classifies.Any();
                    foreach (Cms_Classify cmsClassify in classifies)
                    {
                        ExtTree classifytree = new ExtTree()
                            {
                                id = cmsClassify.ID.ToString(),
                                text = cmsClassify.Name,
                                iconCls = "icon-1056",
                                attributes = new { type = "CmsClassify" }
                            };
                        var pagetypes = cmspagetype.Repository.Query(item => item.ClassifyID == cmsClassify.ID).OrderBy(u=>u.TypeName);
                        classifytree.leaf = !pagetypes.Any();
                        foreach (Cms_PageType cmsPageType in pagetypes)
                        {
                           ExtTree pagetypetree=  new ExtTree()
                                {
                                    id = cmsPageType.ID.ToString(),
                                    text = cmsPageType.TypeName,
                                    iconCls = "icon-1186",
                                    attributes = new {type = "CmsPageType"}
                                };

                           var pages = cmspage.Repository.Query(item => item.PageTypeID == cmsPageType.ID).OrderBy(u=>u.PageName);
                            foreach (Cms_Page cmsPage in pages)
                            {
                                    pagetypetree.children.Add(new ExtTree()
                                    {
                                        id = cmsPage.ID.ToString(),
                                         text=cmsPage.PageName,
                                        iconCls = "icon-1203",
                                        attributes = new {type="CmsPage"},
                                        leaf=true
                                    });
                            }


                           classifytree.children.Add(pagetypetree);


                        }
                        cmsChanneltree.children.Add(classifytree);
                    }
                    tree.children.Add(cmsChanneltree);
                }
                treelst.Add(tree);
            }

            return treelst;
        }
    }
}
