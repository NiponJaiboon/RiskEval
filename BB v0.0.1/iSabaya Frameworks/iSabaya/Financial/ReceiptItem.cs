using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace iSabaya
{
    [Serializable]
    public class ReceiptItem : PersistentEntity
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

        private TreeListNode category;
        public virtual TreeListNode Category
        {
            get { return category; }
            set { category = value; }
        }

        private String detail;
        public virtual String Detail
        {
            get { return detail; }
            set { detail = value; }
        }

        private double units;
        public virtual double Units
        {
            get { return units; }
            set { units = value; }
        }

        private Money unitPrice;
        public virtual Money UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        private Money amount;
        public virtual Money Amount
        {
            get { return amount; }
            set { amount = value; }
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
