using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHelper
{
    public class WebPage
    {
        public WebPage(int id, string name, string url = null)
        {
            this.ID = id;
            this.Name = name;
            this.URL = url;
        }

        public int ID;
        public string Name;
        public string URL;
    }
}
