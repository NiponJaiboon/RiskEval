using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using imSabaya;
using iSabaya;

namespace WebHelper
{
    public class PaymentTemplate : ITemplate
    {
        protected GridViewBaseRowTemplateContainer parent;
        protected Payment payment;
        protected Table tbData;
        //protected Table tbControl;
        protected string strNULL = "NULL";
        protected imSabayaContext context;
        protected string CssPostfix;
        public PaymentTemplate(imSabayaContext context)
        {
            this.context = context;
        }
        public void InstantiateIn(Control container)
        {
            parent = (GridViewBaseRowTemplateContainer)container;
            //CssPostfix = parent.Grid.CssPostfix;
            //tbControl = new Table();
            tbData = new Table();
            //tbControl.SetTableControlStyle(CssPostfix);
            //tbData.SetTableStyle(CssPostfix);
            //TableRow row = new TableRow();
            //TableCell cell = new TableCell();
            //cell.Controls.Add(tbData);
            //row.Cells.Add(cell);
            //tbControl.Rows.Add(row);
            //tbData.SetPageContentStyle(parent.Grid.CssPostfix);
            int paymentID = (int)DataBinder.Eval(parent.DataItem, "PaymentID");
            payment = context.PersistencySession.Get<iSabaya.Payment>(paymentID);
            if (payment.Type.Equals(typeof(Cheque)))
            {
                Cheque c = payment as Cheque;
                this.AddDataItem(c);
            }
            else if (payment.Type.Equals(typeof(Cash)))
            {
                Cash c = payment as Cash;
                this.AddDataItem("Destination Bank Account:", c.DestinationBankAccount);
                this.AddDataItem("Recipient Name:", c.RecipientName);
            }
            else if (payment.Type.Equals(typeof(BankDeposit)))
            {
                BankDeposit bd = payment as BankDeposit;
                this.AddDataItem(bd.Cheque);
                this.AddDataItem("Bank Account:", bd.BankAccount);
                this.AddDataItem("Recipient Name:", bd.RecipientName);
            }
            else if (payment.Type.Equals(typeof(FundTransfer)))
            {
                FundTransfer ft = payment as FundTransfer;
                this.AddDataItem("Status:", ft.Status.ToString());
                this.AddDataItem("Status Date:", ft.StatusDate);
                this.AddDataItem("From Bank Account:", ft.FromBankAccount);
                this.AddDataItem("To Bank Account:", ft.ToBankAccount);
                this.AddDataItem("Recipient Name:", ft.RecipientName);
            }
            parent.Controls.Add(tbData);
        }
        private void SetNoDataTable()
        {
            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            td.Controls.Add(new Literal() { Text = "No Data to display." });
            tr.Cells.Add(td);
            tbData.Rows.Add(tr);
        }
        private void AddDataItem(Cheque c)
        {
            if (c != null)
            {
                this.AddDataItem("Cheque No:", c.ChequeNo);
                this.AddDataItem("Cheque Date:", c.ChequeDate);
                this.AddDataItem("Cheque Status:", c.Status.ToString());
                this.AddDataItem("Cheque Status Date:", c.StatusDate);
                this.AddDataItem("Cheque Print Cheque Date:", c.PrintChequeDate);
                this.AddDataItem("Cheque Bank Account:", c.BankAccount);
                this.AddDataItem("Cheque Recipient Name:", c.RecipientName);
            }
            else
            {
                this.AddDataItem("Cheque:", "-");
            }
        }
        private void AddDataItem(string caption, BankAccount ba)
        {
            if (ba != null)
            {
                string value = ba.ToString(context.CurrentLanguage.Code);
                this.AddDataItem(caption, value);
            }
            else
                this.AddDataItem(caption, strNULL);
        }
        private void AddDataItem(string caption, DateTime date)
        {
            this.AddDataItem(caption, date.ToString(context.imSabayaConfig.PF.DateOutputFormat));
        }
        private void AddDataItem(string caption, string value)
        {
            TableRow tr = new TableRow();
            //tr.SetRowStyle(CssPostfix);
            //caption
            TableCell td = new TableCell() { ForeColor = System.Drawing.Color.Black };
            //td.SetDataCellStyle();
            td.Controls.Add(new Literal() { Text = caption });
            tr.Cells.Add(td);
            //data
            td = new TableCell() { ForeColor = System.Drawing.Color.Black };
            //td.SetDataCellStyle();
            td.Controls.Add(new Literal() { Text = value });
            tr.Cells.Add(td);
            tbData.Rows.Add(tr);
        }
    }
}