using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFundSellingAgent2
    {
        private int fundAgentID;
        public int FundAgentID
        {
            get { return fundAgentID; }
            set { fundAgentID = value; }
        }

        private string fund;
        public string Fund
        {
            get { return fund; }
            set { fund = value; }
        }

        private string agent;
        public string Agent
        {
            get { return agent; }
            set { agent = value; }
        }

        private TimeInterval effectivePeriod;
        public TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        private String reference;
        public String Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        private string commissionRule;
        public string CommissionRule
        {
            get { return commissionRule; }
            set { commissionRule = value; }
        }

        private string commissionStructure;
        public string CommissionStructure
        {
            get { return commissionStructure; }
            set { commissionStructure = value; }
        }
    }
}
