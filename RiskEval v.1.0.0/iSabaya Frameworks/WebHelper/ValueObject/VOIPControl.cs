using System;
using imSabaya;
using imSabaya.MutualFundSystem;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOIPControl
    {
        private InvestmentPlanner instance;
        private imSabayaContext context;

        public VOIPControl(imSabayaContext context, InvestmentPlanner instance)
        {
            this.instance = instance;
            this.context = context;
        }

        public InvestmentPlanner Instance
        {
            get { return this.instance; }
        }

        public int PersonID
        {
            get { return instance.Person.PersonID; }
        }

        public String CurrentSellingAgent
        {
            get { return instance.GetCurrentSellingAgent(this.context).Organization.CurrentName.ToString(); }
        }

        public String IPName
        {
            get { return instance.Person.CurrentName.ToString(); }
        }

        public String LicenseNo
        {
            get { return instance.LicenseNo; }
        }
    }
}