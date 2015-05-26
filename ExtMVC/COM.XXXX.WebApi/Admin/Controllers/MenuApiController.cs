using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using COM.XXXX.EasyUIModels;
using COM.XXXX.Models;
using COM.XXXX.Models.Admin;
using Repository.DAL.Repository;
using System.IO;
using System.Web;

namespace COM.XXXX.WebApi.Admin.Controllers
{
    public class MenuApiController : ApiControllerBase<MenuRepository, Menu>
    {

        public MenuApiController()
        {
            base.SetRepository();
        }

        public IEnumerable<ExtTree> GetMenusByModule(string modulecode, Guid? id)
        {
            if (string.IsNullOrEmpty(modulecode))
            {
                return null;
            }
            var moduleMenus = Repository.Query(menu => menu.Module.Code == modulecode && menu.PMenuID == id).OrderBy(menu => menu.SortKey).ToList();

            List<ExtTree> menulst = new List<ExtTree>();
            foreach (Menu item in moduleMenus)
            {
                ExtTree menu = new ExtTree()
                {
                    id = item.ID.ToString(),
                    text = item.DisplayName,
                    iconCls = item.iconCls,
                    leaf = true,
                    url = string.Format("/{0}/{1}/{2}", item.Module.Code, item.Controller, item.Action),
                    attributes = new
                    {
                        Width = item.Width,
                        Height = item.Height,
                        OpenType = item.OpenModel,
                        type="menu"
                    }
                };
                if (!item.IsLeaf)
                {
                    var sub = GetMenusByModule(modulecode, item.ID);
                    if (sub != null && sub.Count() > 0)
                    {
                        menu.leaf = false;
                        menu.children.AddRange(sub);
                    }
                }
                menulst.Add(menu);
            }
            return menulst;

        }
        //public IEnumerable<ExtTree> GetMenusByRole()
        //{
        //    List<RoleRight> roles = new RoleRightApiController().Repository.Query(role => role.UserID = CurrentUser.ID).ToList();

        //    foreach (RoleRight role in roles)
        //    {
        //        List<Privilege> privileges = new PrivilegeApiController().Repository.Query(p => p.Master == "Role" && p.Access == "Menu" && p.MasterValue == role.RoleID).ToList();
                
        //    }
           
        //    IEnumerable<ExtTree>  Menus=GetMenusByRole(string modulecode, Guid? id,Guid? roleid)
        //}
      
        //public IEnumerable<ExtTree> GetMenusByRole(string modulecode, Guid? id,Guid? roleid)
        //{
        //    if (string.IsNullOrEmpty(modulecode))
        //    {
        //        return null;
        //    }
        //    var moduleMenus = Repository.Query(menu => menu.Module.Code == modulecode && menu.PMenuID == id).OrderBy(menu => menu.SortKey).ToList();

        //    List<ExtTree> menulst = new List<ExtTree>();
        //    foreach (Menu item in moduleMenus)
        //    {
        //        ExtTree menu = new ExtTree()
        //        {
        //            id = item.ID.ToString(),
        //            text = item.DisplayName,
        //            iconCls = item.iconCls,
        //            leaf = true,
        //            url = string.Format("/{0}/{1}/{2}", item.Module.Code, item.Controller, item.Action),
        //            attributes = new
        //            {
        //                Width = item.Width,
        //                Height = item.Height,
        //                OpenType = item.OpenModel,
        //                type = "menu"
        //            }
        //        };
        //        if (!item.IsLeaf)
        //        {
        //            var sub = GetMenusByModule(modulecode, item.ID,roleid);
        //            if (sub != null && sub.Count() > 0)
        //            {
        //                menu.leaf = false;
        //                menu.children.AddRange(sub);
        //            }
        //        }
        //        menulst.Add(menu);
        //    }
        //    return menulst;

        //}
     
        //public IEnumerable<Menu> GetMenusByPage(string modulecode, string controller, string action)
        //{
        //    return Repository.GetMenusByPage(modulecode, controller, action);
        //}

