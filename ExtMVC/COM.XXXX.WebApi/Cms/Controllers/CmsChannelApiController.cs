using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using COM.XXXX.Common;
using COM.XXXX.Models.CMS;
using Newtonsoft.Json.Linq;
using Repository.DAL.Repository;
using System.Web.Http;

namespace COM.XXXX.WebApi.Cms.Controllers
{
    public class CmsChannelApiController : ApiControllerBase<CmsChannelRepository, Cms_Channel>
    {
        public CmsChannelApiController()
        {
            base.SetRepository();
        }

        [HttpPost]
        public virtual HttpResponseMessage GetGridPagerByID(Guid id, JObject p)
        {
            int totalCount = 0;
            if (p != null)
            {
                int startrecord, recordcount;
                if (int.TryParse(p["start"].ToString(), out startrecord) && int.TryParse(p["limit"].ToString(), out recordcount))
                {

                    if (!string.IsNullOrEmpty(id.ToString()))
                    {
                        ////List<Cms_Channel> result1 = base.Repository.Query(u => u.WebSiteID == id).ToList();
                        //var result =  base.Repository.QueryByPage<int>(u => u.WebSiteID.ToString() == id.ToString(),
                        //                                         u => u.SortIndex, recordcount, startrecord,
                        //                                         out totalCount).ToList();

                        string querystr = string.Format(@"select * from(
                                            select a.*,b.SiteTitle,ROW_NUMBER() over (order by a.sortIndex) as rownum 
                                            from cms_channel a left join cms_website b on a.websiteid=b.id
                                            where a.WebSiteID='{0}') temp",
                                                        id.ToString());
                        DataSet dataSet = base.Repository.ExecuteDataSet(querystr);
                        var result = dataSet.Tables[0].Select()
                                                      .Skip(startrecord)
                                                      .Take(recordcount).CopyToDataTable();
                                       
                        return toJson(new { data = result.ToJsonList(), count = dataSet.Tables[0].Rows.Count });
                    }
                }
            }
            else
            {
                return toJson(new { data = Repository.List(), count = Repository.List().Count() });
            }

            return toJson(new { data = "[]", count = 0 });
        }

        [HttpPost]
        [HttpGet]
        public HttpResponseMessage GetChannelCombox()
        {
            var result = this.Get();
            return toJson(result);
        }


        #region 前台调用方法
        [HttpPost]
        public HttpResponseMessage GetWebMenusByWebID(Guid? webid)
        {
           return toJson(base.Repository.Query(u => u.WebSiteID == webid));
        }
        #endregion
    }
}
