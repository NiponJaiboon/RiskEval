using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class NameAffix
    {
        public NameAffix()
        {
        }

        protected int affixID;
        public virtual int AffixID
        {
            get { return affixID; }
            set { affixID = value; }
        }

        protected MultilingualString affix;
        public virtual MultilingualString Affix
        {
            get { return affix; }
            set { affix = value; }
        }

        protected MultilingualString shortAffix;
        public virtual MultilingualString ShortAffix
        {
            get { return shortAffix; }
            set { shortAffix = value; }
        }

        protected bool isSuffix;
        public virtual bool IsSuffix
        {
            get { return isSuffix; }
            set { isSuffix = value; }
        }

        public virtual string ToString(String langCode)
        {
            return Affix.ToString(langCode);
        }

        public override string ToString()
        {
            return Affix.ToString();
        }

        public virtual void Save(Context context)
        {
            if (ShortAffix != null) { ShortAffix.Persist(context); }
            if (Affix != null) { Affix.Persist(context); }
            context.Persist(this);
        }

        public virtual string ToLog()
        {
            return "";
        }

        public static IList<NameAffix> List(Context context)
        {
            return context.PersistenceSession
                            .QueryOver<NameAffix>()
                            .List<NameAffix>();
        }

        public static IList<NameAffix> List(Context context, bool isSuffix)
        {
            return context.PersistenceSession
                            .QueryOver<NameAffix>()
                            .Where(a => a.IsSuffix == isSuffix)
                            .List();
        }

        public static NameAffix Find(Context context, int id)
        {
            return context.PersistenceSession.Get<NameAffix>(id);
        }
    }

} // iSabaya
