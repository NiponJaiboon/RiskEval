using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFundCommissionsSchedule
    {
        #region persistent

        private int fundCommissionScheduleID;
        public virtual int FundCommissionScheduleID
        {
            get { return fundCommissionScheduleID; }
            set { fundCommissionScheduleID = value; }
        }

        private Fund fund;
        public virtual Fund Fund
        {
            get { return fund; }
            set { fund = value; }
        }

        private Organization sellingAgent;
        public virtual Organization SellingAgent
        {
            get { return sellingAgent; }
            set { sellingAgent = value; }
        }

        public virtual String SellingAgentName
        {
            get { return this.sellingAgent.MultilingualName.GetValue("th-TH"); }
        }

        private TreeListNode feeCategory;
        public virtual TreeListNode Category
        {
            get { return feeCategory; }
            set { feeCategory = value; }
        }

        public virtual String Cate
        {
            get { return this.feeCategory.Code.ToString(); }
        }

        private String commissionSchedule;
        public virtual String CommissionSchedule
        {
            get { return commissionSchedule; }
            set { commissionSchedule = value; }
        }

        protected TimeInterval effectivePeriod;
        public virtual TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        public virtual String Period
        {
            get { return this.effectivePeriod.ToString(); }
        }

        protected String reference;
        public virtual String Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        protected String remark;
        public virtual String Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        protected User updatedBy;
        public virtual User UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        protected DateTime updatedTS = DateTime.Now;
        public virtual DateTime UpdatedTS
        {
            get { return updatedTS; }
            set { updatedTS = value; }
        }
		
        #endregion persistent
    }
}
