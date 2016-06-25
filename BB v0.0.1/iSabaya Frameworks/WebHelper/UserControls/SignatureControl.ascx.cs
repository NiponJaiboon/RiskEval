using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using imSabaya;
using imSabaya.MutualFundSystem;
using iSabaya;
using Resources;
using WebHelper;

public partial class ctrls_SignatureControl : iSabayaControl
{
    protected ASPxButton editPerson = null;
    private const string TH_TAGNAME = "th";
    private const string TD_TAGNAME = "td";

    public enum TableDirection
    {
        Horizontal,
        Vertical,
    }

    public IList<Person> PersonList { get; set; }

    private string headerText = "Signature";

    public string HeaderText
    {
        get { return this.headerText; }
        set { this.headerText = value; }
    }

    private TableDirection directionAlign = TableDirection.Horizontal;

    public TableDirection DirectionAlign
    {
        get { return this.directionAlign; }
        set { this.directionAlign = value; }
    }

    private bool showEffective = true;

    public bool ShowEffective
    {
        get { return this.showEffective; }
        set { this.showEffective = value; }
    }

    public string CssPostfix { get; set; }

    private bool showHeader = true;

    public bool ShowHeader
    {
        get { return this.showHeader; }
        set { this.showHeader = value; }
    }

    private bool showHeaderLeft = true;

