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
using DevExpress.Web.ASPxEditors;
using imSabaya;
using System.Collections.Generic;
using NHibernate;
using iSabaya;
using DevExpress.Web.ASPxClasses;
using imSabaya.ProvidentFundSystem;
using WebHelper;

public partial class ctrls_FundControlMini : iSabayaControl
{ 
    //coke 13072009 12:09
    #region Validation Section
    private bool isRequiredField = false;

    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    private string validationGroup;
    /// <summary>
    /// Get or sets the group of controls for which the editor forces validation when it posts back to the server.
    /// </summary>
    public string ValidationGroup
    {
        get { return validationGroup; }
        set { this.validationGroup = value; }
    }
    #endregion

    private String gridOutput;
    public String GridOutput
    {
        get { return gridOutput; }
        set { this.gridOutput=value; }
    }

    private bool isFillterSellingAgent = false;

    public bool IsFillterSellingAgent
    {
        get { return isFillterSellingAgent; }
        set { isFillterSellingAgent = value; }
    }
    //private Dictionary<int, String> dic;

    //public ctrls_FundControlMini()
    //{
    //    dic = new Dictionary<int, string>();
    //}

    //private void refreshControl()
    //{
    //    ComboFundName.Items.Clear();

    //    foreach (int k in dic.Keys)
    //    {
    //        String title = dic[k];
    //        ComboFundName.Items.Add(title, k);
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            InitialControl(this.LanguageCode);
            //dic = (Dictionary<int, String>)ViewState["listFunds"];


            /*Combo change*/
            //ComboFundName.ClientSideEvents.SelectedIndexChanged = @"function(s, e) {";
        }

        //coke 13072009 10:01
        if (IsRequiredField)
        {
             
            ComboFundName.ValidationSettings.ValidationGroup = ValidationGroup;

            ComboFundName.ValidationSettings.SetFocusOnError = true;
            ComboFundName.ValidationSettings.ErrorText = "ErrorText";
            ComboFundName.ValidationSettings.ValidateOnLeave = true;
            ComboFundName.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            ComboFundName.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            ComboFundName.ValidationSettings.ErrorImage.AlternateText = "Error";
            ComboFundName.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            ComboFundName.ValidationSettings.RequiredField.IsRequired = true;
            ComboFundName.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
            ComboFundName.ValidationSettings.ErrorFrameStyle.ForeColor = System.Drawing.Color.Red;
            ComboFundName.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
            ComboFundName.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            ComboFundName.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            ComboFundName.ValidationSettings.ErrorFrameStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FD4D3E");
            ComboFundName.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            ComboFundName.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(0);
            ComboFundName.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);
        }
    }

    public void InitialControl(string currentLanguage)
    {

        IList<ProvidentFund> listFunds = null;

        if (!IsFillterSellingAgent)
        {
            listFunds = ProvidentFund.List(iSabayaContext);
        }
        else
        {
            User user = (User)this.User;
        }   
        //dic.Clear();

        foreach (ProvidentFund fund in listFunds)
        {
            ComboFundName.Items.Add(fund.Code, fund.FundID.ToString());
        }
    }

    public Fund Fund
    {
        get
        {
            if (ComboFundName.SelectedItem == null)
            {
                return null;
            }
            int fId = (int)ComboFundName.SelectedItem.Value;
            if (fId != -1)
            {
                ProvidentFund fund = ProvidentFund.Find(iSabayaContext, fId);
                return fund;
            }
            else
            {
                return null;
            }
        }
        set
        {
            Fund fund = value;
            if (fund != null)
            {
                foreach (ListEditItem item in ComboFundName.Items)
                {
                    if (item.Value.ToString().Equals(fund.FundID.ToString()))
                    {
                        ComboFundName.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }
}
