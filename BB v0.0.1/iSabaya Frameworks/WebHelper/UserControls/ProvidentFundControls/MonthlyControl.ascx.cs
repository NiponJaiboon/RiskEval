using System;
using iSabaya;
using WebHelper;

public partial class ctrls_MonthlyControl : iSabayaControl
{
    public DateTime Date
    {
        get
        {
            int year = Convert.ToInt32(spnYear.Value);
            return new DateTime(year > 2300 ? year - 543 : year, Convert.ToInt32(spnMonth.Value), 1);
        }
        set
        {
            spnMonth.Value = value.Month;
            spnYear.Value = value.Year > 2300 ? value.Year : value.AddYears(543).Year;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!IsPostBack)
        {
            this.InitControl();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private void InitControl()
    {
        //txtMonth.ClientSideEvents.TextChanged = "function(s,e){alert(s.GetText().length)}";
        //txtMonth.MaskSettings.Mask = "<01..12><1800..2300>";
        //txtMonth.MaskSettings.IncludeLiterals = DevExpress.Web.ASPxEditors.MaskIncludeLiteralsMode.DecimalSymbol;
        //txtYear.MaskSettings.Mask = String.Format("<{0}..{1}>",
        //    TimeInterval.MinDate.Year > 1800 ? TimeInterval.MinDate.Year : TimeInterval.MinDate.AddYears(543).Year,
        //    TimeInterval.MaxDate.Year > 2300 ? TimeInterval.MaxDate.Year : TimeInterval.MaxDate.AddYears(543).Year);
        //txtYear.MaskSettings.IncludeLiterals = DevExpress.Web.ASPxEditors.MaskIncludeLiteralsMode.DecimalSymbol;
        //Date = DateTime.Today;

        spnMonth.MaxValue = 12;
        spnMonth.MinValue = 1;
        spnMonth.MaxLength = 2;
        spnMonth.NumberType = DevExpress.Web.ASPxEditors.SpinEditNumberType.Integer;
        spnMonth.DisplayFormatString = "00";

        spnYear.MaxValue = TimeInterval.MaxDate.Year > 2300 ? TimeInterval.MaxDate.Year : TimeInterval.MaxDate.AddYears(543).Year;
        spnYear.MinValue = TimeInterval.MinDate.Year > 1800 ? TimeInterval.MinDate.Year : TimeInterval.MinDate.AddYears(543).Year;
        spnYear.MaxLength = 4;
        spnYear.NumberType = DevExpress.Web.ASPxEditors.SpinEditNumberType.Integer;
        spnYear.DisplayFormatString = "0000";

        Date = DateTime.Today;
    }

    public String getMonthControl
    {
        get { return spnMonth.Value.ToString(); }
    }

    public String getYearControl
    {
        get { return spnYear.Value.ToString(); }
    }
}