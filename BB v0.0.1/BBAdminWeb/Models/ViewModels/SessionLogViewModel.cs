using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBAdminWeb.Models.ViewModels
{
    public class SessionLogViewModel
    {
        public string SessionID { get; set; }
        public string ApplocationName { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string Page { get; set; }
        public string TimeStamp { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }

    }
}