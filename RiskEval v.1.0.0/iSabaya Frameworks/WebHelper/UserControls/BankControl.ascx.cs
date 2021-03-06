using System;
using System.Collections;
using System.Collections.Generic;
using iSabaya;
using WebHelper;

    public partial class BankControl : iSabayaControl
    {
        private String cbxClientName = null;

        public String CbxClientName
        {
            get { return this.cbxClientName; }
            set { this.cbxClientName = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                comboBank.ClientInstanceName = CbxClientName;
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
            comboBank.ValueField = "OrganizationID";
            comboBank.TextField = "Code";
            comboBank.DataSource = banks;
            comboBank.DataBind();
        }

        public Organization Organization
        {
            get
            {
                Organization organization = null;
                if (comboBank.SelectedItem == null)
                {
                    throw new ApplicationException("ระบุธนาคาร");
                }
                if (comboBank.SelectedItem.Value == null)
                {
                    organization = null;
                }
                else
                {
                    organization = Organization.Find(iSabayaContext, int.Parse((String)comboBank.SelectedItem.Value));
                }
                return organization;
            }
            set
            {
            }
        }
    }
