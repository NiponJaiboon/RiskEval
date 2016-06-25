using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

namespace WebHelper
{
    public class TitlePanelTemplate : ITemplate
    {
        private GridViewTitleTemplateContainer parent;
        private string gdvClientID;
        private ASPxButton btnAdd = null;
        private string title;
        public TitlePanelTemplate(string gridClientID, ASPxButton btnAdd)
        {
            this.gdvClientID = gridClientID;
            this.btnAdd = btnAdd;
        }
        public TitlePanelTemplate(string gridClientID, string title, ASPxButton btnAdd)
        {
            this.gdvClientID = gridClientID;
            this.btnAdd = btnAdd;
            this.title = title;
        }
        public void InstantiateIn(Control container)
        {
            parent = (GridViewTitleTemplateContainer)container;
            if (btnAdd != null)
            {
                Table tb = new Table() { Width = Unit.Percentage(100) };
                tb.Attributes.Add("align", "left");
                TableRow tr = new TableRow();
                TableCell td = new TableCell();
                td.Attributes.Add("align", "left");
                td.Controls.Add(btnAdd);
                tr.Controls.Add(td);
                tb.Controls.Add(tr);
                btnAdd.ClientSideEvents.Click = @"function(s,e)
                {
                    " + gdvClientID + @".AddNewRow();
                }";
                parent.Controls.Add(tb);
            }
        }
    }
}
