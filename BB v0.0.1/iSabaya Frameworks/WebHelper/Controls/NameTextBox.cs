using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxCallbackPanel;
using iSabaya;

namespace WebHelper.Controls
{
    [ToolboxData("<{0}:NameTextBox id=NameTextBox{1} runat=server></{0}:NameTextBox>")]
    public class NameTextBox : iSabayaWebControlBase
    {
        //ASPxCallbackPanel cbp = null;
        //Table tbControl = null;
        
        //private MultilingualString value;
        //public MultilingualString Value
        //{
        //    get
        //    {
        //        string code = string.Empty;
        //        if (hddID.Value == "0" || string.IsNullOrEmpty(hddID.Value))
        //        {
        //            value = new MultilingualString();
        //            for (int i = 0; i < listTextBox.Count; i++)
        //            {
        //                code = listTextBox[i].ID.Substring(listTextBox[i].ID.LastIndexOf("txt_") + 4).Replace('_','-');
        //                value.Values.Add(new MLSValue() { Owner = value, Language = new Language(code), Value = listTextBox[i].Text });
        //            }
        //        }
        //        else
        //        {
        //            value = iSabayaContext.PersistencySession.Get<MultilingualString>(int.Parse(hddID.Value));
        //            for (int i = 0; i < listTextBox.Count; i++)
        //            {
        //                code = listTextBox[i].ID.Substring(listTextBox[i].ID.LastIndexOf("txt_") + 4).Replace('_', '-');
        //                value.AddOrReplace(new Language(code), listTextBox[i].Text);
        //            }
        //        }
        //        return value;
        //    }
        //    set
        //    {
        //        if (value != null)
        //            hddID.Value = "0";
        //        else
        //        {
        //            hddID.Value = value.MLSID.ToString();
        //            this.value = value;
        //            string code;
        //            if (listTextBox != null)
        //            {
        //                for (int i = 0; i < listTextBox.Count; i++)
        //                {
        //                    code = listTextBox[i].ID.Substring(listTextBox[i].ID.LastIndexOf("txt_") + 4).Replace('_', '-');
        //                    listTextBox[i].Text = this.value.GetValue(code);
        //                }
        //            }
        //        }
        //    }
        //}
        //protected override void  OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    listTextBox = new List<ASPxButtonEdit>();
        //    string code = null;
        //    for (int i = 0; i < iSabayaContext.Languages.Count; i++)
        //    {
        //        ASPxButtonEdit txt = new ASPxButtonEdit();
        //        code = iSabayaContext.Languages[i].Code.Replace('-', '_');
        //        txt.ID = string.Format("{0}_txt_{1}", this.ClientID, code);
        //        txt.ClientInstanceName = txt.ID;
        //        txt.Width = Width;
        //        EditButton btn = new EditButton();
        //        btn.Enabled = false;
        //        btn.ToolTip = iSabayaContext.Languages[i].Title;
        //        txt.ButtonStyle.Border.BorderWidth = Unit.Pixel(0);
        //        txt.ButtonStyle.Paddings.Padding = Unit.Pixel(0);
        //        txt.Buttons.Add(btn);
        //        txt.ButtonTemplate = new ButtonFlagImage(iSabayaContext.Languages[i].Code, ImageUtil.ImageToBytes(iSabayaContext.Languages[i].SmallImage));
        //        if (IsRequiredField)
        //            txt.SetValidation(ValidationGroup);
        //        listTextBox.Add(txt);
        //    }
        //    tbControl = new Table() { CellPadding = 0, CellSpacing = 0 };
        //    hddID = new HiddenField();
        //}

        ////protected override object SaveControlState()
        ////{
        ////    object obj = base.SaveControlState();
        ////    return new Pair(obj, IsSuffix);
        ////}
        ////protected override void LoadControlState(object savedState)
        ////{
        ////    Pair p = (Pair)savedState;
        ////    base.LoadControlState(p.First);
        ////    IsSuffix = (bool)p.Second;
        ////}
        //protected override void CreateChildControls()
        //{
        //    this.Controls.Add(hddID);
        //    this.Controls.Add(tbControl);
        //    for (int i = 0; i < listTextBox.Count; i++)
        //    {
        //        TableRow row = new TableRow();
        //        row.Cells.Add(new TableCell() { HorizontalAlign = HorizontalAlign.Left, VerticalAlign = VerticalAlign.Top });
        //        row.Cells[0].Controls.Add(listTextBox[i]);
        //        row.Attributes.Add("style", "padding-bottom:2px");
        //        tbControl.Rows.Add(row);
        //    }
        //}
    }
    
}
