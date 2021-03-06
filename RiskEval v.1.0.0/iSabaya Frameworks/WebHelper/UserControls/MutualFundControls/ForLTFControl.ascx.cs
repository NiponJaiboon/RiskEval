using System;
using WebHelper;
using Resources;

public partial class ForLTFControl : iSabayaControl
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
    private int width = 0;

    public int Width
    {
        get { return this.width; }
        set { this.width = value; }
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if(!Page.IsCallback)
            InitializeControls();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            rdnPanel.Width = Width;
            InitializeResources();
        }
    }

    public void InitializeControls()
    {
        rdbtnTransferToDestinationFundUponLiquidation.Items.Clear();
        rdbtnTransferToDestinationFundUponLiquidation.Items.Add(Resource_WarnMassage.ForLTFAgreeText, true);
        rdbtnTransferToDestinationFundUponLiquidation.Items.Add(Resource_WarnMassage.ForLTFDisAgreeText, false);
        rdbtnTransferToDestinationFundUponLiquidation.SelectedIndex = 
            rdbtnTransferToDestinationFundUponLiquidation.Items.Count - 1;
    }
    public void InitializeResources()
    {
        rdnPanel.HeaderText = Resource_TradeTransaction.lblForLTF;
        lblMassage.Text = Resource_WarnMassage.ForLTFText;
    }

    public bool TransferToDestinationFundUponLiquidation
    {
        get { return (bool)rdbtnTransferToDestinationFundUponLiquidation.SelectedItem.Value; }
    }
}