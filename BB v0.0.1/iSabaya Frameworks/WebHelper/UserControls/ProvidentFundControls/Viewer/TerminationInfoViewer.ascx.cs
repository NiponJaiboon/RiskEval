using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using imSabaya;
using iSabaya;
using System.Text;
using WebHelper;
using imSabaya.ProvidentFundSystem;

public partial class ctrls_Viewer_TerminationInfoViewer : iSabayaControl
{
    public TerminationInfo MemberTerminationInfo { get; set; }
    public string CssPostfix { get; set; }
    private string strNull = "NULL";
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
            DataBind();
    }
    public new void DataBind()
    {
        if (MemberTerminationInfo != null)
            this.SetDataTable();
        else
            this.NoDataTable();
    }
    private void InitialTalbe()
    {
        tbData.Rows.Clear();
        TableRow tr = new TableRow();
        TableCell td = new TableCell() { ColumnSpan = 2 };
        td.Controls.Add(new Literal { Text = "Termination Info" });
        td.SetHeaderCellStyle(CssPostfix);
        td.Style[HtmlTextWriterStyle.TextAlign] = "center";
        tr.Cells.Add(td);
        tr.SetRowStyle(CssPostfix);
        tbData.Rows.Add(tr);
        tbData.SetPageContentStyle(CssPostfix);
        tbData.SetTableStyle(CssPostfix);
        tbControl.SetTableControlStyle(CssPostfix);
    }
    private void NoDataTable()
    {
        InitialTalbe();
        TableRow row = new TableRow();
        TableCell cell = new TableCell();
        cell.Text = "No Data to display.";
        cell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        cell.Width = new Unit(400);
        row.Cells.Add(cell);
        tbData.Rows.Add(row);
    }
    private void AddDataItem(string caption, DateTime date)
    {
        this.AddDataItem(caption, date.ToString());
    }
    private void AddDataItem(string caption, Money money)
    {
        if (money != null)
            this.AddDataItem(caption, money.ToString());
        else
            this.AddDataItem(caption, strNull);
    }
    private void AddDataItem(string caption, MultilingualString mls)
    {
        this.AddDataItem(caption, mls.ToString(base.LanguageCode));
    }
    private void AddDataItem(string caption, string value)
    {
        TableRow tr = new TableRow();
        tr.SetRowStyle(CssPostfix);
        //caption
        TableCell td = new TableCell() { ForeColor = System.Drawing.Color.Black };
        td.Controls.Add(new Literal() { Text = caption });
        td.SetDataCellStyle();
        tr.Cells.Add(td);
        //data
        td = new TableCell() { ForeColor = System.Drawing.Color.Black };
        td.Controls.Add(new Literal() { Text = value });
        td.SetDataCellStyle();
        tr.Cells.Add(td);
        tbData.Rows.Add(tr);
    }
    private void AddDataItem(string headerText, IList<BeneficiaryDesignation> beneficiaries)
    {
        Table tbCtrl = new Table()
        {
            Width = Unit.Percentage(80),
            HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center
        };
        tbCtrl.SetTableControlStyle(CssPostfix);
        TableRow tr = new TableRow();
        TableCell td = new TableCell();
        Table tb = new Table()
        {
            Width = Unit.Percentage(100),
            HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center
        };
        tb.SetTableStyle(CssPostfix);
        td.Controls.Add(tb);
        tr.Cells.Add(td);
        tbCtrl.Rows.Add(tr);

        //Header Table
        tr = new TableRow();
        td = new TableCell() { ColumnSpan = 3 };
        td.SetHeaderCellStyle(CssPostfix);
        td.Style[HtmlTextWriterStyle.TextAlign] = "center";
        tr.SetRowStyle(CssPostfix);
        td.Controls.Add(new Literal() { Text = headerText });
        tr.Cells.Add(td);
        tb.Rows.Add(tr);

        //Header Column
        tr = new TableRow();
        td = new TableCell();
        tr.SetRowStyle(CssPostfix);
        td.SetHeaderCellStyle(CssPostfix);
        td.Controls.Add(new Literal() { Text = "Beneficiary" });
        tr.Cells.Add(td);

        td = new TableCell();
        td.SetHeaderCellStyle(CssPostfix);
        td.Controls.Add(new Literal() { Text = "Percentage" });
        tr.Cells.Add(td);

        //td = new TableCell();
        //td.Controls.Add(new Literal() { Text = "Amount" });
        //tr.Cells.Add(td);

        tb.Rows.Add(tr);
        string strValue = strNull;
        if (beneficiaries.Count > 0)
        {
            for (int i = 0; i < beneficiaries.Count; i++)
            {
                tr = new TableRow();
                td = new TableCell();
                td.SetDataCellStyle();
                tr.SetRowStyle(CssPostfix);
                if (beneficiaries[i].Beneficiary != null)
                    strValue = beneficiaries[i].Beneficiary.ToString();
                else
                    strValue = beneficiaries[i].BeneficiaryName;
                td.Controls.Add(new Literal() { Text = strValue });
                tr.Cells.Add(td);

                td = new TableCell();
                td.SetDataCellStyle();
                td.Controls.Add(new Literal() { Text = beneficiaries[i].Percentage.ToString("#0.00") });
                tr.Cells.Add(td);

                //td = new TableCell() { ForeColor = System.Drawing.Color.Black };
                //td.Controls.Add(new Literal() { Text = beneficiaries[i].Amount.ToString() });
                //tr.Cells.Add(td);
                tb.Rows.Add(tr);
            }
        }
        else
        {
            tr = new TableRow();
            td = new TableCell() { ForeColor = System.Drawing.Color.Black, ColumnSpan = 2 };
            td.SetDataCellStyle();
            tr.SetRowStyle(CssPostfix);
            td.Controls.Add(new Literal() { Text = "No Data to display" });
            tr.Cells.Add(td);
            tb.Rows.Add(tr);
        }
        tr = new TableRow();
        td = new TableCell() { ColumnSpan = 2 };
        td.SetDataCellStyle();
        tr.SetRowStyle(CssPostfix);
        td.Controls.Add(tbCtrl);
        tr.Cells.Add(td);
        tbData.Rows.Add(tr);
    }
    private void AddDataItem(string headerText, IList<InvestmentCategoryTransfer> investmentTransfer)
    {
        Table tbCtrl = new Table() 
        {
            Width = Unit.Percentage(80),
            HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center
        };
        tbCtrl.SetTableControlStyle(CssPostfix);
        TableRow tr = new TableRow();
        TableCell td = new TableCell();
        Table tb = new Table() 
        {
            Width = Unit.Percentage(100),
            HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center
        };
        tb.SetTableStyle(CssPostfix);
        td.Controls.Add(tb);
        tr.Cells.Add(td);
        tbCtrl.Rows.Add(tr);

        //Header Table
        tr = new TableRow();
        td = new TableCell() { ColumnSpan = 3 };
        td.SetHeaderCellStyle(CssPostfix);
        td.Style[HtmlTextWriterStyle.TextAlign] = "center";
        tr.SetRowStyle(CssPostfix);
        td.Controls.Add(new Literal() { Text = headerText });
        tr.Cells.Add(td);
        tb.Rows.Add(tr);
        //Header Column
        tr = new TableRow();
        td = new TableCell();
        tr.SetRowStyle(CssPostfix);
        td.SetHeaderCellStyle(CssPostfix);
        td.Controls.Add(new Literal() { Text = "From Category" });
        tr.Cells.Add(td);

        td = new TableCell();
        td.SetHeaderCellStyle(CssPostfix);
        td.Controls.Add(new Literal() { Text = "To Category" });
        tr.Cells.Add(td);

        td = new TableCell();
        td.SetHeaderCellStyle(CssPostfix);
        td.Controls.Add(new Literal() { Text = "Apply Vesting Plan" });
        tr.Cells.Add(td);

        tb.Rows.Add(tr);
        string strValue = strNull;
        if (investmentTransfer.Count > 0)
        {
            for (int i = 0; i < investmentTransfer.Count; i++)
            {
                tr = new TableRow();
                td = new TableCell();
                tr.SetRowStyle(CssPostfix);
                td.SetDataCellStyle();
                td.Controls.Add(new Literal() { Text = investmentTransfer[i].FromCategory.ToString(base.LanguageCode) });
                tr.Cells.Add(td);

                td = new TableCell();
                td.SetDataCellStyle();
                td.Controls.Add(new Literal() { Text = investmentTransfer[i].ToCategory.ToString(base.LanguageCode) });
                tr.Cells.Add(td);

                td = new TableCell();
                td.SetDataCellStyle();
                td.Style[HtmlTextWriterStyle.TextAlign] = "center";
                if (investmentTransfer[i].ApplyVestingPlan)
                    td.Controls.Add(new Image() { ImageUrl = ResImageURL.Accept });
                else
                    td.Controls.Add(new Image() { ImageUrl = ResImageURL.Cross });
                tr.Cells.Add(td);
                tb.Rows.Add(tr);
            }
        }
        else
        {
            tr = new TableRow();
            td = new TableCell() { ForeColor = System.Drawing.Color.Black, ColumnSpan = 2 };
            tr.SetRowStyle(CssPostfix);
            td.SetDataCellStyle();
            td.Controls.Add(new Literal() { Text = "No Data to display" });
            tr.Cells.Add(td);
            tb.Rows.Add(tr);
        }
        tr = new TableRow();
        td = new TableCell() { ColumnSpan = 2 };
        td.Controls.Add(tbCtrl);
        tr.Cells.Add(td);
        tbData.Rows.Add(tr);
    }
    private void AddDataItem(string caption, BankAccount ba)
    {
        if (ba != null)
        {
            string value = string.Format("{0} {1} {2} {3}", ba.AccountNo,
                ba.AccountName.ToString(base.LanguageCode),
                ba.Bank.CurrentName.Name.ToString(base.LanguageCode),
                ba.Branch != null ? ba.Branch.CurrentName.Name.ToString(base.LanguageCode) : "-");
            this.AddDataItem(caption, value);
        }
        else
            this.AddDataItem(caption, strNull);
    }
    private String MonthlyScheduleToString(int month)
    {

        StringBuilder str = new StringBuilder();
        String split = ", ";
        for (int i = 0; i < 12; i++)
        {
            if ((month & (1 << i)) > 0)
            {
                str.Append((new DateTime(1, 1 + i, 1)).ToString("MMM"));
                str.Append(split);
            }
        }
        return str.ToString().TrimEnd(new char[] { ' ', ',' });
    }
    public void SetDataTable()
    {
        InitialTalbe();
        this.AddDataItem("Account No:", MemberTerminationInfo.Member.AccountNo);
        this.AddDataItem("Member Name: ", MemberTerminationInfo.Member.Name);
        this.AddDataItem("Division Code: ", MemberTerminationInfo.Member.DivisionCode);
        this.AddDataItem("Employee No: ", MemberTerminationInfo.Member.EmployeeNo);
        this.AddDataItem("Employer Name: ", MemberTerminationInfo.Employer.Name);
        this.AddDataItem("Termination Category: ", MemberTerminationInfo.TerminationCategory.ToString(base.LanguageCode));
        this.AddDataItem("Termination Date: ", MemberTerminationInfo.TerminationDate);
        this.AddDataItem("Employment Period: ", MemberTerminationInfo.EmploymentPeriod.ToString(base.DateOutputFormat, base.LanguageCode));
        this.AddDataItem("Employment Duration: ", MemberTerminationInfo.EmploymentDuration.ToString());
        this.AddDataItem("Membership Period: ", MemberTerminationInfo.MembershipPeriod.ToString(base.DateOutputFormat, base.LanguageCode));
        this.AddDataItem("Membership Duration: ", MemberTerminationInfo.MembershipDuration.ToString());
        this.AddDataItem("Tax Paying Period: ", MemberTerminationInfo.TaxPayingPeriod.ToString(base.DateOutputFormat, base.LanguageCode));
        this.AddDataItem("Tax Paying Duration: ", MemberTerminationInfo.TaxPayingDuration.ToString());
        this.AddDataItem("Tax Paying Duration: ", MemberTerminationInfo.TaxPayingDuration.ToString());
        this.AddDataItem("Vesting Plan: ", MemberTerminationInfo.VestingPlan.Code + MemberTerminationInfo.VestingPlan.Title.ToString(base.LanguageCode));
        this.AddDataItem("Remark: ", MemberTerminationInfo.Remark);
        switch (MemberTerminationInfo.TerminationCategory.Code)
        {
            case PFConstants.MemberStatusCodeDeceased:
                AddDataItem("Beneficiaries", MemberTerminationInfo.Beneficiaries);
                break;
            case PFConstants.MemberStatusCodeExtEmployerTransferred:
            case PFConstants.MemberStatusCodeIntEmployerTransferred:
                this.AddDataItem("New Organization: ", MemberTerminationInfo.NewEmployerOrg.CurrentName.Name.ToString(base.LanguageCode));
                this.AddDataItem("New Employee No: ", MemberTerminationInfo.NewEmployeeNo);
                this.AddDataItem("Investment Category Transfers", MemberTerminationInfo.InvestmentCategoryTransfers);
                break;
            default:
                this.AddDataItem("Age: ", MemberTerminationInfo.Age > 0 ? MemberTerminationInfo.Age.ToString() : "N/A");
                this.AddDataItem("Redemption Option: ", MemberTerminationInfo.RedemptionOption.ToString());
                this.AddDataItem("Bank Account: ", MemberTerminationInfo.BankAccount);
                switch (MemberTerminationInfo.RedemptionOption)
                {
                    case PFMemberRedemptionOption.InInstallments:
                        this.AddDataItem("Initial Amount: ", MemberTerminationInfo.InitialAmount);
                        this.AddDataItem("Installment Amount: ", MemberTerminationInfo.InstallmentAmount);
                        this.AddDataItem("Installment Months: ", MonthlyScheduleToString(MemberTerminationInfo.InstallmentMonths));
                        this.AddDataItem("Extended Membership Duration: ", MemberTerminationInfo.ExtendedMembershipDuration.ToString());
                        break;
                    case PFMemberRedemptionOption.EntireAmountLater:
                        this.AddDataItem("Extended Membership Duration: ", MemberTerminationInfo.ExtendedMembershipDuration.ToString());
                        break;
                }
                this.AddDataItem("Tax: ", MemberTerminationInfo.Tax);
                break;
        }

    }

}
