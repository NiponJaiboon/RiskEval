using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.MutualFundSystem;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFundTransaction
    {
        private FundTransaction instance;
        public VOFundTransaction(FundTransaction instance)
        {
            this.instance = instance;
        }

        public Int64 TransactionID
        {
            get { return instance.TransactionID; }
        }

        public string TransactionNo
        {
            get { return instance.TransactionNo; }
        }

        public string Type
        {
            get
            {
                if (instance.Type == null)
                    return "-";
                else
                    return instance.Type.ToString();
            }
        }

        public string Fund
        {
            get
            {
                if (instance.Fund == null)
                    return "-";
                else
                    return instance.Fund.ToString();
            }
        }

        public string Account
        {
            get
            {
                if (instance.Portfolio == null)
                    return "-";
                else
                    return instance.Portfolio.ToString();
            }
        }

        public string AccountBalance
        {
            get
            {
                if (instance.Investment == null)
                    return "-";
                else
                    return instance.Investment.ToString();
            }
        }

        public string InvestmentType
        {
            get
            {
                if (instance.InvestmentType == null)
                    return "-";
                else
                    return instance.InvestmentType.ToString();
            }
        }

        public string InvestmentCategory
        {
            get
            {
                if (instance.InvestmentCategory == null)
                    return "-";
                else
                    return instance.InvestmentCategory.ToString();
            }
        }

        public string TransactionChannel
        {
            get
            {
                if (instance.TransactionChannel == null)
                    return "-";
                else
                    return instance.TransactionChannel.ToString();
            }
        }

        public DateTime TradeDate
        {
            get { return instance.TradeDate; }
        }

        public DateTime EffectiveDate
        {
            get { return instance.EffectiveDate; }
        }

        public DateTime SettlementDate
        {
            get { return instance.SettlementDate; }
        }

        public string Reference
        {
            get { return instance.Reference; }
        }

        public string Nav
        {
            get
            {
                if (instance.Nav == null)
                    return "-";
                else
                    return instance.Nav.ToString();
            }
        }

        public string Fee
        {
            get
            {
                if (instance.Fee == null)
                    return "-";
                else
                    return instance.Fee.ToString();
            }
        }

        public string Tax
        {
            get
            {
                if (instance.Tax == null)
                    return "-";
                else
                    return instance.Tax.ToString();
            }
        }

        public DateTime TransactionTS
        {
            get { return instance.TransactionTS; }
        }

        public string Amount
        {
            get
            {
                if (instance is MFTransaction)
                {
                    MFTransaction t = (MFTransaction)instance;
                    if (t.Quantity.Amount != null)
                        return t.Quantity.Amount.ToString();
                }
                return "-";
            }
        }

        public string Capital
        {
            get
            {
                if (instance.Capital == null)
                    return "-";
                else
                    return instance.Capital.ToString();
            }
        }

        //public string Gain
        //{
        //    get
        //    {
        //        if (instance.Gain == null)
        //            return "-";
        //        else
        //            return instance.Gain.ToString();
        //    }
        //}

        public string SpecifiedValue
        {
            get
            {
                if (instance is MFTransaction)
                {
                    MFTransaction t = (MFTransaction)instance;
                    return t.Quantity.QuantityType.ToString();
                }
                return "-";
            }
        }

        public String UnitCost
        {
            get
            {
                if (instance is MFTransaction)
                {
                    MFTransaction t = (MFTransaction)instance;
                    if (t.Quantity.UnitCost != null)
                        return t.Quantity.UnitCost.ToString();
                }
                return "-";
            }
        }

        public string UnitPrice
        {
            get
            {
                if (instance is MFTransaction)
                {
                    MFTransaction t = (MFTransaction)instance;
                    if (t.UnitPrice != null)
                        return t.UnitPrice.ToString();
                }
                return "-";
            }
        }

        public double UnitsBefore
        {
            get
            {
                if (instance is MFTransaction)
                {
                    MFTransaction t = (MFTransaction)instance;
                    return t.Quantity.UnitsBefore;
                }
                return 0d;
            }
        }

        public double Units
        {
            get
            {
                if (instance is MFTransaction)
                {
                    MFTransaction t = (MFTransaction)instance;
                    return t.Quantity.Units;
                }
                return 0d;
            }
        }

        //public double UnitsAfter
        //{
        //    get
        //    {
        //        if (instance is MFTransaction)
        //        {
        //            MFTransaction t = (MFTransaction)instance;
        //            return t.Quantity.UnitsAfter;
        //        }
        //        return 0d;
        //    }
        //}

        public double RemainingUnits
        {
            get
            {
                if (instance is MFTransaction)
                {
                    MFTransaction t = (MFTransaction)instance;
                    return t.RemainingUnits;
                }
                return 0d;
            }
        }

        public string Description
        {
            get { return instance.Description; }
        }

        public string CurrentState
        {
            get
            {
                if (instance.CurrentState == null)
                    return "-";
                else
                    return instance.CurrentState.ToString();
            }

        }

        public bool IsBeingRollbacked
        {
            get { return instance.IsBeingRollbacked; }
        }

        public bool IsRoot
        {
            get { return instance.IsRoot; }

        }

        public bool IsLeaf
        {
            get { return instance.IsLeaf; }
        }

        public bool IsProcessedIndependently
        {
            get { return instance.IsProcessedIndependently; }
        }


        public InvestmentTransactionRollbackStatus RollbackStatus
        {
            get { return instance.RollbackStatus; }
        }


        public DateTime ExternalInvestmentDate
        {
            get { return instance.ExternalInvestmentDate; }
        }

        public string ExternalInvestmentOrg
        {
            get
            {
                if (instance.ExternalInvestmentOrg == null)
                    return "-";
                else
                    return instance.ExternalInvestmentOrg.ToString();
            }
        }

        public string ExternalFund
        {
            get
            {
                if (instance.ExternalFund == null)
                    return "-";
                else
                    return instance.ExternalFund.ToString();
            }
        }

        public string ExternalFundName
        {
            get { return instance.ExternalFundName; }
        }

        public string ExternalPortfolioNo
        {
            get { return instance.ExternalPortfolioNo; }
        }

        public DateTime InitialInvestmentDate
        {
            get { return instance.InitialInvestmentDate; }
        }

        public int SeqNo
        {
            get { return instance.SeqNo; }
        }

        public DateTime PrintedTS
        {
            get { return instance.PrintedTS; }
        }
    }
}
