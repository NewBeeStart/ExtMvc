using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Http;
using System.Web.UI.WebControls;
using COM.XXXX.EasyUIModels;
using COM.XXXX.Models.Admin;
using Repository.DAL.Repository;
using Repository.DAL.Repository.Admin;
using Repository.Domain.Infrastructure;

namespace COM.XXXX.WebApi.Admin.Controllers
{

    public class OrganizationApiController : ApiControllerBase<OrganizationRepository, Organization>
    {

        public OrganizationApiController()
        {
            base.SetRepository();
        }


        public dynamic PutMoveDepartMent(Guid? id, Guid? orgid)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {

                var org = base.Get(Guid.Parse(id.ToString()));

                org.POrganizationID = orgid;

                base.Put(Guid.Parse(id.ToString()), org);
                return "(＾－＾)V移动成功！";
            }
            return "(。﹏。*)移动失败！";
        }


        /// <summary>
        /// 获取组织机构TreeGrid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<Organization> GetOrganizationsTree(Guid? id)
        {
            List<Organization> lst = new List<Organization>();
            if (string.IsNullOrEmpty(id.ToString()))
            {
                lst.AddRange(Repository.Query(org => org.POrganizationID == null).OrderBy(org => org.Sort).ToList());
            }
            else
            {
                lst.AddRange(Repository.Query(org => org.POrganizationID == id).OrderBy(org => org.Sort).ToList());
            }

            for (int i = 0; i < lst.Count; i++)
            {
                var children = GetOrganizationsTree(lst[i].ID);
                if (children.Any() && children != null)
                {
                    lst[i].children = new List<Organization>();
                    lst[i].children.AddRange(children);
                }
            }

            return lst;
        }



        public ExtTree GetTreeNodeById(Guid? id, string type)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                Guid guid = Guid.Parse(id.ToString());
                if (type.Contains("user"))
                {
                    User result = new UserApiController().Repository.Query(user => user.ID == guid).FirstOrDefault();
                    var tree = new ExtTree()
                    {
                        id = result.ID.ToString(),
                        text = result.RealName,
                        iconCls = geticon("user"),
                    };
                    return tree;
                }
                else if (type.Contains("organization") || type.Contains("department"))
                {
                    Organization result = base.Get(guid);
                    var tree = new ExtTree()
                    {
                        id = result.ID.ToString(),
                        text = result.Name,
                        iconCls = geticon(result.OrgType),
                    };
                    return tree;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取组织机构Tree
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<ExtTree> GetOrganizationsComboTree(Guid? id)
        {
            List<ExtTree> treelst = new List<ExtTree>();
            List<Organization> lst = new List<Organization>();
            if (string.IsNullOrEmpty(id.ToString()))
            {
                lst.AddRange(Repository.Query(org => org.POrganizationID == null).OrderBy(org => org.Sort).ToList());
            }
            else
            {
                lst.AddRange(Repository.Query(org => org.POrganizationID == id).OrderBy(org => org.Sort).ToList());
            }
            foreach (var organization in lst)
            {
                var tree = new ExtTree()
                  {
                      id = organization.ID.ToString(),
                      text = organization.Name,
                      iconCls = "",
                  };
                var orgchildren = Repository.Query(org => org.POrganizationID == organization.ID).OrderBy(org => org.Sort).ToList();
                foreach (var child in orgchildren)
                {
                    tree.children.Add(new ExtTree()
                    {
                        id = child.ID.ToString(),
                        text = child.Name,
                        iconCls = "",
                        children = GetOrganizationsComboTree(child.ID).ToList(),
                    });
                }
                treelst.Add(tree);
            }

            return treelst;
        }
        [HttpGet]
        [HttpPost]
        public IEnumerable<ExtTree> GetOrganizationsAndUser(Guid? id)
        {
            List<ExtTree> treelst = new List<ExtTree>();
            List<Organization> lst = new List<Organization>();

            if (string.IsNullOrEmpty(id.ToString()))
            {
                lst.AddRange(Repository.Query(org => org.POrganizationID == null).OrderBy(org => org.Sort).ToList());
            }
            else
            {
                lst.AddRange(Repository.Query(org => org.POrganizationID == id).OrderBy(org => org.Sort).ToList());
            }
            foreach (var organization in lst)
            {
                var tree = new ExtTree()
                {
                    id = organization.ID.ToString(),
                    text = organization.Name,
                    iconCls = organization.iconCls,
                    attributes = new { Type = organization.OrgType },
                    
                };
                var orgchildren = Repository.Query(org => org.POrganizationID == organization.ID).OrderBy(org => org.Sort).ToList();
                var userchildren = GetSubUsersByOrganizationID(organization.ID);

                tree.leaf = orgchildren.Count > 0 || userchildren.Count > 0 ? false : true;

                tree.children.AddRange(userchildren);

                foreach (var child in orgchildren)
                {
                    var uitree = new ExtTree()
                    {
                        id = child.ID.ToString(),
                        text = child.Name,
                        iconCls = child.iconCls,
                        attributes = new { Type = child.OrgType },

                    };

                    var userlst = GetSubUsersByOrganizationID(child.ID);
                    var orglst = GetOrganizationsAndUser(child.ID).ToList();

                    uitree.children.AddRange(userlst);

                    uitree.children.AddRange(orglst);

                    uitree.leaf = userlst.Count > 0 || orglst.Count > 0 ? false : true;

                    tree.children.Add(uitree);
                }

                treelst.Add(tree);
            }

            return treelst;
        }
        [HttpGet]
        [HttpPost]
        public dynamic GetOrganizationsUserComboBox()
        {
            var orgList=base.Get();
            var userList= new UserApiController().Get();
            var result=new List<object>();
            foreach (var item in orgList)
	        {
                result.Add(new { VALUE = item.ID, TEXT = item.Name, PID = item.POrganizationID == null ? Guid.Empty : item.POrganizationID, iconCls =item.iconCls});
	        }
            foreach (var item in userList)
            {
                result.Add(new { VALUE = item.ID, TEXT = item.RealName, PID = item.OrganizationID, iconCls = "icon-1" });
            }
            return  new { msg=result}; 
        }



        /// <summary>
        /// 根据OrganizationID获取Users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<ExtTree> GetSubUsersByOrganizationID(Guid id)
        {
            List<User> userlst = new UserApiController().Repository.Query(user => user.OrganizationID == id).ToList();
            List<ExtTree> usertreelst = new List<ExtTree>();
            foreach (User user in userlst)
            {
                usertreelst.Add(new ExtTree()
                {
                    id = user.ID.ToString(),
                    text = user.RealName,
                    iconCls ="icon-10",
                    leaf=true,
                    attributes = new { Type = "user" },
                });
            }
            return usertreelst;
        }

        private string geticon(string orgtype)
        {
            switch (orgtype)
            {
                case "organization":
                    return "icon-organise";
                case "department":
                    return "icon-usergroup";
                case "usergroup":
                    return "icon2 r6_c9";
                default:
                    return "icon-man";
            }
        }



    }
}
