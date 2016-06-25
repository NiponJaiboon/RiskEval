using System;
using System.Net.Json;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxClasses;
using iSabaya;
using WebHelper;
using Resources;

public partial class IncognitoForm : iSabayaControl
{
    public class AdditionalClientSideEvents
    {
        public string AfterSaveCompleted { get; set; }

        public string AfterCancelCompleted { get; set; }
    }

    private AdditionalClientSideEvents clientSideEvents = null;

    public AdditionalClientSideEvents ClientSideEvents
    {
        get
        {
            if (this.clientSideEvents == null)
                this.clientSideEvents = new AdditionalClientSideEvents();
            return this.clientSideEvents;
        }
        set { this.clientSideEvents = value; }
    }

    public string HiddenFieldValueIDClientID
    {
        get { return hddValueID.ClientID; }
    }

    private Unit editorWidth = Unit.Pixel(170);

    public new Unit EditorWidth
    {
        get { return this.editorWidth; }
        set
        {
            this.editorWidth = value;
            txtReference.Width = value;
            txtAlias.Width = value;
            txtEmail.Width = value;
            txtFaxs.Width = value;
            txtPhone.Width = value;
            txtMobile.Width = value;
        }
    }

    private Incognito IncognitoSource;

    public String CbpSaveClientInstanceName
    {
        get { return cbpSave.ClientInstanceName; }
    }

    public int IncognitoID
    {
        get
        {
            if (string.IsNullOrEmpty(hddValueID.Value))
                return 0;
            return int.Parse(hddValueID.Value);
        }
        set { hddValueID.Value = value.ToString(); }
    }

    public Table TableContent
    {
        get { return headTable; }
    }

    private bool showEditFormFirst = true;

    public bool ShowEditFormFirst
    {
        get { return this.showEditFormFirst; }
        set { this.showEditFormFirst = value; }
    }

    private string ValidationGroup { get; set; }

    private void ShowForm()
    {
        bool isVisibleEditForm = bool.Parse(hddVisibleEditForm.Value);
        if (isVisibleEditForm)
            EditFormDataBind();
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
        {
            headTable.SetCssHtmlTable_V(btnSave.CssPostfix);
            cbpSave.ClientInstanceName = this.ClientID + cbpSave.ClientID;
            lblHAlias.Text = "นามแฝง";
            lblHAgent.Text = "ตัวแทน";
            lblHReference.Text = ResGeneral.Reference;
            lblHOrderedDate.Text = ResGeneral.OrderedDate;
            lblHRemark.Text = ResGeneral.Remark;
            lblHEmail.Text = Resource_Party.Email;
            lblHFaxs.Text = Resource_Party.Fax;
            lblHPhone.Text = Resource_Party.Phone;
            lblHMobile.Text = Resource_Party.MobilePhone;
            lblHReligion.Text = Resource_Person.Religion;
            lblHOccupation.Text = Resource_Person.Occupation;
            lblHNationality.Text = Resource_Person.Nationality;
            lblHCitizenOf.Text = "พลเมืองประเทศ";
            lblHEffectivePeriod.Text = ResGeneral.EffectivePeriod;

            ctrlNationality.ParentNode = iSabayaContext.imSabayaConfig.NationalityParentNode;
            ctrlNationality.DataBind();
            ctrlOccupation.ParentNode = iSabayaContext.imSabayaConfig.Person.OccupationParentNode;
            ctrlOccupation.DataBind();
            ctrlReligion.ParentNode = iSabayaContext.imSabayaConfig.Person.ReligionParentNode;
            ctrlReligion.DataBind();

            hddVisibleEditForm.Value = ShowEditFormFirst.ToString();
            this.SetVisibleEditForm(ShowEditFormFirst);

            btnSave.Text = ResGeneral.Save;
            btnSave.ImageUrl = ResImageURL.Save;
            btnCancel.Text = ResGeneral.Cancel;
            btnCancel.ImageUrl = ResImageURL.Cancel;
            EditorWidth = this.editorWidth;

            ValidationGroup = this.ClientID + "_VGForm";
            txtReference.SetValidation(ValidationGroup);
            txtAlias.SetValidation(ValidationGroup);
            ctrlOrderedDate.SetValidation(ValidationGroup);
            ctrlAgent.IsRequiredField = true;
            ctrlAgent.ValidationGroup = ValidationGroup;
            txtEmail.SetValidation(ValidationGroup, base.EmailExpression, "อีเมล์ไม่ถูกต้อง");

            ctrlOccupation.SetValidation(ValidationGroup);
            ctrlNationality.SetValidation(ValidationGroup);
            ctrlReligion.SetValidation(ValidationGroup);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["Stage"] = null;

            btnSave.ClientSideEvents.Click = @"function(s,e)
            {
                if(ASPxClientEdit.ValidateGroup('" + ValidationGroup + @"'))
                {"
                    + cbpSave.ClientInstanceName + @".PerformCallback('confirm');
                }
            }";

            btnCancel.ClientSideEvents.Click = @"function(s,e)
            {
                ASPxClientEdit.ClearEditorsInContainerById('headTable');"
                + ClientSideEvents.AfterCancelCompleted +
                @"return false;
            }";

            cbpSave.ClientSideEvents.EndCallback = @"function(s,e)
            {
                if(s.cpResult != null)
                {
                    var obj = eval('(' + s.cpResult + ')');
                    if(obj.type == 'save')
                    {
                        if(obj.result)
                        {
                            alert(obj.msg,'s');
                        }
                        else
                        {
                            alert(obj.msg,'e');
                        }
                        var valueID = obj.valueID;
                    " + ClientSideEvents.AfterSaveCompleted + @"
                    }
                }
            }";
        }
        ctrlNationality.ParentNode = iSabayaContext.imSabayaConfig.NationalityParentNode;
        ctrlOccupation.ParentNode = iSabayaContext.imSabayaConfig.Person.OccupationParentNode;
        ctrlReligion.ParentNode = iSabayaContext.imSabayaConfig.Person.ReligionParentNode;
    }

