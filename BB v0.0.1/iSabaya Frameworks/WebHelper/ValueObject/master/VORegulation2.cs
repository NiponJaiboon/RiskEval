using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VORegulation2
    {
        private int regulationID;
        public int RegulationID
        {
            get { return regulationID; }
            set { regulationID = value; }
        }

        private int fundRegulationID;
        public int FundRegulationID
        {
            get { return fundRegulationID; }
            set { fundRegulationID = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string reference;
        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string effectivePeriod;
        public string EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        private DateTime effectiveFrom;
        public DateTime EffectiveFrom
        {
            get { return effectiveFrom; }
            set { effectiveFrom = value; }
        }
        private DateTime effectiveTo;
        public DateTime EffectiveTo
        {
            get { return effectiveTo; }
            set { effectiveTo = value; }
        }

        private string closingNAVThreshold;
        public string ClosingNAVThreshold
        {
            get { return closingNAVThreshold; }
            set { closingNAVThreshold = value; }
        }

        private int minNumberOfDistinctInvestors;
        public int MinNumberOfDistinctInvestors
        {
            get { return minNumberOfDistinctInvestors; }
            set { minNumberOfDistinctInvestors = value; }
        }

        private float maxNAVPercentagePerInvestorGroup;
        public float MaxNAVPercentagePerInvestorGroup
        {
            get { return maxNAVPercentagePerInvestorGroup; }
            set { maxNAVPercentagePerInvestorGroup = value; }
        }

        private string alarmNAVThreshold;
        public string AlarmNAVThreshold
        {
            get { return alarmNAVThreshold; }
            set { alarmNAVThreshold = value; }
        }
    }
}

