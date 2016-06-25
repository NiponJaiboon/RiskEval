using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOPaymentFreeControl
    {
        private int lineNo;
        private Payment payment;
        private Money useAmount;
        private Money remainingAmount;
        public int LineNo
        {
            get { return lineNo; }
            set { lineNo = value; }
        }
        public Payment Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        public Money UseAmount
        {
            get { return useAmount; }
            set { useAmount = value; }
        }

        public Money RemainingAmount
        {
            get { return remainingAmount; }
            set { remainingAmount = value; }
        }

        //-------------------
        /*<dxwgv:GridViewDataTextColumn Caption="Payment type" FieldName="PaymentType" VisibleIndex="1">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="Cheque no" FieldName="ChequeNo" VisibleIndex="2">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="Amount" FieldName="Amount" VisibleIndex="3">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="Remaining Amount" FieldName="RemainingAmount" VisibleIndex="3">
        </dxwgv:GridViewDataTextColumn>*/
        public String PaymentType
        {
            get { return payment.GetType().ToString(); }
       
        }
        public String ChequeNo
        {
            get
            {
                if (payment is Cheque)
                {
                    return ((Cheque)payment).ChequeNo;
                }
                else
                {
                    return "";
                }
            }                

        }

        public Money Amount
        {
            get { return payment.Amount; }

        }

        public int PaymentID
        {
            get { return payment.PaymentID; }

        }
    }
}
