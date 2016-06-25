using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
//using DevExpress.Web.ASPxThemes;
using iSabaya;

namespace WebHelper.Controls
{
    public enum ControlPosition
    {
        Top,
        Right,
        Bottom,
        Left,
    }
    [ToolboxData("<{0}:PagerControl runat=server></{0}:PagerControl>")]
    public class PagerControl : iSabayaWebControlBase
    {
        protected Table layout = null;
        protected ASPxSpinEdit spn = null;
        protected ASPxLabel lbl = null;

        public int PageCount
        {
            get { return Convert.ToInt32(spn.MaxValue); }
            set { spn.MaxValue = value; }
        }
        public int CurrentPageNumber
        {
            get { return Convert.ToInt32(spn.Value); }
            set { spn.Value = value; }
        }
        public string ClientInstanceName
        {
            get { return this.spn.ClientInstanceName; }
            set { this.spn.ClientInstanceName = value; }
        }

        private string summaryFormetString = "Page {0} of {1}";
        public string SummaryFormetString
        {
            get { return this.summaryFormetString; }
            set { this.summaryFormetString = value; }
        }

        private Unit layoutWidth = Unit.Empty;
        public Unit LayoutWidth
        {
            get { return this.layoutWidth; }
            set { this.layoutWidth = value; }
        }

        private ControlPosition summaryPosition = ControlPosition.Right;
        public ControlPosition SummaryPosition
        {
            get { return this.summaryPosition; }
            set { this.summaryPosition = value; }
        }

        private PageControlClientSideEvents clientSideEvents;
        public new PageControlClientSideEvents ClientSideEvents
        {
            get
            {
                if (this.clientSideEvents == null)
                    this.clientSideEvents = new PageControlClientSideEvents();
                return this.clientSideEvents;
            }
            set { this.clientSideEvents = value; }
        }
        public PagerControl()
            : base()
        {
            layout = new Table();
            layout.Rows.Add(new TableRow());
            layout.Rows[0].Cells.Add(new TableCell());
            layout.Rows[0].Cells.Add(new TableCell());
            layout.Rows[0].Cells[1].Style.Add(HtmlTextWriterStyle.PaddingLeft, "4px");
            layout.Rows[0].Cells[1].Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            layout.CellPadding = 0;
            layout.CellSpacing = 0;

            lbl = new ASPxLabel();
            lbl.ID = "lblSummary";

            spn = new ASPxSpinEdit();
            spn.Number = 1;
            spn.MinValue = 1;
            spn.MaxValue = 1;
            spn.NumberType = SpinEditNumberType.Integer;
            spn.SpinButtons.ShowIncrementButtons = false;
            spn.DecimalPlaces = 0;
            spn.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            spn.ButtonStyle.Paddings.Padding = Unit.Pixel(2);
            spn.ButtonStyle.Border.BorderWidth = Unit.Pixel(0);

            spn.ClientSideEvents.ButtonClick = @"function(s,e)
                {
                    var value = s.GetValue();
                    var min = s.GetMinValue();
                    var max = s.GetMaxValue();
                    switch(e.buttonIndex)
                    {
                        case 0 : s.SetText(s.GetMinValue());break;
                        case 1 : 
                            if(value > min)
                                s.SetText(value-1);
                            break;
                        case 2 : if(value < max)
                                s.SetText(value+1);
                            break;
                        case 3 : s.SetText(s.GetMaxValue());break;
                    }
                }";
        }
        private EditButton CreateEditButton(DevExpress.Web.ASPxEditors.ButtonsPosition position, string cssClass, string disabledCssClass)
        {
            EditButton btn = new EditButton();
            btn.Position = position;
            btn.Image.SpriteProperties.CssClass = cssClass + "_" + this.CssPostfix;
            btn.Image.SpriteProperties.DisabledCssClass = disabledCssClass + "_" + this.CssPostfix;
            return btn;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
            this.CssPostfix = WebConstants.GetCssPostfix(Page.Theme);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            layout.Width = LayoutWidth;
            // initialize label
            lbl.Text = string.Format(this.summaryFormetString, spn.Value, spn.MaxValue);
            // initialize spin edit
            if (this.clientSideEvents != null && !string.IsNullOrEmpty(this.clientSideEvents.PageNumberChanged))
                spn.ClientSideEvents.ValueChanged = this.clientSideEvents.PageNumberChanged;

            spn.Buttons.Clear();
            spn.Buttons.Add(CreateEditButton(ButtonsPosition.Left, "dxEditors_edtCalendarPrevYear", "dxEditors_edtCalendarPrevYearDisabled"));
            spn.Buttons.Add(CreateEditButton(ButtonsPosition.Left, "dxEditors_edtCalendarPrevMonth", "dxEditors_edtCalendarPrevMonthDisabled"));
            spn.Buttons.Add(CreateEditButton(ButtonsPosition.Right, "dxEditors_edtCalendarNextMonth", "dxEditors_edtCalendarNextMonthDisabled"));
            spn.Buttons.Add(CreateEditButton(ButtonsPosition.Right, "dxEditors_edtCalendarNextYear", "dxEditors_edtCalendarNextYearDisabled"));
        }
        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object[] values = new object[3];

            values[0] = this.summaryFormetString;
            values[1] = LayoutWidth;
            values[2] = this.summaryPosition;
            return new Pair(obj, values);
        }

        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);
            object[] values = (object[])p.Second;

            this.summaryFormetString = (string)values[0];
            LayoutWidth = (Unit)values[1];
            this.summaryPosition = (ControlPosition)values[2];
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            layout.Rows[0].Cells[0].Controls.Add(spn);
            layout.Rows[0].Cells[1].Controls.Add(lbl);
            this.Controls.Add(layout);

            spn.ClientSideEvents.NumberChanged = @"function(s,e)
            {
                var lbl = document.getElementById('" + lbl.ClientID + @"');
                var display = '" + this.summaryFormetString + @"';
                display = display.replace('{0}',s.GetNumber()).replace('{1}',s.GetMaxValue());
                lbl.innerHTML = display;
            }";
        }
    }
}
