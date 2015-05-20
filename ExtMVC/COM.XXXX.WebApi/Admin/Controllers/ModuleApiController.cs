﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using COM.XXXX.EasyUIModels;
using COM.XXXX.Models.Admin;
using Repository.DAL.Repository;

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
        /// 获取模块及模块下菜单
        /// </summary>
        /// <returns></returns>
        public dynamic GetModuleMenus()
        {
            List<Module> modules = base.Get().ToList();
            List<object> ModuleMenu = new List<object>();
            foreach (var item in modules)
	        {
               ModuleMenu.Add( new {
                  title=item.Name, 
                  menus = new List<object>(new MenuApiController().GetMenusByModule(item.Code, null))
                });
	        }

            return ModuleMenu;
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
                    attributes = new  { type="module"}
                };
                module.children.AddRange(new MenuApiController().GetMenusByModule(item.Code, null));
                ModuleMenu.Add(module);
            }

            return ModuleMenu;
        }
    }
}
