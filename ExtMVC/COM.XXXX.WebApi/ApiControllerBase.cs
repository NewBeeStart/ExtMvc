using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using COM.XXXX.EasyUIModels;
using COM.XXXX.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Domain;
using Repository.Domain.Infrastructure;
using COM.XXXX.Common;
using COM.XXXX.Models.Admin;
using Com.XXXX.Common;
using System.Web;
using Newtonsoft.Json.Converters;

namespace COM.XXXX.WebApi
{
    public class PagerTool
    {
        public string start { get; set; }
        public string limit { get; set; }
    }

    public class ApiControllerBase<R, M> : ApiController
        where R : Repository<M>, new()
        where M : IModel, new()
    {
        public R Repository;
        public IUnitOfWork UnitOfWork { get; set; }
        public TestDbContext DbContext { get; set; }
        public User CurrentUser 
        {
            get
            {
                try
                {
                   return new CookieManage().ReadFromCookie(ConstHelper.UserCookie) as User;
                }
                catch(Exception ex)
                {
                    return null;
                }
             
            }
        }
        /// <summary>
        /// 构建DbContext，UnitOfWork
        /// </summary>
        public ApiControllerBase()
        {
            DbContext = new TestDbContext();
            UnitOfWork = new UnitOfWork(DbContext);
        }

        /// <summary>
        /// 初始化Repository类
        /// </summary>
        public void SetRepository()
        {
            Repository = new R();
            Repository.SetDBContext(DbContext);
        }

        /// <summary>
        /// 让webapi始终返回json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual HttpResponseMessage toJson(Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //str = serializer.Serialize(obj);
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd";
                str = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, timeFormat);
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }

        #region WebApi 函数
        // GET api/<controller> 
        public virtual IEnumerable<M> Get()
        {
            return Repository.List();
        }

        // GET api/<controller>/5

        public virtual M Get(Guid id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var result = Repository.Query(M => M.ID == id);
                return result.Count() > 0 ? result.First() : null;
            }
            return null;
        }

        public virtual dynamic GetForm(Guid id)
        {
            var result = Repository.Query(M => M.ID == id);
            if (result.Count() > 0)
            {
                M temp = result.First();

                if (temp != null)
                {
                    return new { success = true, message = new List<M>() { temp } };
                }
            }
            return new { success = false, message = "数据获取失败！" };
        }

        /// <summary>
        /// grid分页数据
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual HttpResponseMessage GetGridPager(JObject p)
        {
            if (p != null)
            {
                int startrecord, recordcount;
                if (int.TryParse(p["start"].ToString(), out startrecord) && int.TryParse(p["limit"].ToString(), out recordcount))
                {
                    var temp = Repository.List();
                    if (temp!=null)
                    {
                        var result = Repository.List().Skip<M>(startrecord).Take<M>(recordcount);
                        return toJson(new { data = result, count = Repository.List().Count() });
                    }
                }
            }
            else
            {
                return toJson(new { data = Repository.List(), count = Repository.List().Count() });
            }

            return toJson(new { data = "[]", count = 0 });
        }



        // POST api/<controller>
        public virtual HttpResponseMessage Post([FromBody]M model)
        {
            if (PostMD(model))
            {
                return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~添加数据成功了耶！" });
            }
            return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" });
        }


       

        // PUT api/<controller>/5
        public virtual HttpResponseMessage Put(Guid id, [FromBody]M model)
        {
            if (PutMD(id,model))
            {
                return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~更新数据成功了耶！" });
            }
            return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" });
        }

       


        // DELETE api/<controller>/5
        public virtual HttpResponseMessage Delete(Guid id)
        {
            if (DeleteMD(new M { ID = id }))
            {
                return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~删除数据成功了耶！" });
            }
            return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" }); ;
        }



        // DELETE api/<controller>/5
        public virtual HttpResponseMessage Delete(M model)
        {
            if (DeleteMD(model))
            {
                return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~删除数据成功了耶！" });
            }
            return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" }); ;
        }


   
        #endregion


        public virtual JObject QueryByFilter(string sql,params SqlParameter[] sqlParameters )
        {
           DataSet ds=  Repository.ExecuteDataSet(sql, sqlParameters);
           JObject jb = new JObject();
            if (ds!=null&&ds.Tables[0]!=null&&ds.Tables[0].Rows.Count>0)
            {
                jb.Add("data", ds.Tables[0].ToJsonList());
                jb.Add("count", ds.Tables[0].Rows.Count);
                return jb;
            }
            jb.Add("data", new JArray());
            jb.Add("count",0);
            return jb;
        }
        #region 基础操作
        internal bool PostMD(M model)
        {
            Repository.Insert(model);
            if (UnitOfWork.Save() == 1)
            {
                return true;
            }
            return false;
        }
        internal bool PutMD(Guid id, M model)
        {
            model.ID = id;
            Repository.Update(model);
            if (UnitOfWork.Save() == 1)
            {
                return true;
            }
            return false;
        }
        internal bool DeleteMD(M model)
        {
            Repository.Delete(model);
            if (UnitOfWork.Save() == 1)
            {
                return true;
            }
            return false;
        }
        internal int DeleteByWhere(Expression<Func<M, bool>> filter)
        {
            Repository.DeleteByWhere(filter);
            return UnitOfWork.Save();
        }
        #endregion

    }
}