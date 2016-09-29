using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iSabaya;

namespace WebHelper.ServiceLayer
{
    public class BankAccountService : Service
    {
        public static BankAccount GetInstance(string accountName, string accountNo, string currencyCode, BankAccountType accountType, Organization orgBank, OrgUnit branch, string branchCode)
        {
            return new BankAccount
            {
                AccountName = new MultilingualString(new string[] { "th-TH", accountName }),
                AccountNo = accountNo,
                AccountType = accountType,
                CurrencyCode = currencyCode,
                Bank = (Bank)orgBank,
                BranchCode = branchCode,
                //make
                BankCode = orgBank.Code,
                Status = BankAccountStatus.Active,
                Branch = branch,
                UniqueAccountNo = orgBank.Code + "-" + accountNo,
            };
        }

        public static BankAccount GetInstance(string accountNo, Organization org)
        {
            BankAccountType accountType;
            string type = accountNo.Substring(2, 2);
            switch (type)
            {
                case "80":
                    accountType = BankAccountType.Current;
                    return new BankAccount
                    {
                        AccountNo = accountNo,
                        AccountType = accountType,
                        Bank = (Bank)org,
                        BankCode = org.OfficialIDNo
                    };

                case "70":
                    accountType = BankAccountType.Saving;
                    return new BankAccount
                    {
                        AccountNo = accountNo,
                        AccountType = accountType,
                        Bank = (Bank)org,
                        BankCode = org.OfficialIDNo
                    };
                case "50":
                    //accountType = BankAccountType.TimeDeposit;
                    accountType = BankAccountType.FixedDeposit;//edit by kittikun
                    return new BankAccount
                    {
                        AccountNo = accountNo,
                        AccountType = accountType,
                        Bank = (Bank)org,
                        BankCode = org.OfficialIDNo
                    };
                default: return null;
            }
        }

        public static BankAccount GetInstanceOtherBank(string accountNo, Organization org)
        {
            return new BankAccount
            {
                AccountNo = accountNo,
                Bank = (Bank)org,
                BankCode = org.OfficialIDNo
            };
        }
    }
}
