using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOPersonName
    {
        private PersonName instance;
        public VOPersonName(PersonName instance)
        {
            this.instance = instance;
        }

        public int PersonNameID
        {
            get { return instance.PersonNameID; }
        }

        public string Person
        {
            get
            {
                if (instance.Person == null)
                    return "-";
                else
                    return instance.Person.ToString();
            }
        }

        public string EffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod == null)
                    return "-";
                else
                    return instance.EffectivePeriod.ToString();
            }
        }

        public string FirstName
        {
            get
            {
                if (instance.FirstName == null)
                    return "-";
                else
                    return instance.FirstName.ToString();
            }
        }

        public string LastName
        {
            get
            {
                if (instance.LastName == null)
                    return "-";
                return instance.LastName.ToString();
            }
        }

        public string MiddleName
        {
            get
            {
                if (instance.MiddleName == null)
                    return "-";
                else
                    return instance.MiddleName.ToString();
            }
        }

        public string Suffix
        {
            get
            {
                if (instance.Suffix == null)
                    return "-";
                else
                    return instance.Suffix.ToString();
            }
        }

        public string Prefix
        {
            get
            {
                if (instance.Prefix == null)
                    return "-";
                else
                    return instance.Prefix.ToString();
            }
        }

        public string Reference
        {
            get { return instance.Reference; }
        }

        public string Remark
        {
            get { return instance.Remark; }
        }
        public bool IsEffectivePeriod
        {
            get
            {
                return instance.EffectivePeriod.Includes(DateTime.Now);
            }
        }
    }
}
