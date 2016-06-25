using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using iSabaya;

namespace WebHelper.Controls
{
    [ToolboxData("<{0}:DateTimeControl runat=server></{0}:DateTimeControl>")]
    public class DateTimeControl : ASPxDateEdit
    {
        public enum DefaultDate
        {
            ToDay,
            MinDate,
            MaxDate,
        }
        private DefaultDate defaultValue = DefaultDate.ToDay;
        public DefaultDate DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
        private bool showDescription;
        public bool ShowDescription
        {
            get { return this.showDescription; }
            set { this.showDescription = value; }
        }
        private string description = string.Empty;
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public DateTimeControl()
            : base()
        {
            if (this.Page == null)
                this.Page = this.Context.Handler as Page;

            base.EditFormatString = this.DateInputFormat;
            base.MinDate = TimeInterval.MinDate;
            base.MaxDate = TimeInterval.MaxDate;
            base.AnimationType = DevExpress.Web.ASPxClasses.AnimationType.None;
        }

        protected Context iSabayaContext
        {
            get { return ((iSabayaWebPageBase)base.Page).SessionContext; }
        }
        protected Language Language
        {
            get { return ((iSabayaWebPageBase)base.Page).Language; }
        }
        protected String LanguageCode
        {
            get { return ((iSabayaWebPageBase)base.Page).LanguageCode; }
        }
        protected String DateInputFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).DateInputFormat; }
        }

        private void InitializeClientScript()
        {
            base.ClientSideEvents.Init = @"
            function(s, e) 
            {
                SetThaiYear(s,s.GetText());
                s.SetValue = function(date) 
                { 
                    this.date = date; 
                    if(this.maskInfo != null) 
                    { 
                        _aspxMaskDateTimeHelper.SetDate(this.maskInfo, date); 
                        this.ApplyMaskInfo(false); 
                        this.SavePrevMaskValue(); 
                    } else 
                    {
                        var str = this.GetFormattedDate();
                        var format = '" + base.EditFormatString + @"';
                        var firstIndex = format.indexOf('y');
                        var lastIndex = format.lastIndexOf('y');
                        if(str.length == format.length)
                        {
                            var year = parseInt((str.substring(firstIndex,lastIndex+1))*1);
                            if(year <= 2300)
                                year += 543;
                            var postfix = str.substring(lastIndex+1);
                            if(postfix != '')
	                            this.GetInputElement().value = str.substring(0,firstIndex) + year + postfix;
                            else
	                            this.GetInputElement().value = str.substring(0,firstIndex) + year;
                            this.SyncRawInputValue(); 
                        }
                    } 
                    
                    if(this.styleDecoration != null) 
                        this.styleDecoration.Update();
                }
                s.GetInputElement().maxLength = 8;
                s.SetIsValid(s.date != null);
            }";

            base.ClientSideEvents.KeyUp = @"
            function(s, e) 
            {
                if(e.htmlEvent.keyCode > 46)
                {
                    var str = s.GetTextInternal();
                    var format = '" + base.EditFormatString + @"';
                    if(str.length == format.length)
                    {
                        var day = parseInt((str.substring(format.indexOf('d'),format.lastIndexOf('d')+1))*1);
                        var month = parseInt((str.substring(format.indexOf('M'),format.lastIndexOf('M')+1))*1)-1;
                        var year = parseInt((str.substring(format.indexOf('y'),format.lastIndexOf('y')+1))*1);
                        if(isNaN(day) || isNaN(month) || isNaN(year))
                        {
                            s.SetValue(s.date);
                        }
                        else
                        {
                            s.SetValue(ValidateDate(day, month, year));
                            s.RaiseValueChanged();
                        }
                    }
                }
            }";
            base.ClientSideEvents.ParseDate = @"
            function(s, e) 
            {
                var myDate=new Date();
                var str = e.value;
                var format = '" + base.EditFormatString + @"';
                if(str.length == format.length)
                {
                    var day = parseInt((str.substring(format.indexOf('d'),format.lastIndexOf('d')+1))*1);
                    var month = parseInt((str.substring(format.indexOf('M'),format.lastIndexOf('M')+1))*1)-1;
                    var year = parseInt((str.substring(format.indexOf('y'),format.lastIndexOf('y')+1))*1);
                    if(isNaN(day) || isNaN(month) || isNaN(year))
                    {
                        myDate = s.date;
                    }else
                    {
                        if(year > 2300)
                            year -= 543;
                        myDate.setFullYear(year,month,day);
                    }
                    s.date = myDate;
                }
            }";
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object[] values = new object[3];
            values[0] = showDescription;
            values[1] = description;
            values[2] = defaultValue;
            return new Pair(obj, values);
        }

        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);
            object[] values = (object[])p.Second;
            showDescription = (bool)values[0];
            description = (string)values[1];
            defaultValue = (DefaultDate)values[2];
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            InitializeClientScript();

            if (!ShowDescription)
                base.Controls.Add(new Literal() { Text = description });
            if (base.Value == null)
            {
                switch (defaultValue)
                {
                    case DefaultDate.ToDay:
                        base.Date = DateTime.Today;
                        break;
                    case DefaultDate.MinDate:
                        base.Date = TimeInterval.MinDate;
                        break;
                    default:
                        base.Date = TimeInterval.MaxDate;
                        break;
                }
            }
        }
    }
}