    protected void cbpSave_Callback(object sender, CallbackEventArgsBase e)
    {
        string stage = e.Parameter;
        JsonObjectCollection objs = new JsonObjectCollection();
        switch (stage)
        {
            case "confirm":
                btnSave.ClientSideEvents.Click = @"function(s,e)
                {"
                  + cbpSave.ClientInstanceName + @".PerformCallback('save');
                }";

                btnCancel.ClientSideEvents.Click = @"function(s,e)
                {
                    " + ClientSideEvents.AfterCancelCompleted
                     + cbpSave.ClientInstanceName + @".PerformCallback('cancle_confirm');
                }";
                headTable.setTableColumnToConfirm();
                break;

            case "save":

                btnSave.ClientSideEvents.Click = @"function(s,e)
                {
                    if(ASPxClientEdit.ValidateGroup('" + ValidationGroup + @"'))
                    {"
                        + cbpSave.ClientInstanceName + @".PerformCallback('confirm');
                    }
                }";

                btnCancel.ClientSideEvents.Click = @"function(s,e)
                {
                    ASPxClientEdit.ClearEditorsInContainerById('headTable');"
                    + ClientSideEvents.AfterCancelCompleted +
                    @"return false;
                }";

                try
                {
                    SaveOrUpdateValue();
                    objs.Add(new JsonBooleanValue("result", true));
                    objs.Add(new JsonStringValue("type", e.Parameter));
                    objs.Add(new JsonStringValue("msg", Resource_Global.SaveSuccess));
                    objs.Add(new JsonNumericValue("valueID", int.Parse(hddValueID.Value)));
                }
                catch (Exception)
                {
                    objs.Add(new JsonBooleanValue("result", false));
                    objs.Add(new JsonStringValue("type", e.Parameter));
                    objs.Add(new JsonStringValue("msg", Resource_Global.SaveSuccess));
                    objs.Add(new JsonNumericValue("valueID", int.Parse(hddValueID.Value)));
                }
                cbpSave.JSProperties["cpResult"] = objs.ToString();
                break;

            case "edit":
                this.SetVisibleEditForm(true);
                this.ShowForm();
                break;

            case "cancle_confirm":
                btnSave.ClientSideEvents.Click = @"function(s,e)
                {
                    if(ASPxClientEdit.ValidateGroup('" + ValidationGroup + @"'))
                    {"
                        + cbpSave.ClientInstanceName + @".PerformCallback('confirm');
                    }
                }";

                btnCancel.ClientSideEvents.Click = @"function(s,e)
                {
                    ASPxClientEdit.ClearEditorsInContainerById('headTable');"
                    + ClientSideEvents.AfterCancelCompleted +
                    @"return false;
                }";

                headTable.setTableColumnVisible(true, 1);
                break;

            default: break;
        }
    }

