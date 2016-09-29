using System;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOPartyAddress
    {
        private PartyAddress instance;

        public VOPartyAddress(PartyAddress instance)
        {
            this.instance = instance;
        }

        public int ID
        {
            get { return instance.ID; }
        }

        public int SeqNo
        {
            get { return instance.SeqNo; }
        }

        public string Party
        {
            get
            {
                if (instance.Party == null)
                    return "-";
                else
                    return instance.Party.ToString();
            }
        }

        public string CategoryRoot
        {
            get
            {
                if (instance.CategoryRoot == null)
                    return "-";
                else
                    return instance.CategoryRoot.ToString();
            }
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

        public string Description
        {
            get { return instance.Description; }
        }

        public string Reference
        {
            get { return instance.Reference; }
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

        public string Remark
        {
            get { return instance.Remark; }
        }

        public string GeographicAddress
        {
            get
            {
                if (instance.GeographicAddress == null)
                    return "-";
                else
                    return instance.GeographicAddress.ToString();
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