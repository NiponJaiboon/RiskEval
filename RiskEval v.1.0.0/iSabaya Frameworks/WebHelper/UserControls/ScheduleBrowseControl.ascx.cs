using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using iSabaya;
using NHibernate;
using System.Collections.Generic;
using NHibernate.Criterion;
using WebHelper;

public partial class ScheduleBrowseControl : iSabayaControl
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
    #endregion

    private bool isFundSchedule = true;
    public bool IsFundSchedule
    {
        get { return isFundSchedule; }
        set { this.isFundSchedule = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Session["ctrls_ScheduleControl_Schedules"] = null;
        }
        if (Session["ctrls_ScheduleControl_Schedules"] != null)
        {
            gridSchedules.DataSource = (List<TimeSchedule>)Session["ctrls_ScheduleControl_Schedules"];
            gridSchedules.DataBind();
        }

        //coke 14072009 hh:mm
        if (IsRequiredField)
        {

            textScheduleEdit.ValidationSettings.ValidationGroup = ValidationGroup;

            textScheduleEdit.ValidationSettings.SetFocusOnError = true;
            textScheduleEdit.ValidationSettings.ErrorText = "ErrorText";
            textScheduleEdit.ValidationSettings.ValidateOnLeave = true;
            textScheduleEdit.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            textScheduleEdit.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            textScheduleEdit.ValidationSettings.ErrorImage.AlternateText = "Error";
            textScheduleEdit.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            textScheduleEdit.ValidationSettings.RequiredField.IsRequired = true;
            textScheduleEdit.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
            textScheduleEdit.ValidationSettings.ErrorFrameStyle.ForeColor = System.Drawing.Color.Red;
            textScheduleEdit.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
            textScheduleEdit.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            textScheduleEdit.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            textScheduleEdit.ValidationSettings.ErrorFrameStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FD4D3E");
            textScheduleEdit.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            textScheduleEdit.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(0);
            textScheduleEdit.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);
        }
    }

    protected void cbSearch_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
       
        //if (!String.IsNullOrEmpty(txtHiddenAccountId.Text))
        {
            String keyWord = textKeyWord.Text;
    
            IList<TimeSchedule> schedules = null;
            if (IsFundSchedule)
            {
                TreeListNode type = TreeListNode.FindByCode(iSabayaContext, "MutualFundSchedule");
                schedules = iSabaya.TimeSchedule.List(iSabayaContext, type);
            }
            else
            {
                if (txtHiddenAccountId.Text == "") { return; }
                TreeListNode category = TreeListNode.FindByCode(iSabayaContext, "MFAccountSchedule");
                ICriteria crit = iSabayaContext.PersistenceSession.CreateCriteria(typeof(TimeSchedule));
                crit.Add(Expression.Eq("Category", category));
                crit.Add(Expression.Eq("MFAccountID", int.Parse(txtHiddenAccountId.Text)));
                schedules = crit.List<TimeSchedule>();


            }

            //List<VOSchedule> vos = new List<VOSchedule>();
            //foreach (TimeSchedule s in schedules)
            //{
            //    vos.Add(new VOSchedule(s));
            //}



            Session["ctrls_ScheduleControl_Schedules"] = schedules;
            gridSchedules.DataSource = schedules;
            gridSchedules.DataBind();
        }

    }
    //protected void cbSelectedSchedule_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    //{
    //    int index = int.Parse(e.Parameter);
    //    VOSchedule vo = (VOSchedule)gridSchedules.GetRow(index);
    //    Session["ctrls_ScheduleControl_SelectedSchedule"] = vo;
    //}


    protected void cbpTextScheduleEdit_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        // VOSchedule vo = (VOSchedule)Session["ctrls_ScheduleControl_SelectedSchedule"];
        int index = int.Parse(e.Parameter);
        TimeSchedule vo = (TimeSchedule)gridSchedules.GetRow(index);
        textScheduleEdit.Text = vo.Title.ToString();
        textScheduleEdit.Value = vo.ID;
        labelSchedule.Text = vo.Title.ToString();
    }

    public TimeSchedule Schedule
    {
        get
        {
    
            //Schedule vo = (TimeSchedule)Session["ctrls_ScheduleControl_SelectedSchedule"];
            TimeSchedule schedule = TimeSchedule.Find(iSabayaContext, int.Parse(textScheduleEdit.Text));

            return schedule;
        }
        set
        {
            textScheduleEdit.Text = value.ID.ToString();
        }
    }
}
