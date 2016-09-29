using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VORegulation
    {
        private int regulationID;
        public int RegulationID
        {
            get { return regulationID; }
            set { regulationID = value; }
        }

        private int fundRegulationID;
        public virtual int FundRegulationID
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

        private TimeInterval effectivePeriod;
        public TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        private Money closingNAVThreshold;
        public Money ClosingNAVThreshold
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

        private Money alarmNAVThreshold;
        public Money AlarmNAVThreshold
        {
            get { return alarmNAVThreshold; }
            set { alarmNAVThreshold = value; }
        }

        private User updatedBy;
        public User UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        private DateTime updatedTS = DateTime.Now;
        public DateTime UpdatedTS
        {
            get { return updatedTS; }
            set { updatedTS = value; }
        }
    }
}
