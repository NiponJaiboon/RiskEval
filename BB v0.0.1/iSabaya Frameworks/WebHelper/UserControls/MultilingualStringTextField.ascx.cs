using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using iSabaya;
using WebHelper;

public partial class ctrls_MultilingualStringTextField : iSabayaControl
{
    //IList<String> languages;

    public ctrls_MultilingualStringTextField()
    {
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public String TextLang1ClientInstanceName
    {
        get { return txtLang1.ClientInstanceName; }
        set { txtLang1.ClientInstanceName = value; }
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public String TextLang2ClientInstanceName
    {
        get { return txtLang2.ClientInstanceName; }
        set { txtLang2.ClientInstanceName = value; }
    }

    private bool expand = false;

    public bool Expand
    {
        get { return expand; }
        set { this.expand = value; }
    }

    //coke 10072009 16:41

    #region Validation Section

    private bool isRequiredField = false;

    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    private String validationGroup;

    /// <summary>
    /// Get or sets the group of controls for which the editor forces validation when it posts back to the server.
    /// </summary>
    public String ValidationGroup
    {
        get { return validationGroup; }
        set { this.validationGroup = value; }
    }

    #endregion Validation Section

    private String uId = "";

    public String UID
    {
        get
        {
            if (uId == null)
                return this.ID;
            else
            {
                if (uId.Length == 0)
                    return this.ID;
                else
                    return uId;
            }
        }
        set
        {
            this.uId = value;
            ViewState["UID"] = this.uId;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        uId = (String)ViewState["UID"];
        //lblLang1.Text = "ไทย";
        //lblLang2.Text = "English";
        txtLang1.ID = "th" + this.UID;
        txtLang2.ID = "en" + this.UID;

        pcHint.ClientInstanceName = this.ClientID + pcHint.ID;
        if (String.IsNullOrEmpty(txtLang1.ClientInstanceName))
            txtLang1.ClientInstanceName = this.ClientID + txtLang1.ID;
        if (String.IsNullOrEmpty(txtLang2.ClientInstanceName))
            txtLang2.ClientInstanceName = this.ClientID + txtLang2.ID;

        txtLang1.ClientSideEvents.GotFocus = @"function(s,e)
        {
			var hintWindow = " + pcHint.ClientInstanceName + @".GetWindowByName('Lang1');
			" + pcHint.ClientInstanceName + @".ShowWindow(hintWindow);
        }";

        txtLang1.ClientSideEvents.LostFocus = @"function(s,e)
        {
			var hintWindow = " + pcHint.ClientInstanceName + @".GetWindowByName('Lang1');
			" + pcHint.ClientInstanceName + @".HideWindow(hintWindow);
        }";

        txtLang2.ClientSideEvents.GotFocus = @"function(s,e)
        {
			var hintWindow = " + pcHint.ClientInstanceName + @".GetWindowByName('Lang2');
			" + pcHint.ClientInstanceName + @".ShowWindow(hintWindow);
        }";

        txtLang2.ClientSideEvents.LostFocus = @"function(s,e)
        {
			var hintWindow = " + pcHint.ClientInstanceName + @".GetWindowByName('Lang2');
			" + pcHint.ClientInstanceName + @".HideWindow(hintWindow);
        }";

        for (int i = 0; i < pcHint.Windows.Count; i++)
        {
            pcHint.Windows[i].ShowOnPageLoad = false;
        }
        pcHint.Windows[0].PopupElementID = txtLang1.ClientID;
        pcHint.Windows[1].PopupElementID = txtLang2.ClientID;

        #region Validate

        //coke 10072009 16:41
        if (IsRequiredField)
        {
            txtLang1.ValidationSettings.ValidationGroup = ValidationGroup;
            txtLang2.ValidationSettings.ValidationGroup = ValidationGroup;

            txtLang1.ValidationSettings.SetFocusOnError = true;
            txtLang1.ValidationSettings.ErrorText = "ErrorText";
            txtLang1.ValidationSettings.ValidateOnLeave = true;
            txtLang1.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            txtLang1.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            txtLang1.ValidationSettings.ErrorImage.AlternateText = "Error";
            txtLang1.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            txtLang1.ValidationSettings.RequiredField.IsRequired = true;
            txtLang1.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
            txtLang1.ValidationSettings.ErrorFrameStyle.ForeColor = Color.Red;
            txtLang1.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
            txtLang1.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            txtLang1.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            txtLang1.ValidationSettings.ErrorFrameStyle.Border.BorderColor = ColorTranslator.FromHtml("#FD4D3E");
            txtLang1.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            txtLang1.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(0);
            txtLang1.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);

            txtLang2.ValidationSettings.SetFocusOnError = true;
            txtLang2.ValidationSettings.ErrorText = "ErrorText";
            txtLang2.ValidationSettings.ValidateOnLeave = true;
            txtLang2.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            txtLang2.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            txtLang2.ValidationSettings.ErrorImage.AlternateText = "Error";
            txtLang2.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            txtLang2.ValidationSettings.RequiredField.IsRequired = true;
            txtLang2.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
            txtLang2.ValidationSettings.ErrorFrameStyle.ForeColor = Color.Red;
            txtLang2.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
            txtLang2.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            txtLang2.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            txtLang2.ValidationSettings.ErrorFrameStyle.Border.BorderColor = ColorTranslator.FromHtml("#FD4D3E");
            txtLang2.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            txtLang2.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(0);
            txtLang2.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);
            /*
              <ValidationSettings SetFocusOnError="True" ErrorText="ErrorText" ValidateOnLeave="true"
                                                        ValidationGroup="SaveGroup">
                                                        <ErrorImage Height="16px" Width="16px" AlternateText="Error" Url="../../Images/iconError.png" />
                                                        <RequiredField IsRequired="True" ErrorText="กรุณากรอกข้อมูล" />
                                                        <ErrorFrameStyle ForeColor="Red">
                                                            <Paddings Padding="3px" PaddingLeft="4px" />
                                                            <BackgroundImage ImageUrl="~/Images/bgError.png" />
                                                            <Border BorderColor="#FD4D3E" BorderStyle="Solid" BorderWidth="1px" />
                                                            <ErrorTextPaddings PaddingRight="3px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
             */
        }

        #endregion Validate

        #region MyRegion

        /*

        languages = new List<String>();
        languages.Add("th");
        languages.Add("en");
        Session["languages"] = languages;
        if (IsPostBack == false)
        {
            //List<ASPxTextBox> textBoxs = new List<ASPxTextBox>();
            foreach (String lang in languages)
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                ASPxLabel labe1 = new ASPxLabel();
                labe1.Text = lang;
                labe1.CssClass = "defaultFont";
                cell1.Controls.Add(labe1);
                ASPxTextBox textInput = new ASPxTextBox();
                textInput.EnableViewState = true;
                textInput.EnableClientSideAPI = true;

                ASPxCallback callBack = new ASPxCallback();
                callBack.ClientInstanceName = "Callback" + textInput.ID;
                callBack.Callback += new CallbackEventHandler(callBack_Callback);

                textInput.ClientSideEvents.LostFocus = "function(s, e) { alert(Callback" + textInput.ID + "); }";
                textInput.Width = 400;

                textInput.ID = lang + this.UID;
                TableCell cell2 = new TableCell();
                cell2.Controls.Add(textInput);

                row.Cells.Add(cell1);

                row.Cells.Add(cell2);

                Table1.Rows.Add(row);
                textBoxs.Add(textInput);
            }
            Session["textBoxs" + this.UID] = textBoxs;
        }
        else if (IsPostBack)
        {
            List<ASPxTextBox> textBoxs = (List<ASPxTextBox>)Session["textBoxs" + this.UID];
            int i = 0;
            foreach (String lang in languages)
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                Label labe1 = new Label();
                labe1.Text = lang;
                cell1.Controls.Add(labe1);
                ASPxTextBox textInput = textBoxs[i++];

                TableCell cell2 = new TableCell();
                cell2.Controls.Add(textInput);

                row.Cells.Add(cell1);

                row.Cells.Add(cell2);

                Table1.Rows.Add(row);
            }
        }
        */

        #endregion MyRegion
    }

    public void setVal(String code, String value)
    {
        if (code.Equals("th-TH"))
        {
            txtLang1.Text = value;
        }
        else if (code.Equals("en-US"))
        {
            txtLang2.Text = value;
        }
    }

    public void Clear()
    {
        txtLang1.Text = "";
        txtLang2.Text = "";
    }

    public new MultilingualString Value
    {
        get
        {
            MultilingualString mls = new MultilingualString();
            DateTime updatedTS = DateTime.Now;

            Language lang = Language.FindByCode(iSabayaContext, "th-TH");
            //MLSValuePk pk = new MLSValuePk(mls, lang);
            //MLSValue value = new MLSValue(pk, txtLang1.Text);
            MLSValue value = new MLSValue(mls, lang.Code, txtLang1.Text);
            value.UpdatedTS = updatedTS;
            mls.Values.Add(value);

            lang = Language.FindByCode(iSabayaContext, "en-US");
            //pk = new MLSValuePk(mls, lang);
            //value = new MLSValue(pk, txtLang2.Text);
            value = new MLSValue(mls, lang.Code, txtLang2.Text);
            value.UpdatedTS = updatedTS;
            mls.Values.Add(value);

            return mls;
        }
        set
        {
            MultilingualString mls = value;
            if (mls != null)
            {
                if (mls.Values.Count > 0)
                {
                    this.txtLang1.Text = mls.GetValue("th-TH");
                    this.txtLang2.Text = mls.GetValue("en-US");
                }
            }
        }
    }

    public override string Text
    {
        get
        {
            return Value.ToString(base.LanguageCode);
        }
    }
}
