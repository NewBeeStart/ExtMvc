using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.XXXX.EasyUIModels
{
    public class ExtTree
    {
        public string id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; } 
        public bool leaf { get; set; } 
        public string url { get; set; }
        public object attributes{get;set;}
        List<ExtTree> _children = new List<ExtTree>();
        public List<ExtTree> children
        {
            get { return _children; }
            set { _children = value; }
        }
    }

}
