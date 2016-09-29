using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxCallback;
using imSabaya.MutualFundSystem;
using imSabaya;


namespace WebHelper.Controls
{
    public class FundControl : iSabayaWebControlBase  
    {
        public ASPxComboBox cbxFund = null;
        public ASPxLabel lblFUnd = null;
        public ASPxCallback cbFund = null;

        private AdditionalClientScript additionClientScriptEvents = null;
        public AdditionalClientScript AdditionClientScriptEvents
        {
            get
            {
                if (additionClientScriptEvents == null)
                    additionClientScriptEvents = new AdditionalClientScript();
                return additionClientScriptEvents;
            }
            set { this.additionClientScriptEvents = value; }
        }

        private String clientInstanceName;
        public String ClientInstanceName
        {
            get { return this.clientInstanceName; }
            set { this.clientInstanceName = value; }
        }

        private String lblClientName;
        public String LabelFundName
        {
            get { return this.lblClientName; }
            set { this.lblClientName = value; }
        }

        private String gridOutput = "nogrid";
        public String GridOutput
        {
            get { return gridOutput; }
            set { this.gridOutput = value; }
        }

        private Boolean showDetail = true;
        public Boolean ShowDetail
        {
            get { return this.showDetail; }
            set { this.showDetail = value; }
        }

        private bool includeAllFundItem = false;
        public bool IncludeAllFundItem
        {
            get { return includeAllFundItem; }
            set { includeAllFundItem = value; }
        }

        private String transactionTypeCode = "";
        public String TransactionTypeCode
        {
            get { return transactionTypeCode; }
            set { this.transactionTypeCode = value; }
        }

        private bool isShowFundName = true;
        public bool IsShowFundName
        {
            get { return this.isShowFundName; }
            set { this.isShowFundName = value; }
        }

        //private bool isRequiredField;
        //public bool IsRequiredField
        //{
        //    get { return this.isRequiredField; }
        //    set { this.isRequiredField = value; }
        //}

        //private String validationGroup;
        //public String ValidationGroup
        //{
        //    get { return this.validationGroup; }
        //    set { this.validationGroup = value; }
        //}

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(cbxFund);
            this.Controls.Add(lblFUnd);
            this.Controls.Add(cbFund);
            #region javascript area
            cbxFund.SetValidation(ValidationGroup, IsRequiredField);
            cbxFund.ClientInstanceName = (ClientInstanceName != null ? ClientInstanceName : cbxFund.ClientID );
            lblFUnd.ClientInstanceName = (LabelFundName != null ? LabelFundName : lblFUnd.ClientID);
            cbFund.ClientInstanceName = cbFund.ClientID;
            /*Combo change*/
            cbxFund.ClientSideEvents.SelectedIndexChanged = @"function(s, e)
            {
                if(" + isShowFundName.ToString().ToLower() + @")"
                    + cbFund.ClientInstanceName + @".SendCallback('');"
                + AdditionClientScriptEvents.AfterSelectedChanged + @"
            }";

            #endregion
        }

     

        public class AdditionalClientScript
        {
            public string AfterSelectedChanged { get; set; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            cbxFund = new ASPxComboBox();
            lblFUnd = new ASPxLabel();
            cbFund = new ASPxCallback();
        }
    }
    public class AdditionalClientScript
    {
        public string AfterSelectedChanged { get; set; }
    }
}
