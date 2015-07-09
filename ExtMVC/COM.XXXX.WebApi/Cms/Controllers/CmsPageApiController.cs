using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Transactions;
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


        public override HttpResponseMessage Delete(Guid id)
        {
            try
            {
                using (System.Transactions.TransactionScope trans = new TransactionScope())
                {
                    if (new CmsPageTitleApiController().DeleteByWhere(u => u.PageID == id) < 0)
                    {
                        throw new Exception();
                    }

                    if ( new CmsPageAttributeApiController().DeleteByWhere(u => u.PageID == id) < 0)
                    {
                        throw new Exception();
                    }
                    ;
                    if (new CmsPageContentApiController().DeleteByWhere(u => u.PageID == id) < 0)
                    {
                        throw new Exception();
                    }
                    if (new CmsPagePictureApiController().DeleteByWhere(u => u.PageID == id) < 0)
                    {
                        throw new Exception();
                    }
                    if (!this.DeleteMD(new Cms_Page() { ID = id }))
                    {
                        throw new Exception();
                    }
                    ;
                    trans.Complete();
                    return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~删除数据成功了耶！" });
                }

            }
            catch (Exception ex)
            {
                return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" }); ;
            }
        }

        #region 前台调用
        [HttpPost]
        public HttpResponseMessage GetPages(Guid? webid, string code)
        {
            Cms_Channel channels = new CmsChannelApiController().Repository.Query(u => u.ChannelCode == code && u.WebSiteID == webid).First();

            Cms_Classify cmsClassifies = new CmsClassifyApiController().Repository.Query(u => u.Code == code && u.ChannelID == channels.ID).First();

            Cms_PageType cmsPageTypes = new CmsPageTypeApiController().Repository.Query(u => u.TypeCode == code && u.ClassifyID == cmsClassifies.ID).First();

            List<Cms_Page> pages =new CmsPageApiController().Repository.Query(u => u.PageCode == code && u.PageTypeID == cmsPageTypes.ID)
                    .ToList();


            List<object> result = new List<object>();
            foreach (var page in pages)
            {
                var title = new CmsPageTitleApiController().Repository.Query(item => item.PageID == page.ID).ToList();
                var attribute = new CmsPageAttributeApiController().Repository.Query(item => item.PageID == page.ID).ToList();
                var content =new CmsPageContentApiController().Repository.Query(item => item.PageID == page.ID).ToList();
                var picture =new CmsPagePictureApiController().Repository.Query(item => item.PageID == page.ID).ToList();
                result.Add(new
                {
                    page = page,
                    title = title,
                    attribute = attribute,
                    content = content,
                    picture = picture
                });
            }
            return toJson(new
            {
                channel = channels,
                data = result
            });
        }


        /// <summary>
        /// 获取通过pagecode页面列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetPageTitleListByCode(string code)
        {
            List<Cms_Page> pages =new CmsPageApiController().Repository.Query(u => u.PageCode == code).ToList();
            List<Cms_PageTitle> result = new List<Cms_PageTitle>();
            foreach (var page in pages)
            {
               result.Add(new CmsPageTitleApiController().Repository.Query(u => u.PageID == page.ID).First());
            }
            return toJson(result);
        }


        /// <summary>
        /// 通过pageid获取Page页面详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetPageInfo(Guid id)
        {
            Cms_Page page = new CmsPageApiController().Get(id);
            if (page != null)
            {
                return toJson(new
                {
                    title = new CmsPageTitleApiController().Repository.Query(item => item.PageID == page.ID).First(),
                    attribute = new CmsPageAttributeApiController().Repository.Query(item => item.PageID == page.ID).First(),
                    content = new CmsPageContentApiController().Repository.Query(item => item.PageID == page.ID).First(),
                    pictures = new CmsPagePictureApiController().Repository.Query(item => item.PageID == page.ID).ToList()
                });
            }
            return toJson(new
            {
                title = new Cms_PageTitle(),
                attribute = new Cms_PageAttribute(),
                content = new Cms_PageContent(),
                pictures = new List<Cms_PagePicture>()
            });
        }

        #endregion
    }
}
