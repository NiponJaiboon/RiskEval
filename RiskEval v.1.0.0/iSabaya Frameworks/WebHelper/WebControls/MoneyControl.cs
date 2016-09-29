using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using iSabaya;

namespace WebHelper.Controls
{
    /// <summary>
    /// Money control present amount and currency
    /// </summary>
    /// <!--Client Script Members-->
    /// <!-- Methods -->
    /// GetValue() => reutrn currency and amount in format "[curency][amount]"
    /// <!--End Client Script Members-->
    [ToolboxData("<{0}:MoneyControl runat=server></{0}:MoneyControl>")]
    public class MoneyControl : iSabayaWebControlBase
    {
        protected const decimal DEFAULT_AMOUNT = 0;
        protected ASPxSpinEdit spnAmount = null;
        protected ASPxComboBox cboCurrency = null;
        protected Table layoutTable = null;

        public const string POST_AMOUNT_KEY = "amount";
        public const string POST_CURRENCY_KEY = "currency";

        #region MyRegion

        private Money lowerBound;

        public Money LowerBound
        {
            get { return this.lowerBound; }
            set { this.lowerBound = value; }
        }

        private Money upperBound;

        public Money UpperBound
        {
            get { return this.upperBound; }
            set { this.upperBound = value; }
        }

        private string fixCurrencyCode;

        public string FixCurrencyCode
        {
            get { return this.fixCurrencyCode; }
            set { this.fixCurrencyCode = value; }
        }

        public bool ReadOnly
        {
            get { return spnAmount.ReadOnly; }
            set
            {
                spnAmount.ReadOnly = value;
                cboCurrency.ReadOnly = value;
            }
        }

        public ReadOnlyStyle AmountReadOnlyStyle
        {
            get { return spnAmount.ReadOnlyStyle; }
        }

        public ReadOnlyStyle CurrencyReadOnlyStyle
        {
            get { return cboCurrency.ReadOnlyStyle; }
        }

        public string ClientInstanceName
        {
            get { return base.ClientInstanceNameInternal; }
            set { base.ClientInstanceNameInternal = value; }
        }

        public SpinEditClientSideEvents AmountClientSideEvents
        {
            get { return spnAmount.ClientSideEvents; }
        }

        public ComboBoxClientSideEvents CurrencyClientSideEvents
        {
            get { return cboCurrency.ClientSideEvents; }
        }

        #endregion MyRegion

        #region Output properties

        public Money Value
        {
            get
            {
                decimal amount = 0m;
                string currencyCode = null;
                if (spnAmount.Value != null)
                    amount = Convert.ToDecimal(spnAmount.Value);
                if (cboCurrency.SelectedItem != null)
                    currencyCode = (string)cboCurrency.SelectedItem.Value;
                return new Money(currencyCode, amount);
            }
            set
            {
                ListEditItem item = null;
                if (value != null)
                {
                    spnAmount.Value = value.Amount;
                    item = cboCurrency.Items.FindByValue(value.CurrencyCode);
                    if (item != null)
                        item.Selected = true;
                }
                else
                {
                    spnAmount.Value = DEFAULT_AMOUNT;
                    if (iSabayaContext.Configuration.DefaultCurrency != null)
                    {
                        item = cboCurrency.Items.FindByValue(iSabayaContext.Configuration.DefaultCurrency.Code);
                        if (item != null)
                            item.Selected = true;
                    }
                    else
                        cboCurrency.SelectedIndex = 0;
                }
            }
        }

        public string Text
        {
            get
            {
                Money m = Value;
                if (m.Currency != null)
                    return m.Amount.ToString(base.CurrencyFormat) + " " + m.Currency.Code;
                else
                    return m.Amount.ToString(base.CurrencyFormat);
            }
        }

        #endregion Output properties

        public MoneyControl()
        {
            spnAmount = new ASPxSpinEdit()
            {
                ID = POST_AMOUNT_KEY,
                Number = 0,
            };
            cboCurrency = new ASPxComboBox()
            {
                ID = POST_CURRENCY_KEY,
                ValueType = typeof(int),
                AnimationType = DevExpress.Web.ASPxClasses.AnimationType.None,
                IncrementalFilteringMode = IncrementalFilteringMode.StartsWith,
                DropDownStyle = DropDownStyle.DropDown,
                AutoResizeWithContainer = true,
                Width = Unit.Pixel(60),
            };

            layoutTable = new Table()
            {
                CellPadding = 0,
                CellSpacing = 0,
            };
            //HtmlTableRow row = new HtmlTableRow();
            //HtmlTableCell cell = new HtmlTableCell();
        }

