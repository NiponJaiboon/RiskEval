using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using imSabaya;
using NHibernate;
using NHibernate.Criterion;
using imSabaya.ProvidentFundSystem;
using WebHelper;

public partial class ctrls_VestingPlanControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Employer employer = getPageEmployer();
        if (IsPostBack)
            BindingVestingPlans();
    }
    public void BindingVestingPlans()
    {
        Employer employer = getPageEmployer();
        if (employer != null)
        {
            IList<VestingPlan> vestingPlans;
            if (TerminationCategory != null)
                vestingPlans = employer.GetVestingPlans(TerminationCategory, DateTime.Today);
            else
                vestingPlans = employer.GetVestingPlans(DateTime.Today);
            cboVesting.DataSource = vestingPlans;
            cboVesting.TextField = "Title";
            cboVesting.ValueField = "VestingPlanID";
            cboVesting.SelectedIndex = 0;
            cboVesting.DataBind();
        }
    }
    public iSabaya.TreeListNode TerminationCategory { get; set; }
    public VestingPlan VestingPlan
    {
        get
        {
            if (cboVesting.SelectedItem == null) return null;
            VestingPlan vesting = (VestingPlan)this.iSabayaContext.PersistencySession.Get(typeof(VestingPlan), 
                                            int.Parse(cboVesting.SelectedItem.Value.ToString()));
            return vesting;
        }
        set
        {
            if (value != null)
                cboVesting.SelectedItem = cboVesting.Items[cboVesting.Items.IndexOfValue(value.VestingPlanID)];
        }
    }

    public Employer getPageEmployer()
    {
        if (Session["SessionEmployer"] == null) return null;
        Employer employer = (Employer)Session["SessionEmployer"];
        return employer;
    }
}
