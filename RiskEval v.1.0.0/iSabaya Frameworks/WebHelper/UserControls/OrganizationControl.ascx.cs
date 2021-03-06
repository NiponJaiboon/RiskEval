using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using iSabaya;
using WebHelper;

namespace WebHelper.UserControls
{
    public partial class OrganizationControl : iSabayaControl
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

        private bool isBank = false;

        /// <summary>
        /// ต้องการแสดงเฉพาะบริษัทที่เป็นธนาคารเท่านั้น
        /// </summary>
        public bool IsBank
        {
            get { return isBank; }
            set { this.isBank = value; }
        }

        private bool organizationOnly = false;

        /// <summary>
        /// ต้องการแสดงเฉพาะบริษัท // isobsolete
        /// </summary>
        public bool OrganizationOnly
        {
            get { return organizationOnly; }
            set { this.organizationOnly = value; }
        }

        private bool canSelectUnitOnly = false;

        /// <summary>
        /// ต้องการให้ผู้ใช้เลือกเฉพาะสาขาเท่านั้น // isobsolete
        /// </summary>
        public bool CanSelectUnitOnly
        {
            get { return canSelectUnitOnly; }
            set { this.canSelectUnitOnly = value; }
        }

        #endregion Validation Section

        private bool filterOrgUnitOut = false;

        public bool FilterOrgUnitOut
        {
            get { return this.filterOrgUnitOut; }
            set { this.filterOrgUnitOut = value; }
        }

        public Unit Width
        {
            get { return comboOrganizationList.Width; }
            set { comboOrganizationList.Width = value; }
        }

        private bool enabled = true;

        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }

        private bool selectAll = true;

        public bool SelectAll
        {
            get { return this.selectAll; }
            set { this.selectAll = value; }
        }

        private AdditionalClientScript additionalClientScript = new AdditionalClientScript();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AdditionalClientScript AdditionalClientScriptEvents
        {
            get { return additionalClientScript; }
            set { this.additionalClientScript = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //if (!Page.IsCallback)
            //{
            //    InitailizeControls();
            //}
        }

        private void InitailizeControls()
        {
            if (isRequiredField)
                comboOrganizationList.SetValidation(ValidationGroup);
        }

        public void EnableControls(bool enabled)
        {
            comboOrganizationList.ClientEnabled = enabled;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitailizeControls();
                this.EnableControls(true);
                Session[this.ClientID + "OrganizationLists"] = null;
                comboOrganizationList.SelectedIndex = 0;
                comboOrganizationList.ClientInstanceName = this.ClientID + comboOrganizationList.ClientID;
                comboOrganizationList.ClientSideEvents.SelectedIndexChanged = comboOrganizationList.ClientSideEvents.Init = @"function(s, e){
                if(typeof(oncompleteLoadOrganization) != 'undefined'){
	                oncompleteLoadOrganization();
                }
            }";
            }
            this.BindCombo();
        }

        private void BindCombo()
        {
            comboOrganizationList.DataBind();
        }

        protected void comboOrganizationList_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            comboOrganizationList.DataBind();
        }

        protected void comboOrganizationList_DataBinding(object sender, EventArgs e)
        {
            if (Session[this.ClientID + "OrganizationLists"] != null)
                comboOrganizationList.DataSource = (IList<OrgBase>)Session[this.ClientID + "OrganizationLists"];
            else
                comboOrganizationList.DataSource = OrganizationList();
        }

        private IList<OrgBase> OrganizationList()
        {
            IList<OrgBase> tempOrgList = new List<OrgBase>();
            IList<OrgBase> OrgList = new List<OrgBase>();

            if (this.selectAll)
            {
                foreach (Organization org in Organization.List(iSabayaContext))
                {
                    if (org.EffectivePeriod.IsEffectiveOn(DateTime.Today))
                    {
                        tempOrgList.Add(org);
                        if (!this.filterOrgUnitOut)
                        {
                            foreach (OrgUnit orgunit in org.OrgUnits)
                            {
                                if (orgunit.EffectivePeriod.IsEffectiveOn(DateTime.Today))
                                {
                                    tempOrgList.Add(orgunit);
                                }
                            }
                        }
                        else
                            continue;
                    }
                }
                OrgList = OrgList.Union<OrgBase>(tempOrgList).ToList<OrgBase>();
            }
            if (this.isBank)
            {
                IList<Organization> banks = Organization.Find(iSabayaContext, TreeListNode.FindRootByCode(iSabayaContext, "Bank"));
                IList<OrgBase> bankLists = new List<OrgBase>();
                foreach (Organization bank in banks)
                {
                    bankLists.Add(bank);
                    foreach (OrgUnit branch in bank.OrgUnits)
                    {
                        bankLists.Add(branch);
                    }
                }
                OrgList = OrgList.Union<OrgBase>(bankLists).ToList<OrgBase>();
            }

            Session[this.ClientID + "OrganizationLists"] = OrgList;
            return OrgList;
        }

        public Organization SelectedOrg
        {
            get
            {
                if (comboOrganizationList.SelectedItem == null)
                    return null;
                Organization organization = Organization.Find(iSabayaContext,
                                                Convert.ToInt32(comboOrganizationList.SelectedItem.Value));
                if (null == organization)
                    return null;
                return organization;
            }
            set
            {
                //this.EnableControls(this.enabled);
                if (value != null)
                {
                    ListEditItem item = comboOrganizationList.Items.FindByValue(value.OrganizationID);
                    if (item != null)
                        comboOrganizationList.SelectedItem = item;
                }
                else
                    comboOrganizationList.SelectedIndex = -1;
            }
        }

        public OrgUnit SelectedOrgUnit
        {
            get
            {
                OrgUnit orgUnit = OrgUnit.Find(iSabayaContext,
                                                Convert.ToInt32(comboOrganizationList.SelectedItem.Value));
                if (null == orgUnit)
                    return null;
                return orgUnit;
            }
            set
            {
                //this.EnableControls(this.enabled);
                if (value != null)
                {
                    ListEditItem item = comboOrganizationList.Items.FindByValue(value.ID);
                    if (item != null)
                        comboOrganizationList.SelectedItem = item;
                }
                else
                    comboOrganizationList.SelectedIndex = -1;
            }
        }

        public class AdditionalClientScript
        {
            public string AfterValueChanged { get; set; }
        }

        public override string Text
        {
            get
            {
                return SelectedOrg != null ? SelectedOrg.ToString() : "";
            }
        }

        public override object Value
        {
            get { return SelectedOrg; }
            set { SelectedOrg = (Organization)value; }
        }
    }
}