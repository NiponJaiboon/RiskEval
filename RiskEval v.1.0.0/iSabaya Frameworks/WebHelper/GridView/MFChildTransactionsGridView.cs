using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web;
using System.Web.UI;

namespace WebHelper
{
    public class MFChildTransactionsGridView : ChildTransactionsGridView
    {
        public MFChildTransactionsGridView(): base()
        {
        }
        protected override void SetDetailColumn()
        {
            GridViewEditDataColumn column;
            GridViewCommandColumn commandColumn = new GridViewCommandColumn() { Name = "Action", Caption = "Action", ButtonType = ButtonType.Image };
            GridViewCommandColumnCustomButton btnView = new GridViewCommandColumnCustomButton() { ID = "btnView" };
            btnView.Image.Url = "~/Images/led_icon/view_detail.png";
            btnView.Image.AlternateText = "View Detail";
            btnView.Visibility = GridViewCustomButtonVisibility.BrowsableRow;
            commandColumn.CustomButtons.Add(btnView);
            detailGrid.Columns.Add(commandColumn);

            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ID", FieldName = "TransactionID", });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "หมายเลขธุรกรรม", FieldName = "TransactionNo", });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ประเภทธุรกรรม", FieldName = "TransactionType", });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "รหัสบัญชีหน่วย", FieldName = "AccountNo", });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "กองทุน", FieldName = "FundCode" });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "จำนวนเงิน", FieldName = "Amount" });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "จำนวนหน่วย", FieldName = "Units" });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ต้นทุน", FieldName = "UnitCost" });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ราคาซื้อ", FieldName = "UnitPrice" });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Applicable Amount", FieldName = "ApplicableAmount" });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Applicable Units", FieldName = "ApplicableUnits" });
            detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ภาษี", FieldName = "TaxAmount" });
            column = new GridViewDataTimeEditColumn() { Caption = "งวดวันที่", FieldName = "TradeDate" };
            column.PropertiesEdit.DisplayFormatString = "dd MMM yyyy";
            detailGrid.Columns.Add(column);
            column = new GridViewDataTimeEditColumn() { Caption = "วันที่มีผล", FieldName = "EffectiveDate" };
            column.PropertiesEdit.DisplayFormatString = "dd MMM yyyy";
            detailGrid.Columns.Add(column);
            column = new GridViewDataTimeEditColumn() { Caption = "วันที่ทำธุรกรรม", FieldName = "TransactionTS" };
            column.PropertiesEdit.DisplayFormatString = "dd MMM yyyy HH:mm";
            detailGrid.Columns.Add(column);
        }
        protected override void LoadDetailGridView()
        {
            this.detailGrid.Templates.DetailRow = new MFChildTransactionsGridView();
        }
    }
}
