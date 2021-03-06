using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using imSabaya.MutualFundSystem;
using WebHelper;

public partial class ctrls_RegulationControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback || IsRaiseInitial)
        {
            InitailControl();
        }
    }

    private void InitailControl()
    {
        IList<Regulation> ips = Regulation.List(iSabayaContext);
        ComboRegulation.ValueType = typeof(int);
        ComboRegulation.Items.Clear();
        foreach (Regulation c in ips)
        {
            ComboRegulation.Items.Add(c.Code, c.RegulationID);
        }
        ComboRegulation.SelectedIndex = 0;

        if (IsRequiredField)
        {
            ComboRegulation.ValidationSettings.ValidationGroup = ValidationGroup;
            ComboRegulation.ValidationSettings.SetFocusOnError = true;
            ComboRegulation.ValidationSettings.ErrorText = "ErrorText";
            ComboRegulation.ValidationSettings.ValidateOnLeave = true;
            ComboRegulation.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            ComboRegulation.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            ComboRegulation.ValidationSettings.ErrorImage.AlternateText = "Error";
            ComboRegulation.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            ComboRegulation.ValidationSettings.RequiredField.IsRequired = true;
            ComboRegulation.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
            ComboRegulation.ValidationSettings.ErrorFrameStyle.ForeColor = System.Drawing.Color.Red;
            ComboRegulation.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
            ComboRegulation.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            ComboRegulation.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            ComboRegulation.ValidationSettings.ErrorFrameStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FD4D3E");
            ComboRegulation.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            ComboRegulation.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(0);
            ComboRegulation.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);
        }
    }

    //protected override void OnDataBinding(EventArgs e)
    //{
    //    base.OnDataBinding(e);
    //    if (RegulationID > 0)
    //    {
    //        ListEditItem item = ComboRegulation.Items.FindByValue(RegulationID);
    //        if (item != null)
    //            item.Selected = true;
    //    }
    //}
    public Regulation Regulation
    {
        get
        {
            if (ComboRegulation.SelectedItem == null) return null;
            int id = (int)ComboRegulation.SelectedItem.Value;
            Regulation reg = Regulation.Find(iSabayaContext, id);
            return reg;
        }
        set
        {
            if (value != null)
            {
                foreach (ListEditItem item in ComboRegulation.Items)
                {
                    if (item.Value.Equals(value.RegulationID))
                    {
                        ComboRegulation.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }

    [Bindable(true, BindingDirection.TwoWay)]
    public object RegulationID
    {
        get
        {
            if (ComboRegulation.SelectedItem == null)
                return 0;
            return ComboRegulation.SelectedItem.Value;
        }
        set
        {
            int id = value == null ? 0 : (int)value;
            if (id > 0)
            {
                ListEditItem item = ComboRegulation.Items.FindByValue(value);
                if (item != null)
                    item.Selected = true;
            }
        }
    }

    private bool isRaiseInitial = false;

    public bool IsRaiseInitial
    {
        get { return isRaiseInitial; }
        set { isRaiseInitial = value; }
    }

    //coke 13072009 13:09

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
}