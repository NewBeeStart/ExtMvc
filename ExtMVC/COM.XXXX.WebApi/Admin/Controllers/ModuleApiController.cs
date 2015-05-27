using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using COM.XXXX.EasyUIModels;
using COM.XXXX.Models.Admin;
using Repository.DAL.Repository;
using System.Web;
using Com.XXXX.Common;

namespace COM.XXXX.WebApi.Admin.Controllers
{
    public class ModuleApiController : ApiControllerBase<ModuleRepository, Module>
    {
        public ModuleApiController()
        {
            base.SetRepository();
        }

        /// <summary>
        /// 获取Module的Tree列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public dynamic GetModulesTree()
        {
            List<Module> modules = base.Get().ToList();
            List<ExtTree> treelst = new List<ExtTree>();
            foreach (Module module in modules)
            {
                treelst.Add(new ExtTree()
                    {
                        id = module.ID.ToString(),
                        text = module.Name,
                        iconCls = module.iconCls,
                    });
            }
            return treelst;
        }

        /// <summary>
        /// 获取模块及模块下菜单--Home页面使用
        /// </summary>
        /// <returns></returns>
        public dynamic GetModuleMenus()
        {
            PrivilegeApiController privilegedal = new PrivilegeApiController();
            RoleRightApiController rolerdal = new RoleRightApiController();

            List<RoleRight> roles = new RoleRightApiController().Repository.Query(role => role.UserID == CurrentUser.ID).ToList();

            List<object> ModuleMenu = new List<object>();
            foreach (RoleRight role in roles)
            {
                List<Privilege> modulePrivileges = privilegedal.Repository.Query(p => p.Master == "Role" && p.Access == "Module" && p.MasterValue == role.RoleID.ToString()).ToList();
                List<Privilege> menuPrivileges = privilegedal.Repository.Query(p => p.Master == "Role" && p.Access == "Menu" && p.MasterValue == role.RoleID.ToString()).ToList();
                foreach (Privilege item in modulePrivileges)
                {
                    var module = base.Get(Guid.Parse(item.AccessValue));
                    ModuleMenu.Add(new
                    {
                        title = module.Name,
                        menus = new List<object>(new MenuApiController().GetRoleMenusByModule(module.Code, null, menuPrivileges))
                    });
                }
            }

            return ModuleMenu;
            //List<Module> modules = base.Get().ToList();
            //List<object> ModuleMenu = new List<object>();
            //foreach (var item in modules)
            //{
            //    ModuleMenu.Add(new
            //    {
            //        title = item.Name,
            //        menus = new List<object>(new MenuApiController().GetMenusByModule(item.Code, null))
            //    });
            //}

            //return ModuleMenu;
        }


        /// <summary>
        /// 获取模块及模块下菜单Tree
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public dynamic GetModuleMenusTree()
        {
            List<Module> modules = base.Get().ToList();
            List<object> ModuleMenu = new List<object>();
            foreach (var item in modules)
            {
                ExtTree module = new ExtTree()
                {
                    id = item.ID.ToString(),
                    text = item.Name,
                    iconCls = item.iconCls,
                    leaf = false,
                    attributes = new { type = "module" }
                };
                module.children.AddRange(new MenuApiController().GetMenusByModule(item.Code, null));
                ModuleMenu.Add(module);
            }

            return ModuleMenu;
        }
        /// <summary>
        /// 获取Extjs ComboBoxTree中store的数据
        /// 模块--菜单--tree
        /// </summary>
        /// <returns></returns>
        public dynamic GetModuleMenusComboBoxTree()
        {
            List<Menu> menuList = new MenuApiController().Repository.List().ToList();
            List<Module> moduleList = Repository.List().ToList();
            var result = new List<object>();
            foreach (Module item in moduleList)
            {
                result.Add(new { VALUE = item.ID, TEXT = item.Name, PID = Guid.Empty, Type = "module" });
            }
            foreach (Menu item in menuList)
            {
                result.Add(new { VALUE = item.ID, TEXT = item.DisplayName, PID = item.PMenuID == null ? item.ModuleID : item.PMenuID, Type = "menu" });
            }
            return new { msg = result };
        }

        /// <summary>
        /// 单独获取模块的tree
        /// Module tree
        /// </summary>
        /// <returns></returns>
        public dynamic GetModuleComboBoxTree()
        {
            List<Module> moduleList = Repository.List().ToList();
            var result = new List<object>();
            foreach (Module item in moduleList)
            {
                result.Add(new { VALUE = item.ID, TEXT = item.Name, PID = Guid.Empty, Type = "module", leaf = true });
            }
            return new { msg = result };
        }
    }
}
