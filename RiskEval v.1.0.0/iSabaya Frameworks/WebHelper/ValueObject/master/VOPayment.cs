using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOPayment
    {
        private Payment instance;
        public VOPayment(Payment instance)
        {
            this.instance = instance;
        }

        public int PaymentID
        {
            get { return instance.PaymentID; }
        }

        public bool IsPaymentToCustomer
        {
            get { return instance.IsPaymentToCustomer; }
        }

        public DateTime DueDate
        {
            get { return instance.DueDate; }
        }

        public DateTime PaymentDate
        {
            get { return instance.PaymentDate; }
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

        public string Payer
        {
            get
            {
                if (instance.Payer == null)
                    return "";
                else

                    return instance.Payer.ToString();
            }
        }

        public string Payee
        {
            get
            {
                if (instance.Payee == null)
                    return "";
                else

                    return instance.Payee.ToString();
            }
        }

        public string RecipientName
        {
            get { return instance.RecipientName; }
        }

        public string Reference
        {
            get { return instance.Reference; }
        }

        public string Remark
        {
            get { return instance.Remark; }
        }
    }
}
