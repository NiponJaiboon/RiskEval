using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VoTransactionState
    {
        private imSabaya.InvestmentTransactionState instance;
        public VoTransactionState(imSabaya.InvestmentTransactionState instance)
        {
            this.instance = instance;
        }
        public Int64 TransactionStateID
        {
            get { return instance.TransactionStateID; }
        }

        public string SystemMessage
        {
            get { return instance.SystemMessage; }
        }

        public string Transaction
        {
            get
            {
                if (instance.Transaction == null)
                    return "";
                else
                    return instance.Transaction.ToString();
            }
        }

        public string State
        {
            get
            {
                if (instance.State == null)
                    return "";
                else
                    return instance.State.ToString();
            }
        }

        public string StateCategory
        {
            get { return instance.StateCategory.ToString(); }
        }

        public string AlertCategory
        {
            get
            {
                if (instance.AlertCategory == null)
                    return "";
                else
                    return instance.AlertCategory.ToString();
            }
        }

        public DateTime EnteredTS
        {
            get { return instance.EnteredTS; }
        }

        public double Units
        {
            get { return instance.Units; }
        }

        public string UnitPrice
        {
            get
            {
                if (instance.UnitPrice == null)
                    return "";
                else
                    return instance.UnitPrice.ToString();
            }
        }

        public string Amount
        {
            get
            {
                if (instance.Amount == null)
                    return "";
                else
                    return instance.Amount.ToString();
            }
        }

        public string Remark
        {
            get { return instance.Remark; }
        }

        public string Reference
        {
            get { return instance.Reference; }
        }
    }
}
