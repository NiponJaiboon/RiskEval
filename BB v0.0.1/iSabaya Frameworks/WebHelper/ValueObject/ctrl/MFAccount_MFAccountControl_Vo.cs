using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using iSabaya;
using imSabaya.MutualFundSystem;
namespace WebHelper.ValueObject
{
    public class MFAccount_MFAccountControl_Vo
    {
        private MFAccount account;
        public String LanguageCode;
        public MFAccount_MFAccountControl_Vo(String languageCode, MFAccount account)
        {
            this.account = account;
            this.LanguageCode = languageCode;
        }
        public int AccountID
        {
            get { return this.account.AccountID; }
        }
        public String AccountNo
        {
            get { return this.account.AccountNo; }
        }
        public String Name
        {
            get { return this.account.Name.ToString(this.LanguageCode); }
        }
    }
}
