using System;
using System.Web.UI;
using iSabaya;

namespace WebHelper.UserControls
{
    public partial class TimeIntervalControl : iSabayaControl
    {
        //coke 14072009 hh:mm

        #region Validation Section

        private bool isRequiredField = false;

        public bool IsRequiredField
        {
            get { return isRequiredField; }
            set { this.isRequiredField = value; }
        }

        private String validationGroup;

        /// <summary>
        /// Get or sets the group of controls for which the editor forces validation when it posts back to the server.
        /// </summary>
        public String ValidationGroup
        {
            get { return validationGroup; }
            set { this.validationGroup = value; }
        }

        #endregion Validation Section

        private bool hideDateFrom;
        private bool hideDateTo;
        private bool hideTimeFrom;
        private bool hideTimeTo;
        private bool hideMinMax = true;
        private bool hideLabelFrom = true;
        private bool hideLabelTo = true;

        public bool HideMinMax
        {
            get { return this.hideMinMax; }
            set { this.hideMinMax = value; }
        }

        public bool HideDateFrom
        {
            get { return this.hideDateFrom; }
            set { this.hideDateFrom = value; }
        }

        public bool HideDateTo
        {
            get { return this.hideDateTo; }
            set { this.hideDateTo = value; }
        }

        public bool HideTimeFrom
        {
            get { return this.hideTimeFrom; }
            set { this.hideTimeFrom = value; }
        }

        public bool HideTimeTo
        {
            get { return this.hideTimeTo; }
            set { this.hideTimeTo = value; }
        }

        public bool HideLabelFrom
        {
            get { return this.hideLabelFrom; }
            set { this.hideLabelFrom = value; }
        }

        public bool HideLabelTo
        {
            get { return this.hideLabelTo; }
            set { this.hideLabelTo = value; }
        }

        private bool isRaiseInitial = false;

        public bool IsRaiseInitial
        {
            get { return isRaiseInitial; }
            set
            {
                this.isRaiseInitial = value;
            }
        }

        private bool enabled = true;

        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }

        public void EnableControls(bool enabled)
        {
            DateFrom.ReadOnly = !enabled;
            DateFrom.ReadOnlyStyle.ForeColor = System.Drawing.Color.Gray;
            DateFrom.DropDownButton.Enabled = enabled;
            DateTo.ReadOnly = !enabled;
            DateTo.ReadOnlyStyle.ForeColor = System.Drawing.Color.Gray;
            DateTo.DropDownButton.Enabled = enabled;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsCallback || isRaiseInitial)
            {
                if (this.DateTo.Value == null)
                    this.DateTo.Date = TimeInterval.MaxDate;
                if (this.DateFrom.Value == null)
                    this.DateFrom.Date = DateTime.Today;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.EnableControls(true);
            if (!Page.IsCallback || isRaiseInitial)
            {
                if (HideMinMax)
                {
                    btnMaxdate.Visible = false;
                    btnMindate.Visible = false;
                }
                if (HideDateFrom)
                {
                    tdLabelFrom.Visible = false;
                    DateFrom.Visible = false;
                }
                if (HideDateTo)
                {
                    lblToSym.Visible = false;
                    tdLabelTo.Visible = false;
                    DateTo.Visible = false;
                }
                if (HideLabelFrom)
                {
                    tdLabelFrom.Visible = false;
                }
                if (HideLabelTo)
                {
                    tdLabelTo.Visible = false;
                }

                btnMaxdate.ClientSideEvents.Click = @"
            function(s, e) {
                var newDate = new Date();
                newDate.setFullYear(" + TimeInterval.MaxDate.Day + "," + TimeInterval.MaxDate.Month + "," + TimeInterval.MaxDate.Year + @")
                " + DateTo.ClientInstanceName + @".SetDate(newDate);
            }
            ";

                btnMindate.ClientSideEvents.Click = @"
            function(s, e) {
                var newDate = new Date();
                newDate.setFullYear(" + TimeInterval.MinDate.Day + "," + TimeInterval.MinDate.Month + "," + TimeInterval.MinDate.Year + @")
            " + DateFrom.ClientInstanceName + @".SetDate(newDate);
            }
            ";
            }
        }

        public TimeInterval Period
        {
            get
            {
                DateTime date = HideDateFrom ? TimeInterval.MinDate : DateFrom.Date;
                DateTime fromDate;

                if (HideTimeFrom)
                {
                    fromDate = date;
                }
                else
                {
                    fromDate = new DateTime(date.Year, date.Month, date.Day);
                }

                date = HideDateTo ? TimeInterval.MaxDate : DateTo.Date;

                if (HideTimeTo)
                {
                    return new TimeInterval(fromDate, date);
                }
                else
                {
                    return new TimeInterval(fromDate,
                                new DateTime(date.Year, date.Month, date.Day));
                }
            }
            set
            {
                if (value != null)
                {
                    DateFrom.Date = value.EffectiveDate.Date;
                    DateTo.Date = value.ExpiryDate.Date;
                }
            }
        }

        public void Clear()
        {
            DateFrom.Date = DateTime.Today;
            DateTo.Date = TimeInterval.MaxDate;
        }

        public override string Text
        {
            get
            {
                return Period.ToString();
            }
        }
    }
}