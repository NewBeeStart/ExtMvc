﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using COM.XXXX.Common;
using COM.XXXX.Models.CMS;
using Newtonsoft.Json.Linq;
using Repository.DAL.Repository;

namespace COM.XXXX.WebApi.Cms.Controllers
{
    public class CmsClassifyApiController : ApiControllerBase<CmsClassifyRepository, Cms_Classify>
    {
        public CmsClassifyApiController()
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
                        string querystr = string.Format(@"select a.*,b.ChannelName from Cms_Classify a
                                             left join Cms_Channel b on a.ChannelID=b.ID
                                             where a.ChannelID='{0}'",id.ToString());
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

    }
}
