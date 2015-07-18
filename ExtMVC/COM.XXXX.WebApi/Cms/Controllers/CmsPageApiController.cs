using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            Cms_Channel channels = new CmsChannelApiController().Repository.Query(u => u.ChannelCode == code && u.WebSiteID == webid && u.InUse == true).First();

            Cms_Classify cmsClassifies = new CmsClassifyApiController().Repository.Query(u => u.Code == code && u.ChannelID == channels.ID && u.InUse == true).First();

            Cms_PageType cmsPageTypes = new CmsPageTypeApiController().Repository.Query(u => u.TypeCode == code && u.ClassifyID == cmsClassifies.ID && u.InUse == true).First();

            List<Cms_Page> pages =new CmsPageApiController().Repository.Query(u => u.PageCode == code && u.PageTypeID == cmsPageTypes.ID&&u.InUse==true).OrderBy(item=>item.SortIndex)
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

            List<Cms_Page> pages =
                new CmsPageApiController().Repository.SqlQuery<Cms_Page>(@"select * from Cms_Page 
                where PageCode=@Code and InUse=1
                order by sortindex", new SqlParameter("@Code",code)).ToList();

            List<Cms_PageTitle> result = new List<Cms_PageTitle>();
            foreach (var page in pages)
            {
               result.Add(new CmsPageTitleApiController().Repository.Query(u => u.PageID == page.ID).First());
            }
            return toJson(result);
        }


        [HttpPost]
        public HttpResponseMessage GetClassifyListByChannelCode(string code) 
        {
            var cmsChannel = new CmsChannelApiController().Repository.Query(u => u.ChannelCode == code);
            if (cmsChannel.Any())
            {
                var model = cmsChannel.First();
                List<Cms_Classify> classifies =
                     new CmsClassifyApiController().Repository.Query(u => u.ChannelID==model.ID).ToList();
                return toJson(classifies);

            }

            return toJson(new List<Cms_Classify>());
          
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
                var titleList = new CmsPageTitleApiController().Repository.Query(item => item.PageID == page.ID);
                var attributeList = new CmsPageAttributeApiController().Repository.Query(item => item.PageID == page.ID);
                var contentList = new CmsPageContentApiController().Repository.Query(item => item.PageID == page.ID);
                var picturesList = new CmsPagePictureApiController().Repository.Query(item => item.PageID == page.ID);
                return toJson(new
                {
                    title = titleList.Any() ? titleList.First() : new Cms_PageTitle(),
                    attribute = attributeList.Any() ? attributeList.First() : new Cms_PageAttribute(),
                    content = contentList.Any() ? contentList.First() : new Cms_PageContent(),
                    pictures = picturesList.Any() ? picturesList.ToList() : new List<Cms_PagePicture>()

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



        /// <summary>
        /// 通过pagecode获取Page页面详细信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetPageInfoByCode(string code) 
        {
            var  pages = new CmsPageApiController().Repository.Query(u=>u.PageCode==code);
            if (pages.Any())
            {
                var page = pages.First();
                var titleList = new CmsPageTitleApiController().Repository.Query(item => item.PageID == page.ID);
                var attributeList = new CmsPageAttributeApiController().Repository.Query(item => item.PageID == page.ID);
                var contentList = new CmsPageContentApiController().Repository.Query(item => item.PageID == page.ID);
                var picturesList = new CmsPagePictureApiController().Repository.Query(item => item.PageID == page.ID);
                return toJson(new
                {
                    title = titleList.Any()? titleList.First():new Cms_PageTitle(),
                    attribute = attributeList.Any() ?  attributeList.First():new Cms_PageAttribute() ,
                    content = contentList.Any() ? contentList.First() : new Cms_PageContent(),
                    pictures = picturesList.Any() ? picturesList.ToList() : new List<Cms_PagePicture>()

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

        /// <summary>
        /// 通过pagecode获取Page页面详细信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost] 
        public HttpResponseMessage GetOnline(JObject form)
        {
            var name = form["name"].ToString().Trim();
            var telephone = form["telephone"].ToString().Trim();
            var email = form["email"].ToString().Trim();
            var product = string.Empty;
            var remark = string.Empty;
            var address = string.Empty;
            if (form["product"]!=null)
                product= form["product"].ToString().Trim();
            if (form["Remark"]!=null)
            {
               remark= form["Remark"].ToString().Trim();
            }
            if (form["address"]!=null)
            {
              address= form["address"].ToString().Trim();
            }
           
            if (string.IsNullOrEmpty(name)||string.IsNullOrEmpty(telephone)||string.IsNullOrEmpty(email)||string.IsNullOrEmpty(product)||string.IsNullOrEmpty(remark)||string.IsNullOrEmpty(address))
            {
                return toJson(new {success = false, msg = "请填写完整的表单，否则我们无法与您取得联系"});
            }
            var channel = new CmsChannelApiController().Repository.Query(u => u.ChannelCode == "Online");
            if (channel.Any())
            {
                var model = channel.First();
                var count=new CmsClassifyApiController().Repository.GetCount(u => u.ChannelID == model.ID);
                var flag = new CmsClassifyApiController().PostMD(new Cms_Classify()
                {
                    Code = "Online",
                    ChannelID = model.ID,
                    InUse = true,
                    PageContent =
                        string.Format(@"姓名:{0}<br/>电话:{1}<br/>email:{2}<br/>产品：{3}<br/>备注:{4}<br/>地址:{5}<br/>",
                            name, telephone, email, product, remark, address),
                    Name = "在线订单-" + DateTime.Now.ToString("YY-MM-DD"),
                    Remark = "在线订单",
                    SortIndex = 0

                });
                if (flag)
                {
                    return toJson(new { success = true, msg = "谢谢您的支持,我们将尽快与您联系！" }); 
                }
            }
            return toJson(new { success = true, msg = "您的信息有误!请重新输入" }); 
        }

        #endregion
    }
}
