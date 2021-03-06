using System;
using System.Web.UI;
using DevExpress.Web.ASPxEditors;
using iSabaya;
using WebHelper;

public partial class ctrls_MoneyControl : iSabayaControl
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

    public decimal MaxValue
    {
        get { return txtAmount.MaxValue; }
        set { txtAmount.MaxValue = value; }
    }

    public decimal MinValue
    {
        get { return txtAmount.MinValue; }
        set { txtAmount.MinValue = value; }
    }

    public decimal DefaultValue { get; set; }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
        {
            InitializeControls();
        }
        if (!IsPostBack)
        {
            txtAmount.SetValidation(ValidationGroup);
            ComboCurrency.SetValidation(ValidationGroup);
        }
    }

    public void InitializeControls()
    {
        txtAmount.DisplayFormatString = base.CurrencyFormat;
        if (txtAmount.MaxValue == 0 && txtAmount.MinValue == 0)
        {
            txtAmount.MinValue = 0;
            txtAmount.MaxValue = decimal.MaxValue;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            foreach (Currency c in Currency.Currencies)
            {
                ComboCurrency.Items.Add(c.Code, c.Code);
            }
            ComboCurrency.SelectedIndex = 0;
        }

        if (IsRequiredField)
            ComboCurrency.SetValidation(ValidationGroup);
    }

    public Money Money
    {
        get
        {
            if (ComboCurrency.SelectedItem == null) return null;
            String id = (String)ComboCurrency.SelectedItem.Value;
            Currency curr = Currency.Find(iSabayaContext, id);
            return new Money(curr,(decimal)txtAmount.Value);
        }
        set
        {
            if (value != null)
            {
                txtAmount.Value = value.Amount;
                foreach (ListEditItem item in ComboCurrency.Items)
                {
                    if (item.Value.Equals(value.Currency.Code))
                    {
                        ComboCurrency.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }

    public void Clear()
    {
        txtAmount.Number = DefaultValue;
        ComboCurrency.SelectedIndex = 0;
    }

    public override string Text
    {
        get
        {
            return Money.ToString();
        }
    }
}