using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.MutualFundSystem;

namespace WebHelper.ValueObject.transview
{
    public class VOTransactionSelect_GridTransaction
    {
        MFTransaction mftransaction;
        private imSabayaContext context;
        public VOTransactionSelect_GridTransaction(imSabayaContext context, MFTransaction mftransaction)
        {
            this.mftransaction = mftransaction;
            this.context = context;
        }

        public Int64 TransactionID
        {
            get { return mftransaction.TransactionID; }
        }
        public String TransactionNo
        {
            get { return mftransaction.TransactionNo; }
        }
        public decimal Amount
        {
            get
            {
                if (mftransaction.Amount == null) return 0m;
                return mftransaction.Amount.Amount; }
        }
        public decimal Fee
        {
            get
            {
                if (mftransaction.Fee == null) return 0m;
                return mftransaction.Fee.Amount;
            }
        }
        public decimal Tax
        {
            get
            {
                if (mftransaction.Tax == null) return 0m;
                return mftransaction.Tax.Amount;
            }
        }
        public double Units
        {
            get { return mftransaction.Units; }
        }
        public String SellingAgent
        {
            get {
                if (mftransaction.SellingAgent == null) { return ""; }
                return mftransaction.SellingAgent.FullName; }
        }

        public String Account
        {
            get {
                if (mftransaction.Account == null || mftransaction.Account.Name == null || this.context.imSabayaConfig.DefaultLanguage == null) { return ""; }
                return mftransaction.Account.Name.ToString(this.context.imSabayaConfig.DefaultLanguage.Code);
            }
        }
        public DateTime TransactionTS
        {
            get { return mftransaction.TransactionTS; }
        }
        public DateTime TradeDate
        {
            get { return mftransaction.TradeDate; }
        }
        public DateTime EffectiveDate
        {
            get { return mftransaction.EffectiveDate; }
        }
        public DateTime SettlementDate
        {
            get { return mftransaction.SettlementDate; }
        }
        public String Type
        {
            get {
                if (mftransaction.Type.Title == null) { return ""; }
                return mftransaction.Type.Title.ToString(this.context.imSabayaConfig.DefaultLanguage.Code);
            }
        }


    }
}
