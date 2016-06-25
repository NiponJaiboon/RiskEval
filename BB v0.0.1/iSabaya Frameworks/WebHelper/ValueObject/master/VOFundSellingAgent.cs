using System;
using imSabaya.MutualFundSystem;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFundSellingAgent : FundSellingAgent
    {
        private FundSellingAgent instance;

        public VOFundSellingAgent(FundSellingAgent instance)
        {
            this.instance = instance;
            base.FundAgentID = instance.FundAgentID;
            base.Fund = instance.Fund;
            base.Agent = instance.Agent;
            base.EffectivePeriod = instance.EffectivePeriod;
            base.Reference = instance.Reference;
        }

        public new String Fund
        {
            get
            {
                if (base.Fund == null)
                    return "-";
                else
                    return base.Fund.ToString();
            }
        }

        public new String Agent
        {
            get
            {
                if (base.Agent == null)
                    return "-";
                else
                    return base.Agent.FullName.ToString();
            }
        }

        public new String EffectivePeriod
        {
            get
            {
                if (base.EffectivePeriod == null)
                    return "-";
                else
                    return base.EffectivePeriod.ToString();
            }
        }
    }
}