using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using iSabaya;

namespace WebHelper
{
    public class MySiteMapProvider : StaticSiteMapProvider
    {
        SiteMapNodeHelper rootNode;
        public new SiteMapNodeHelper RootNode
        {
            get { return this.rootNode; }
            set { this.rootNode = value; }
        }
        private IList<DynamicMenu> menus = null;
        public IList<DynamicMenu> Menus
        {
            get { return menus; }
            set { menus = value; }
        }

        public MySiteMapProvider()
        {
        }
        public MySiteMapProvider(IList<DynamicMenu> menus)
        {
            this.menus = menus;
        }

        public MySiteMapProvider(SiteMapNodeHelper root)
        {
            this.rootNode = root;
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
        {
            base.Initialize(name, attributes);
            if (attributes == null)
                throw new ConfigurationErrorsException("Missing ConnectionStringName attribute.");
        }

        protected override void Clear()
        {
            //root = null;
            base.Clear();
            rootNode = null;
        }

        public override SiteMapNode BuildSiteMap()
        {
            if (base.RootNode == null)
            {
                if (rootNode == null)
                {
                    this.Clear();
                    rootNode = new SiteMapNodeHelper(this, 0, "Home", "Home", "~/default.aspx", null);
                    buildNode(this.menus, this.rootNode);
                }
            }
            return rootNode;
        }

        private void buildNode(IList<DynamicMenu> menus, SiteMapNodeHelper parent)
        {
            if (parent == null)
                return;
            if (menus == null)
                return;
            for (int i = 0; i < menus.Count; i++)
            {
                DynamicMenu menu = menus[i];
                if (menu.Show && menu.SeqNo >= 0)
                {
                    SiteMapNodeHelper child = new SiteMapNodeHelper(this, menu.Id, menu.Code, menu.PageCode, menu.LinkURL, null);
                    AddNode(child, parent);
                    if (menu.Children.Count > 0)
                    {
                        //child.Url = "";
                        buildNode(menu.Children, child);
                    }
                }
            }
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            return rootNode;
        }
    }
}