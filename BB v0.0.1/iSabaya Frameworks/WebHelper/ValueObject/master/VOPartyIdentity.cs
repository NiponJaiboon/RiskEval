using System;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOPartyIdentity
    {
        private PartyIdentity instance;

        public VOPartyIdentity(PartyIdentity instance)
        {
            this.instance = instance;
        }

        public int ID
        {
            get { return instance.ID; }
        }

        public string IdentityNo
        {
            get
            {
                return instance.IdentityNo;
            }
        }

        public string IssuedBy
        {
            get { return instance.IssuedBy; }
        }

        public string Category
        {
            get
            {
                if (instance.Category == null)
                    return "-";
                else
                    return instance.Category.ToString();
            }
        }

        public DateTime EffectiveFrom { get { return instance.EffectivePeriod.From; } }

        public DateTime EffectiveTo { get { return instance.EffectivePeriod.To; } }

        public TimeInterval EffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod == null)
                    return null;
                else
                    return instance.EffectivePeriod;
            }
        }

        public bool IsEffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod.Includes(DateTime.Now))
                    return true;
                else
                    return false;
            }
        }
    }
}