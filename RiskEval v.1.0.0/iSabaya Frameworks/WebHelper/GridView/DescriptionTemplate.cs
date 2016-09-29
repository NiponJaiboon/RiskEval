using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxHiddenField;
using System.Web.UI.WebControls;
using System.Web;

namespace WebHelper
{
    public class DescriptionTemplate : ITemplate
    {
        private Control parent;
        private object isFinalState;
        private object description;
        //private object transactionID;
        private ASPxTextBox txtDescription;
        public string TextBoxDescriptioClientID
        { 
            get { return txtDescription.ClientID; } 
        }
        public void InstantiateIn(Control container)
        {
            parent = container;
            isFinalState = DataBinder.Eval(((GridViewDataItemTemplateContainer)parent).DataItem, "IsFinalState");
            description = DataBinder.Eval(((GridViewDataItemTemplateContainer)parent).DataItem, "Description");
            if (!(bool)isFinalState)
                CreateTextBox();
            else
                CreateLabelNone();
        }
        private void CreateTextBox()
        {
            txtDescription = new ASPxTextBox()
            {
                ID = "txtDescription",
                Width = Unit.Pixel(170),
            };
            txtDescription.Text = description != null ? description.ToString() : "";
            parent.Controls.Add(txtDescription);
        }
   
        private void CreateLabelNone()
        {
            Label label = new Label() { Text = "&nbsp;" };
            parent.Controls.Add(label);
        }
    }
}
