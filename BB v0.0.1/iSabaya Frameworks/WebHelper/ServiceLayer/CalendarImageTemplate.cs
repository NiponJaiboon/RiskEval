using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web.ASPxEditors;
using Resources;

namespace WebHelper.ServiceLayer
{
    public class CalendarImageTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            ASPxImage img = new ASPxImage { ID = DateTime.Now.ToString("ddMMyyHHmmssffff"), ImageUrl = ResImageURL.Calendar, Width = 16, Height = 16 };
            container.ID = DateTime.Now.ToString("ddMMyyHHmmssffff");
            container.Controls.Add(img);
        }
    }
}