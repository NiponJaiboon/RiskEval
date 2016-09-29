using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using iSabaya;
using DevExpress.Web.ASPxGridView;

namespace WebHelper
{
    public class DataTextItemTemplate : ITemplate
    {
        private Control parent;
        private string fieldName;
        private Type dataType;

        public DataTextItemTemplate(string fieldName, Type type)
        {
            this.fieldName = fieldName;
            this.dataType = type;
        }
        public void InstantiateIn(Control container)
        {
            parent = container;
            object value = DataBinder.Eval(((GridViewDataItemTemplateContainer)parent).DataItem, fieldName);
            string valueStr = "&nbsp;";
            if (value != null)
            {

                //if (this.dataType == typeof(iSabaya.RateType))
                //    valueStr = Enum.GetName(this.dataType, value);
                //else
                    valueStr = value.ToString();
            }
            Label label = new Label();
            label.Text = valueStr;
            parent.Controls.Add(label);
        }
    }
}
