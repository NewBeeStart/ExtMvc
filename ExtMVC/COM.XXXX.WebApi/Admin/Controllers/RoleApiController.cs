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
using System.Net.Http;
using System.Web;
using System.Transactions;


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
            IList<Role> roleLst = Repository.List();
            List<ExtTree> treeLst = new List<ExtTree>();
            if (roleLst != null)
            {
                foreach (Role item in roleLst)
                {
                    var tree = new ExtTree()
                    {
                        id = item.ID.ToString(),
                        text = item.Name,
                        leaf = true
                    };
                    treeLst.Add(tree);
                }
            }
            return treeLst;
        }

        public override dynamic  GetForm(Guid id)
        {
            PrivilegeApiController privilegecontroller = new PrivilegeApiController();
            Role role = Repository.Query(r => r.ID == id).First();
            List<Privilege> privilegeMenulst = privilegecontroller.Repository.Query(p => p.Master == "Role" && p.Access=="Menu" && p.MasterValue == id.ToString()).ToList();
            List<Privilege> privilegeModulelst = privilegecontroller.Repository.Query(p => p.Master == "Role" && p.Access == "Module" && p.MasterValue == id.ToString()).ToList();
            StringBuilder menussb = new StringBuilder(); 
            foreach (Privilege item in privilegeMenulst)
            {
                menussb.Append(item.AccessValue).Append(",");
            }
            StringBuilder modulessb = new StringBuilder();
            foreach (Privilege item in privilegeModulelst)
            {
                modulessb.Append(item.AccessValue).Append(",");
            }
            if (role != null)
            {
                return new  { success = true, message = new List<object>() 
                { 
                    new  {ID=role.ID,  Name=role.Name,Desc=role.Desc,Menus=menussb.ToString().TrimEnd(','),ModuleNames=modulessb.ToString().TrimEnd(',')}
                }};
            }
            return  new  { success = false, message = "数据获取失败！" };
        }

        /// <summary>
        /// 更新Role实例
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public override HttpResponseMessage Put(Guid id, Role model)
        {

            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    Repository.Update(model);

                    if (base.UnitOfWork.Save() != 1)
                    {
                        throw new Exception("update role error!");
                    }
                    //如果Role实例更新成功，那么批量更新Privilege实例,先删除，后插入
                    string AccessValues = HttpContext.Current.Request["AccessValue"];
                    List<string> accessvalueLst = AccessValues.Split(',').ToList();
                    List<Privilege> privilegeLst = new List<Privilege>();

                    foreach (string item in accessvalueLst)
                    {
                        privilegeLst.Add(new Privilege()
                        {
                            Master = HttpContext.Current.Request["Master"],
                            MasterValue = HttpContext.Current.Request["MasterValue"],
                            Access = HttpContext.Current.Request["Access"],
                            Operation = HttpContext.Current.Request["Operation"],
                            AccessValue = item
                        });
                    }

                    string modules = HttpContext.Current.Request["Modules"];
                    List<string> modulesLst = modules.Split(',').ToList();
                    foreach (var item in modulesLst)
                    {
                        privilegeLst.Add(new Privilege()
                        {
                            Master = HttpContext.Current.Request["Master"],
                            MasterValue = model.ID.ToString(),
                            Access = "Module",
                            Operation = HttpContext.Current.Request["Operation"],
                            AccessValue = item
                        });
                    }
                    PrivilegeApiController privilegecontroller = new PrivilegeApiController();
                    privilegecontroller.Repository.DeleteByWhere(p => p.Master == "Role" &&p.MasterValue == model.ID.ToString());
                    privilegecontroller.UnitOfWork.Save();
                    privilegecontroller.Repository.InsertBatch(privilegeLst);
                    privilegecontroller.UnitOfWork.Save();
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    return toJson(new  { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" });
                }

            }
            return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~更新数据成功了耶！" });
        }

        /// <summary>
        /// 根据RoleModel新增Role实例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public override HttpResponseMessage Post(Role model)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    Repository.Insert(model);

                    if (base.UnitOfWork.Save() != 1)
                    {
                        throw new Exception("add role error!");
                    }
                    //如果Role实例更新成功，那么批量更新Privilege实例,先删除，后插入
                    string AccessValues = HttpContext.Current.Request["AccessValue"];
                    List<string> accessvalueLst = AccessValues.Split(',').ToList();
                    List<Privilege> privilegeLst = new List<Privilege>();

                    foreach (string item in accessvalueLst)
                    {
                        privilegeLst.Add(new Privilege()
                        {
                            Master = HttpContext.Current.Request["Master"],
                            MasterValue = model.ID.ToString(),
                            Access = HttpContext.Current.Request["Access"],
                            Operation = HttpContext.Current.Request["Operation"],
                            AccessValue = item
                        });

                    }
                    string modules = HttpContext.Current.Request["Modules"];
                    List<string> modulesLst = modules.Split(',').ToList();
                    foreach (var item in modulesLst)
                    {
                        privilegeLst.Add(new Privilege()
                        {
                            Master = HttpContext.Current.Request["Master"],
                            MasterValue = model.ID.ToString(),
                            Access = "Module",
                            Operation = HttpContext.Current.Request["Operation"],
                            AccessValue = item
                        });
                    }

                    PrivilegeApiController privilegecontroller = new PrivilegeApiController();
                    privilegecontroller.Repository.InsertBatch(privilegeLst);
                    privilegecontroller.UnitOfWork.Save();
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    return toJson(new { success = false, message = "Σ( ° △ °|||)︴~,由于某种原因导致数据失败，请稍后重新操作！" });
                }

            }
            return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~新增数据成功了耶！" });
        }

        /// <summary>
        /// 根据role id删除role信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public override HttpResponseMessage Delete(Guid id)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    //如果Role实例更新成功，那么批量更新Privilege实例,先删除，后插入
                    string AccessValues = HttpContext.Current.Request["AccessValue"];
                    List<string> accessvalueLst = AccessValues.Split(',').ToList();
                    List<Privilege> privilegeLst = new List<Privilege>();
                    foreach (string item in accessvalueLst)
                    {
                        if(item!=string.Empty)
                        privilegeLst.Add(new Privilege()
                        {
                            Master = HttpContext.Current.Request["Master"],
                            MasterValue = HttpContext.Current.Request["MasterValue"],
                            Access = HttpContext.Current.Request["Access"],
                            Operation = HttpContext.Current.Request["Operation"],
                            AccessValue = item
                        });

                    }
                    string modules = HttpContext.Current.Request["Modules"];
                    List<string> modulesLst = modules.Split(',').ToList();
                    foreach (var item in modulesLst)
                    {
                        if (item != string.Empty)
                        privilegeLst.Add(new Privilege()
                        {
                            Master = HttpContext.Current.Request["Master"],
                            MasterValue = HttpContext.Current.Request["MasterValue"],
                            Access = "Module",
                            Operation = HttpContext.Current.Request["Operation"],
                            AccessValue = item
                        });
                    }
                    PrivilegeApiController privilegecontroller = new PrivilegeApiController();
                    privilegecontroller.Repository.DeleteByWhere(p => p.Master == "Role" && p.MasterValue == id.ToString());
                    privilegecontroller.UnitOfWork.Save();
                    Repository.Delete(new Role { ID = id });

                    if (base.UnitOfWork.Save() != 1)
                    {
                        throw new Exception("Delete role error!");
                    }
                   
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    return toJson(new { success = true, message = "Σ( ° △ °|||)︴~,存在用户使用该角色，无法删除，请稍后重新操作！" });
                }

            }
            return toJson(new { success = true, message = "恭喜你,~O(∩_∩)O~删除数据成功了耶！" });
        }
    }
}
