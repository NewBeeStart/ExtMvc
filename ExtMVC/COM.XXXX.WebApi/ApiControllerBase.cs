using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using COM.XXXX.EasyUIModels;
using COM.XXXX.Models;
using Newtonsoft.Json;
using Repository.Domain;
using Repository.Domain.Infrastructure;
using COM.XXXX.Common;
using COM.XXXX.Models.Admin;
using Com.XXXX.Common;

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
                return new CookieManage().ReadFromCookie(ConstHelper.UserCookie) as User;
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
                str = JsonConvert.SerializeObject(obj);
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
            if (string.IsNullOrEmpty(id.ToString()))
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
        public virtual HttpResponseMessage GetGridPager([FromBody]PagerTool p)
        {
            if (p != null)
            {
                int startrecord, recordcount;
                if (int.TryParse(p.start, out startrecord) && int.TryParse(p.limit, out recordcount))
                {
                    var result = Repository.List().Skip<M>(startrecord).Take<M>(recordcount);
                    return toJson(new { data = result, count = Repository.List().Count() });
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
            Repository.Insert(model);

            if (UnitOfWork.Save() == 1)
            {
                return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~添加数据成功了耶！" });
            }
            return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" });
        }

        // PUT api/<controller>/5
        public virtual HttpResponseMessage Put(Guid id, [FromBody]M model)
        {
            model.ID = id;
            Repository.Update(model);
            if (UnitOfWork.Save() == 1)
            {
                return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~更新数据成功了耶！" });
            }
            return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" });
        }

        // DELETE api/<controller>/5
        public virtual HttpResponseMessage Delete(Guid id)
        {
            Repository.Delete(new M { ID = id });
            if (UnitOfWork.Save() == 1)
            {
                return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~删除数据成功了耶！" });
            }
            return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" }); ;
        }
        // DELETE api/<controller>/5
        public virtual HttpResponseMessage Delete(M model)
        {
            Repository.Delete(model);
            if (UnitOfWork.Save() == 1)
            {
                return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~删除数据成功了耶！" });
            }
            return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" }); ;
        }
        #endregion
    }
}