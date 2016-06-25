using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOPartyCategory
    {
        private PartyCategory instance;
        public VOPartyCategory(PartyCategory instance)
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
        public string OderedDate
        {
            get
            {
                return instance.OrderedDate.ToString();
            }
        }

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
        public bool IsNotExpire
        {
            get
            {
                return instance.EffectivePeriod.Includes(DateTime.Today);
            }
        }

    }
}