    public bool ShowHeaderLeft
    {
        get { return this.showHeaderLeft; }
        set { this.showHeaderLeft = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            CreatetSignatureTable();
    }

    public MultiOwnerPortfolio MFAccount
    {
        get
        {
            if (String.IsNullOrEmpty(hddMfAccountID.Value))
                return null;
            int id = int.Parse(hddMfAccountID.Value);
            if (id == 0)
                return null;
            return iSabayaContext.PersistencySession.Get<MFAccount>(id);
        }
        set
        {
            PersonList = new List<Person>();
            if (value != null)
            {
                hddMfAccountID.Value = value.InvestmentPortfolioID.ToString();
                Party owner;
                int count = value.Owners.Count;
                if (count <= 0)
                    return;
                if (value.OwnerConnective != Connective.And)
                    count = 1;
                for (int i = 0; i < count; i++)
                {
                    owner = value.Owners[i].Owner;
                    if (owner is Person)
                    {
                        PersonList.Add((Person)owner);
                    }
                    else if (owner is Organization)
                    {
                        Organization org = (Organization)owner;
                        foreach (PersonOrgRelation por in org.ListAuthorizedDirectors(iSabayaContext))
                            PersonList.Add(por.Person);
                    }
                }
            }
            else
                hddMfAccountID.Value = "0";
        }
    }

    public void CreatetSignatureTable()
    {




        HtmlTableCell cell;
        CssPostfix = WebConstants.GetCssPostfix(Page.Theme);
        tbSignature.Rows.Clear();
        tbSignature.SetTableStyle(CssPostfix);
        tbCtrl_Signature.SetTableControlStyle(CssPostfix);
        tbCtrl_Signature.Attributes["border"] = "1";
        int count = 0;
        if (PersonList != null)
            count = PersonList.Count;



        HtmlTableRow trHeader = null;
        if (showHeader)
        {
            cell = new HtmlTableCell() { InnerHtml = HeaderText };
            cell.SetHeaderCellStyle(CssPostfix);
            cell.Attributes["style"] = "text-align:center";
            trHeader = new HtmlTableRow();
            trHeader.SetRowStyle(CssPostfix);
            trHeader.Cells.Add(cell);
            tbSignature.Rows.Add(trHeader);
        }

        HtmlTableRow trSignature = new HtmlTableRow();
        HtmlTableRow trPartyName = new HtmlTableRow();
        HtmlTableRow trEffectivePeriod = null;
        HtmlTableRow trEditPerson = new HtmlTableRow();
        trSignature.SetRowStyle(CssPostfix);
        trPartyName.SetRowStyle(CssPostfix);
        trEditPerson.SetRowStyle(CssPostfix);
        tbSignature.Rows.Add(trSignature);
        tbSignature.Rows.Add(trPartyName);
        tbSignature.Rows.Add(trEditPerson);

        if (count > 0)
        {
            ASPxBinaryImage signature;
            switch (DirectionAlign)
            {
                case TableDirection.Vertical:
                    break;
                default:
                    count = PersonList.Count;
                    if (showHeaderLeft)
                    {
                        cell = new HtmlTableCell() { Align = "Center", InnerHtml = Resource_Person.Signature };
                        cell.SetHeaderCellStyle(CssPostfix);
                        trSignature.Cells.Add(cell);

                        cell = new HtmlTableCell() { Align = "Center", InnerHtml = Resource_Person.FullName };
                        cell.SetHeaderCellStyle(CssPostfix);
                        trPartyName.Cells.Add(cell);

                        // Edit Person Button
                        cell = new HtmlTableCell() { Align = "Center", InnerHtml = "แก้ไขข้อมูล" };
                        cell.SetHeaderCellStyle(CssPostfix);
                        trEditPerson.Cells.Add(cell);

                        if (showEffective)
                        {
                            trEffectivePeriod = new HtmlTableRow();
                            cell = new HtmlTableCell(TH_TAGNAME) { Align = "Center", InnerHtml = Resource_Global.EffectivePeriod };
                            cell.SetHeaderCellStyle(CssPostfix);
                            trEffectivePeriod.Cells.Add(cell);
                        }
                    }
                    for (int i = 0; i < PersonList.Count; i++)
                    {
                        // Signature Row
                        System.Drawing.Image sig = PersonList[i].GetSignature(DateTime.Now);
                        cell = new HtmlTableCell() { ID = string.Format("sig{0}", i), Align = "Center" };
                        cell.SetDataCellStyle();
                        cell.NoWrap = true;
                        trSignature.Cells.Add(cell);
                        if (sig != null)
                        {
                            signature = new ASPxBinaryImage();
                            signature.ContentBytes = WebHelper.ImageUtil.ImageToBytes(sig);
                            cell.Controls.Add(signature);
                        }
                        else
                        {
                            cell.InnerHtml = "&nbsp;";
                        }
                        // Party Name Row
                        cell = new HtmlTableCell() { ID = string.Format("signame{0}", i), Align = "Center" };
                        cell.SetDataCellStyle();
                        cell.NoWrap = true;
                        cell.InnerHtml = PersonList[i].FullName;
                        trPartyName.Cells.Add(cell);

                        // Edit Person
                        cell = new HtmlTableCell() { ID = string.Format("editPers{0}", i), Align = "Center" };
                        cell.SetDataCellStyle();
                        cell.NoWrap = true;

                        editPerson = new ASPxButton()
                        {
                            AutoPostBack = false
                        };
                        editPerson.SetEditButton();
                        editPerson.ClientInstanceName = this.ClientID + editPerson.ID;

                        editPerson.ClientSideEvents.Click = @"function(s, e)
                        {
                            cbpEditPerson.PerformCallback('" + PersonList[i].PartyID + @"');
                        }";

                        cbpEditPerson.ClientSideEvents.EndCallback = @"function(s, e)
		                {
		                    popupEditPerson.Show();
		                }";

                        cell.Controls.Add(editPerson);
                        trEditPerson.Cells.Add(cell);

                        // Effective Period Row
                        if (showEffective)
                        {
                            cell = new HtmlTableCell() { ID = string.Format("sigeff{0}", i), Align = "Center" };
                            cell.SetDataCellStyle();
                            cell.NoWrap = true;
                            TimeInterval effectivePeriod = PersonList[i].EffectivePeriod;
                            if (effectivePeriod != null)
                                cell.InnerHtml = PersonList[i].EffectivePeriod.ToString(base.DateOutputFormat, base.LanguageCode);
                            else
                                cell.InnerHtml = "N/A";
                            trEffectivePeriod.Cells.Add(cell);
                        }
                    }
                    break;
            }
            if (trEffectivePeriod != null)
            {
                trEffectivePeriod.SetRowStyle(CssPostfix);
                tbSignature.Rows.Add(trEffectivePeriod);
            }
        }
        else
        {
            HtmlTableRow trNoData = new HtmlTableRow();
            trNoData.SetRowStyle(CssPostfix);
            cell = new HtmlTableCell()
                {
                    ID = "sigNoData",
                    Align = "Center",
                    InnerHtml = "No Person Data"
                };
            cell.SetDataCellStyle();
            trNoData.Cells.Add(cell);
            tbSignature.Rows.Add(trNoData);
        }
        if (trHeader != null)
            if (trPartyName.Cells.Count > 1)
                trHeader.Cells[0].ColSpan = trPartyName.Cells.Count;
    }

    protected void cbpEditPerson_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        Party owner = FindPartyByID(int.Parse(e.Parameter.ToString()));
        popupEditPerson.HeaderText = "แก้ไขข้อมูล";
        if (owner is Person)
        {
            popupEditPerson.ContentUrl = "~/modules/master/ListPerson.aspx?personID=" + owner.PartyID;
        }
        else if (owner is Organization)
        {
            popupEditPerson.ContentUrl = "~/modules/master/Organization/OrganizationDetail.aspx?organizationID=" + owner.PartyID;
        }
        else // incognito
        {
            popupEditPerson.ContentUrl = "~/modules/master/ListIncognito.aspx?incognitoID=" + owner.PartyID;
        }
    }

    private Party FindPartyByID(int partyID)
    {
        return iSabayaContext.PersistencySession.Get<Party>(partyID);
    }

    //protected override void OnPreRender(EventArgs e)
    //{
    //    base.OnPreRender(e);
    //    this.SetThemeTable(ref tbSignature);
    //}

    //private void SetThemeTable(ref HtmlTable table)
    //{
    //    string cssPostfix = GetCssPostfix();
    //    table.Attributes.Add("class", string.Format("dxgvTable_{0}", cssPostfix));
    //    for (int i = 0; i < table.Rows.Count; i++)
    //    {
    //        for (int j = 0; j < table.Rows[i].Cells.Count; j++)
    //        {
    //            HtmlTableCell cell = table.Rows[i].Cells[j];
    //            if (cell.TagName.Equals(TD_TAGNAME))
    //                cell.Attributes.Add("class", "dxgv");
    //            else if (cell.TagName.Equals(TH_TAGNAME))
    //                cell.Attributes.Add("class", string.Format("dxgvHeader_{0}", cssPostfix));
    //        }
    //        table.Rows[i].Attributes.Add("class", string.Format("dxgvDataRow_{0}", cssPostfix));
    //    }
    //}
}