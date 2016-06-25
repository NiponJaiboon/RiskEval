using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web;
using System.Web.UI;
using imSabaya.ProvidentFundSystem;
namespace WebHelper
{
    public class PFChildTransactionsGridView : ChildTransactionsGridView
    {
        public PFChildTransactionsGridView() : base() 
        { 

        }

        private GridViewDataTextColumn createGrid( string caption, string fieldName, string columnFormat)
        {
            GridViewDataTextColumn g = new GridViewDataTextColumn();
            g.Caption = caption;
            g.FieldName = fieldName;
            g.PropertiesTextEdit.DisplayFormatString = columnFormat;
            return g;
        }

        private GridViewDataTextColumn createGrid(string caption, string fieldName)
        {
            GridViewDataTextColumn g = new GridViewDataTextColumn();
            g.Caption = caption;
            g.FieldName = fieldName;
            return g;
        }

        protected override void SetDetailColumn()
        {
            string formatUnit = "#,#.0000";
            detailGrid.Columns.Add(createGrid("ID", "TransactionID"));
            detailGrid.Columns.Add(createGrid("หมายเลขธุรกรรม", "TransactionNo"));
            detailGrid.Columns.Add(createGrid("ประเภทธุรกรรม", "TransactionType"));
            detailGrid.Columns.Add(createGrid("รหัสพนักงาน", "AccountNo"));
            detailGrid.Columns.Add(createGrid("ชื่อพนักงาน", "MemberName"));
            detailGrid.Columns.Add(createGrid("กองทุน", "FundCode"));
            detailGrid.Columns.Add(createGrid("ประเภทเงิน", "InvestmentCategory"));
            detailGrid.Columns.Add(createGrid("เงินสะสม", "Amount", formatUnit));
            detailGrid.Columns.Add(createGrid("หน่วยสะสม", "Units", formatUnit));
            detailGrid.Columns.Add(createGrid("เงินสมทบ", "EmployerAmount", formatUnit));
            detailGrid.Columns.Add(createGrid("หน่วยสมทบ", "EmployerUnits", formatUnit));
            detailGrid.Columns.Add(createGrid("ต้นทุน/หน่วย", "UnitCost"));
            detailGrid.Columns.Add(createGrid("ราคาซื้อ", "UnitPrice"));
            if (base.transactionTypeCode == PFConstants.PFTTCodeVest || base.transactionTypeCode == PFConstants.PFTTCodeMemberTermination)
            {
                detailGrid.Columns.Add(createGrid("Unvested Amount(D)", "DonatedUnvestedAmount", formatUnit));
                detailGrid.Columns.Add(createGrid("Unvested Capital(D)", "DonatedUnvestedCapital", formatUnit));
                detailGrid.Columns.Add(createGrid("Unvested Gain(D)", "donatedGain", formatUnit));

                detailGrid.Columns.Add(createGrid("Unvested Amount(E)", "EmployerUnvestedAmount", formatUnit));
                detailGrid.Columns.Add(createGrid("Unvested Capital(E)", "EmployerUnvestedCapital", formatUnit));
                detailGrid.Columns.Add(createGrid("Unvested Gain(E)", "employerUnvestedGain", formatUnit));

                detailGrid.Columns.Add(createGrid("Vested Amount", "VestedAmount", formatUnit));
                detailGrid.Columns.Add(createGrid("Vested Capital", "VestedCapital", formatUnit));
                detailGrid.Columns.Add(createGrid("Vested Gain", "VestedGain", formatUnit));
            }
            detailGrid.Columns.Add(createGrid("ภาษี", "TaxAmount"));
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ID", FieldName = "TransactionID" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "หมายเลขธุรกรรม", FieldName = "TransactionNo" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ประเภทธุรกรรม", FieldName = "TransactionType" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "รหัสพนักงาน", FieldName = "AccountNo" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ชื่อพนักงาน", FieldName = "MemberName" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "กองทุน", FieldName = "FundCode" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ประเภทเงิน", FieldName = "InvestmentCategory" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "เงินสะสม", FieldName = "Amount",});
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "หน่วยสะสม", FieldName = "Units" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "เงินสมทบ", FieldName = "EmployerAmount" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "หน่วยสมทบ", FieldName = "EmployerUnits" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ต้นทุน/หน่วย", FieldName = "UnitCost" });
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ราคาซื้อ", FieldName = "UnitPrice" });
            //if (base.transactionTypeCode == PFConstants.PFTTCodeVest || base.transactionTypeCode == PFConstants.PFTTCodeMemberTermination)
            //{
            //    detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Unvested Amount(D)", FieldName = "DonatedUnvestedAmount" });
            //    detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Unvested Capital(D)", FieldName = "DonatedUnvestedCapital" });
            //    detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Unvested Gain(D)", FieldName = "donatedGain" });

            //    detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Unvested Amount(E)", FieldName = "EmployerUnvestedAmount" });
            //    detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Unvested Capital(E)", FieldName = "EmployerUnvestedCapital" });
            //    detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Unvested Gain(E)", FieldName = "employerUnvestedGain" });

            //    detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Vested Amount", FieldName = "VestedAmount" });
            //    detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Vested Capital", FieldName = "VestedCapital" });
            //    detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "Vested Gain", FieldName = "VestedGain" });
            //}
            //detailGrid.Columns.Add(new GridViewDataTextColumn() { Caption = "ภาษี", FieldName = "TaxAmount" });
        }
        protected override void LoadDetailGridView()
        {
            this.detailGrid.Templates.DetailRow = new PFChildTransactionsGridView();
        }
    }
}
