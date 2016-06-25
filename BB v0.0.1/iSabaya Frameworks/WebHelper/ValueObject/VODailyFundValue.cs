using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VODailyFundValue
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

        private String date;

        public String Date
        {
            get { return date; }
            set { date = value; }
        }
        private String fundCode;

        public String FundCode
        {
            get { return fundCode; }
            set { fundCode = value; }
        }
        private String units;
        public String Units
        {
            get { return units; }
            set { units = value; }
        }
        private String nav;
        public String NAV
        {
            get { return nav; }
            set { nav = value; }
        }
        private String navAmount;
        public String NavAmount
        {
            get { return navAmount; }
            set { navAmount = value; }
        }
        private String fundFeeRate;
        public String FundFeeRate
        {
            get { return fundFeeRate; }
            set { fundFeeRate = value; }
        }
        private String fundFee;
        public String FundFee
        {
            get { return fundFee; }
            set { fundFee = value; }
        }
        private String managementFeeRate;

        public String ManagementFeeRate
        {
            get { return managementFeeRate; }
            set { managementFeeRate = value; }
        }
        private String managementFee;

        public String ManagementFee
        {
            get { return managementFee; }
            set { managementFee = value; }
        }
        private String ipoPurchaseUnits;

        public String IpoPurchaseUnits
        {
            get { return ipoPurchaseUnits; }
            set { ipoPurchaseUnits = value; }
        }
        private String ipoPurchaseAmount;

        public String IpoPurchaseAmount
        {
            get { return ipoPurchaseAmount; }
            set { ipoPurchaseAmount = value; }
        }
        private String ipoPurchaseFee;

        public String IpoPurchaseFee
        {
            get { return ipoPurchaseFee; }
            set { ipoPurchaseFee = value; }
        }
        private String purchaseUnits;

        public String PurchaseUnits
        {
            get { return purchaseUnits; }
            set { purchaseUnits = value; }
        }
        private String purchaseAmount;

        public String PurchaseAmount
        {
            get { return purchaseAmount; }
            set { purchaseAmount = value; }
        }
        private String purchaseFee;

        public String PurchaseFee
        {
            get { return purchaseFee; }
            set { purchaseFee = value; }
        }
        private String redeemUnits;

        public String RedeemUnits
        {
            get { return redeemUnits; }
            set { redeemUnits = value; }
        }
        private String redeemAmount;

        public String RedeemAmount
        {
            get { return redeemAmount; }
            set { redeemAmount = value; }
        }
        private String redeemFee;

        public String RedeemFee
        {
            get { return redeemFee; }
            set { redeemFee = value; }
        }
        private String switchInUnits;

        public String SwitchInUnits
        {
            get { return switchInUnits; }
            set { switchInUnits = value; }
        }

        private String switchInAmount;
        public String SwitchInAmount
        {
            get { return switchInAmount; }
            set { switchInAmount = value; }
        }

        private String switchInFee;
        public String SwitchInFee
        {
            get { return switchInFee; }
            set { switchInFee = value; }
        }

        private String switchOutUnits;
        public String SwitchOutUnits
        {
            get { return switchOutUnits; }
            set { switchOutUnits = value; }
        }

        private String switchOutAmount;
        public String SwitchOutAmount
        {
            get { return switchOutAmount; }
            set { switchOutAmount = value; }
        }

        private String switchOutFee;
        public String SwitchOutFee
        {
            get { return switchOutFee; }
            set { switchOutFee = value; }
        }

        private String externalSwitchInUnits;
        public String ExternalSwitchInUnits
        {
            get { return externalSwitchInUnits; }
            set { externalSwitchInUnits = value; }
        }

        private String externalSwitchInAmount;
        public String ExternalSwitchInAmount
        {
            get { return externalSwitchInAmount; }
            set { externalSwitchInAmount = value; }
        }

        private String externalSwitchInFee;
        public String ExternalSwitchInFee
        {
            get { return externalSwitchInFee; }
            set { externalSwitchInFee = value; }
        }

        private String externalSwitchOutUnits;
        public String ExternalSwitchOutUnits
        {
            get { return externalSwitchOutUnits; }
            set { externalSwitchOutUnits = value; }
        }

        private String externalSwitchOutAmount;
        public String ExternalSwitchOutAmount
        {
            get { return externalSwitchOutAmount; }
            set { externalSwitchOutAmount = value; }
        }

        private String externalSwitchOutFee;
        public String ExternalSwitchOutFee
        {
            get { return externalSwitchOutFee; }
            set { externalSwitchOutFee = value; }
        }

        private String transferUnits;
        public String TransferUnits
        {
            get { return transferUnits; }
            set { transferUnits = value; }
        }

        private String transferFee;
        public String TransferFee
        {
            get { return transferFee; }
            set { transferFee = value; }
        }

        private String capitalGainTax;
        public String CapitalGainTax
        {
            get { return capitalGainTax; }
            set { capitalGainTax = value; }
        }

        private String valueAddedTax;
        public String ValueAddedTax
        {
            get { return valueAddedTax; }
            set { valueAddedTax = value; }
        }
    }
}
