using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Json;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using Resources;
using WebHelper;

public partial class PartyAttributeControl : iSabayaControl
{
    private const string HKEY_PARTY_ID = "partyID";
    private const string HKEY_PARTY_DISCRIMINATOR = "partyDiscriminator";
    private const string KEY_DISPLAY = "display";
    private const string KEY_EDIT = "editmode";
    private const string KEY_CREATE = "create";
    private const string KEY_EXPIRE = "expire";
    private const string KEY_CP_RESULT = "cpResult";
    private const string HKEY_VALUE_IMG = "imgBase64";
    private const string HKEY_ATTRIBUTE_ID = "attributeID";

    public int OwnerID { get; set; }

    public int OwnerDiscriminator { get; set; }

    private bool isAlreadyOwner = true;

    public bool IsAlreadyOwner
    {
        get { return this.isAlreadyOwner; }
        set { this.isAlreadyOwner = value; }
    }

    public string GridViewClientInstanceName
    {
        get { return gdvPartyAttribute.ClientInstanceName; }
        set { gdvPartyAttribute.ClientInstanceName = value; }
    }

    public ICategorizedTemporalList<PartyAttribute> PartyAttributes
    {
        get
        {
            if (OwnerID == 0)
            {
                CategorizedTemporalList<PartyAttribute> attributes = (CategorizedTemporalList<PartyAttribute>)Session[Page.GetType() + this.ID + "Attributes"];
                if (attributes == null)
                    Session[Page.GetType() + this.ID + "Attributes"] = new CategorizedTemporalList<PartyAttribute>();
                return (CategorizedTemporalList<PartyAttribute>)Session[Page.GetType() + this.ID + "Attributes"];
            }
            else
                return null;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        Page.RegisterRequiresControlState(this);
        if (!Page.IsPostBack)
        {
            Session.Remove(Page.GetType() + this.ID + "Attributes");
            if (string.IsNullOrEmpty(gdvPartyAttribute.ClientInstanceName))
                gdvPartyAttribute.ClientInstanceName = this.ID + gdvPartyAttribute.ID;
        }
        base.OnInit(e);
        sdsPartyAttribute.SelectCommand = @"
            SELECT PartyAttributeID,
                  dbo.f_treelistnode(AttributeKeyNodeID,@langCode) as AttributeKey,
                  EffectiveFrom,
                  EffectiveTo,
                  PartyDiscriminator,
                  PartyID
            FROM PartyAttribute
            WHERE PartyID = @partyID and PartyDiscriminator = @discriminator";
        if (!Page.IsCallback)
            InitializeControls();
    }

    protected override object SaveControlState()
    {
        base.SaveControlState();
        object[] objs = new object[2];
        objs[0] = this.OwnerID;
        objs[1] = this.OwnerDiscriminator;
        return objs;
    }

    protected override void LoadControlState(object savedState)
    {
        object[] objs = (object[])savedState;
        this.OwnerID = (int)objs[0];
        this.OwnerDiscriminator = (int)objs[1];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
        if (!Page.IsCallback)
        {
            gdvPartyAttribute.DataBind();
        }
    }

    /// <summary>
    /// Load data use for Page_Load and rase load data for already owner
    /// </summary>
    public void LoadData()
    {
        if (IsAlreadyOwner)
        {
            if (OwnerID != 0 && OwnerDiscriminator != 0)
            {
                sdsPartyAttribute.SelectParameters["partyID"].DefaultValue = OwnerID.ToString();
                sdsPartyAttribute.SelectParameters["discriminator"].DefaultValue = OwnerDiscriminator.ToString();
                gdvPartyAttribute.DataSourceID = "sdsPartyAttribute";
                gdvPartyAttribute.DataSource = null;

                ((GridViewDataTextColumn)gdvPartyAttribute.Columns["EffectiveFrom"]).FieldName = "EffectiveFrom";
                GridViewDataTextColumn c = (GridViewDataTextColumn)gdvPartyAttribute.Columns["EffectiveTo"];
                c.FieldName = "EffectiveTo";
                c.Visible = true;
                c.Caption = ResGeneral.EffectiveFrom;
                gdvPartyAttribute.HtmlRowPrepared += gdvPartyAttribute_HtmlRowPrepared;
                btnAdd.ClientEnabled = true;
            }
            else
            {
                btnAdd.ClientEnabled = false;
            }
        }
        else
        {
            // Initial for only is null;
            gdvPartyAttribute.DataSourceID = "";
            gdvPartyAttribute.DataSource = PartyAttributes;
            if (!Page.IsCallback)
            {
                GridViewDataTextColumn c = (GridViewDataTextColumn)gdvPartyAttribute.Columns["EffectiveFrom"];
                c.FieldName = "EffectivePeriod";
                c.Caption = ResGeneral.EffectivePeriod;
                c = (GridViewDataTextColumn)gdvPartyAttribute.Columns["EffectiveTo"];
                c.FieldName = "";
                c.Visible = false;
                btnExpire.Text = ResGeneral.Delete;
                btnExpire.ImageUrl = ResImageURL.Delete;
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        tbCtrl_PartyAttributes.SetTableControlStyle(gdvPartyAttribute.CssPostfix);
    }

    protected void InitializeControls()
    {
        cbpEdit.ClientInstanceName = this.ClientID + cbpEdit.ID;
        hddPartyAttribute.ClientInstanceName = this.ClientID + hddPartyAttribute.ID;
        gdvPartyAttribute.ClientInstanceName = this.ClientID + gdvPartyAttribute.ID;
        btnAdd.ClientInstanceName = this.ClientID + btnAdd.ID;
        btnExpire.ClientInstanceName = this.ClientID + btnExpire.ID;
        cbpValueImage.ClientInstanceName = this.ClientID + cbpValueImage.ID;
        popupActualSizeImage.ClientInstanceName = this.ClientID + popupActualSizeImage.ID;
        imgActualSize.ClientInstanceName = this.ClientID + imgActualSize.ID;
        uplImage.ClientInstanceName = this.ClientID + uplImage.ID;

        btnSave.Text = ResGeneral.Save;
        btnSave.ImageUrl = ResImageURL.Save;
        btnCancel.Text = ResGeneral.Cancel;
        btnCancel.ImageUrl = ResImageURL.Cancel;
        btnAdd.ImageUrl = ResImageURL.Add;
        btnAdd.Text = ResGeneral.Add;
        btnExpire.ImageUrl = ResImageURL.Delete;
        btnExpire.Text = ResGeneral.Delete;

        lblEffectiveFrom.Text = ResGeneral.EffectiveFrom;
        lblEffectiveTo.Text = ResGeneral.EffectiveTo;

        btnUploadImg.Text = ResGeneral.Upload;

        btnSave.ClientSideEvents.Click = @"function(s,e)
        {
            if(ASPxClientEdit.ValidateGroup('attributeEdit'))
            {
                var id = " + hddPartyAttribute.ClientInstanceName + @".Get('" + HKEY_ATTRIBUTE_ID + @"');
                " + gdvPartyAttribute.ClientInstanceName + @".PerformCallback('" + KEY_EDIT + @";' + id);
                " + btnAdd.ClientInstanceName + @".SetEnabled(true);
            }
        }";
        btnCancel.ClientSideEvents.Click = @"function(s,e)
            {
                " + gdvPartyAttribute.ClientInstanceName + @".SetFocusedRowIndex(0);
                " + btnAdd.ClientInstanceName + @".SetEnabled(true);
                " + cbpEdit.ClientInstanceName + @".PerformCallback('" + KEY_DISPLAY + @"');
            }";
        btnAdd.ClientSideEvents.Click = @"function(s,e)
            {
                " + gdvPartyAttribute.ClientInstanceName + @".SetFocusedRowIndex(-1);
                " + cbpEdit.ClientInstanceName + @".PerformCallback('" + KEY_EDIT + @"');
            }";
        btnExpire.ClientSideEvents.Click = @"function(s,e)
            {
                var gdv = " + gdvPartyAttribute.ClientInstanceName + @";
                var index = gdv.GetFocusedRowIndex();
                if(index > -1)
                {
                    gdv.PerformCallback('" + KEY_EXPIRE + @";' + index);
                }
            }";

        btnUploadImg.ClientSideEvents.Click = @"function(s,e)
            {
                " + uplImage.ClientInstanceName + @".Upload();
            }";
        uplImage.ClientSideEvents.FileUploadComplete = @"function(s,e)
            {
                if(e.isValid)
                {
                    //" + hddPartyAttribute.ClientInstanceName + @".Set('" + HKEY_VALUE_IMG + @"',e.callbackData);
                    " + cbpValueImage.ClientInstanceName + @".PerformCallback(e.callbackData);
                }
            }";
        ctrlValueImage.ClientSideEvents.Click = @"function(s,e)
            {
                var imgActual = document.getElementById('" + imgActualSize.ClientID + @"');
                var thumbnail = document.getElementById('" + ctrlValueImage.ClientID + @"');
                imgActual.src = thumbnail.src;
                " + popupActualSizeImage.ClientInstanceName + @".Show();
            }";
        cbpEdit.ClientSideEvents.Init = @"function(s,e)
            {
                s.PerformCallback('" + KEY_DISPLAY + @"');
            }";
        cbpEdit.ClientSideEvents.EndCallback = @"function(s,e)
            {
                var objs = eval('(' + s." + KEY_CP_RESULT + @" + ')');
                var table = document.getElementById('" + tbPartyAttributeForm.ClientID + @"');
                if(table != null)
                    table.style.visibility='visible';
                " + btnExpire.ClientInstanceName + @".SetEnabled(objs.isEffective);
                " + hddPartyAttribute.ClientInstanceName + @".Set('" + HKEY_ATTRIBUTE_ID + @"', objs.AttributeID );
            }";
        popupActualSizeImage.PopupElementID = ctrlValueImage.ClientID;
        rpanelPartyAttributeDetail.HeaderText = "รายละเอียดคุณสมบัติ";
        initializeGridView();
    }

    private void initializeGridView()
    {
        GridViewDataTextColumn column;
        column = new GridViewDataTextColumn() { FieldName = "AttributeKey", Caption = "AttributeKey" };
        column.PropertiesEdit.DisplayFormatString = base.DateOutputFormat;
        gdvPartyAttribute.Columns.Add(column);
        column = new GridViewDataTextColumn() { Name = "EffectiveFrom", Caption = ResGeneral.EffectiveFrom };
        column.PropertiesEdit.DisplayFormatString = base.DateOutputFormat;
        gdvPartyAttribute.Columns.Add(column);
        column = new GridViewDataTextColumn() { Name = "EffectiveTo", Caption = ResGeneral.EffectiveTo };
        column.PropertiesEdit.DisplayFormatString = base.DateOutputFormat;
        gdvPartyAttribute.Columns.Add(column);

        gdvPartyAttribute.ClientSideEvents.EndCallback = @"
            function(s,e)
            {
                var objs = eval('(' + s." + KEY_CP_RESULT + @" + ')');
                if(objs == null)return;
                if(objs.Message != '')
                {
                    alert(objs.Message);
                }
                var index = s.GetFocusedRowIndex();
                if(index > -1)
                {
                    " + btnAdd.ClientInstanceName + @".SetEnabled(true);
                    " + cbpEdit.ClientInstanceName + @".PerformCallback('" + KEY_DISPLAY + @";' + index);
                }else
                {
                    " + cbpEdit.ClientInstanceName + @".PerformCallback('" + KEY_DISPLAY + @"');
                }
                //" + hddPartyAttribute.ClientInstanceName + @".Set('" + HKEY_VALUE_IMG + @"', '');
                s." + KEY_CP_RESULT + @" = null;
            }
            ";
        gdvPartyAttribute.ClientSideEvents.FocusedRowChanged = @"function(s,e)
        {
            if(typeof(" + cbpEdit.ClientInstanceName + @") != 'undefined' && " + cbpEdit.ClientInstanceName + @" != null)
            {
                var index = s.GetFocusedRowIndex();
                if(index > -1)
                {
                    " + btnExpire.ClientInstanceName + @".SetEnabled(false);
                    " + cbpEdit.ClientInstanceName + @".PerformCallback('" + KEY_DISPLAY + @";' + index);
                }
            }
        }";
    }

    private Party GetPartyOwner()
    {
        Party party = null;
        if (OwnerID != 0 && OwnerDiscriminator != 0)
        {
            switch (OwnerDiscriminator)
            {
                case 10: party = Organization.Find(iSabayaContext, OwnerID); break;
                case 15: party = OrgUnit.Find(iSabayaContext, OwnerID); break;
                case 20: party = Person.Find(iSabayaContext, OwnerID); break;
                //case 30: party = MutualFund.Find(iSabayaContext, OwnerID); break;
                //case 40: party = ProvidentFund.Find(iSabayaContext, OwnerID); break;
                //case 50: party = MFAccount.Find(iSabayaContext, OwnerID); break;
                //case 60: party = Employee.Find(iSabayaContext, OwnerID); break;
                default: break;
            }
        }
        return party;
    }

    private long CreatePartyAttribute(long ID)
    {
        PartyAttribute attribute = null;
        if (ID == 0)
        {
            attribute = new PartyAttribute();
            attribute.ID = 0;
        }
        else
        {
            attribute = PartyAttribute.Find(iSabayaContext, ID);
            if (attribute == null) return 0;
            // Set Properties
        }
        if (ctrlAttributeKey.SelectedNode == null) return 0;
        attribute.Party = this.GetPartyOwner();
        attribute.AttributeKey = ctrlAttributeKey.SelectedNode;
        attribute.AttributeKeyRootNode = ctrlAttributeKey.SelectedNode.Root;

        DateTime dateFrom = ctrlEffectiveFrom.Date;
        dateFrom = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        attribute.EffectivePeriod = new TimeInterval(dateFrom, ctrlEffectiveTo.Date);
        attribute.ValueDate = ctrlValueDate.Date;
        if (!string.IsNullOrEmpty(hddValueImage.Value))
            attribute.ValueImage = ImageUtil.Base64ToImage(hddValueImage.Value);
        attribute.ValueMLS = ctrlValueMLS.Value;
        attribute.ValueNode = null;
        attribute.ValueNodeRoot = null;
        attribute.ValueNumber = Convert.ToSingle(spnValueNumber.Value);
        attribute.ValueText = txtValueText.Text;
        if (OwnerID != 0)
            attribute.Save(iSabayaContext);
        else
        {
            ICategorizedTemporalList<PartyAttribute> attributes = PartyAttributes;
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i].AttributeKey.NodeID == attribute.AttributeKey.NodeID)
                {
                    attributes.Remove(attributes[i]);
                    break;
                }
            }
            PartyAttributes.Add(attribute);
        }
        return attribute.ID;
    }

