using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using iSabaya;
using NHibernate;
using DevExpress.Web.ASPxEditors;
using WebHelper;

public partial class CountryControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            foreach (Country c in Country.List(iSabayaContext))
                cboCountry.Items.Add(c.ToString(), c.ID.ToString());
            cboCountry.SelectedIndex = 0;
            cboCountry.DataBind();

        }
        if (IsRequiredField)
            cboCountry.SetValidation(ValidationGroup);
    }

    public new Country Country
    {
        get
        {
            if (cboCountry.SelectedItem == null) return null;
            String id = (String)cboCountry.SelectedItem.Value;

            return Country.Find(iSabayaContext, int.Parse(id));
        }
        set
        {
            if (value != null)
            {
                foreach (ListEditItem item in cboCountry.Items)
                {
                    if (item.Value.Equals(value.ID.ToString()))
                    {
                        cboCountry.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                cboCountry.SelectedIndex = -1;
            }
        }
    }

    public void Blind()
    {
        foreach (Country c in Country.List(iSabayaContext))
            cboCountry.Items.Add(c.ToString(), c.ID.ToString());
        cboCountry.SelectedIndex = 0;
        cboCountry.DataBind();
    }

    public override string Text
    {
        get
        {
            return Country != null ? Country.ToString() : "";
        }
    }

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
    #endregion
}