    private void EditFormDataBind()
    {
        //hddValueID.Value = IncognitoID.ToString();
        //ctrlNationality.RootNode = iSabayaContext.imSabayaConfig.NationalityParentNode;
        //ctrlOccupation.RootNode = iSabayaContext.imSabayaConfig.Person.OccupationParentNode;
        //ctrlReligion.RootNode = iSabayaContext.imSabayaConfig.Person.ReligionParentNode;
        int id = IncognitoID;
        if (id > 0)
        {
            IncognitoSource = Incognito.Find(iSabayaContext, id);
            if (IncognitoSource != null)
            {
                lblTitle.Text = IncognitoSource.ToString();
                hrTitle.Visible = true;
                txtReference.Text = IncognitoSource.Reference;
                txtRemark.Text = IncognitoSource.Remark;
                ctrlOrderedDate.Date = IncognitoSource.OrderedDate;
                ctrlAgent.SelectedOrg = IncognitoSource.Agent;
                txtAlias.Text = IncognitoSource.Alias;
                txtEmail.Text = IncognitoSource.Email;
                txtFaxs.Text = IncognitoSource.Faxes;
                txtMobile.Text = IncognitoSource.MobilePhone;
                txtPhone.Text = IncognitoSource.Phone;
                ctrlNationality.SelectedNode = IncognitoSource.Nationality;
                ctrlOccupation.SelectedNode = IncognitoSource.Occupation;
                ctrlReligion.SelectedNode = IncognitoSource.Religion;
                ctrlCitizenOf.Country = IncognitoSource.CitizenOf;
                ctrlEffectivePeriod.Period = IncognitoSource.EffectivePeriod;
                return;
            }
        }
        lblTitle.Text = string.Empty;
        hrTitle.Visible = false;
        IncognitoSource = null;
        txtReference.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ctrlOrderedDate.Date = DateTime.Today;
        ctrlAgent.SelectedOrg = null;
        txtAlias.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtFaxs.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtPhone.Text = string.Empty;
        ctrlCitizenOf.Country = null;
        ctrlEffectivePeriod.Period = new TimeInterval(DateTime.Today);
    }

    private void SetVisibleEditForm(bool visible)
    {
        hddVisibleEditForm.Value = visible.ToString();
        tbSaveCommand.Visible = visible;
        txtReference.Visible = visible;
        ctrlOrderedDate.Visible = visible;
        txtRemark.Visible = visible;
        txtAlias.Visible = visible;
        txtEmail.Visible = visible;
        txtFaxs.Visible = visible;
        txtMobile.Visible = visible;
        txtPhone.Visible = visible;
        ctrlAgent.Visible = visible;
        ctrlNationality.Visible = visible;
        ctrlOccupation.Visible = visible;
        ctrlReligion.Visible = visible;
        ctrlCitizenOf.Visible = visible;
        ctrlEffectivePeriod.Visible = visible;
    }

    private void SaveOrUpdateValue()
    {
        NHibernate.ITransaction tran = iSabayaContext.PersistencySession.BeginTransaction();
        try
        {
            int id = 0;
            if (!string.IsNullOrEmpty(hddValueID.Value))
                id = int.Parse(hddValueID.Value);
            Incognito inc = null;
            if (id > 0)
                inc = Incognito.Find(iSabayaContext, id);
            else
                inc = new Incognito();

            inc.Reference = txtReference.Text;
            inc.Remark = txtRemark.Text;
            inc.OrderedDate = ctrlOrderedDate.Date;
            inc.Agent = ctrlAgent.SelectedOrg;
            inc.Alias = txtAlias.Text;
            inc.Email = txtEmail.Text;
            inc.Faxes = txtFaxs.Text;
            inc.MobilePhone = txtMobile.Text;
            inc.Phone = txtPhone.Text;
            inc.CitizenOf = ctrlCitizenOf.Country;
            inc.Nationality = ctrlNationality.SelectedNode;
            inc.Occupation = ctrlOccupation.SelectedNode;
            inc.Religion = ctrlReligion.SelectedNode;
            inc.EffectivePeriod = ctrlEffectivePeriod.Period;
            inc.UpdatedBy = base.User;
            inc.UpdatedTS = DateTime.Now;
            inc.Save(iSabayaContext);
            tran.Commit();
            hddValueID.Value = inc.PartyID.ToString();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            if (ex.InnerException != null)
                throw ex.InnerException;
            else
                throw ex;
        }
    }
}