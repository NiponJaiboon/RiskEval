using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class ChequeBatchItem : PersistentEntity
    {
        public ChequeBatchItem()
        {
        }

        public ChequeBatchItem(ChequeBatch chequeBatch, string chequeNo)
        {
            this.isClean = false;
            this.chequeBatch = chequeBatch;
            this.chequeNo = chequeNo;
        }

        #region persistent

        private ChequeBatch chequeBatch;
        public virtual ChequeBatch ChequeBatch
        {
            get { return chequeBatch; }
            set { chequeBatch = value; }
        }

        private string chequeNo;
        public virtual String ChequeNo
        {
            get { return chequeNo; }
            set { chequeNo = value; }
        }

        private Cheque cheque;
        public virtual Cheque Cheque
        {
            get { return cheque; }
            set { cheque = value; }
        }

        #endregion persistent

        private bool isClean = true;

        public virtual bool Allocate(Cheque cheque)
        {
            if (null != this.Cheque) return false;
            this.Cheque = cheque;
            cheque.ChequeNo = this.ChequeNo;
            cheque.BatchItem = this;
            this.isClean = false;
            return true; 
        }

        public static IList<ChequeBatchItem> ListRemaining(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(ChequeBatchItem));
            crit.Add(Expression.IsNull("Cheque"));
            return crit.List<ChequeBatchItem>();
        }

        public static IList<ChequeBatchItem> List(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(ChequeBatchItem));            
            return crit.List<ChequeBatchItem>();
        }

        //coke 09062009
        public virtual void Save(Context context)
        {
            if (this.isClean) return;
            context.PersistenceSession.SaveOrUpdate(this);
            this.isClean = true;
        }

        //coke 09062009
        public virtual void Update(Context context)
        {
            if (this.isClean) return;
            context.PersistenceSession.Update(this);
            this.isClean = true;
        }
    }
}
