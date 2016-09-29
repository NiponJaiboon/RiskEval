using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxTabControl;
using System.Drawing;

namespace BizPortalAdminWeb.UserControls
{
    public partial class Ads : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void AdsTabControl_TabDataBound(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
        {
            e.Tab.Text = e.Tab.Name = DataBinder.Eval(e.Tab.DataItem, "ID").ToString();
        }

        protected void AdsTabControl_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
        {
            Color[] co = new Color[] { Color.FromArgb(255, 0, 0), Color.FromArgb(205, 0, 0), Color.FromArgb(155, 0, 0), Color.FromArgb(105, 0, 0), Color.FromArgb(55, 0, 0) };

            var pageIDHash = new Hashtable();
            foreach (Tab tab in AdsTabControl.Tabs)
            {
                pageIDHash[tab.Name] = GetAdsPageID(DataBinder.Eval(tab.DataItem, "ID"));
                AdsTabControl.Tabs[tab.Index].TabStyle.BackColor = co[tab.Index];
            }
            e.Properties["cpPageIDHash"] = pageIDHash;
        }

        protected string GetAdsPageID(object id)
        {
            return string.Format("{0}_{1}Page", ClientID, id);
        }

    }
}