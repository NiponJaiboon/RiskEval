using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using DevExpress.Web.ASPxEditors;
using WebHelper.Controls;
using iSabaya;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WebHelper
{
    public class RowInConfirmPopup
    {
        String title;
        public String Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        Control control;
        public Control Control
        {
            get { return this.control; }
            set { this.control = value; }
        }

        String detail;
        public String Detail
        {
            get { return this.detail; }
            set { this.detail = value; }
        }

        Object value;
        public Object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public RowInConfirmPopup(String Title , Control control)
        {
            this.Title = Title;
            this.Control = control;
        }

        public RowInConfirmPopup(String Title, Control control, Object value)
        {
            this.Title = Title;
            this.Control = control;
            this.Value = value;
        }

        public RowInConfirmPopup(String Title, object value)
        {
            this.Title = Title;
            this.Value = value;
        }
    }
}
