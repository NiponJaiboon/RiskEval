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

public partial class ctrls_TimeHourMinuteControl : System.Web.UI.UserControl
{
    public enum DefaultDate
    {
        ToDay,
        MinDate,
        MaxDate,
    }
    private bool isHideDesc = true;
    public bool IsHideDesc
    {
        get { return this.isHideDesc; }
        set { this.isHideDesc = value; }
    }

    private bool isRequiredField;
    public bool IsRequiredField
    {
        get { return this.isRequiredField; }
        set { this.isRequiredField = value; }
    }

    private String validationGroup;
    public String ValidationGroup
    {
        get { return this.validationGroup; }
        set { this.validationGroup = value; }
    }

    public String strIdent = "timepicker";
    public String strTime;
    public bool IsEnabled = true;
    public DateTime Time
    {
        get
        {
            String strTime = Hours.Text + ":" + Minute.Text + ":00";
            DateTime time = DateTime.Parse(DateTime.MaxValue.ToString("dd/MM/yyyy") + " " + strTime);
            return time;
        }
        set
        {
            Hours.Text = value.Hour.ToString();
            Minute.Text = value.Minute.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            intitControl(); 
        }
    }
    private void intitControl()
    {
        int Hour = DateTime.Now.Hour;
        int minute = DateTime.Now.Minute;
        Hours.Value = Hour;
        Minute.Value = minute;
    }
}


