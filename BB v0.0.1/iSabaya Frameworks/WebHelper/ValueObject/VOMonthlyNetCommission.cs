using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOMonthlyNetCommission
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

        private int monthlyNetCommissionID;
        public int MonthlyNetCommissionID
        {
            get { return monthlyNetCommissionID; }
            set { monthlyNetCommissionID = value; }
        }

        private String sellingAgent;
        public String SellingAgent
        {
            get { return sellingAgent; }
            set { sellingAgent = value; }
        }

        private String person;
        public String Person
        {
            get { return person; }
            set { person = value; }
        }

        private String year;
        public String Year
        {
            get { return year; }
            set { year = value; }
        }

        private String month;
        public String Month
        {
            get { return month; }
            set { month = value; }
        }

        private String managementFee;
        public String ManagementFee
        {
            get { return managementFee; }
            set { managementFee = value; }
        }

        private String expense;
        public String Expense
        {
            get { return expense; }
            set { expense = value; }
        }

        private String commissionAmount;
        public String CommissionAmount
        {
            get { return commissionAmount; }
            set { commissionAmount = value; }
        }

        private String netCommissionAmount;
        public String NetCommissionAmount
        {
            get { return netCommissionAmount; }
            set { netCommissionAmount = value; }
        }
    }
}
