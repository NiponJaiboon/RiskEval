using System;
using imSabaya.MutualFundSystem;
using WebHelper;
using Resources;

public partial class WarnMassageControl : iSabayaControl
{
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

    private String transactionTypeCode = null;

    public String TransactionTypeCode
    {
        get { return this.transactionTypeCode; }
        set { this.transactionTypeCode = value; }
    }

    private int width = 0;

    public int Width
    {
        get { return this.width; }
        set { this.width = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
            InitializeControls();
    }

    public void InitializeControls()
    {
        rdnPanel.HeaderText = Resource_TradeTransaction.lblWarning;
        rdnPanel.Width = Width;

        lblWraningMassage.Text = Resource_WarnMassage.RiskWarnMassageText;
        if (TransactionTypeCode == MFConstants.TransTypeCodePurchase ||
                TransactionTypeCode == MFConstants.TransTypeCodeIPOPurchase)
            lblWraningTranTypeMassage.Text = Resource_WarnMassage.SubscriptionWarningText;
        else if (TransactionTypeCode == MFConstants.TransTypeCodeRedemption)
            lblWraningTranTypeMassage.Text = Resource_WarnMassage.RedemptionWraningTex;
        else if (TransactionTypeCode == MFConstants.TransTypeCodeSwitch ||
                    TransactionTypeCode == MFConstants.TransTypeCodeTransfer ||
                    TransactionTypeCode == MFConstants.TransTypeCodeExternalSwitchIn ||
                    TransactionTypeCode == MFConstants.TransTypeCodeExternalSwitchOut)
            lblWraningTranTypeMassage.Text = Resource_WarnMassage.SwitchingTransferText;
    }
}