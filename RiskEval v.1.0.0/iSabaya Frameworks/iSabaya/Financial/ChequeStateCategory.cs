using iSabaya;
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public enum ChequeStateCategory
    {
        New,        //Non-issue
        Outstanding,//Issued but not cleared
        Cleared,    //cashed 
        Rejected,   //by bank
        Lost,
        Damaged,
        Expired,
        Cancelled, //ByCustomer
        Reinvested, //นำกลับมาลงทุนใหม่
        Other,
        Unpaid,
    }
}
