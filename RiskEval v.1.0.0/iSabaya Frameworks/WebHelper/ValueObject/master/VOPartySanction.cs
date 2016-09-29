using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOPartySanction
    {
        private PartySanction instance;
        public VOPartySanction(PartySanction instance)
        {
            this.instance = instance;
        }

        public int PartySanctionID
        {
            get { return instance.PartySanctionID; }
        }

        public string SanctionAction
        {
            get
            {
                if (instance.SanctionAction == null)
                    return "-";
                else
                    return instance.SanctionAction.ToString();
            }
        }

        public int RiskLevel
        {
            get { return instance.RiskLevel; }
        }

        public bool STRReport
        {
            get { return instance.STRReport; }
        }
    }
}
