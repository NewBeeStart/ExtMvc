using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
    }
}
