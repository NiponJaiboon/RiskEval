using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.pvd.ContributionModule
{
    public class HelperChangeMoney
    {
        private List<VOChangeMoney> voChangeMoney;
        public List<VOChangeMoney> VoChangeMoney
        {
            get
            {
                if (voChangeMoney == null)
                {
                    voChangeMoney = new List<VOChangeMoney>();
                }
                return voChangeMoney;
            }
            set { this.voChangeMoney = value; }
        }
    }
}
