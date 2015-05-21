using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Http;
using COM.XXXX.Models.Admin;
using Repository.DAL.Repository.Admin;
using COM.XXXX.EasyUIModels;
using Repository.DAL.Repository;

namespace COM.XXXX.WebApi.Admin.Controllers
{
    public class RoleApiController : ApiControllerBase<RoleRepository, Role>
    {
        public RoleApiController()
        {
            base.SetRepository();
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<ExtTree> GetRoleTree()
        {
            IList<Role> roleLst =Repository.List();
            List<ExtTree> treeLst = new List<ExtTree>();
            if (roleLst != null) 
            {
                foreach (Role item in roleLst)
                {
                    var tree = new ExtTree()
                    {
                        id = item.ID.ToString(),
                        text = item.Name,
                        leaf=true
                    };
                    treeLst.Add(tree);
                }
            }
            return treeLst;
        }

      
    }
}
