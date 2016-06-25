using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iSabaya;
using imSabaya;
using imSabaya.Resources;
using imSabaya.ProvidentFundSystem;
using WebHelper;

public partial class Controls_DistributionPolicyControl : iSabayaControl
{

    private bool isDistPolicyAmongMember = false;
    public bool IsDistPolicyAmongMember
    {
        get { return this.isDistPolicyAmongMember; }
        set { this.isDistPolicyAmongMember = value; }
    }

    private bool isDistPolicyPerMember = false;
    public bool IsDistPolicyPerMember
    {
        get { return this.isDistPolicyPerMember; }
        set { this.isDistPolicyPerMember = value; }
    }

    private string validationGroup = "";
    public string ValidationGroup
    {
        get { return this.validationGroup; }
        set { this.validationGroup = value;  }
    }

    public DistPolicyAmongMember getAmongMemberPolicy()
    {
        return (DistPolicyAmongMember)Enum.Parse(typeof(DistPolicyAmongMember), cbxPolicy.Value.ToString());      
    }

    public DistPolicyPerMember getPerMemberPolicy()
    {
        return (DistPolicyPerMember)Enum.Parse(typeof(DistPolicyPerMember), cbxPolicy.Value.ToString());   
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initializeControl();
        }
    }

    private void initializeControl()
    {
        cbxPolicy.SetValidation(ValidationGroup);
        if (IsDistPolicyAmongMember)
        {
            foreach (DistPolicyAmongMember dp in Enum.GetValues(typeof(DistPolicyAmongMember)))
            {
                string c = TextResource.ResourceManager.GetString(iSabaya.Context.GetEnumResourceName<DistPolicyAmongMember>(dp)
                    ,iSabayaContext.CurrentCulture);
                cbxPolicy.Items.Add(c, dp);
            }
        }
        if (IsDistPolicyPerMember)
        {
            foreach (DistPolicyPerMember dp in Enum.GetValues(typeof(DistPolicyPerMember)))
            {
                string c = TextResource.ResourceManager.GetString(iSabaya.Context.GetEnumResourceName<DistPolicyPerMember>(dp)
                    ,iSabayaContext.CurrentCulture);
                cbxPolicy.Items.Add(c, dp);
            }
        }

    }
}