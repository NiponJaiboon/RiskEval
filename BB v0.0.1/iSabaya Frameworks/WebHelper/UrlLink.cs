using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper
{
    public class UrlLink
    {
        public string URL { get; set; }
        public MultilingualString Title { get; set; }
        public UrlLink(string url, MultilingualString title)
        {
            URL = url;
            Title = title;
        }
    }
}
