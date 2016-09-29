using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.pvd.ContributionModule
{
    public class VOImportContribution
    {
        public Int64 TransactionID
        {
            get { return PFTransaction.TransactionID; }
        }

        //public int LineNo
        //{
        //    get { return PFTransaction.LineNo; }

        //}

        public String DivisionCode
        {
            get
            {
                if (PFTransaction.ContributionInfo == null)
                    if (PFTransaction.Member != null)
                        return PFTransaction.Member.DivisionCode;
                return PFTransaction.DivisionCode;
            }
            set { PFTransaction.DivisionCode = value; }
        }

        private PFTransaction transaction;

        public PFTransaction PFTransaction
        {
            get { return transaction; }
            set { transaction = value; }
        }

        public Fund Fund
        {
            get { return PFTransaction.Fund; }
        }

        public String FundCode
        {
            get { return PFTransaction.Fund.Code; }
        }

        private String employeeNo;

        public String EmployeeNo
        {
            get { return employeeNo; }
            set { employeeNo = value; }
        }

        private String firstName;

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private String lastName;

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private decimal employeeAmount;

        public decimal EmployeeAmount
        {
            get { return employeeAmount; }
            set { employeeAmount = value; }
        }

        private decimal employerAmount;

        public decimal EmployerAmount
        {
            get { return employerAmount; }
            set { employerAmount = value; }
        }

        public ContributionInfo ToContributionInfo()
        {
            return new ContributionInfo
            {
                DivisionNo = this.PFTransaction.DivisionCode,
                MemberNo = this.PFTransaction.EmployeeNo,
                NamePrefix = this.PFTransaction.NamePrefix,
                FirstName = this.PFTransaction.EmployeeFirstName,
                LastName = this.PFTransaction.EmployeeLastName,
                MemberContribution = this.PFTransaction.MemberQuantity.Amount.Amount,
                EmployerContribution = this.PFTransaction.EmployerQuantity.Amount.Amount,
                EmployerCode = this.PFTransaction.EmployerNo,
                Year = this.PFTransaction.Year,
                Month = this.PFTransaction.Month,
                Day = this.PFTransaction.Day,
                PreviousDivisionNo = this.PFTransaction.ContributionInfo.PreviousDivisionNo,
                PreviousMemberNo = this.PFTransaction.ContributionInfo.PreviousMemberNo,
                InstrumentCode = (this.PFTransaction.Fund != null) ? this.PFTransaction.Fund.Code : null,
                OtherMemberContribution = this.PFTransaction.ContributionInfo.OtherMemberContribution,
                OtherEmployerContribution = this.PFTransaction.ContributionInfo.OtherEmployerContribution
            };
        }
    }
}