        private void InitializeControls()
        {
            if (lowerBound != null && upperBound != null)
            {
                if (lowerBound.CurrencyCode != upperBound.CurrencyCode)
                    throw new Exception("Currency of lower and upper bound are not equal.");
            }

            // Initialize amount spin edit.
            spnAmount.DisplayFormatString = "#,##0.00000";
            spnAmount.DecimalPlaces = 5;
            if (lowerBound != null)
                spnAmount.MinValue = lowerBound.Amount;

            if (upperBound != null)
                spnAmount.MaxValue = upperBound.Amount;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object[] values = new object[4];
            values[0] = this.LowerBound;
            values[1] = this.UpperBound;
            values[2] = this.Value;
            values[3] = this.FixCurrencyCode;
            return new Pair(obj, values);
        }

        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);
            object[] values = (object[])p.Second;
            this.LowerBound = (Money)values[0];
            this.UpperBound = (Money)values[1];
            this.Value = (Money)values[2];
            this.FixCurrencyCode = (string)values[3];
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeControls();
            IEnumerable<Currency> currencies = Currency.Currencies;
            cboCurrency.Items.Clear();
            if (!string.IsNullOrEmpty(FixCurrencyCode) || UpperBound != null || LowerBound != null)
            {
                string code = FixCurrencyCode;
                if (UpperBound != null && LowerBound != null)
                    code = UpperBound.Currency.Code;
                foreach (Currency c in currencies)
                {
                    if (c.Code == code)
                        cboCurrency.Items.Add(c.Code, c.Code);
                }
                cboCurrency.SelectedIndex = 0;
                cboCurrency.Enabled = false;
            }
            else
            {
                foreach (Currency c in currencies)
                    cboCurrency.Items.Add(c.Code, c.Code);
                ListEditItem item = cboCurrency.Items.FindByValue(iSabayaContext.Configuration.DefaultCurrency.Code);
                if (item != null)
                    item.Selected = true;
                else
                    cboCurrency.SelectedIndex = 0;
            }

            spnAmount.Width = this.Width;
            cboCurrency.ClientInstanceName = this.ClientInstanceName + "cbo";
            spnAmount.ClientInstanceName = this.ClientInstanceName + "spn";

            if (!string.IsNullOrEmpty(ClientInstanceNameInternal))
            {
                StringBuilder script = null;
                if (!Page.ClientScript.IsClientScriptBlockRegistered("imMoneyControl"))
                {
                    script = new StringBuilder();
                    script.AppendLine("<script type='text/javascript'>");
                    script.AppendLine("function imMoneyControl(name)");
                    script.AppendLine("{");
                    script.AppendLine("this.name = name;");
                    // get value function
                    script.AppendLine("this.GetValue = function()");
                    script.AppendLine("{");
                    script.AppendLine("\tvar value = '';");
                    script.AppendLine("\tvar cbo = window[this.name + 'cbo'];");
                    script.AppendLine("\tvar spn = window[this.name + 'spn'];");
                    script.AppendLine("\tif(cbo != null && spn != null)");
                    script.AppendLine("\t{");
                    script.AppendLine("\t\tvar item = cbo.GetSelectedItem();");
                    script.AppendLine("\t\tif(item != null)");
                    script.AppendLine("\t\tvalue = item.text + '' + spn.GetValue();");
                    script.AppendLine("\t}");
                    script.AppendLine("\treturn value;");
                    script.AppendLine("}");
                    // ---------
                    script.AppendLine("}");
                    script.AppendLine("</script>");
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "imMoneyControl", script.ToString());
                }
                if (!Page.ClientScript.IsClientScriptBlockRegistered(ClientInstanceName))
                {
                    script = new StringBuilder();
                    script.AppendLine("<script type='text/javascript'>");
                    script.AppendFormat("var mc = new imMoneyControl('{0}');", ClientInstanceName).AppendLine();
                    script.AppendFormat("window['{0}'] = mc;", ClientInstanceName).AppendLine();
                    script.AppendLine("</script>");
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), ClientInstanceName, script.ToString());
                }
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // add amount spin controls
            layoutTable.Rows.Add(new TableRow());
            TableCell cell = new TableCell();
            cell.Style.Add(HtmlTextWriterStyle.PaddingRight, "3px");
            cell.Controls.Add(spnAmount);
            layoutTable.Rows[0].Cells.Add(cell);
            // add currency combo box
            cell = new TableCell();
            cell.Style.Add(HtmlTextWriterStyle.PaddingRight, "3px");
            cell.Controls.Add(cboCurrency);
            layoutTable.Rows[0].Cells.Add(cell);
            // add all control
            this.Controls.Add(layoutTable);
        }
    }
}