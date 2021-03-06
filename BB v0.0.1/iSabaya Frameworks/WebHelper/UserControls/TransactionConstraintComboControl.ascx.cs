using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using imSabaya;
using NHibernate;
using WebHelper;

public partial class ctrls_TransactionConstraintComboControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            ISession session = PersistenceLayer.WebSessionManager.PersistenceSession;
            ICriteria crit = session.CreateCriteria(typeof(TransactionConstraint));
            IList<TransactionConstraint> ips = crit.List<TransactionConstraint>();
            //IList<TransactionConstraint> ips = TransactionConstraint.List(PersistenceLayer.WebSessionManager.PersistenceSession);
            foreach (TransactionConstraint c in ips)
            {
                ComboTransactionConstraint.Items.Add(c.Description.ToString(), c.TransactionConstraintID.ToString());
            }
            ComboTransactionConstraint.SelectedIndex = 0;
        }

        if (IsRequiredField)
        {
            ComboTransactionConstraint.ValidationSettings.ValidationGroup = ValidationGroup;

            ComboTransactionConstraint.ValidationSettings.SetFocusOnError = true;
            ComboTransactionConstraint.ValidationSettings.ErrorText = "ErrorText";
            ComboTransactionConstraint.ValidationSettings.ValidateOnLeave = true;
            ComboTransactionConstraint.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            ComboTransactionConstraint.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            ComboTransactionConstraint.ValidationSettings.ErrorImage.AlternateText = "Error";
            ComboTransactionConstraint.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            ComboTransactionConstraint.ValidationSettings.RequiredField.IsRequired = true;
            ComboTransactionConstraint.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
            ComboTransactionConstraint.ValidationSettings.ErrorFrameStyle.ForeColor = System.Drawing.Color.Red;
            ComboTransactionConstraint.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
            ComboTransactionConstraint.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            ComboTransactionConstraint.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            ComboTransactionConstraint.ValidationSettings.ErrorFrameStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FD4D3E");
            ComboTransactionConstraint.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            ComboTransactionConstraint.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(0);
            ComboTransactionConstraint.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);
        }
    }

    public TransactionConstraint TransactionConstraint
    {
        get
        {
            if (ComboTransactionConstraint.SelectedItem == null) return null;
            String id = (String)ComboTransactionConstraint.SelectedItem.Value;
            TransactionConstraint ip = TransactionConstraint.Find(iSabayaContext, int.Parse(id));
            return ip;
        }
        set
        {
            if (value != null)
            {
                foreach (ListEditItem item in ComboTransactionConstraint.Items)
                {
                    if (item.Value.Equals(value.TransactionConstraintID.ToString()))
                    {
                        ComboTransactionConstraint.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }

    public void InitializeControls(String currentLanguage, ISession session)
    {
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