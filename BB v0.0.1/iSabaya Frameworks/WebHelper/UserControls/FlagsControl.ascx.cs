using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iSabaya;
using System.Web;
using System.Web.Security;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxCallback;
using DevExpress.Web.ASPxSiteMapControl;
using WebHelper;

public partial class FlagsControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ASPxBinaryImage img = (ASPxBinaryImage)FindControl(base.LanguageCode);
            if (img != null)
                img.Border.BorderWidth = Unit.Pixel(1);
        }
        else
        {
            if (!Page.IsCallback)
            {
                string languageCode = base.LanguageCode;
                foreach (Language l in Language.Languages)
                {
                    String code = l.Code.ToString();
                    ASPxBinaryImage img = (ASPxBinaryImage)FindControl(code);
                    if (code == languageCode)
                        img.Border.BorderWidth = Unit.Pixel(1);
                    else
                        img.Border.BorderWidth = Unit.Pixel(0);
                }
            }
        }
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
        {
            InitializeControls();
        }
    }

    private void InitializeControls()
    {
        String idName;
        HtmlTableRow hr = new HtmlTableRow();
        foreach (Language l in Language.Languages)
        {
            idName = l.Code.ToString();
            ASPxBinaryImage aspxImage = new ASPxBinaryImage();
            //aspxImage.Border.BorderWidth = Unit.Pixel(0);
            HtmlTableCell hc = new HtmlTableCell();
            aspxImage.ToolTip = l.Code;
            if (l.SmallImageBytes != null)
            {
                byte[] bytes = new byte[l.SmallImageBytes.Length];
                l.SmallImageBytes.CopyTo(bytes, 0);
                aspxImage.ContentBytes = bytes;
            }
            //aspxImage.EmptyImage.Url = "~/Images/flags/th.png";
            hc.Controls.Add(aspxImage);
            hr.Cells.Add(hc);
            ///////////////// Client Event ///////////////////
            aspxImage.ID = idName;
            aspxImage.ClientInstanceName = idName + this.ID;
            aspxImage.ClientSideEvents.Click = @"function(s,e)
            {
                var form1 = document.forms[0];
                if (!form1.onsubmit || (form1.onsubmit() != false)) 
                {
                    form1.__EVENTTARGET.value = '__Page';
                    form1.__EVENTARGUMENT.value = 'language," + idName + @"';
                    form1.submit();
                }
            }";
            //////////////////////////////////////////////////
        }
        tableFlag.Rows.Add(hr);
    }
}
