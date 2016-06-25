using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizPortalAdminWeb.Util
{
    public class Advertise
    {
        public int ID { get; set; }
        public string Detail { get; set; }
        public string LargeImage { get; set; }
        public string Link { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Advertise() { }
    }
}