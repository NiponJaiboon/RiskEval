using System;
using System.Collections.Generic;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class Receipt : PersistentEntity
    {
        #region persistent

        public virtual UserAction CancelAction {get;set;}
        public virtual TreeListNode Category { get; set; }
        public virtual Party Payer { get; set; }
        public virtual String PayerName {get;set;}
        public virtual String PayerAddress { get; set; }
        public virtual String Description { get; set; }

        //private IList<ReceiptPayment> payments;
        //public virtual IList<ReceiptPayment> Payments
        //{
        //    get
        //    {
        //        if (null == payments) payments = new List<ReceiptPayment>();
        //        return payments;
        //    }
        //    set { payments = value; }
        //}

        public virtual UserAction PrintAction {get;set;}

        public virtual DateTime ReceiptDate { get; set; }

        public virtual String ReceiptNo { get; set; }

        //private Party recipient;
        //public virtual Party Recipient
        //{
        //    get { return recipient; }
        //    set { recipient = value; }
        //}

        //private String recipientName;
        //public virtual String RecipientName
        //{
        //    get { return recipientName; }
        //    set { recipientName = value; }
        //}

        //private String recipientAddress;
        //public virtual String RecipientAddress
        //{
        //    get { return recipientAddress; }
        //    set { recipientAddress = value; }
        //}

        //private DateTime receiptDate;
        //public virtual DateTime ReceiptDate
        //{
        //    get { return receiptDate; }
        //    set { receiptDate = value; }
        //}

        private IList<ReceiptItem> items;
        public virtual IList<ReceiptItem> Items
        {
            get
            {
                if (null == items) items = new List<ReceiptItem>();
                return items;
            }
            set { items = value; }
        }

        public virtual Money TaxAmount { get; set; }

        public virtual Money TotalAmount { get; set; }


        #endregion persistent

        public virtual void Cancel(Person cancelledBy, DateTime cancelledDate, String cancelRemark)
        {
        }

        public virtual void Save(Context context)
        {
            //this.ReceiptNo = context.GenReceiptNo(this);

            context.PersistenceSession.SaveOrUpdate(this);

            int seqNo = 0;
            foreach (ReceiptItem item in this.Items)
            {
                item.SeqNo = ++seqNo;
                context.PersistenceSession.SaveOrUpdate(item);
            }

            seqNo = 0;
            //foreach (ReceiptPayment p in this.Payments)
            //{
            //    p.SeqNo = ++seqNo;
            //    context.PersistenceSession.SaveOrUpdate(p);
            //}
        }

    }
}
