using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using COM.XXXX.Models.CMS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.DAL.Repository;
using Repository.DAL.Repository.Cms;

namespace COM.XXXX.WebApi.Cms.Controllers
{
    public class CmsPagePictureApiController : ApiControllerBase<CmsPagePictureRepository, Cms_PagePicture>
    {
        public CmsPagePictureApiController()
        {
            base.SetRepository();
        }

        /// <summary>
        /// 获取页面picture
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JObject GetPictureByPage(string id)
        {
            List<Cms_PagePicture> lst = this.Repository.Query(u => u.PageID.ToString() == id.ToString()).ToList();
            JObject job=new JObject();
            JArray arr=new JArray();
            foreach (Cms_PagePicture item  in lst)
            {
                JObject sub=new JObject();
                sub.Add("ID", item.ID);
                sub.Add("PageID", item.PageID);
                sub.Add("PicPath", item.PicPath);
                sub.Add("Remark", item.Remark);
                sub.Add("SortIndex", item.SortIndex);
                arr.Add(sub);
            }
            job.Add("images", arr);
            return job;
        }
        #region 前台Ajax
        [HttpPost]
        public JObject GetPictureByCode(string code)
        {
            Cms_Page page = new CmsPageApiController().Repository.Query(u => u.PageCode == code).FirstOrDefault();

            List<Cms_PagePicture> lst = this.Repository.Query(u => u.PageID == page.ID).OrderBy(u=>u.SortIndex).ToList();
            JObject job = new JObject();
            JArray arr = new JArray();
            foreach (Cms_PagePicture item in lst)
            {
                JObject sub = new JObject();
                sub.Add("ID", item.ID);
                sub.Add("PageID", item.PageID);
                sub.Add("PicPath", item.PicPath.TrimStart('~'));
                sub.Add("Remark", item.Remark);
                sub.Add("SortIndex", item.SortIndex);
                arr.Add(sub);
            }
            job.Add("images", arr);
            return job;
        }


        public override HttpResponseMessage GetGridPager(JObject p)
        {
            if (p != null)
            {
                string pageid=p["PageID"].ToString();
                int startrecord, recordcount;
                if (int.TryParse(p["start"].ToString(), out startrecord) && int.TryParse(p["limit"].ToString(), out recordcount))
                {
                    var temp = Repository.List();
                    if (temp != null)
                    {
                        var result = base.Repository.Query(u => u.PageID.ToString() == pageid).OrderBy(u=>u.SortIndex).Skip<Cms_PagePicture>(startrecord).Take<Cms_PagePicture>(recordcount).ToList();
                        var count = base.Repository.GetCount(u => u.PageID.ToString() == pageid);
                        return toJson(new { data = result, count = count });
                    }
                }
            }
            else
            {
                return toJson(new { data = Repository.List(), count = Repository.List().Count() });
            }

            return toJson(new { data = "[]", count = 0 });
        }

        #endregion
    }
}
