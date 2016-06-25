using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using iSabaya;


namespace WebHelper.pvd.ContributionModule
{
    public class VOChangeMoney
    {
        private int lineNo;
		private FundTransaction instance;
		private PFTransaction pfTran;
		private imSabayaContext context;
		public VOChangeMoney(imSabayaContext context, FundTransaction instance)
		{
			this.instance = instance;
			this.context = context;
			this.pfTran = PFTransaction.FindByTransactionNo(this.context, this.instance.TransactionNo);
		}

		public long TransactionID
		{
			get { return instance.TransactionID; }
		}

		public String EmployerName
		{
			get { return this.pfTran.Employer.FullName; }
		}

		public decimal EmployerQuantityAmount
		{
			get { return Convert.ToDecimal(this.pfTran.EmployerQuantity.Amount); }
		}

		public decimal MemberQuantityAmount
		{
			get { return Convert.ToDecimal(this.pfTran.MemberQuantity.Amount); }
		}

        private Member employee;
        public Member Member
        {
            get { return employee; }
            set { employee = value; }
        }

        public String EmployeeName
        {
            get { return employee.FullName; }
          
        }

        private string funcode;
        public String fundCode
        {
            get { return funcode; }
            set { funcode = value; }
        }

        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private decimal employerAmount;
        public decimal EmployerAmount
        {
            get { return employerAmount; }
            set { employerAmount = value; }
        }

        private decimal changeAmount;
        public decimal ChangeAmount
        {
            get { return changeAmount; }
            set { changeAmount = value; }
        }

        private decimal changeReturnAmount;
        public decimal ChangeReturnAmount
        {
            get { return changeReturnAmount; }
            set { changeReturnAmount = value; }
        }
        private decimal unit;
        public decimal Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        private decimal changeEmployerAmount;
        public decimal ChangeEmployerAmount
        {
            get { return changeEmployerAmount; }
            set { changeEmployerAmount = value; }
        }

        private decimal changeReturnEmployerAmount;
        public decimal ChangeReturnEmployerAmount
        {
            get { return changeReturnEmployerAmount; }
            set { changeReturnEmployerAmount = value; }
        }

        private decimal employerUnit;
        public decimal EmployerUnit
        {
            get { return employerUnit; }
            set { employerUnit = value; }
        }

		

        public int LineNo
        {
            get { return lineNo; }
            set { this.lineNo=value; }
        }

    }
}