    private void ExpirePartyAttribute(long ID)
    {
        PartyAttribute attribute = PartyAttribute.Find(iSabayaContext, ID);
        if (attribute == null) return;
        if (attribute.EffectivePeriod.IsEffective())
        {
            attribute.EffectivePeriod.ExpiryDate = DateTime.Now;
            attribute.Save(iSabayaContext);
        }
    }

    protected void gdvPartyAttribute_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ASPxGridView gdv = (ASPxGridView)sender;
        ITransaction tx = iSabayaContext.PersistenceSession.BeginTransaction();
        JsonObjectCollection objs = new JsonObjectCollection();
        bool isSetNewFoucused = false;
        bool isCommit = true;
        long attributeID = 0;
        string msg = "";
        try
        {
            string[] parameters = e.Parameters.Split(';');
            switch (parameters[0])
            {
                case KEY_EDIT:
                    attributeID = this.CreatePartyAttribute(long.Parse(parameters[1]));
                    isSetNewFoucused = true;
                    break;
                case KEY_EXPIRE:
                    int index = Int32.Parse(parameters[1]);
                    if (OwnerID == 0)
                    {
                        gdv.DataBind();
                        PartyAttribute attribute = (PartyAttribute)gdv.GetRow(index);
                        ICategorizedTemporalList<PartyAttribute> attributes = PartyAttributes;
                        for (int i = 0; i < attributes.Count; i++)
                        {
                            if (attributes[i].AttributeKey == attribute.AttributeKey)
                            {
                                attributes.Remove(attributes[i]);
                                break;
                            }
                        }
                    }
                    else
                    {
                        attributeID = (int)gdv.GetRowValues(index, gdv.KeyFieldName);
                        this.ExpirePartyAttribute(attributeID);
                    }
                    break;
                // when owner id = 0 only
                case "clear":
                    if (OwnerID == 0)
                        PartyAttributes.Clear();
                    break;
                default:
                    isCommit = false;
                    break;
            }

            if (isCommit)
                tx.Commit();
            gdv.DataBind();
            if (attributeID > 0 && isSetNewFoucused)
            {
                for (int i = gdv.VisibleRowCount - 1; i >= gdv.VisibleStartIndex; i--)
                {
                    if ((int)gdv.GetRowValues(i, gdv.KeyFieldName) == attributeID)
                    {
                        gdv.FocusedRowIndex = i;
                        break;
                    }
                }
            }
            else
            {
                gdv.FocusedRowIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (isCommit)
                tx.Rollback();
            while (ex != null)
            {
                msg = ex.Message + "\n" + msg;
                ex = ex.InnerException;
            }
            gdv.DataBind();
        }
        objs.Add(new JsonNumericValue("AttributeID", attributeID));
        objs.Add(new JsonStringValue("Message", msg));
        gdv.JSProperties[KEY_CP_RESULT] = objs.ToString();
    }

