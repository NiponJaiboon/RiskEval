using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Json;
using System.Web.UI;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using iSabaya;
using Resources;
using WebHelper;

public partial class ScheduleControl : iSabayaControl
{
    public String ValidationGroup { get; set; }

    private bool isRequiredField = false;

    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    public string ScheduleCategoryCode { get; set; }

    private IList<TimeSchedule> Schedules
    {
        get { return (IList<TimeSchedule>)this.Cache.Get("Schedules"); }
        set { this.Cache.Insert("Schedules", value); }
    }

    public int SelectedScheduleID
    {
        get
        {
            int id = 0;
            if (hddSchedules.Contains("scheduleID"))
                id = int.Parse(hddSchedules.Get("scheduleID").ToString());
            return id;
        }
        set
        {
            TimeSchedule s = null;
            if (value > 0)
            {
                IList<TimeSchedule> schedules = this.Schedules;
                for (int i = 0; i < schedules.Count; i++)
                {
                    if (schedules[i].ID == value)
                    {
                        s = schedules[i];
                        return;
                    }
                }
            }
            SelectedSchedule = s;
        }
    }

    public TimeSchedule SelectedSchedule
    {
        get
        {
            TimeSchedule schedule = null;
            int id = this.SelectedScheduleID;
            if (id > 0)
                schedule = TimeSchedule.Find(iSabayaContext, id);
            return schedule;
        }
        set
        {
            if (value != null)
            {
                hddSchedules.Set("scheduleID", value.ID);
                cboSchedule.SelectedItem = cboSchedule.Items.FindByValue(value.ID);
                lblScheduleTitle.Text = value.Title.ToString(base.LanguageCode);
            }
            else
            {
                hddSchedules.Set("scheduleID", 0);
                cboSchedule.SelectedIndex = -1;
                lblScheduleTitle.Text = "";
            }
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
        {
            InitializeControls();
        }
    }

    private void InitializeControls()
    {
        hddSchedules.ClientInstanceName = this.ClientID + hddSchedules.ID;
        cboSchedule.ClientInstanceName = this.ClientID + cboSchedule.ID;
        popSchedules.ClientInstanceName = this.ClientID + popSchedules.ID;
        cbSelectedSchedule.ClientInstanceName = this.ClientID + cbSelectedSchedule.ID;
        lblScheduleTitle.ClientInstanceName = this.ClientID + lblScheduleTitle.ID;

        cboSchedule.SetValidation(this.ValidationGroup, this.isRequiredField);
        cboSchedule.ValueType = typeof(int);
        cboSchedule.ValueField = "ScheduleID";
        cboSchedule.TextField = "Code";

        EditButton btn = new EditButton();
        btn.Image.Url = ResImageURL.Detail;
        btn.Position = ButtonsPosition.Left;
        btn.ToolTip = "Browse";
        cboSchedule.Buttons.Add(btn);
        popSchedules.HeaderText = "ปฏิทิน";

        #region Client script

        cboSchedule.ClientSideEvents.SelectedIndexChanged = @"function(s,e)
        {
            var item = s.GetSelectedItem();
            var id = 0;
            if(item != null)
                id = item.value;
            " + cbSelectedSchedule.ClientInstanceName + @".SendCallback('cbo;'+ id);
        }";
        gdvSchedules.ClientSideEvents.RowClick = gdvSchedules.ClientSideEvents.RowDblClick = @"function(s,e)
        {
            " + cbSelectedSchedule.ClientInstanceName + @".SendCallback('gdv;'+ e.visibleIndex);
            " + popSchedules.ClientInstanceName + @".Hide();
        }";
        //        gdvSchedules.ClientSideEvents.CustomButtonClick = @"function(s,e)
        //        {
        //            if(e.buttonID == 'btnSelectGdvSchedules')
        //            {
        //               " + cbSelectedSchedule.ClientInstanceName + @".SendCallback('gdv;'+ e.visibleIndex);
        //               " + popSchedules.ClientInstanceName + @".Hide();
        //            }
        //        }";
        cbSelectedSchedule.ClientSideEvents.CallbackComplete = @"function(s,e)
        {
            var objs = eval('(' + e.result + ')');
            " + lblScheduleTitle.ClientInstanceName + @".SetText(objs.Title);
            " + hddSchedules.ClientInstanceName + @".Set('scheduleID', objs.ScheduleID);
            " + cboSchedule.ClientInstanceName + @".SetSelectedIndex(objs.cboIndex);
        }";

        if (cboSchedule.Buttons[0] != null)
        {
            cboSchedule.ClientSideEvents.ButtonClick =
            @"function(s,e)
            {
                if(e.buttonIndex == 0)
                    " + popSchedules.ClientInstanceName + @".Show();
            }";
        }

        #endregion Client script
    }

    // assign schedule category of control at page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            gdvSchedules.DataBind();
        }
    }

    private IList<TimeSchedule> TimeScheduleLists()
    {
        if (!string.IsNullOrEmpty(ScheduleCategoryCode))
        {
            TreeListNode cate = iSabayaContext.Configuration.ScheduleCategoryParentNode.GetChild(ScheduleCategoryCode);
            this.Schedules = iSabaya.TimeSchedule.List(iSabayaContext, cate);
            return iSabaya.TimeSchedule.List(iSabayaContext, cate);
        }
        else
            return null;
    }

    public void BindCombo()
    {
        cboSchedule.DataBind();
    }

    protected void cboSchedule_DataBinding(object sender, EventArgs e)
    {
        cboSchedule.DataSource = this.TimeScheduleLists();
    }

    protected void gdvSchedules_DataBinding(object sender, EventArgs e)
    {
        gdvSchedules.DataSource = this.Schedules;
    }

    protected void cbSelectedSchedule_Callback(object sender, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        JsonObjectCollection objs = new JsonObjectCollection();
        string[] parameters = e.Parameter.Split(';');
        TimeSchedule schedule = null;
        switch (parameters[0])
        {
            case "gdv":
                if (!string.IsNullOrEmpty(parameters[1]))
                    schedule = (TimeSchedule)gdvSchedules.GetRow(int.Parse(parameters[1]));
                break;
            case "cbo":
                if (!string.IsNullOrEmpty(parameters[1]))
                    schedule = TimeSchedule.Find(iSabayaContext, int.Parse(parameters[1]));
                break;
            default: break;
        }
        if (schedule != null)
        {
            objs.Add(new JsonStringValue("Title", schedule.Title.ToString(base.LanguageCode)));
            objs.Add(new JsonNumericValue("ScheduleID", schedule.ID));
            if (parameters[0].Equals("gdv"))
                objs.Add(new JsonNumericValue("cboIndex", cboSchedule.Items.FindByValue(schedule.ID).Index));
            else
                objs.Add(new JsonNumericValue("cboIndex", cboSchedule.SelectedIndex));
        }
        else
        {
            objs.Add(new JsonStringValue("Title", string.Empty));
            objs.Add(new JsonNumericValue("ScheduleID", 0));
            objs.Add(new JsonNumericValue("cboIndex", -1));
        }
        e.Result = objs.ToString();
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