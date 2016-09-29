using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class CreditTransaction : BankAccountTransaction
    {
        public override void Execute(Context context, DateTime datetime, string remark)
        {
            //base.BankAccount.Credit(context, datetime, base.Amount, remark);
        }
    }
}
