using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using imSabaya;
using WebHelper;
using imSabaya.ProvidentFundSystem;
using Resources;

public partial class ctrls_Viewer_TransactionViewer : iSabayaControl
{
    public PFTransaction Transaction { get; set; }
    public string CssPostfix { get; set; }
    private string NullString = "NULL";
    private delegate void InitialTable();
    private string TransactionHeaderText = "Transaction Info";
    private string QuantityHeaderText = "Quantity";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            tbControl.SetTableControlStyle(CssPostfix);
            if (Transaction != null)
            {
                this.SetDataTable();
                this.SetQuantityTable();
            }
            else
            {
                this.NoDataTable(tbData, InitialDataTable);
                this.NoDataTable(tbQuantity, SetQuantityTable);
            }
        }
    }
    private void InitialDataTable()
    {
        tbData.SetTableStyle(CssPostfix);
        tbData.Rows.Clear();
        TableRow row = new TableRow();
        TableCell cell = new TableCell() { ColumnSpan = 2 };
        row.SetRowStyle(CssPostfix);
        cell.SetHeaderCellStyle(CssPostfix);
        cell.Style[HtmlTextWriterStyle.TextAlign] = "center";
        cell.Controls.Add(new Literal() { Text = TransactionHeaderText });
        row.Cells.Add(cell);
        tbData.Rows.Add(row);
    }
    private void NoDataTable(Table t, InitialTable initial)
    {
        initial();
        TableRow row = new TableRow();
        TableCell cell = new TableCell();
        cell.Text = "No Data to display.";
        cell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        row.Cells.Add(cell);
        t.Rows.Add(row);
    }
    private void SetHeader(ref TableRow row, string caption)
    {
        TableCell header = new TableCell();
        header.SetHeaderCellStyle(CssPostfix);
        header.SetDataCellStyle();
        header.Text = caption;
        header.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
        row.Cells.Add(header);
    }
    private void AddDataToTable(string caption, string value)
    {
        TableRow row = new TableRow();
        row.SetRowStyle(CssPostfix);
        SetHeader(ref row, caption);

        TableCell dataCell = new TableCell();
        dataCell.SetDataCellStyle();
        dataCell.Text = value;
        dataCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
        row.Cells.Add(dataCell);
        tbData.Rows.Add(row);
    }
    public void SetDataTable()
    {
        PFTransaction fundTransaction = Transaction;
        //tbControl.SetTableControlStyle(CssPostfix);
        InitialDataTable();
        //tbData.SetTableStyle(CssPostfix);
        string value = NullString;
        AddDataToTable(Resource_Transaction.TransactionNo, fundTransaction.TransactionNo);
        value = !string.IsNullOrEmpty(fundTransaction.Reference) ? fundTransaction.Reference : NullString;
        AddDataToTable(Resource_Global.Reference, value);
        AddDataToTable(Resource_Global.OrderedDate, fundTransaction.TradeDate.ToString("dd MMMM yyyy"));
        AddDataToTable(Resource_Global.TradeDate, fundTransaction.TradeDate.ToString("dd MMMM yyyy hh:mm"));

        AddDataToTable(Resource_Transaction.TransactionType, fundTransaction.Type.Title.ToString());
        if(fundTransaction.Employer != null)
            AddDataToTable("นายจ้าง", fundTransaction.Employer.EmployerOrg.FullName);
        else
            AddDataToTable("นายจ้าง", NullString);
        if (fundTransaction.Fund != null && fundTransaction.Fund.Title != null)
            AddDataToTable(Resource_Fund.txtFundSelect, string.Format("{0} : {1}", fundTransaction.Fund.Code, fundTransaction.Fund.Title.ToString()));
        else
            AddDataToTable(Resource_Fund.txtFundSelect, NullString);


        Member employee = fundTransaction.Portfolio as Member;
        if (employee != null)
        {
            AddDataToTable("รหัสพนักงาน", employee.EmployeeNo);
            AddDataToTable("ชื่อพนักงาน", employee.Name.ToString(base.LanguageCode));
        }
        #region quantity old version
        //else
        //{
        //    AddDataToTable("รหัสพนักงาน", "-");
        //    AddDataToTable("ชื่อพนักงาน", "-");
        //}
        //value = fundTransaction.InvestmentCategory != null ? fundTransaction.InvestmentCategory.ToString(base.LanguageCode) : NullString;
        //AddDataToTable("ประเภทเงิน", value);
        //value = fundTransaction.MemberQuantity.Amount != null ?
        //    fundTransaction.MemberQuantity.Amount.ToString() + fundTransaction.MemberQuantity.Amount.Currency.Symbol : 
        //    NullString;
        //AddDataToTable("เงินสะสม", value);
        //value = fundTransaction.MemberQuantity.Units != null ? fundTransaction.MemberQuantity.Units.ToString(base.UnitsFormat) : NullString;
        //AddDataToTable("หน่วยสะสม", value);
        //value = fundTransaction.MemberQuantity.UnitCost != null ?
        //    fundTransaction.MemberQuantity.UnitCost.ToString() + fundTransaction.MemberQuantity.UnitCost.Currency.Symbol : 
        //    NullString;
        //AddDataToTable("ต้นทุนสะสม/หน่วย", value);

        //value = fundTransaction.EmployerQuantity.Amount != null ?
        //    fundTransaction.EmployerQuantity.Amount.ToString() + fundTransaction.EmployerQuantity.Amount.Currency.Symbol: 
        //    NullString;
        //AddDataToTable("เงินสมทบ", value);
        //value = fundTransaction.EmployerQuantity.Units != null ? fundTransaction.EmployerQuantity.Units.ToString(base.UnitsFormat) : NullString;
        //AddDataToTable("หน่วยสมทบ", value);
        //value = fundTransaction.EmployerQuantity.UnitCost != null ?
        //    fundTransaction.EmployerQuantity.UnitCost.ToString() + fundTransaction.EmployerQuantity.UnitCost.Currency.Symbol : 
        //    NullString;
        //AddDataToTable("ต้นทุนสมทบ/หน่วย", value);
        #endregion
        value = fundTransaction.Fee != null ? fundTransaction.Fee.ToString() : "-";
        AddDataToTable("ค่าธรรมเนียม", value);

        AddDataToTable(Resource_Global.UpdatedTS, fundTransaction.TransactionTS.ToString("dd MMMM yyyy hh:mm"));
        AddDataToTable(Resource_Global.UpdatedTS, fundTransaction.CreatedBy.ToString(base.LanguageCode));
        //value = fundTransaction.UnitPrice != null ? fundTransaction.UnitPrice.ToString() : "-";
        //AddDataToTable("UnitPrice", value);

        value = fundTransaction.Tax != null ? fundTransaction.Tax.ToString() : Decimal.Zero.ToString(base.CurrencyFormat);
        AddDataToTable("ภาษีมูลค่าเพิ่ม", value);

        value = !string.IsNullOrEmpty(fundTransaction.Description) ? fundTransaction.Description : NullString;
        AddDataToTable(Resource_Global.Description, value);

        AddDataToTable("RollBack Status", fundTransaction.RollbackStatus.ToString());
    }
    private void AddQuantityData(string caption, string value)
    {
        TableCell header = new TableCell();
        header.SetHeaderCellStyle(CssPostfix);
        header.Text = caption;
        header.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
        tbQuantity.Rows[1].Cells.Add(header);

        TableCell dataCell = new TableCell();
        dataCell.SetDataCellStyle();
        dataCell.Text = value;
        dataCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
        tbQuantity.Rows[2].Cells.Add(dataCell);
    }
    private void SetQuantityTable()
    {
        tbQuantity.Rows.Clear();
        tbQuantity.SetTableStyle(CssPostfix);

        TableRow headerRow = new TableRow();
        headerRow.SetRowStyle(CssPostfix);
        TableRow dataRow = new TableRow();
        TableCell cell = new TableCell() { ColumnSpan = 7 };
        cell.SetHeaderCellStyle(CssPostfix);
        cell.Style.Add(HtmlTextWriterStyle.TextAlign, "Center");
        cell.Controls.Add(new Literal() { Text = QuantityHeaderText });
        headerRow.Cells.Add(cell);
        tbQuantity.Rows.Add(headerRow);

        headerRow = new TableRow();
        headerRow.SetRowStyle(CssPostfix);
        dataRow.SetRowStyle(CssPostfix);
        tbQuantity.Rows.Add(headerRow);
        tbQuantity.Rows.Add(dataRow);
        string nullString = "NULL";
        string value;

        value = Transaction.InvestmentCategory != null ? Transaction.InvestmentCategory.ToString(base.LanguageCode) : NullString;
        AddQuantityData("ประเภทเงิน", value);
        value = Transaction.MemberQuantity.Amount != null ?
            Transaction.MemberQuantity.Amount.ToString() + Transaction.MemberQuantity.Amount.Currency.Symbol :
            nullString;
        AddQuantityData("เงินสะสม", value);
        value = Transaction.MemberQuantity != null ? Transaction.MemberQuantity.Units.ToString(base.UnitsFormat) : nullString;
        AddQuantityData("หน่วยสะสม", value);
        value = Transaction.MemberQuantity.UnitCost != null ?
            Transaction.MemberQuantity.UnitCost.ToString() + Transaction.MemberQuantity.UnitCost.Currency.Symbol :
            nullString;
        AddQuantityData("ต้นทุนสะสม/หน่วย", value);

        value = Transaction.EmployerQuantity.Amount != null ?
            Transaction.EmployerQuantity.Amount.ToString() + Transaction.EmployerQuantity.Amount.Currency.Symbol :
            nullString;
        AddQuantityData("เงินสมทบ", value);
        value = Transaction.EmployerQuantity != null ? Transaction.EmployerQuantity.Units.ToString(base.UnitsFormat) : nullString;
        AddQuantityData("หน่วยสมทบ", value);
        value = Transaction.EmployerQuantity.UnitCost != null ?
            Transaction.EmployerQuantity.UnitCost.ToString() + Transaction.EmployerQuantity.UnitCost.Currency.Symbol :
            nullString;
        AddQuantityData("ต้นทุนสมทบ/หน่วย", value);
    }
}