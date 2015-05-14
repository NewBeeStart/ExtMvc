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
    public class DictionaryApiController : ApiControllerBase<DictionaryRepository, Dictionary>
    {

        public DictionaryApiController()
        {
            base.SetRepository();
        }

        /// <summary>
        /// 获取所有字典信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public dynamic GetAllDictionary()
        {
            var list = base.Get();
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

        /// <summary>
        /// 根据Code获取子字典的元素
        /// </summary>
        /// <param name="pcode"></param>
        /// <returns></returns>

        [HttpGet]
        [HttpPost]
        public IEnumerable<Dictionary> GetDictionaryByPCode(string pcode) 
       {
           var pdics = base.Repository.Query(dic => dic.Code == pcode);
           if ( pdics!= null&&pdics.Count<Dictionary>()>0) 
           {
               Dictionary pdic = base.Repository.Query(dic => dic.Code == pcode).First();
               var lst = base.Repository.Query(dic => dic.PDictionaryID == pdic.ID).ToList();
               return lst;
           }
           return null;
        }

    }
}
