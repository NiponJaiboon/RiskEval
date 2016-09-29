using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using iSabaya;
using WebHelper;

public partial class ctrls_FlagByLanguage : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ASPxBinaryImage img = (ASPxBinaryImage)FindControl(base.LanguageCode);
            img.Border.BorderWidth = Unit.Pixel(1);
        }
        else
        {
            if (!Page.IsCallback)
            {
                string languageCode = base.LanguageCode;
                //IList<Language> listLanguage = Language.List(iSabayaContext);
                foreach (Language l in Language.List(iSabayaContext))
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
        //IList<Language> listLanguage = Language.List(iSabayaContext);
        String idName;
        HtmlTableRow hr = new HtmlTableRow();
        foreach (Language l in Language.List(iSabayaContext))
        {
            idName = l.Code.ToString();
            ASPxBinaryImage aspxImage = new ASPxBinaryImage();
            //aspxImage.Border.BorderWidth = Unit.Pixel(0);
            HtmlTableCell hc = new HtmlTableCell();
            aspxImage.ToolTip = l.Code;
            aspxImage.ContentBytes = ImageUtil.ImageToBytes(l.SmallImage);
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