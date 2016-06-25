using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFundRelatedOrg
    {
        private string fund;
        public string Fund
        {
            get { return fund; }
            set { fund = value; }
        }

        private string organization;
        public string Organization
        {
            get { return organization; }
            set { organization = value; }
        }

        private string role;
        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        private int fundRelatedOrgID;
        public int FundRelatedOrgID
        {
            get { return fundRelatedOrgID; }
            set { fundRelatedOrgID = value; }
        }

        private TimeInterval effectivePeriod;
        public TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private string reference;
        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        private float accountabilityPercentage;
        public float AccountAbilityPercentage
        {
            get { return accountabilityPercentage; }
            set { accountabilityPercentage = value; }
        }
    }
}
