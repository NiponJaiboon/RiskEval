using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using imSabaya;
using WebHelper;
using iSabaya;
using System.Data;
using DevExpress.Web.ASPxGridView;
using imSabaya.ProvidentFundSystem;

public partial class ctrls_Viewer_TransactionPaymentsViewer : iSabayaControl
{
    public PFTransaction Transaction { get; set; }
    public string CssPostfix { get; set; }
    private string strNull = "NULL";
    //private List<Payment> Payments = new List<Payment>();
    private DataTable dataTable;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Payer", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Payee", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Type", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("PaymentDate", typeof(string)));
            dataTable.Columns.Add(new DataColumn("DueDate", typeof(string)));
            dataTable.Columns.Add(new DataColumn("TransactionNo", typeof(string)));
            dataTable.Columns.Add(new DataColumn("PaymentID", typeof(int)));
            ViewState[this.ClientID + "Payments"] = dataTable;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Transaction != null)
            {
                InitialPayments(Transaction);
                ViewState[this.ClientID + "Payments"] = gdvPayments.DataSource = dataTable;
                gdvPayments.DataBind();
            }
            else
            {
                gdvPayments.DataSource = ViewState[this.ClientID + "Payments"];
            }
        }
        else
        {
            gdvPayments.DataSource = ViewState[this.ClientID + "Payments"];
        }
    }
    private void InitialPayments(FundTransaction transaction)
    {
        IList<imSabaya.TransactionPayment> tp = transaction.Payments;
        for (int i = 0; i < tp.Count; i++)
        {
            //Payments.Add(tp[i].Payment);
            DataRow dr = dataTable.NewRow();
            Payment p = tp[i].Payment;
            dr["ID"] = p.PaymentID;
            dr["Payer"] = p.Payer != null ? p.Payer.ToString() : strNull;
            dr["Payee"] = p.Payee != null ? p.Payee.ToString() : strNull;
            dr["Type"] = p.Type.Name;
            dr["Amount"] = p.Amount != null ? p.Amount.Amount : 0m;
            dr["PaymentDate"] = p.PaymentDate.ToString(base.DateOutputFormat);
            dr["DueDate"] = p.DueDate.ToString(base.DateOutputFormat);
            dr["TransactionNo"] = tp[i].Transaction.TransactionNo;
            dr["PaymentID"] = p.PaymentID;
            dataTable.Rows.Add(dr);
        }
        for (int j = 0; j < transaction.Children.Count; j++)
            InitialPayments(transaction.Children[j].Child as FundTransaction);
    }
    protected void gdvPayments_Init(object sender, EventArgs e)
    {
        ASPxGridView gdv =(ASPxGridView)sender;
        GridViewDataTextColumn column = gdv.Columns["Amount"] as GridViewDataTextColumn;
        if (column != null)
            column.PropertiesEdit.DisplayFormatString = base.CurrencyFormat;
        gdv.TotalSummary.Add(new ASPxSummaryItem("Amount", DevExpress.Data.SummaryItemType.Sum) { DisplayFormat = base.CurrencyFormat });
        gdv.Templates.PreviewRow = new PaymentTemplate(iSabayaContext);
    }
    protected void gdvPayments_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Preview)
        {
            e.Row.BackColor = System.Drawing.Color.Azure;
        }
    }
}
