using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.pvd
{
    [Serializable]
    public class VOEmployee
    {
        private Member instance;
        public VOEmployee(Member instance)
        {
            this.instance = instance;
        }

        public int AccountID
        {
            get { return instance.AccountID; }
        }

        public string Person
        {
            get
            {
                if (instance.Person == null)
                    return String.Empty;
                else
                    return instance.Person.ToString();
            }
        }

        public string Loan
        {
            get
            {
                //if (instance.Loan == null)
                //    return "-";
                //else
                //    return "L";
                return "";
            }
        }

        public string EmployeeNo
        {
            get { return instance.EmployeeNo; }
        }

        public string DivisionCode
        {
            get
            {
                if (instance.CurrentInfo == null || String.IsNullOrEmpty(instance.CurrentInfo.DivisionCode))
                    return "-";
                else
                    return instance.CurrentInfo.DivisionCode;

            }
        }

        public string CurrentInfo
        {
            get
            {
                if (instance.CurrentInfo == null)
                    return String.Empty;
                else
                    return instance.CurrentInfo.ToString();
            }
        }

        public string Employer
        {
            get
            {
                if (instance.Employer == null)
                    return String.Empty;
                else
                    return instance.Employer.ToString();
            }
        }

        //public string MembershipDuration
        //{
        //    get
        //    {
        //        if (instance.MembershipDuration == null)
        //            return String.Empty;
        //        else
        //            return instance.MembershipDuration.ToString();
        //    }
        //}

        //public string EmploymentDuration
        //{
        //    get
        //    {
        //        if (instance.EmploymentDuration == null)
        //            return String.Empty;
        //        else
        //            return instance.EmploymentDuration.ToString();
        //    }
        //}

        //public string TaxDuration
        //{
        //    get
        //    {
        //        if (instance.TaxPayingDuration == null)
        //            return String.Empty;
        //        else
        //            return instance.TaxPayingDuration.ToString();
        //    }
        //}

        public string EmploymentPeriod
        {
            get
            {
                if (instance.EmploymentPeriod == null)
                    return String.Empty;
                else
                    return instance.EmploymentPeriod.ToString();
            }
        }

        public string Status
        {
            get
            {
                if (instance.Status == null)
                    return String.Empty;
                else
                    return instance.Status.Code;
            }
        }

        //wichan 03032010
        public string Memo
        {
            get
            {
                if (instance.Notes.Count > 0)
                    return "M";
                else
                    return "-";
            }
        }
    }
}
