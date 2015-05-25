using COM.XXXX.Models.Admin;
using Repository.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Transactions;
using System.Web.Http;

namespace COM.XXXX.WebApi.Admin.Controllers
{
    public class RoleRightApiController : ApiControllerBase<RoleRightRepository, RoleRight>
    {
        public RoleRightApiController()
        {
            base.SetRepository();
        }

        public virtual RoleRight GetRoleByUser(Guid id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var Userole = base.Repository.Query(right => right.UserID.ToString() == id.ToString()).ToList();
                if (Userole.Count() > 0)
                {
                    return Userole.FirstOrDefault();
                }
            }
            return null;
        }

        /// <summary>
        /// 判断是否存在角色记录，如果存在，那么删除，更新，如果不存在，那么新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override HttpResponseMessage Post(RoleRight model)
        {
            var result= base.Repository.Query(right => right.UserID.ToString() == model.UserID.ToString()).ToList();
            if (result.Count() == 0)
            {
                return base.Post(model);
            }
            else 
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {    
                        base.Delete(result.First());
                        base.Post(model);
                        transaction.Complete();
                    }
                    catch (Exception ex)
                    {
                        return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" });
                    }
                }
                return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~更新数据成功了耶！" });
            }
        }
    }
}
