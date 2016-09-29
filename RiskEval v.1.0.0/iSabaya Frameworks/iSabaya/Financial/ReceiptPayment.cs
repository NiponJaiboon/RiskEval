using System;
using System.Collections.Generic;

namespace iSabaya
{
    [Serializable]
    public class ReceiptPayment : PersistentEntity
    {
        private int seqNo;
        public virtual int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        private Receipt receipt;
        public virtual Receipt Receipt
        {
            get { return receipt; }
            set { receipt = value; }
        }

        private Payment payment;
        public virtual Payment Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        private Money appliedAmount;
        public virtual Money AppliedAmount
        {
            get { return appliedAmount; }
            set { appliedAmount = value; }
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
    }
}
