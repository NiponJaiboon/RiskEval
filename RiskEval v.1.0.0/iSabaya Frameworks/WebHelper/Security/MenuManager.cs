using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using iSabaya;
using NHibernate;
using DevExpress.Web.ASPxSiteMapControl;
using System.Web.SessionState;
using System.Threading;
using System.Globalization;
using System.Resources;

/// <summary>
/// Summary description for MenuManager
/// </summary>
namespace WebHelper
{
    public class MenuManager
    {
        public MenuManager()
        {
        }
        public static MySiteMapProvider BuildMenu(iSabaya.Context context, IList<DynamicMenu> menus)
        {
            MySiteMapProvider provider = new MySiteMapProvider(menus);
            return provider;
        }
    }
}