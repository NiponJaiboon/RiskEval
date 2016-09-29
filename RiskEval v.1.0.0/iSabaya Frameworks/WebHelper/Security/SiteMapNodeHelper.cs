using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace WebHelper
{
    public class SiteMapNodeHelper : SiteMapNode
    {
        private List<SiteMapNodeHelper> children = new List<SiteMapNodeHelper>();
        public List<SiteMapNodeHelper> Children
        {
            get { return children; }
            set { children = value; }
        }
        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public SiteMapNodeHelper() : base(null, null)
        {
        }
        public SiteMapNodeHelper(SiteMapProvider provider, int key)
            : base(provider, key.ToString())
        {
        }
        public SiteMapNodeHelper(int key, string code, string title, string url, string description)
            : base(null, key.ToString(), url, title, description)
        {
            Code = code;
        }
        public SiteMapNodeHelper(SiteMapProvider provider, int key, string code, string title, string url, string description)
            : base(provider, key.ToString(), url, title, description)
        {
            Code = code;
        }
    }
}
