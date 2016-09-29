using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFundManager
    {
        private FundManager instance;
        public VOFundManager(FundManager instance)
        {
            this.instance = instance;
        }

        public int FundManagerID
        {
            get { return instance.FundManagerID; }
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

        public float AccountablityRatio
        {
            get { return instance.AccountablityRatio; }
        }

        public string ManagerCategory
        {
            get
            {
                if (instance.ManagerCategory == null)
                    return "-";
                else
                    return instance.ManagerCategory.ToString();
            }
        }

        public string Manager
        {
            get
            {
                if (instance.Manager == null)
                    return "-";
                else
                    return instance.Manager.ToString();
            }
        }

        public string Fund
        {
            get
            {
                if (instance.Fund == null)
                    return "-";
                else
                    return instance.Fund.ToString();
            }
        }

        #region instance.Manager
        public string LivePeriod
        {
            get
            {
                if (instance.Manager == null || instance.Manager.LivePeriod == null)
                    return "-";
                else
                    return instance.Manager.LivePeriod.ToString();
            }
        }

        //public string Category
        //{
        //    get
        //    {
        //        if (instance.Manager == null || instance.Manager.Category == null)
        //            return "-";
        //        else
        //            return instance.Manager.Category.ToString();
        //    }
        //}

        public string Properties
        {
            get
            {
                if (instance.Manager == null || instance.Manager.Properties == null)
                    return "-";
                else
                    return instance.Manager.Properties.ToString();
            }
        }

        public string Gender
        {
            get
            {
                if (instance.Manager == null || instance.Manager.Gender == null)
                    return "-";
                else
                    return instance.Manager.Gender.ToString();
            }
        }

        public string BloodGroup
        {
            get
            {
                if (instance.Manager == null || instance.Manager.BloodGroup == null)
                    return "-";
                else
                    return instance.Manager.BloodGroup.ToString();
            }
        }

        public string Religion
        {
            get
            {
                if (instance.Manager == null || instance.Manager.Religion == null)
                    return "-";
                else
                    return instance.Manager.Religion.ToString();
            }
        }

        public string Email
        {
            get { return instance.Manager.Email; }
        }

        public string Phone
        {
            get { return instance.Manager.Phone; }
        }

        public string MobilePhone
        {
            get { return instance.Manager.MobilePhone; }
        }

        public string Faxes
        {
            get { return instance.Manager.Faxes; }
        }

        public string CurrentName
        {
            get
            {
                if (instance.Manager == null || instance.Manager.CurrentName == null)
                    return "-";
                else
                    return instance.Manager.CurrentName.ToString();
            }

        }

        public string CitizenOf
        {
            get
            {
                if (instance.Manager == null || instance.Manager.CitizenOf == null)
                    return "-";
                else
                    return instance.Manager.CitizenOf.ToString();
            }
        }
        public string URL
        {
            get { return instance.Manager.URL; }
        }

        public string Occupation
        {
            get
            {
                if (instance.Manager == null || instance.Manager.Occupation == null)
                    return "-";
                else
                    return instance.Manager.Occupation.ToString();
            }
        }

        public string Nationality
        {
            get
            {
                if (instance.Manager == null || instance.Manager.Nationality == null)
                    return "-";
                else
                    return instance.Manager.Nationality.ToString();
            }
        }

        #endregion instance.Manager
    }
}