    protected void cbpEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        JsonObjectCollection objs = new JsonObjectCollection();
        ASPxCallbackPanel cbp = (ASPxCallbackPanel)sender;
        int attributeID = 0;
        bool isEffective = false;
        PartyAttribute partyAttribute = null;
        string[] parameters = e.Parameter.Split(';');
        if (parameters.Length > 1)
        {
            int index = int.Parse(parameters[1]);
            if (index >= 0)
            {
                if (OwnerID != 0)
                {
                    attributeID = (int)gdvPartyAttribute.GetRowValues(index, gdvPartyAttribute.KeyFieldName);
                    partyAttribute = PartyAttribute.Find(iSabayaContext, attributeID);
                }
                else
                {
                    TreeListNode node = (TreeListNode)gdvPartyAttribute.GetRowValues(index, "AttributeKey");
                    ICategorizedTemporalList<PartyAttribute> attributes = PartyAttributes;
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        if (attributes[i].AttributeKey.NodeID == node.NodeID)
                        {
                            partyAttribute = attributes[i];
                            break;
                        }
                    }
                }
            }
        }
        switch (parameters[0])
        {
            case KEY_DISPLAY:
                tbButtonSaveCancel.Visible = false;
                uplImage.Visible = false;
                btnUploadImg.Visible = false;
                if (partyAttribute != null)
                {
                    isEffective = partyAttribute.EffectivePeriod.IsEffective();
                    if (!isEffective)
                    {
                        string color = ColorTranslator.ToHtml(System.Drawing.Color.Red);
                        tdAttributeKey.Style.Add(HtmlTextWriterStyle.Color, color);
                        tdEffectiveTo.Style.Add(HtmlTextWriterStyle.Color, color);
                        tdEffectiveFrom.Style.Add(HtmlTextWriterStyle.Color, color);
                        tdValueDate.Style.Add(HtmlTextWriterStyle.Color, color);
                        tdValueMLS.Style.Add(HtmlTextWriterStyle.Color, color);
                        tdValueNode.Style.Add(HtmlTextWriterStyle.Color, color);
                        tdValueNumber.Style.Add(HtmlTextWriterStyle.Color, color);
                        tdValueText.Style.Add(HtmlTextWriterStyle.Color, color);
                        tdValueImage.Style.Add(HtmlTextWriterStyle.Color, color);
                    }
                    tdAttributeKey.InnerText = partyAttribute.AttributeKey.Title.ToString(LanguageCode);
                    tdEffectiveTo.InnerText = partyAttribute.EffectivePeriod.ExpiryDate.ToString(base.DateOutputFormat);
                    tdEffectiveFrom.InnerText = partyAttribute.EffectivePeriod.EffectiveDate.ToString(base.DateOutputFormat);
                    tdValueDate.InnerText = partyAttribute.ValueDate.ToString(base.DateOutputFormat);
                    tdValueMLS.InnerHtml = "&nbsp;";
                    if (partyAttribute.ValueMLS != null)
                    {
                        string value = partyAttribute.ValueMLS.ToString(base.LanguageCode);
                        if (!string.IsNullOrEmpty(value))
                            tdValueMLS.InnerHtml = partyAttribute.ValueMLS.ToString(base.LanguageCode);
                    }
                    tdValueNode.InnerHtml = partyAttribute.ValueNode != null ? partyAttribute.ValueNode.ToString(base.LanguageCode) : "&nbsp;";
                    tdValueNumber.InnerHtml = partyAttribute.ValueNumber.ToString();
                    tdValueText.InnerHtml = !String.IsNullOrEmpty(partyAttribute.ValueText) ? partyAttribute.ValueText : "&nbsp;";
                    if (partyAttribute.ValueImage != null)
                    {
                        Size aspectSize = ImageUtil.GetAspectSize(partyAttribute.ValueImage.Size, 100);
                        byte[] imageBytes = ImageUtil.ImageToBytes(partyAttribute.ValueImage);
                        ctrlValueImage.ContentBytes = imageBytes;
                        ctrlValueImage.Width = aspectSize.Width;
                        ctrlValueImage.Height = aspectSize.Height;
                    }
                    else
                        tdValueImage.InnerHtml = "&nbsp;";
                }
                else
                {
                    panelEditForm.Controls.Clear();
                    panelEditForm.Controls.Add(new Label() { Text = "No Party Attribute" });
                }

                break;
            //===============================================================================
            // Edit case is obsoleted
            //===============================================================================
            case KEY_EDIT:
                tbButtonSaveCancel.Visible = true;
                TreeListNode rootNode = null;
                switch (OwnerDiscriminator)
                {
                    case 10: rootNode = iSabayaContext.Configuration.Organization.AttributeKeyParentNode; break; //Organization;
                    case 15: rootNode = iSabayaContext.Configuration.Organization.AttributeKeyParentNode; break; //OrgUnit
                    case 20: rootNode = iSabayaContext.Configuration.Person.AttributeKeyParentNode; break; //Person
                    //case 30: rootNode = iSabayaContext.iSabayaConfig.MF.AttributeKeyRootNode; break; //MutualFund
                    //case 40: rootNode = iSabayaContext.iSabayaConfig.MF.AttributeKeyRootNode; break; // ProvidentFund
                    //case 50: rootNode = iSabayaContext.iSabayaConfig.MF.AttributeKeyRootNode; break; // MFAccount
                    //case 60: rootNode = iSabayaContext.iSabayaConfig.MF.AttributeKeyRootNode; break; //Employee
                    default: break;
                }
                ctrlAttributeKey.ParentNode = rootNode;
                ctrlAttributeKey.DataBind();
                ctrlAttributeKey.SetValidation("attributeEdit");
                //ctrlAttributeKey.InitializeTreeListNode("Attribute Key", rootNode, null);
                uplImage.Visible = true;
                btnUploadImg.Visible = true;
                if (gdvPartyAttribute.FocusedRowIndex >= 0)
                {
                    if (partyAttribute != null)
                    {
                        ctrlAttributeKey.SelectedNode = partyAttribute.AttributeKey;
                        ctrlEffectiveFrom.Date = partyAttribute.EffectivePeriod.EffectiveDate;
                        ctrlEffectiveTo.Date = partyAttribute.EffectivePeriod.ExpiryDate;
                        ctrlValueDate.Value = partyAttribute.ValueDate != null ? (object)partyAttribute.ValueDate : null;
                        ctrlValueMLS.Value = partyAttribute.ValueMLS;
                        spnValueNumber.Value = partyAttribute.ValueNumber;
                        txtValueText.Value = partyAttribute.ValueText;
                        if (partyAttribute.ValueImage != null)
                        {
                            Size aspectSize = ImageUtil.GetAspectSize(partyAttribute.ValueImage.Size, 100);
                            byte[] imageBytes = ImageUtil.ImageToBytes(partyAttribute.ValueImage);
                            ctrlValueImage.ContentBytes = imageBytes;
                            ctrlValueImage.Width = aspectSize.Width;
                            ctrlValueImage.Height = aspectSize.Height;
                        }
                    }
                }
                else
                {
                    ctrlEffectiveTo.Date = TimeInterval.MaxDate;
                }
                break;
            //=================================================================================
            default: break;
        }
        //jsonCollection.Add(new JsonStringValue("CallbackType", e.Parameter));
        objs.Add(new JsonBooleanValue("isEffective", isEffective));
        objs.Add(new JsonNumericValue("AttributeID", attributeID));
        cbp.JSProperties[KEY_CP_RESULT] = objs.ToString();
    }

    protected void uplImage_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        if (e.UploadedFile.IsValid)
            e.CallbackData = Convert.ToBase64String(e.UploadedFile.FileBytes);
    }

    protected void cbpValueImage_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (!string.IsNullOrEmpty(e.Parameter))
        {
            System.Drawing.Image originalimage = ImageUtil.Base64ToImage(e.Parameter);
            Size aspectSize = ImageUtil.GetAspectSize(originalimage.Size, 100);
            ctrlValueImage.ContentBytes = ImageUtil.ImageToBytes(originalimage);
            ctrlValueImage.Width = aspectSize.Width;
            ctrlValueImage.Height = aspectSize.Height;
            hddValueImage.Value = e.Parameter;
        }
    }

    protected void gdvPartyAttribute_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        ASPxGridView gdv = (ASPxGridView)sender;
        object[] EffectivePeriod = (object[])gdv.GetRowValues(e.VisibleIndex, "EffectiveFrom", "EffectiveTo");
        if (e.RowType == GridViewRowType.Data)
        {
            if (!(((DateTime)EffectivePeriod[0]) <= DateTime.Now && ((DateTime)EffectivePeriod[1]) >= DateTime.Now))
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void cbpEdit_PreRender(object sender, EventArgs e)
    {
        SetThemeTable(ref tbPartyAttributeForm);
    }

    private void SetThemeTable(ref HtmlTable table)
    {
        string cssPostfix = gdvPartyAttribute.CssPostfix;
        table.SetTableStyle(cssPostfix);
        for (int i = 0; i < table.Rows.Count; i++)
        {
            table.Rows[i].Cells[0].SetHeaderCellStyle(cssPostfix);
            for (int j = 1; j < table.Rows[i].Cells.Count; j++)
                table.Rows[i].Cells[j].SetDataCellStyle();
            table.Rows[i].SetRowStyle(cssPostfix);
        }
    }
}