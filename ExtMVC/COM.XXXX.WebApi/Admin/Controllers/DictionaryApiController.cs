using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Http;
using COM.XXXX.Models.Admin;
using Repository.DAL.Repository.Admin;
using COM.XXXX.EasyUIModels;

namespace COM.XXXX.WebApi.Admin.Controllers
{               
   public class DictionaryApiController: ApiControllerBase<DictionaryRepository, Dictionary>
    {

        public DictionaryApiController()
        {
            base.SetRepository();
        }
       [HttpPost]
       public dynamic GetAllDictionary()
       {
           var list= base.Get();
           return new
           {
               total = list.Count(),
               rows = list
           };
       }
       /// <summary>
       /// 获取组织机构Tree
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       [HttpPost]
       public IEnumerable<ExtTree> GetDictionaryTree(Guid? id)
       {
           List<ExtTree> treelst = new List<ExtTree>();
           List<Dictionary> lst = new List<Dictionary>();
           if (string.IsNullOrEmpty(id.ToString())) 
           {
               lst.AddRange(Repository.Query(dic => dic.PDictionaryID == null).OrderBy(dic => dic.Sort).ToList());
           }
           else
           {
               lst.AddRange(Repository.Query(dic => dic.PDictionaryID == id).OrderBy(dic => dic.Sort).ToList());
           }
           foreach (var item in lst) 
           {
               var tree = new ExtTree()
               {
                   id = item.ID.ToString(),
                   text = item.Title,
                   iconCls = item.iconCls,
               };
               var dicchildren = Repository.Query(dic => dic.PDictionaryID == item.ID).OrderBy(dic => dic.Sort).ToList();
               foreach (var child in dicchildren)
               {
                   tree.children.Add(new ExtTree()
                   {
                       id = child.ID.ToString(),
                       text = child.Title,
                       iconCls = child.iconCls,
                       children = GetDictionaryTree(child.ID).ToList(),
                   });
               }
               treelst.Add(tree);
           }

           return treelst;
       }

       //public string GetIconClass() { 
       //System.Text.StringBuilder innerhtml = new System.Text.StringBuilder();
       //             string path = Server.MapPath(Com.XXXX.Class.ConstHelper.IconPath);

       //             DirectoryInfo icondir = new DirectoryInfo(path);

       //             foreach (FileInfo item in icondir.GetFiles())
       //             {

       //                  string iconclass = "icon-" + item.Name.Split('.')[0];
       
       //                  innerhtml.Append("<span class='iconwidth "+iconclass+"' value='"+iconclass+"' onclick='seticon(this)'>&nbsp;&nbsp;</span>
         
       //              } 
       //}
    }
}
