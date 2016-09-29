using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using System.Collections;
using WebHelper.ValueObject;

namespace WebHelper
{
    public interface IService
    {
        void Service(ISession session, ArrayList param);//all
        List<VO_AccountManyFundRow> GetAccountManyFundRowItem();//A020100
        List<VO_Transaction> GetTransactionRowItem();//A020200


        //List<VOMFAccountProcessTransaction> GetVOMFAccountProcessTransactions();

        //List<VOReserveTransaction> GetVOReserveTransactions();

        imSabaya.MutualFundSystem.MFCustomer GetMFCustomer();
    }
}
