using System;
using System.Collections.Generic;
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
        public virtual HttpResponseMessage GetGridPagerByID(Guid id,JObject p)
        {
            int totalCount = 0;
            if (p != null)
            { 
                int startrecord, recordcount;
                if (int.TryParse(p["start"].ToString(), out startrecord) && int.TryParse(p["limit"].ToString(), out recordcount))
                {
                    
                    if (!string.IsNullOrEmpty(id.ToString()))
                    {
                        List<Cms_Channel> result1 = base.Repository.Query(u => u.WebSiteID == id).ToList();
                        var result = base.Repository.QueryByPage(u => u.WebSiteID.ToString() == id.ToString(),
                                                                 u => u.WebSite.SiteTitle, recordcount, startrecord,
                                                                 out totalCount).ToList();

                        return toJson(new { data = result, count = totalCount });
                    }
                }
            }
            else
            {
                return toJson(new { data = Repository.List(), count = Repository.List().Count() });
            }

            return toJson(new { data = "[]", count = 0 });
        }

    }
}
