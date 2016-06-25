using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Web;
using iSabaya;
using DevExpress.Web.ASPxEditors;

namespace WebHelper
{
    public static class WebUtil
    {
        public static void SetPropertyByName(string controlPropName, System.Web.UI.UserControl control, string val)
        {
            PropertyInfo[] propertyInfos = control.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanRead && propertyInfo.Name == controlPropName)
                {
                    propertyInfo.SetValue(control, val, null);
                    break;
                }
            }
        }
        public static void AppendEnumToCombo(Type t, ASPxComboBox combo)
        {
            string[] names = Enum.GetNames(t);
            int i = 0;
            foreach (string str in names)
            {
                combo.Items.Add(new ListEditItem(str, Enum.ToObject(t, i++)));
            }
        }
    }
}