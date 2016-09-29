using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOMonthlyCommission
    {
        private int tableRowID;

        public int TableRowID
        {
            get { return tableRowID; }
            set { tableRowID = value; }
        }

        private int parentTableRowID;

        public int ParentTableRowID
        {
            get { return parentTableRowID; }
            set { parentTableRowID = value; }
        }

        private int monthlyCommissionID;
        public virtual int MonthlyCommissionID
        {
            get { return monthlyCommissionID; }
            set { monthlyCommissionID = value; }
        }

        protected String sellingAgent;
        public virtual String SellingAgent
        {
            get { return sellingAgent; }
            set { sellingAgent = value; }
        }

        protected String sellingAgentBranch;
        public virtual String SellingAgentBranch
        {
            get { return sellingAgentBranch; }
            set { sellingAgentBranch = value; }
        }

        protected String adviser;
        public virtual String Adviser
        {
            get { return adviser; }
            set { adviser = value; }
        }

        protected String fund;
        public virtual String Fund
        {
            get { return fund; }
            set { fund = value; }
        }

        protected String year;
        public virtual String Year
        {
            get { return year; }
            set { year = value; }
        }

        protected String month;
        public virtual String Month
        {
            get { return month; }
            set { month = value; }
        }

        protected String units;
        public virtual String Units
        {
            get { return units; }
            set { units = value; }
        }

        protected String iPOPurchaseUnits;
        public virtual String IPOPurchaseUnits
        {
            get { return iPOPurchaseUnits; }
            set { iPOPurchaseUnits = value; }
        }

        protected String purchaseUnits;
        public virtual String PurchaseUnits
        {
            get { return purchaseUnits; }
            set { purchaseUnits = value; }
        }

        protected String redeemUnits;
        public virtual String RedeemUnits
        {
            get { return redeemUnits; }
            set { redeemUnits = value; }
        }

        protected String externalSwitchInUnits;
        public virtual String ExternalSwitchInUnits
        {
            get { return externalSwitchInUnits; }
            set { externalSwitchInUnits = value; }
        }

        protected String externalSwitchOutUnits;
        public virtual String ExternalSwitchOutUnits
        {
            get { return externalSwitchOutUnits; }
            set { externalSwitchOutUnits = value; }
        }

        protected String switchInUnits;
        public virtual String SwitchInUnits
        {
            get { return switchInUnits; }
            set { switchInUnits = value; }
        }

        protected String switchOutUnits;
        public virtual String SwitchOutUnits
        {
            get { return switchOutUnits; }
            set { switchOutUnits = value; }
        }

        protected String transferInUnits;
        public virtual String TransferInUnits
        {
            get { return transferInUnits; }
            set { transferInUnits = value; }
        }

        protected String transferOutUnits;
        public virtual String TransferOutUnits
        {
            get { return transferOutUnits; }
            set { transferOutUnits = value; }
        }

        protected String iPOPurchaseAmount;
        public virtual String IPOPurchaseAmount
        {
            get { return iPOPurchaseAmount; }
            set { iPOPurchaseAmount = value; }
        }

        protected String purchaseAmount;
        public virtual String PurchaseAmount
        {
            get { return purchaseAmount; }
            set { purchaseAmount = value; }
        }

        protected String redeemAmount;
        public virtual String RedeemAmount
        {
            get { return redeemAmount; }
            set { redeemAmount = value; }
        }

        protected String externalSwitchInAmount;
        public virtual String ExternalSwitchInAmount
        {
            get { return externalSwitchInAmount; }
            set { externalSwitchInAmount = value; }
        }

        protected String externalSwitchOutAmount;
        public virtual String ExternalSwitchOutAmount
        {
            get { return externalSwitchOutAmount; }
            set { externalSwitchOutAmount = value; }
        }

        protected String switchInAmount;
        public virtual String SwitchInAmount
        {
            get { return switchInAmount; }
            set { switchInAmount = value; }
        }

        protected String switchOutAmount;
        public virtual String SwitchOutAmount
        {
            get { return switchOutAmount; }
            set { switchOutAmount = value; }
        }

        protected String iPOPurchaseFee;
        public virtual String IPOPurchaseFee
        {
            get { return iPOPurchaseFee; }
            set { iPOPurchaseFee = value; }
        }

        protected String purchaseFee;
        public virtual String PurchaseFee
        {
            get { return purchaseFee; }
            set { purchaseFee = value; }
        }

        protected String redeemFee;
        public virtual String RedeemFee
        {
            get { return redeemFee; }
            set { redeemFee = value; }
        }

        protected String externalSwitchInFee;
        public virtual String ExternalSwitchInFee
        {
            get { return externalSwitchInFee; }
            set { externalSwitchInFee = value; }
        }

        protected String externalSwitchOutFee;
        public virtual String ExternalSwitchOutFee
        {
            get { return externalSwitchOutFee; }
            set { externalSwitchOutFee = value; }
        }

        protected String switchInFee;
        public virtual String SwitchInFee
        {
            get { return switchInFee; }
            set { switchInFee = value; }
        }

        protected String switchOutFee;
        public virtual String SwitchOutFee
        {
            get { return switchOutFee; }
            set { switchOutFee = value; }
        }

        protected String transferInFee;
        public virtual String TransferInFee
        {
            get { return transferInFee; }
            set { transferInFee = value; }
        }

        protected String transferOutFee;
        public virtual String TransferOutFee
        {
            get { return transferOutFee; }
            set { transferOutFee = value; }
        }

        //protected String transferOutFee;
        //public virtual String TransferOutFee
        //{
        //    get { return transferOutFee; }
        //    set { transferOutFee = value; }
        //}

        protected String iPOPurchaseCommission;
        public virtual String IPOPurchaseCommission
        {
            get { return iPOPurchaseCommission; }
            set { iPOPurchaseCommission = value; }
        }

        protected String purchaseCommission;
        public virtual String PurchaseCommission
        {
            get { return purchaseCommission; }
            set { purchaseCommission = value; }
        }

        protected String redeemCommission;
        public virtual String RedeemCommission
        {
            get { return redeemCommission; }
            set { redeemCommission = value; }
        }

        protected String externalSwitchInCommission;
        public virtual String ExternalSwitchInCommission
        {
            get { return externalSwitchInCommission; }
            set { externalSwitchInCommission = value; }
        }

        protected String externalSwitchOutCommission;
        public virtual String ExternalSwitchOutCommission
        {
            get { return externalSwitchOutCommission; }
            set { externalSwitchOutCommission = value; }
        }

        protected String switchInCommission;
        public virtual String SwitchInCommission
        {
            get { return switchInCommission; }
            set { switchInCommission = value; }
        }

        protected String switchOutCommission;
        public virtual String SwitchOutCommission
        {
            get { return switchOutCommission; }
            set { switchOutCommission = value; }
        }

        //protected String transferInCommission;
        //public virtual String TransferInCommission
        //{
        //    get { return transferInCommission; }
        //    set { transferInCommission = value; }
        //}

        //protected String transferOutCommission;
        //public virtual String TransferOutCommission
        //{
        //    get { return transferOutCommission; }
        //    set { transferOutCommission = value; }
        //}

        protected String unitNAV;
        public virtual String UnitNAV
        {
            get { return unitNAV; }
            set { unitNAV = value; }
        }

        protected String navAmount;
        public virtual String NAVAmount
        {
            get { return navAmount; }
            set { navAmount = value; }
        }

        protected String fundFeeRate;
        public virtual String FundFeeRate
        {
            get { return fundFeeRate; }
            set { fundFeeRate = value; }
        }

        protected String managementFeeRate;
        public virtual String ManagementFeeRate
        {
            get { return managementFeeRate; }
            set { managementFeeRate = value; }
        }

        protected String managementFee;
        public virtual String ManagementFee
        {
            get { return managementFee; }
            set { managementFee = value; }
        }

        protected String managementFeeCommission;
        public virtual String ManagementFeeCommission
        {
            get { return managementFeeCommission; }
            set { managementFeeCommission = value; }
        }

        /// <summary>
        /// The amount to be deducted from FundFee before the calculation of retention fee
        /// </summary>
        protected String fundFeeDeductible;
        public virtual String FundFeeDeductible
        {
            get { return fundFeeDeductible; }
            set { fundFeeDeductible = value; }
        }

        protected String withholdingTax;
        public virtual String WithholdingTax
        {
            get { return withholdingTax; }
            set { withholdingTax = value; }
        }

        protected String totalCommission;
        public virtual String TotalCommission
        {
            get { return totalCommission; }
            set { totalCommission = value; }
        }
    }
}
