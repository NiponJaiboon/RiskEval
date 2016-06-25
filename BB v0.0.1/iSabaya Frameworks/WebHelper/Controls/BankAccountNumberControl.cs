using System;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxCallback;
using DevExpress.Web.ASPxEditors;

namespace WebHelper.Controls
{
    public class BankAccountNumberControl : iSabayaWebControlBase
    {
        public ASPxTextBox tbxBankNumber = null;
        public ASPxCallback cbValidate = null;
        public ASPxImage imgValidate = null;
        public HtmlTable table = null;
        public HtmlTableRow tableRow = null;
        public HtmlTableCell tableCell = null;

        private int setWidth = 170;

        public int SetWidth
        {
            get { return this.setWidth; }
            set { this.setWidth = value; }
        }

        private int setMaxLength = 15;

        public int SetMaxLength
        {
            get { return this.setMaxLength; }
            set { this.setMaxLength = value; }
        }

        private String clientInstanceName = null;

        public String ClientInstanceName
        {
            get { return this.clientInstanceName; }
            set { this.clientInstanceName = value; }
        }

        private String imageClientInstanceName = null;

        public String ImageClientInstanceName
        {
            get { return this.imageClientInstanceName; }
            set { this.imageClientInstanceName = value; }
        }

        public String CallbackClientInstanceName
        {
            get { return this.clientInstanceName; }
            set { this.clientInstanceName = value; }
        }

        public String AccountNo
        {
            get { return this.tbxBankNumber.Text; }
            set { this.tbxBankNumber.Text = value; }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            tableCell = new HtmlTableCell();
            tbxBankNumber.Width = this.setWidth;
            tbxBankNumber.MaxLength = this.setMaxLength;
            tableCell.Controls.Add(tbxBankNumber);
            tableRow.Cells.Add(tableCell);

            //tableCell = new HtmlTableCell();
            //tableCell.Controls.Add(imgValidate);
            //tableCell.Controls.Add(cbValidate);
            //tableRow.Cells.Add(tableCell);

            table.CellPadding = 0;
            table.CellSpacing = 0;
            table.Rows.Add(tableRow);

            this.Controls.Add(table);

            //tbxBankNumber.NumberType = SpinEditNumberType.Integer;
            //tbxBankNumber.ToolTip = "กรุณาระบุจำนวนเลขที่บัญชี 10 หลัก หรือ 15 หลัก";
            //imgValidate.ImageUrl = "~/Images/led_icon/cross.png";
            tbxBankNumber.SetValidation(ValidationGroup, IsRequiredField, "(^([0-9]{10})$)|(^([0-9]{15})$)", "กรุณาระบุจำนวนเลขที่บัญชี 10 หลัก หรือ 15 หลัก");
            tbxBankNumber.ClientInstanceName = (ClientInstanceName != null ? ClientInstanceName : tbxBankNumber.ClientID);
            //imgValidate.ClientInstanceName = (ImageClientInstanceName != null ? ImageClientInstanceName : imgValidate.ClientID);
            //cbValidate.ClientInstanceName = (callbackClientInstanceName != null ? callbackClientInstanceName : cbValidate.ClientID);

            //cbValidate.Callback += new CallbackEventHandler(cbValidate_Callback);

            #region javascript area

            tbxBankNumber.ClientSideEvents.KeyUp = @"function(s,e){
                /*var a = s.GetText();
                if( e.htmlEvent.keyCode == 8 )
                {
                    if( a.length == 10 || a.length == 15)
                    {"
                        + cbValidate.ClientInstanceName + @".SendCallback('no');
                    }
                }
                else
                {
                    if( a.length == 9 || a.length == 14 || a.length == 15)
                    {"
                        + cbValidate.ClientInstanceName + @".SendCallback('ok');
                    }
                    else
                    {"
                        + cbValidate.ClientInstanceName + @".SendCallback('no');
                    }
                }*/

                " + tbxBankNumber.ClientInstanceName + @".Validate();
            }";

            //            cbValidate.ClientSideEvents.CallbackComplete = @"function(s,e)
            //            {
            //                " + imgValidate.ClientInstanceName + @".SetImageUrl(e.result);
            //            }";

            #endregion javascript area
        }

        //void cbValidate_Callback(object source, CallbackEventArgs e)
        //{
        //    string imageUrl = "";
        //    if (e.Parameter == "ok")
        //    {
        //        imageUrl = "~/Images/led_icon/accept.png";
        //    }
        //    else
        //    {
        //        imageUrl = "~/Images/led_icon/cross.png";
        //    }
        //    e.Result = Page.ResolveUrl(imageUrl);
        //}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            tbxBankNumber = new ASPxTextBox();
            cbValidate = new ASPxCallback();
            imgValidate = new ASPxImage();
            table = new HtmlTable();
            tableRow = new HtmlTableRow();
        }
    }
}