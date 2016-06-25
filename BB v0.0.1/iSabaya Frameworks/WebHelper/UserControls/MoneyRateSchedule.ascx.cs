using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxCallback;
using DevExpress.Web.ASPxTreeList;
using iSabaya;
using WebHelper;

public partial class ctrls_MoneyRateSchedule : iSabayaControl
{
    private bool isRequiredField = false;
    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }
    public string ValidationGroup { get; set; }
    private bool raiseInitial = false;
    public bool RaiseInitial
    {
        get { return raiseInitial; }
        set { this.raiseInitial = value; }
    }
    public bool Enabled
    {
        get { return btnCommission.Enabled; }
        set { btnCommission.Enabled = value; }
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback || RaiseInitial)
            InitializeControls();
    }
    private void InitializeControls()
    {
        if (IsRequiredField)
            btnCommission.SetValidation(ValidationGroup, true);
        popup.ClientInstanceName = string.Format("{0}_{1}", this.ClientID, popup.ID);
        tltMoneyRateSchedule.ClientInstanceName = string.Format("{0}_{1}", this.ClientID, tltMoneyRateSchedule.ID);
        btnCommission.ClientInstanceName = string.Format("{0}_{1}", this.ClientID, btnCommission.ID);
        btnCommission.ClientSideEvents.ButtonClick = @"function(s,e)
        {
            " + popup.ClientInstanceName + @".Show();
            " + popup.ClientInstanceName + @".AdjustControl();
        }";
        tltMoneyRateSchedule.ClientSideEvents.NodeDblClick = @"function(s,e)
        {
            s.GetNodeValues(e.nodeKey, 'Code', function(value)
                    {
                        " + popup.ClientInstanceName + @".Hide();
                        " + btnCommission.ClientInstanceName + @".SetValue(value);
                    });
        }";

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        tltMoneyRateSchedule.DataBind();
    }
    public String Code
    {
        get { return btnCommission.Value.ToString(); }
        set { btnCommission.Text = value; }
    }
    //private MoneyRateSchedule m;
    //public new MoneyRateSchedule Value
    //{
    //    get
    //    {
    //        m = (m == null ? MoneyRateSchedule.Find(iSabayaContext, Code) : m);
    //        return m;
    //    }
    //    set
    //    {
    //        m = value;
    //        if (value != null)
    //            btnCommission.Value = value.Code;
    //    }
    //}

    public override string Text
    {
        get
        {
            return Code;
        }
    }

}
