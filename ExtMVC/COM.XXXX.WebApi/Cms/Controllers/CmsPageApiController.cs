using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using COM.XXXX.Models.CMS;
using Newtonsoft.Json.Linq;
using Repository.DAL.Repository;

namespace COM.XXXX.WebApi.Cms.Controllers
{
    public class CmsPageApiController : ApiControllerBase<CmsPageRepository, Cms_Page>
    {
        public CmsPageApiController()
        {
            base.SetRepository();
        }

        public override HttpResponseMessage GetGridPager(JObject p)
        {

            if (p != null)
            {
                int startrecord, recordcount;
                if (int.TryParse(p["start"].ToString(), out startrecord) && int.TryParse(p["limit"].ToString(), out recordcount))
                {
                    int count = 0;

                    string PageTypeID = p["PageTypeID"].ToString();

                    var result =
                        base.Repository.QueryByPage<string>(u => u.PageTypeID.ToString() == PageTypeID,
                                                            u => u.PageName, recordcount, startrecord, out count)
                            .ToList();
                    //var result = base.Repository.Query(u => u.PageTypeID.ToString() == p["PageTypeID"].ToString()).Skip<Cms_Page>(startrecord).Take<Cms_Page>(recordcount);
                    return toJson(new { data = result, count = count });

                }
            }
            else
            {
                return toJson(new { data = Repository.List(), count = Repository.List().Count() });
            }

            return toJson(new { data = "[]", count = 0 });
        }


        #region 前台调用
        [HttpPost]
        public HttpResponseMessage GetPages(Guid? webid, string code)
        {
            Cms_WebSite website = new WebSiteApiController().Repository.Query(item => item.ID == webid).First();

            Cms_Channel channels = new CmsChannelApiController().Repository.Query(u => u.ChannelCode == code && u.WebSiteID == webid).First();

            Cms_Classify cmsClassifies = new CmsClassifyApiController().Repository.Query(u => u.Code == code && u.ChannelID == channels.ID).First();

            Cms_PageType cmsPageTypes = new CmsPageTypeApiController().Repository.Query(u => u.TypeCode == code && u.ClassifyID == cmsClassifies.ID).First();

            List<Cms_Page> pages =
                new CmsPageApiController().Repository.Query(u => u.PageCode == code && u.PageTypeID == cmsPageTypes.ID)
                    .ToList();


            List<object> result = new List<object>();
            foreach (var page in pages)
            {
                var title = new CmsPageTitleApiController().Repository.Query(item => item.PageID == page.ID).ToList();
                var attribute =
                    new CmsPageAttributeApiController().Repository.Query(item => item.PageID == page.ID).ToList();
                var content =
                    new CmsPageContentApiController().Repository.Query(item => item.PageID == page.ID).ToList();
                
                var picture =
                    new CmsPagePictureApiController().Repository.Query(item => item.PageID == page.ID).ToList();
                result.Add(new
                {
                    page = page,
                    title = title,
                    attribute = attribute,
                    content = content,
                    picture = picture
                });
            }
            return toJson( new
            {
                channel = channels,
                data= result
            });

            //var channel = (from item in website.Channels
            //    where item.ChannelCode == code
            //    select item).First();
            //var classifies = (from item in channel.Classifies
            //    where item.Code == code
            //    select item).First();
            //var pagetypes = (from item in classifies.PageTypes
            //    where item.TypeCode == code
            //    select item).First(); 
            //var pages = from item in pagetypes.Pages
            //    where item.PageCode == code
            //    select item;
            //List<object> result=new List<object>();
            //foreach (var page in pages)
            //{
            //     result.Add(new {page=page,title=page.Title,attribute=page.Attributes,content=page.PageContent,picture=page.Picture});
            //}
            //toJson(result);



            //List<Cms_Classify> cmsClassifies =
            //    new CmsClassifyApiController().Repository.Query(u => u.Code == code).ToList();

            //List<Cms_PageType> cmsPageTypes = new CmsPageTypeApiController().Repository.Query(u => u.TypeCode == code).ToList();

            //List<Cms_Page> cmsPages = new CmsPageApiController().Repository.Query(u => u.PageCode == code).ToList();

           

        }

        #endregion
    }
}
