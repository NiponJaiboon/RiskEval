using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using WebHelper.ValueObject;

namespace WebHelper
{
    public class HelperPaymentMethod
    {
        private FundTransfer fundTransfer;
        public FundTransfer FundTransfer
        {
            get { return fundTransfer; }
            set { this.fundTransfer=value; }
        }
        private List<VOPaymethodGrid> voPaymentLines;
        public List<VOPaymethodGrid> VoPaymentLines
        {
            get
            {
                if (voPaymentLines == null)
                {
                    voPaymentLines = new List<VOPaymethodGrid>();
                }
                return voPaymentLines;
            }
            set { this.voPaymentLines = value; }
        }
    }
}
