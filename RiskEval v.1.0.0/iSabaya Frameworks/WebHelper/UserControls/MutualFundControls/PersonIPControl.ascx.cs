using System;
using System.Collections;
using System.Collections.Generic;
using DevExpress.Web.ASPxEditors;
using imSabaya.MutualFundSystem;
using iSabaya;
using NHibernate.Criterion;
using WebHelper;
using imSabaya;

public partial class ctrls_PersonIPControl : iSabayaControl
{
    //coke 14072009 hh:mm

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

    private String controlName;

    public String ControlName
    {
        get { return controlName; }
        set { this.controlName = value; }
    }

    private String controlName2;

    public String ControlName2
    {
        get { return controlName2; }
        set { this.controlName2 = value; }
    }

    private bool useAccountIP = true;

    public bool UseAccountIP
    {
        get { return this.useAccountIP; }
        set { this.useAccountIP = value; }
    }

    /// <summary>
    /// Use to set control in general mode.
    /// </summary>
    private bool isGeneralContorl = false;

    public bool IsGeneralContorl
    {
        get { return this.isGeneralContorl; }
        set { this.isGeneralContorl = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session[this.ClientID + "IPLists"] = null;
            comboIPLists.SetValidation(ValidationGroup, IsRequiredField);
            comboIPLists.SelectedIndex = 0;
            //hplChangeIPUsage.Text = "เปลี่ยน";
            hplChangeIPUsage.Cursor = "pointer";
            hplCancelChange.Text = "ยกเลิก";
            hplCancelChange.Cursor = "pointer";

            #region script

            //Tune
            comboIPLists.ClientInstanceName = this.controlName != null ? this.controlName : this.ClientID + comboIPLists.ClientID;
            //lblAccountIP.ClientInstanceName = ControlName2 != null ? ControlName2 : this.ClientID + lblAccountIP.ClientID;
            hplChangeIPUsage.ClientInstanceName = this.controlName2 != null ? this.controlName2 : this.ClientID + hplChangeIPUsage.ClientID;
            //lblSelectedIP.ClientInstanceName = this.ClientID + lblSelectedIP.ClientID;
            //hplChangeIPUsage.ClientInstanceName = this.ClientID + hplChangeIPUsage.ClientID;
            hplCancelChange.ClientInstanceName = this.ClientID + hplCancelChange.ClientID;
            hdfUseAccountIP.ClientInstanceName = this.ClientID + hdfUseAccountIP.ClientID;

            if (!this.isGeneralContorl)
            {
                hplChangeIPUsage.ClientSideEvents.Init = @"function(s, e){
                    document.getElementById('trAccountIP').style.display = '';
                    document.getElementById('trSelectedIP').style.display = 'none';
                    //document.getElementById('tdhplCancelChange').style.display = 'none';
                }";
            }

            comboIPLists.ClientSideEvents.Init = @"function(s, e){
                " + hdfUseAccountIP.ClientInstanceName + @".Set('UseAccountIP', 'true');
            }";

            //            comboIPLists.ClientSideEvents.SelectedIndexChanged = @"function(s, e){
            //                " + lblSelectedIP.ClientInstanceName + @".SetText(s.GetSelectedItem().text);
            //            }";

            hplChangeIPUsage.ClientSideEvents.Click = @"function(s, e){
                document.getElementById('trAccountIP').style.display = 'none';
                document.getElementById('trSelectedIP').style.display = '';
                document.getElementById('tdhplCancelChange').style.display = '';
                " + hdfUseAccountIP.ClientInstanceName + @".Set('UseAccountIP', 'false');
            }";

            hplCancelChange.ClientSideEvents.Click = @"function(s, e){
                document.getElementById('trAccountIP').style.display = '';
                document.getElementById('trSelectedIP').style.display = 'none';
                " + hdfUseAccountIP.ClientInstanceName + @".Set('UseAccountIP', 'true');
            }";

            #endregion script
        }
        else
        {
            if (hdfUseAccountIP.Count > 0)
                this.useAccountIP = Convert.ToBoolean(hdfUseAccountIP.Get("UseAccountIP").ToString());
        }
        this.BindCombo();
    }

    private void BindCombo()
    {
        comboIPLists.DataBind();
    }

    protected void comboIPLists_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        comboIPLists.DataBind();
    }

    private IList<InvestmentPlanner> InvestmentPlannerLists()
    {
        IList<InvestmentPlanner> ipLists = iSabayaContext.PersistencySession.CreateCriteria<InvestmentPlanner>()
                               .Add(Expression.Ge("LicenseEffectivePeriod.To", DateTime.Now))
                               .Add(Expression.Le("LicenseEffectivePeriod.From", DateTime.Now))
                               .List<InvestmentPlanner>();
        Session[this.ClientID + "IPLists"] = ipLists;
        return ipLists;
    }

    protected void comboIPLists_DataBinding(object sender, EventArgs e)
    {
        if (Session[this.ClientID + "IPLists"] != null)
            comboIPLists.DataSource = (IList<InvestmentPlanner>)Session[this.ClientID + "IPLists"];
        else
            comboIPLists.DataSource = InvestmentPlannerLists();
    }

    private InvestmentPlanner GetInvestmentPlannerByID(int id)
    {
        return iSabayaContext.PersistencySession.Get<InvestmentPlanner>(id);
    }

    public Person Person
    {
        get
        {
            if (comboIPLists.SelectedItem == null) { return null; }
            InvestmentPlanner selectedIP = GetInvestmentPlannerByID(Convert.ToInt32(comboIPLists.SelectedItem.Value));
            return selectedIP.Person;
        }
        set
        {
            if (value != null)
            {
                comboIPLists.SelectedItem.Value = InvestmentPlanner.FindByPersonId(iSabayaContext, value.PersonID).InvestmentPlannerID;
                hplChangeIPUsage.Text = value.FullName;
            }
            else
            {
                comboIPLists.SelectedIndex = -1;
                hplChangeIPUsage.Text = "";
            }
        }
    }

    public InvestmentPlanner InvestmentPlanner
    {
        get
        {
            InvestmentPlanner selectedIP = GetInvestmentPlannerByID(Convert.ToInt32(comboIPLists.SelectedItem.Value));
            if (null == selectedIP)
                return null;
            return selectedIP;
        }
        set
        {
            if (value != null)
            {
                ListEditItem item = comboIPLists.Items.FindByValue(value.InvestmentPlannerID);
                if (item != null)
                    comboIPLists.SelectedItem = item;
            }
            else
                comboIPLists.SelectedIndex = -1;
        }
    }

    public Organization Organization
    {
        get
        {
            OrgUnit unit = this.OrgUnit;
            if (unit != null)
            {
                return unit.OrganizationParent;
            }
            else
            {
                PersonOrgRelation saip = getEmployment();
                if (saip == null) return null;
                return saip.Organization;
            }
        }
    }

    public OrgUnit OrgUnit
    {
        get
        {
            PersonOrgRelation saip = getEmployment();
            if (saip == null) return null;
            return saip.OrgUnit;
        }
    }

    private PersonOrgRelation getEmployment()
    {
        return this.InvestmentPlanner.GetCurrentSellingAgent(iSabayaContext);
    }

    public override string Text
    {
        get
        {
            return InvestmentPlanner != null ? InvestmentPlanner.ToString() : "" ;
        }
    }
}