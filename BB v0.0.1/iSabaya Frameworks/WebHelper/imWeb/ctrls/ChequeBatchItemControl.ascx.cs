using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using iSabaya;
using WebHelper;

public partial class ctrls_ChequeBatchItemControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"] != null)
        {
            gridChequeBatchItems.DataSource = (List<ChequeBatchItem>)Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"];
            gridChequeBatchItems.DataBind();
        }

        //coke 13072009 hh:mm
        if (IsRequiredField)
        {
            textChequeBatchItemEdit.ValidationSettings.ValidationGroup = ValidationGroup;
            textChequeBatchItemEdit.ValidationSettings.SetFocusOnError = true;
            textChequeBatchItemEdit.ValidationSettings.ErrorText = "ErrorText";
            textChequeBatchItemEdit.ValidationSettings.ValidateOnLeave = true;
            textChequeBatchItemEdit.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            textChequeBatchItemEdit.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            textChequeBatchItemEdit.ValidationSettings.ErrorImage.AlternateText = "Error";
            textChequeBatchItemEdit.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            textChequeBatchItemEdit.ValidationSettings.RequiredField.IsRequired = true;
            textChequeBatchItemEdit.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
            textChequeBatchItemEdit.ValidationSettings.ErrorFrameStyle.ForeColor = System.Drawing.Color.Red;
            textChequeBatchItemEdit.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
            textChequeBatchItemEdit.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            textChequeBatchItemEdit.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            textChequeBatchItemEdit.ValidationSettings.ErrorFrameStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FD4D3E");
            textChequeBatchItemEdit.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            textChequeBatchItemEdit.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(0);
            textChequeBatchItemEdit.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);
        }
    }

    protected void cbSearch_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        IList<ChequeBatch> chequeBatchs = ChequeBatch.FindAvailable(iSabayaContext, BankAccountTextBoxControl21.BankAccount);
        IList<ChequeBatchItem> chequeBL = new List<ChequeBatchItem>();
        foreach (ChequeBatch cb in chequeBatchs)
        {
            foreach (ChequeBatchItem cbi in cb.ChequeBatchMembers)
            {
                if (cbi.Cheque == null)
                {
                    chequeBL.Add(cbi);
                    break;
                }
            }
        }

        Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"] = chequeBL;
        gridChequeBatchItems.DataSource = chequeBL;
        gridChequeBatchItems.DataBind();
    }

    protected void cbSelectedChequeBatchItem_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        int index = int.Parse(e.Parameter);
        ChequeBatchItem vo = (ChequeBatchItem)gridChequeBatchItems.GetRow(index);
        Session["ctrls_ChequeBatchItemControl_SelectedChequeBatchItem"] = vo;
    }

    protected void cbpTextChequeBatchItemEdit_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        ChequeBatchItem vo = (ChequeBatchItem)Session["ctrls_ChequeBatchItemControl_SelectedChequeBatchItem"];
        textChequeBatchItemEdit.Text = vo.ChequeNo.ToString();
        textChequeBatchItemEdit.Value = vo.ChequeBatchItemID;
        labelChequeBatchItem.Text = vo.ChequeNo.ToString();
    }

    public ChequeBatchItem ChequeBatchItem
    {
        get
        {
            ChequeBatchItem vo = (ChequeBatchItem)Session["ctrls_ChequeBatchItemControl_SelectedChequeBatchItem"];
            return vo;
        }
        set
        {
            textChequeBatchItemEdit.Text = value.ChequeNo.ToString();
        }
    }

    //coke 13072009 17:17

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