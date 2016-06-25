using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iSabaya;

namespace iSabaya
{
    public class Bank : Organization
    {
        public virtual string SwiftCode { get; set; }
        public virtual bool IsITMXMember { get; set; }
        public virtual bool IsBahtNetMember { get; set; }
        public virtual bool IsORFTMember { get; set; }
        public virtual bool IsBCMember { get; set; }


        public new static IList<Bank> List(Context context)
        {
            IList<Bank> orgs = context.PersistenceSession.QueryOver<Bank>().List();
            SetLanguage(context, orgs);
            return orgs;
        }

        public static Bank GetEffectiveBankByOfficialIDNo(Context context, string officialIDNo)
        {
            DateTime now = DateTime.Today;
            Bank bank = context.PersistenceSession.QueryOver<Bank>()
                                .Where(b => b.OfficialIDNo == officialIDNo && b.EffectivePeriod.From <= now && now <= b.EffectivePeriod.To)
                                .SingleOrDefault();
            SetLanguage(context, bank);
            return bank;
        }

        public static Bank GetEffectiveBankByCode(Context context, string code)
        {
            DateTime now = DateTime.Today;
            Bank bank = context.PersistenceSession.QueryOver<Bank>()
                                .Where(b => b.Code == code && b.EffectivePeriod.From <= now && now <= b.EffectivePeriod.To)
                                .SingleOrDefault<Bank>();
            SetLanguage(context, bank);
            return bank;
        }

        public static IList<Bank> ListByOfficialIDNo(Context context, string officialIDNo)
        {
            IList<Bank> banks = context.PersistenceSession.QueryOver<Bank>()
                                .Where(b => b.OfficialIDNo == officialIDNo)
                                .List<Bank>();
            SetLanguage(context, banks);
            return banks;
        }

        public static IList<Bank> ListByCode(Context context, string code)
        {
            IList<Bank> banks = context.PersistenceSession.QueryOver<Bank>()
                                .Where(b => b.Code == code)
                                .List<Bank>();
            SetLanguage(context, banks);
            return banks;
        }
    }
}
