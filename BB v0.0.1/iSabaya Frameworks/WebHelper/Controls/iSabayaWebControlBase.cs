using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using iSabaya;
using System.Collections.Generic;
using NHibernate;
using DevExpress.Web.ASPxClasses;
using imSabaya;
using WebHelper.Controls;
//using imSabaya.MutualFundSystem;
namespace WebHelper
{
    public abstract class iSabayaWebControlBase : DevExpress.Web.ASPxClasses.ASPxWebControl
    {
        public iSabayaWebControlBase()
            : base()
        {
            if (this.Page == null)
                this.Page = this.Context.Handler as Page;
        }
        protected imSabayaContext iSabayaContext
        {
            get { return ((iSabayaWebPageBase)base.Page).iSabayaContext; }
        }
        protected User User
        {
            get { return ((iSabayaWebPageBase)base.Page).User; }
        }
        protected long UserSessionID
        {
            get { return ((iSabayaWebPageBase)base.Page).UserSessionID; }
        }
        protected Currency Currency
        {
            get { return ((iSabayaWebPageBase)base.Page).Currency; }
        }
        protected Language Language
        {
            get { return ((iSabayaWebPageBase)base.Page).Language; }
        }
        protected String LanguageCode
        {
            get { return ((iSabayaWebPageBase)base.Page).LanguageCode; }
        }
        protected Country Country
        {
            get { return ((iSabayaWebPageBase)base.Page).Country; }
        }
        protected string ConnectionString
        {
            get { return ((iSabayaWebPageBase)base.Page).ConnectionString; }
        }
        public override Unit Width
        {
            get
            {
                if (base.Width.IsEmpty)
                    base.Width = Unit.Pixel(170);
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }
        protected string DateInputFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).DateInputFormat; }
        }
        protected string DateOutputFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).DateOutputFormat; }
        }
        protected string DateTimeOutputFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).DateTimeOutputFormat; }
        }

        protected virtual String CurrencyFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).CurrencyFormat; }
        }

        protected virtual String UnitsFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).UnitsFormat; }
        }
        private bool isRequiredField;
        public bool IsRequiredField
        {
            get { return this.isRequiredField; }
            set { this.isRequiredField = value; }
        }
        private string validationGroup = string.Empty;
        public string ValidationGroup
        {
            get { return this.validationGroup; }
            set { this.validationGroup = value; }
        }
        private AdditionalClientSideEvents clientSideEvents = null;
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual AdditionalClientSideEvents ClientSideEvents
        {
            get
            {
                if (clientSideEvents == null)
                    clientSideEvents = new AdditionalClientSideEvents();
                return clientSideEvents;
            }
            set
            {
                clientSideEvents = value;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }
        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object[] values = new object[3];
            values[0] = IsRequiredField;
            values[1] = ValidationGroup;
            values[2] = ClientSideEvents;
            return new Pair(obj, values);
        }
        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);
            object[] values = (object[])p.Second;
            IsRequiredField = (bool)values[0];
            ValidationGroup = (string)values[1];
            ClientSideEvents = (AdditionalClientSideEvents)values[2];
        }

    }
}