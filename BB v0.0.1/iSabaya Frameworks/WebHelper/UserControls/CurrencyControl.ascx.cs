using System;
using DevExpress.Web.ASPxEditors;
using iSabaya;
using WebHelper;

public partial class ctrls_CurrencyControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboCurrency.SetValidation(ValidationGroup);
            foreach (Currency c in Currency.Currencies)
            {
                ComboCurrency.Items.Add(c.Code, c.Code);
            }
            ComboCurrency.SelectedIndex = 0;
        }
    }

    public new Currency Currency
    {
        get
        {
            if (ComboCurrency.SelectedItem == null) return null;
            String id = (String)ComboCurrency.SelectedItem.Value;
            Currency curr = Currency.Find(iSabayaContext, id);
            return curr;
        }
        set
        {
            if (value != null)
            {
                foreach (ListEditItem item in ComboCurrency.Items)
                {
                    if (item.Value.Equals(value.Code))
                    {
                        ComboCurrency.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }

    //public void InitializeControls()
    //{
    //}

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