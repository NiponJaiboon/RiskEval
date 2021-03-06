using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;
using imSabaya;
using imSabaya.MutualFundSystem;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using WebHelper;

    public partial class BankAccountTextBoxControl : iSabayaControl
    {
        private String defaultPanelName;

        public String DefaultPanelName
        {
            get { return defaultPanelName; }
            set { this.defaultPanelName = value; }
        }

        private String fundCode;

        public String FundCode
        {
            get { return fundCode; }
            set { this.fundCode = value; }
        }

        private String accountNo;

        public String AccountNo
        {
            get { return accountNo; }
            set { this.accountNo = value; }
        }

        private BankAccountType bankAccountType;

        public BankAccountType BankAccountType
        {
            get { return bankAccountType; }
            set { this.bankAccountType = value; }
        }

        private BankAccount defaultBankAccount;

        public BankAccount DefaultBankAccount
        {
            get
            {
                if (Session[this.ID + "defaultBankAccount" + defaultPanelName] != null)
                {
                    defaultBankAccount = BankAccount.Find(iSabayaContext, (int)Session[this.ID + "defaultBankAccount" + defaultPanelName]);
                }
                return defaultBankAccount;
            }
            set
            {
                this.defaultBankAccount = value;
                if (value != null)
                {
                    Session[this.ID + "defaultBankAccount" + defaultPanelName] = this.defaultBankAccount.BankAccountID;
                }
                else
                {
                    Session[this.ID + "defaultBankAccount" + defaultPanelName] = null;
                }
            }
        }

        private List<BankAccount> posibleBankAccounts;

        public List<BankAccount> PosibleBankAccounts
        {
            get { return posibleBankAccounts; }
            set { this.posibleBankAccounts = value; }
        }

        //coke 13072009 18:09

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                #region coke 13072009 18:09

                if (IsRequiredField)
                {
                    txtBtnBankAccount.ValidationSettings.ValidationGroup = ValidationGroup;

                    txtBtnBankAccount.ValidationSettings.SetFocusOnError = true;
                    txtBtnBankAccount.ValidationSettings.ErrorText = "ErrorText";
                    txtBtnBankAccount.ValidationSettings.ValidateOnLeave = true;
                    txtBtnBankAccount.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
                    txtBtnBankAccount.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
                    txtBtnBankAccount.ValidationSettings.ErrorImage.AlternateText = "Error";
                    txtBtnBankAccount.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
                    txtBtnBankAccount.ValidationSettings.RequiredField.IsRequired = true;
                    txtBtnBankAccount.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
                    txtBtnBankAccount.ValidationSettings.ErrorFrameStyle.ForeColor = System.Drawing.Color.Red;
                    txtBtnBankAccount.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
                    txtBtnBankAccount.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
                    txtBtnBankAccount.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
                    txtBtnBankAccount.ValidationSettings.ErrorFrameStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FD4D3E");
                    txtBtnBankAccount.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                    txtBtnBankAccount.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(0);
                    txtBtnBankAccount.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);
                }

                #endregion coke 13072009 18:09

                gridList.ClientInstanceName = gridList.ClientID + this.ClientID;
                cbpOtherBank.ClientInstanceName = cbpOtherBank.ClientID + this.ClientID;
                cbLostFocusText.ClientInstanceName = cbLostFocusText.ClientID + this.ClientID;
                panelList.ClientInstanceName = panelList.ClientID + this.ClientID;
                panelSearch.ClientInstanceName = panelSearch.ClientID + this.ClientID;

                txtBtnBankAccount.ClientInstanceName = txtBtnBankAccount.ClientID + this.ClientID;
                popupCustom.ClientInstanceName = popupCustom.ClientID + this.ClientID;
                cbpTxtBtnBankAccount.ClientInstanceName = "cbpTxtBtnBankAccount_" + defaultPanelName;
                cbUpdateCode.ClientInstanceName = "cbUpdateCode_" + defaultPanelName;

                lblAccountName.ClientInstanceName = this.ClientID + this.lblAccountName.ClientID;

                txtBtnBankAccount.ClientSideEvents.ButtonClick = @"function(s,e){
                    var win = " + popupCustom.ClientInstanceName + @".GetWindow(0);
                    " + popupCustom.ClientInstanceName + @".ShowWindow(win);
                    }";

                gridList.ClientSideEvents.CustomButtonClick = @"function(s,e){
                var buttonID = e.buttonID;
                var visibleIndex = e.visibleIndex;
                  if(buttonID = 'buttonSelect')
                  {
                       " + gridList.ClientInstanceName + @".GetRowValues(visibleIndex,'BankAccountID;AccountNo',
                        function OnGridSelectionComplete(values) {
                            var textBank = values[1];
                            " + lblAccountName.ClientInstanceName + @".SetValue(textBank);

                            " + txtBtnBankAccount.ClientInstanceName + @".SetValue( values[0]);
                        });

                        " + popupCustom.ClientInstanceName + @".Hide();
                  }
            }";

                rdoType.ClientInstanceName = this.ClientID + rdoType.ClientID + "rdoType";
                callbackAcc.ClientInstanceName = callbackAcc.ClientID + "callbackAcc";
                lblSourceAccount.ClientInstanceName = lblSourceAccount.ClientID + "lblSourceAccount";

                //txtAccountNo.ClientSideEvents.LostFocus
                //    = @"function(s, e) {";
                //txtAccountNo.ClientSideEvents.LostFocus += "if(" + rdoType.ClientInstanceName + ".GetValue()==0){";
                //txtAccountNo.ClientSideEvents.LostFocus += callbackAcc.ClientInstanceName + ".PerformCallback(s.GetValue());";
                //txtAccountNo.ClientSideEvents.LostFocus += cbpTxtBtnBankAccount.ClientInstanceName + ".PerformCallback();";
                //txtAccountNo.ClientSideEvents.LostFocus += "}";
                //txtAccountNo.ClientSideEvents.LostFocus += "}";

                //            gridList.ClientSideEvents.Init = @"function(s,e){
                //                                        " + gridList.ClientInstanceName + @".SetFocusedRowIndex(-1);
                //                                        }";

                //rdoType.ClientSideEvents.SelectedIndexChanged = @"function(s, e) {";
                //rdoType.ClientSideEvents.SelectedIndexChanged += "if(s.GetValue()==0){";
                //rdoType.ClientSideEvents.SelectedIndexChanged += panelSearch.ClientInstanceName + ".SetVisible(true);";
                //rdoType.ClientSideEvents.SelectedIndexChanged += panelList.ClientInstanceName + ".SetVisible(false);";

                //rdoType.ClientSideEvents.SelectedIndexChanged += "}else if(s.GetValue()==3){";
                //rdoType.ClientSideEvents.SelectedIndexChanged += panelSearch.ClientInstanceName + ".SetVisible(false);";
                //rdoType.ClientSideEvents.SelectedIndexChanged += panelList.ClientInstanceName + ".SetVisible(true);";
                //rdoType.ClientSideEvents.SelectedIndexChanged += "}";
                //rdoType.ClientSideEvents.SelectedIndexChanged += "}";

                //            cbUpdateCode.ClientSideEvents.CallbackComplete = @"function(s, e) {
                //            " + gridList.ClientInstanceName + @".PerformCallback();
                //            }";

                //            txtBtnBankAccount.ClientSideEvents.TextChanged = @"function(s,e){
                //            " + cbLostFocusText.ClientInstanceName + @".SendCallback();
                //            }";

                //            cbLostFocusText.ClientSideEvents.CallbackComplete = @"function(s,e){
                //
                //            " + cbpTxtBtnBankAccount.ClientInstanceName + @".PerformCallback(e.result);
                //
                //            }";

                Session[defaultPanelName + "BankAccountTextBoxControl_List"] = null;
                Session["BankAccountControl_SelectedAccountNo"] = null;
                Session["BankAccountControl_SelectedFundCode"] = null;
                loadDataToGrid();
            }
            else
            {
                if (Session[defaultPanelName + "BankAccountTextBoxControl_List"] != null)
                {
                    gridList.DataSource = (IList<BankAccount>)Session[defaultPanelName + "BankAccountTextBoxControl_List"];
                    gridList.DataBind();
                }
            }
        }

        private void loadDataToGrid()
        {
            List<BankAccount> bankAccounts = new List<BankAccount>();

            if (this.BankAccountType == BankAccountType.CustomerBankAccount)
            {
                if (Session["BankAccountControl_SelectedAccountNo"] != null)
                {
                    String accountNo = (String)Session["BankAccountControl_SelectedAccountNo"];
                    MFAccount account = MFAccount.FindByAccountNo(iSabayaContext, accountNo);
                    foreach (PartyBankAccount db in account.AssociatedBankAccounts)
                    {
                        bankAccounts.Add(db.BankAccount);
                    }
                    //find other owner
                }
                else
                {
                    ICriteria crit = iSabayaContext.PersistencySession.CreateCriteria<PartyBankAccount>();
                    IList<PartyBankAccount> fbss = crit.List<PartyBankAccount>();
                    foreach (PartyBankAccount fba in fbss)
                    {
                        bankAccounts.Add(fba.BankAccount);
                    }
                }
            }
            else if (this.BankAccountType == BankAccountType.SellingAgentBankAccount)
            {
                if (Session["BankAccountControl_SelectedFundCode"] != null)
                {
                    String fundCode = (String)Session["BankAccountControl_SelectedFundCode"];
                    MutualFund fund = MutualFund.FindByCode(iSabayaContext, fundCode);
                    foreach (PartyBankAccount fba in fund.AssociatedBankAccounts)
                    {
                        bankAccounts.Add(fba.BankAccount);
                    }
                }
                else
                {
                    ICriteria crit = iSabayaContext.PersistencySession.CreateCriteria<PartyBankAccount>();
                    IList<PartyBankAccount> fbss = crit.List<PartyBankAccount>();
                    foreach (PartyBankAccount fba in fbss)
                    {
                        bankAccounts.Add(fba.BankAccount);
                    }
                }
            }
            //}
            //else
            //{
            //    ICriteria crit = iSabayaContext.PersistencySession.CreateCriteria <PartyBankAccount>();
            //    IList<BankAccount> list = crit.List<BankAccount>();
            //    bankAccounts.AddRange(list);
            //}
            Session[defaultPanelName + "BankAccountTextBoxControl_List"] = bankAccounts;
            gridList.DataSource = bankAccounts;
            gridList.DataBind();
        }

        protected void callbackAcc_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BankAccount account = BankAccount.FindByAccountNoAndBankCode(iSabayaContext, e.Parameter, ctrlOrganizationControlMini1.SelectedOrg);
            if (account != null)
            {
                lblSourceAccount.Text = account.AccountName.ToString(this.LanguageCode);
                //lblSourceAccount.BackColor = Color.White;
            }
            else
            {
                lblSourceAccount.Text = "";
                //lblSourceAccount.Style. = Color.Red;
            }
        }

        public BankAccount BankAccount
        {
            get
            {
                #region old

                /*
            if (rdoType.SelectedItem.Value.Equals("0"))
            {
                BankAccount account = BankAccount.FindByAccountNoAndBankCode(txtAccountNo.Text, ctrlOrganizationControlMini1.Organization, PersistenceLayer.WebSessionManager.PersistenceSession);

                return account;
            }

            else
            {
                if (Session[defaultPanelName + "BankAccountTextBoxControl_ListIndex"] == null)
                {
                    return DefaultBankAccount;
                }
                else
                {
                    ISession session = PersistenceLayer.WebSessionManager.PersistenceSession;
                    BankAccount bankAccount = BankAccount.Find(session, (int)Session[defaultPanelName + "BankAccountTextBoxControl_ListIndex"]);

                    return bankAccount;
                }
            }
             */

                #endregion old

                return this.DefaultBankAccount;
            }
            set
            {
                this.DefaultBankAccount = value;
                if (value != null)
                {
                    txtBtnBankAccount.Value = this.DefaultBankAccount.UniqueAccountNo;
                    lblAccountName.Text = this.DefaultBankAccount.Bank.ToString() + "/" + this.DefaultBankAccount.AccountName.ToString() + "/" + this.DefaultBankAccount.AccountNo;
                }
            }
        }

        protected void cbpOtherBank_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
        }

        protected void cbpInnerBank_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            txtAccountNo.Text = "";
            lblSourceAccount.Text = "";
        }

        protected void panelDefault_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //if (DefaultBankAccount != null)
            //{
            //    lblDefaultBankName.Text = DefaultBankAccount.AccountNameDisplay;
            //}
        }

        protected void cbpTxtBtnBankAccount_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (!e.Parameter.Equals("0"))
            {
                if (BankAccount != null)
                {
                    txtBtnBankAccount.Value = BankAccount.UniqueAccountNo;
                    lblAccountName.Text = BankAccount.Bank.ToString() + "/" + BankAccount.AccountName.ToString() + "/" + BankAccount.AccountNo;
                    txtBtnBankAccount.ForeColor = Color.Black;
                    lblAccountName.ForeColor = Color.Black;
                }
            }
            else
            {
                // txtBtnBankAccount.Value = BankAccount.UniqueAccountNo;
                lblAccountName.Text = "ไม่พบบัญชีธนาคาร";
                txtBtnBankAccount.ForeColor = Color.Red;
                lblAccountName.ForeColor = Color.Red;
            }
        }

        protected void cbSelectedList_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            if (e.Parameter != null)
            {
                if (!e.Parameter.Equals("-1"))
                {
                    this.DefaultBankAccount = (BankAccount)gridList.GetRow(int.Parse(e.Parameter));
                    e.Result = this.DefaultBankAccount.UniqueAccountNo;
                }
            }

            //if (e.Parameter == null)
            //{
            //    Session[defaultPanelName + "BankAccountTextBoxControl_ListIndex"] = null;
            //}

            //if (!e.Parameter.Equals("-1"))
            //{
            //    BankAccount bankAccount = (BankAccount)gridList.GetRow(int.Parse(e.Parameter));

            //    Session[defaultPanelName + "BankAccountTextBoxControl_ListIndex"] = bankAccount.BankAccountID;
            //}
            //else
            //{
            //    Session[defaultPanelName + "BankAccountTextBoxControl_ListIndex"] = null;
            //}
        }

        protected void cbUpdateCode_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            loadDataToGrid();
        }

        //protected void cbBAccountNo_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        //{
        //    ISession session = PersistenceLayer.WebSessionManager.PersistenceSession;
        //    Organization bank = Organization.Find(session, int.Parse(e.Parameter));
        //    IList < BankAccount > banks = BankAccount.Find(session, bank);
        //    cbBAccountNo.TextField = "AccountNo";
        //    cbBAccountNo.ValueField = "BankAccountID";
        //    cbBAccountNo.DataSource = banks;
        //    cbBAccountNo.DataBind();
        //}
        protected void cbLostFocusText_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            ISession session = PersistenceLayer.WebSessionManager.PersistenceSession;
            ICriteria crit = session.CreateCriteria(typeof(BankAccount));
            crit.Add(Expression.Eq("UniqueAccountNo", txtBtnBankAccount.Text.TrimEnd()));
            this.DefaultBankAccount = crit.UniqueResult<BankAccount>();
            if (this.DefaultBankAccount != null)
            {
                BankAccount ba = this.DefaultBankAccount;
                txtBtnBankAccount.Value = ba.UniqueAccountNo.Trim();
                lblAccountName.Text = ba.Bank.ToString() + "/" + ba.AccountName.ToString() + "/" + BankAccount.AccountNo;
                e.Result = "1";
            }
            else
            {
                e.Result = "0";
            }
        }
    }
