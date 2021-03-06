using System;
using System.Collections.Generic;
using System.Web.UI;
using DevExpress.Web.ASPxGridView;
using iSabaya;
using Resources;
using WebHelper;

public partial class ViewScheduleControl : iSabayaControl
{
    public long ScheduleID { get; set; }

    private IList<ScheduleDetail> ScheduleDetails
    {
        get { return (IList<ScheduleDetail>)Session[this.ClientID + "gridScheduleDetail"]; }
        set { Session[this.ClientID + "gridScheduleDetail"] = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
            InitializeControls();
    }

    private void InitializeControls()
    {
        btnViewDetail.ImageUrl = ResImageURL.Detail;
        btnViewDetail.ClientInstanceName = this.ClientID + btnViewDetail.ID;
        popSchedules.ClientInstanceName = this.ClientID + popSchedules.ID;

        btnViewDetail.ClientSideEvents.Click =
            @"function(s, e){
                " + popSchedules.ClientInstanceName + @".Show();
            }";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void LoadSchedule()
    {
        TimeSchedule sche = TimeSchedule.Find(iSabayaContext, this.ScheduleID);
        SetScheduleDetail(sche);
    }

    private void SetScheduleDetail(TimeSchedule s)
    {
        if (s != null)
        {
            this.ScheduleID = s.ID;
            this.ScheduleDetails = s.ScheduleDetails;
            gridScheduleDetail.DataBind();
            lblScheduleTitle.Text = string.Format("{0} : {1}", s.Code, s.Title.ToString(base.LanguageCode));
        }
        else
        {
            this.ScheduleID = 0;
            this.ScheduleDetails = new List<ScheduleDetail>();
            gridScheduleDetail.DataBind();
            lblScheduleTitle.Text = "No Schedule.";
        }
    }

    protected void gridScheduleDetail_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
    }

    protected void gridScheduleDetail_DataBinding(object sender, EventArgs e)
    {
        gridScheduleDetail.DataSource = this.ScheduleDetails;
    }

    protected void gdvSchedules_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightgray';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
        }
    }
}