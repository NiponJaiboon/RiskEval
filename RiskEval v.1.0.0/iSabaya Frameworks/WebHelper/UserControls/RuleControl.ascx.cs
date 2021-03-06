using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using NHibernate;
using WebHelper;

public partial class ctrls_RuleControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            IList<iSabaya.Rule> rules =
                iSabaya.Rule.List(iSabayaContext);
            foreach (iSabaya.Rule r in rules)
            {
                ComboRule.Items.Add(r.Name, r.ID);
            }
            ComboRule.SelectedIndex = 0;
        }

        //coke 13072009 13:19
        if (IsRequiredField)
        {
            ComboRule.ValidationSettings.ValidationGroup = ValidationGroup;

            ComboRule.ValidationSettings.SetFocusOnError = true;
            ComboRule.ValidationSettings.ErrorText = "ErrorText";
            ComboRule.ValidationSettings.ValidateOnLeave = true;
            ComboRule.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            ComboRule.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            ComboRule.ValidationSettings.ErrorImage.AlternateText = "Error";
            ComboRule.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            ComboRule.ValidationSettings.RequiredField.IsRequired = true;
            ComboRule.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
            ComboRule.ValidationSettings.ErrorFrameStyle.ForeColor = System.Drawing.Color.Red;
            ComboRule.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
            ComboRule.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            ComboRule.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            ComboRule.ValidationSettings.ErrorFrameStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FD4D3E");
            ComboRule.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            ComboRule.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(0);
            ComboRule.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);
        }
    }

    //coke 13072009 13:18

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

    public iSabaya.Rule Rule
    {
        get
        {
            if (ComboRule.SelectedItem == null) return null;
            String id = (String)ComboRule.SelectedItem.Value;
            iSabaya.Rule rule = iSabaya.Rule.Find(iSabayaContext, int.Parse(id));
            return rule;
        }
        set
        {
            if (value != null)
            {
                foreach (ListEditItem item in ComboRule.Items)
                {
                    if (item.Value.Equals(value.ID.ToString()))
                    {
                        ComboRule.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }
}