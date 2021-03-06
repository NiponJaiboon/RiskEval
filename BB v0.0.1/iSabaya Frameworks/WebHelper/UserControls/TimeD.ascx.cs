using System;
using iSabaya;
using WebHelper;

public partial class ctrls_TimeD : iSabayaControl
{
    public DateTime dtTime = DateTime.Now;
    public String strIdent = "timepicker";
    public String strTime;
    public bool IsEnabled = true;

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

    private bool hideYears;
    private bool hideMounths;
    private bool hideDays;

    public bool HideYears
    {
        get { return this.hideYears; }
        set { this.hideYears = value; }
    }

    public bool HideMounths
    {
        get { return this.hideMounths; }
        set { this.hideMounths = value; }
    }

    public bool HideDays
    {
        get { return this.hideDays; }
        set { this.hideDays = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            if (HideYears)
            {
                lblYear.Visible = false;
                year.Visible = false;
                ObsYearPlus.Visible = false;
                ObsYearMinus.Visible = false;
            }
            if (HideMounths)
            {
                lblMonth.Visible = false;
                month.Visible = false;
                ObsMonthPlus.Visible = false;
                ObsMonthMinus.Visible = false;
            }
            if (HideDays)
            {
                lblDay.Visible = false;
                day.Visible = false;
                ObsDayPlus.Visible = false;
                ObsDayMinus.Visible = false;
            }
        }

        ObsYearPlus.Attributes.Add("onclick", "hr" + strIdent + "('" + year.ClientID + "' , 'plus')");
        ObsYearMinus.Attributes.Add("onclick", "hr" + strIdent + "('" + year.ClientID + "' , 'minus')");
        ObsMonthPlus.Attributes.Add("onclick", "mn" + strIdent + "('" + month.ClientID + "' , 'plus')");
        ObsMonthMinus.Attributes.Add("onclick", "mn" + strIdent + "('" + month.ClientID + "' , 'minus')");
        ObsDayPlus.Attributes.Add("onclick", "dy" + strIdent + "('" + day.ClientID + "' , 'plus')");
        ObsDayMinus.Attributes.Add("onclick", "dy" + strIdent + "('" + day.ClientID + "' , 'minus')");

        //ObsAMPMPlus.Attributes.Add("onclick", "am" + strIdent + "('" + ampm.ClientID + "' , 'plus')");
        //ObsAMPMMinus.Attributes.Add("onclick", "am" + strIdent + "('" + ampm.ClientID + "' , 'minus')");
        year.Attributes.Add("onchange", "checkvalue" + strIdent + "('" + year.ClientID + "')");
        month.Attributes.Add("onchange", "checkvalue" + strIdent + "('" + month.ClientID + "')");
        day.Attributes.Add("onchange", "checkvalue" + strIdent + "('" + day.ClientID + "')");

        String baseUrl = "http://" + Request.ServerVariables["SERVER_NAME"] + ":" +
                   Request.ServerVariables["SERVER_PORT"] + Request.ApplicationPath;
        ObsYearPlus.ImageUrl = baseUrl + "/ctrls/plus.gif";
        ObsYearPlus.ImageUrl = baseUrl + "/ctrls/plus.gif";
        ObsDayPlus.ImageUrl = baseUrl + "/ctrls/plus.gif";

        ObsYearMinus.ImageUrl = baseUrl + "/ctrls/minus.gif";
        ObsMonthMinus.ImageUrl = baseUrl + "/ctrls/minus.gif";
        ObsDayMinus.ImageUrl = baseUrl + "/ctrls/minus.gif";

        //DateFrom.ClientInstanceName = this.ClientID + DateFrom.ClientID;
        //DateTo.ClientInstanceName = this.ClientID + DateTo.ClientID;

        //        btnMaxdate.ClientSideEvents.Click = @"
        //        function(s, e) {
        //            " + DateTo.ClientInstanceName + @".SetText('31/12/2300');
        //
        //        }
        //        ";

        //        btnMindate.ClientSideEvents.Click = @"
        //        function(s, e) {
        //        " + DateFrom.ClientInstanceName + @".SetText('01/01/1800');
        //
        //        }
        //        ";
    }

    public TimeDuration Duration
    {
        get
        {
            TimeDuration Duration = new TimeDuration();

            String years = "";
            if (HideYears)
            {
                //years = "99";
            }
            else
            {
                years = year.Text;
            }
            Duration.Years = int.Parse(years);

            String months = "";
            if (HideMounths)
            {
                //months = "99";
            }
            else
            {
                months = month.Text;
            }

            String days = "";
            if (HideDays)
            {
                //days = "99";
            }
            else
            {
                days = day.Text;
            }
            Duration.Days = int.Parse(days);

            return Duration;
        }
        set
        {
            if (value != null)
            {
                year.Text = value.Years.ToString();
                day.Text = value.Days.ToString();
            }
        }
    }

    public void Clear()
    {
        year.Text = "00";
        month.Text = "00";
        day.Text = "00.00";
    }
}