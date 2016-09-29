using System;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOOrgUnit
    {
        private OrgUnit instance;

        public VOOrgUnit(OrgUnit instance)
        {
            this.instance = instance;
        }

        public int ID
        {
            get { return instance.OrgUnitID; }
        }

        public string Code
        {
            get { return instance.Code; }
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

        public string CurrentName
        {
            get
            {
                if (instance.CurrentName == null)
                    return "-";
                else
                    return instance.CurrentName.ToString();
            }
        }

        //public string Property
        //{
        //    get
        //    {
        //        if (instance.Property == null)
        //            return "-";
        //        else
        //            return instance.Property.ToString();
        //    }
        //}

        public string URL
        {
            get { return instance.URL; }
        }

        public int LevelNo
        {
            get { return instance.LevelNo; }
        }

        public string OrganizationParent
        {
            get
            {
                if (instance.OrganizationParent == null)
                    return "-";
                else
                    return instance.OrganizationParent.CurrentName.ToString();
            }
        }
    }
}