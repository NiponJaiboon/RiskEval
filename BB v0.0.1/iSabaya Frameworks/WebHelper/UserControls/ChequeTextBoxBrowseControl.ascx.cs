using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Json;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using WebHelper;

namespace WebHelper.UserControls
{
    public partial class ChequeTextBoxBrowseControl : iSabayaControl
    {
        private String defaultPanelName;

        public String DefaultPanelName
        {
            get { return defaultPanelName; }
            set { this.defaultPanelName = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                txtChequeNo.ClientInstanceName = this.ClientID + txtChequeNo.ClientID;
                comboBank.ClientInstanceName = this.ClientID + comboBank.ClientID;
                cb1.ClientInstanceName = this.ClientID + cb1.ClientID;
                lblChequeName.ClientInstanceName = this.ClientID + lblChequeName.ClientID;
                cbpTxtBtnBankAccount.ClientInstanceName = "cbpTxtBtnBankAccount_" + defaultPanelName;
                txtChequeAmount.ClientInstanceName = this.ClientID + txtChequeAmount.ClientID;
                //txtUseAmount.ClientInstanceName = this.ClientID + txtUseAmount.ClientID;

                txtChequeNo.ClientSideEvents.ButtonClick = @"function(s,e ){
                " + cb1.ClientInstanceName + @".SendCallback();
            }";

                cb1.ClientSideEvents.CallbackComplete = @"function(s,e ){
                var obj1 = eval('('+e.result+')');
                " + lblChequeName.ClientInstanceName + @".SetValue(obj1.Label);
                " + txtChequeAmount.ClientInstanceName + @".SetValue(obj1.ChequeAmount);
            }";
                cbpTxtBtnBankAccount.ClientSideEvents.CallbackComplete = @"function(s,e ){
	            var obj1 = eval('('+e.result+')');
                " + txtChequeNo.ClientInstanceName + @".SetValue(obj1.ChequeNo);
                " + comboBank.ClientInstanceName + @".SetValue(obj1.bankId);
            }";
            }
            IList<Organization> banks = null;
            if (Session[this.GetType().ToString() + "Banks"] == null)
            {
                banks = Organization.Find(iSabayaContext, TreeListNode.FindRootByCode(iSabayaContext, "Bank"));
                Session[this.GetType().ToString() + "Banks"] = banks;
            }
            else
            {
                banks = (IList<Organization>)Session[this.GetType().ToString() + "Banks"];
            }
            comboBank.ValueField = "ID";
            comboBank.TextField = "Code";
            comboBank.DataSource = banks;
            comboBank.DataBind();
        }

        public Cheque Cheque
        {
            get
            {
                ICriteria crit = iSabayaContext.PersistenceSession.CreateCriteria<Cheque>();
                Organization bank = Organization.Find(iSabayaContext, int.Parse((String)comboBank.SelectedItem.Value));
                crit.Add(Expression.Eq("Bank", bank));
                crit.Add(Expression.Eq("ChequeNo", txtChequeNo.Text));
                return crit.UniqueResult<Cheque>();
            }
        }

        protected void cb1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            Cheque cheque = this.Cheque;
            if (cheque != null)
            {
                JsonObjectCollection json = new JsonObjectCollection();

                json.Add(new JsonStringValue("Label", cheque.Bank.FullName + ", " + cheque.Payer + ", " + cheque.Payee));
                json.Add(new JsonNumericValue("ChequeAmount", cheque.Amount));
                e.Result = json.ToString();
            }
            else
            {
                e.Result = "ไม่พบเช็ค!";
            }
        }

        protected void cbpTxtBtnBankAccount_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            String[] ids = e.Parameter.Split(new char[] { ',' });
            int bankId = int.Parse(ids[0]);
            string ChequeNo = ids[1];

            JsonObjectCollection obj = new JsonObjectCollection();
            obj.Add(new JsonStringValue("bankId", ids[0]));
            obj.Add(new JsonStringValue("ChequeNo", ChequeNo));

            e.Result = obj.ToString();
        }
    }
}