        //public IEnumerable<UITree> GetSubMenusByPMenu(Guid id, string modulecode)
        //{
        //    IEnumerable<Menu> submenus = Repository.GetSubMenusByPMenu(id, modulecode);
        //    List<UITree> menulst = new List<UITree>();
        //    foreach (Menu item in submenus)
        //    {
        //        UITree menu = new UITree()
        //        {
        //            id = item.ID.ToString(),
        //            text = item.DisplayName,
        //            iconCls = item.IconCls,
        //            attributes = new
        //            {
        //                URL = string.Format("/{0}/{1}/{2}", item.Module.Code, item.Controller, item.Action),
        //                Width = item.Width,
        //                Height = item.Height,
        //                OpenType = item.OpenModel
        //            }
        //        };
        //        if (!item.IsLeaf)
        //        {
        //            var sub = GetSubMenusByPMenu(item.ID, modulecode);
        //            if (sub != null)
        //            {
        //                menu.children.AddRange(sub);
        //            }
        //        }
        //        menulst.Add(menu);
        //    }
        //    return menulst;
        //}

        //public IEnumerable<Menu> GetMenusByPMenu(Guid id, string modulecode)
        //{
        //    return Repository.GetMenusByPMenu(id, modulecode);
        //}
        /// <summary>
        /// 获取组织机构TreeGrid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<Menu> GetMenuTree(Guid? id)
        {
            List<Menu> lst = new List<Menu>();
            if (string.IsNullOrEmpty(id.ToString()))
            {
                lst.AddRange(Repository.Query(menu => menu.PMenuID == null).OrderBy(org => org.SortKey).ToList());
            }
            else
            {
                lst.AddRange(Repository.Query(menu => menu.PMenuID == id).OrderBy(org => org.SortKey).ToList());
            }

            for (int i = 0; i < lst.Count; i++)
            {
                var children = GetMenuTree(lst[i].ID);
                if (children.Any() && children != null)
                {
                    lst[i].children = new List<Menu>();
                    lst[i].children.AddRange(children);
                }
            }

            return lst;
        }

        /// <summary>
        /// 获取组织机构TreeGrid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<Menu> GetMenuTreeByModule(Guid? id, Guid? moduleid)
        {
            List<Menu> lst = new List<Menu>();
            Guid module = Guid.Parse(moduleid.ToString());
            if (string.IsNullOrEmpty(moduleid.ToString()))
            {
                return null;
            }
            if (string.IsNullOrEmpty(id.ToString()) && string.IsNullOrEmpty(moduleid.ToString()))
            {
                lst.AddRange(Repository.Query(menu => menu.PMenuID == null && menu.ModuleID == module).OrderBy(org => org.SortKey).ToList());
            }
            else
            {
                lst.AddRange(Repository.Query(menu => menu.PMenuID == id && menu.ModuleID == module).OrderBy(org => org.SortKey).ToList());
            }

            for (int i = 0; i < lst.Count; i++)
            {
                var children = GetMenuTreeByModule(lst[i].ID, moduleid);
                if (children.Any() && children != null)
                {
                    lst[i].children = new List<Menu>();
                    lst[i].children.AddRange(children);
                }
            }

            return lst;
        }


        [HttpPost]
        public dynamic GetMenuIcon(string path)
        {
          
            //System.Text.StringBuilder innerhtml = new System.Text.StringBuilder();
         
            List<object> lst = new List<object>();
            DirectoryInfo icondir = new DirectoryInfo(HttpUtility.UrlDecode(path));

            foreach (FileInfo item in icondir.GetFiles())
            {
                lst.Add(new {
                    name = "icon-" + item.Name.Split('.')[0],
                });
                //innerhtml.Append("<span class='iconwidth " + iconclass + "' value='" + iconclass + "' onclick='seticon(this)'>&nbsp;&nbsp;</span>");
            }
            return lst ;
        }


      
    }
}