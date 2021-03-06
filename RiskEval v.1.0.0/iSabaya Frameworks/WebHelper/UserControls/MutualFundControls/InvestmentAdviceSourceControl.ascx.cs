using System;
using iSabaya;
using WebHelper;
using Resources;

//using Spring.Data.NHibernate;

public partial class InvestmentAdviceSourceControl : iSabayaControl
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
            InitializeControls();
    }

    public void InitializeControls()
    {
        ctrlCategory.SetValidation(ValidationGroup, IsRequiredField);
        rdnPanel.HeaderText = Resource_TradeTransaction.lblDecidedToExe;
        rdnPanel.Width = Width;
        ctrlCategory.ParentNode = iSabayaContext.imSabayaConfig.InvestmentAdviceSourceParentNode;
        ctrlCategory.DataBind();
        ctrlCategory.SelectedIndex = 0;
    }

    public TreeListNode InvestmentAdviceSource
    {
        get { return ctrlCategory.SelectedNode; }
    }
}