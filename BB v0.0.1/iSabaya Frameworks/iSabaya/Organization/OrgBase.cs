using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public abstract class OrgBase : Party
    {
        #region persistent

        protected string code;

        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        protected OrgName currentName;

        public virtual OrgName CurrentName
        {
            get { return currentName; }
            set
            {
                if (value == null) return;
                if (!object.ReferenceEquals(this.currentName, value))
                {
                    this.currentName = value;
                    value.Owner = this;
                    if (!this.Names.Contains(value))
                        this.Names.Add(value);
                }
            }
        }

        public virtual bool IsActive { get; set; }

        private String url;

        public virtual String URL
        {
            get { return url; }
            set { url = value; }
        }

        protected new ITemporalList<OrgName> names;

        public virtual new ITemporalList<OrgName> Names
        {
            get
            {
                if (null == this.names)
                    names = new TemporalList<OrgName>();
                return names;
            }
            protected set { names = value; }
        }

        public virtual TimeSchedule HolidayCalendar { get; set; }
        public virtual TimeSchedule WorkCalendar { get; set; }

        #endregion persistent

        //protected System.Collections.ArrayList persons;

        #region Party Members

        public override String ToString()
        {
            if (null == this.CurrentName)
                if (String.IsNullOrEmpty(this.Code))
                    return "id=" + this.ID;
                else
                    return "id=" + this.ID + " code=" + this.Code;
            else
                return "id=" + this.ID + " code=" + this.Code + "-" + this.CurrentName.ToString(this.LanguageCode); // +" - " + this.CurrentName.ToString();
        }

        public override MultilingualString MultilingualName
        {
            get { return null == this.CurrentName ? null : CurrentName.Name; }
        }

        public override void Persist(Context context)
        {
            base.Persist(context);
            foreach (OrgName n in this.Names)
            {
                n.Owner = this;
                n.Save(context);
            }
            if (null != this.WorkCalendar)
                this.WorkCalendar.Save(context);
            if (null != this.HolidayCalendar)
                this.HolidayCalendar.Save(context);
            foreach (PartyContact a in this.Contacts)
            {
                a.Party = this;
                a.Save(context);
            }
        }

        public override string FullName
        {
            get
            {
                if (null == this.CurrentName)
                    return "";
                else
                    if (null == base.Context)
                        return this.CurrentName.ToString();
                    else
                        return this.CurrentName.ToString(base.Context.CurrentLanguage.Code);
            }
        }

        #endregion

        #region operations

        public override String ToString(String langCode)
        {
            if (null == this.CurrentName)
                return "";
            else
                return this.CurrentName.ToString(langCode);
        }

        //public virtual OrgName GetName(DateTime onDate)
        //{
        //    return TemporalCollection<OrgName>.GetInstanceOn(this.Names, onDate);
        //}

        #endregion operations

        public override string LanguageCode
        {
            get
            {
                return base.LanguageCode;
            }
            set
            {
                base.LanguageCode = value;
                this.CurrentName.LanguageCode = value;
            }
        }

        public virtual IList<OrgName> GetOrgNamesEffectiveLongerThan(Context context, TimeInterval period)
        {
            return context.PersistenceSession
                                .QueryOver<OrgName>()
                                .Where(n => n.EffectivePeriod.From <= period.To 
                                            && n.EffectivePeriod.To > period.From 
                                            && n.Owner == this)
                                .List();
        }
    }
}
