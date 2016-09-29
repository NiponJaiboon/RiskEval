using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using System.Data.SqlClient;
using NHibernate;
namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOBankAccountBalance
    {

        
        protected int accountID;
        public virtual int AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }

        protected string accountNo;
        public virtual string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        protected string accountName;
        public virtual string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }
        protected string transactionNo;
        public virtual string TransactionNo
        {
            get { return transactionNo; }
            set { transactionNo = value; }
        }
        protected string transactionType;
        public virtual string TransactionType
        {
            get { return transactionType; }
            set { transactionType = value; }
        }

        #region Static Method
        public static List<VOBankAccountBalance> ListByParentId(string xConnectionString, ISession session)
        {
            //return value
            List<VOBankAccountBalance> list = new List<VOBankAccountBalance>();

            try
            {
                using (SqlConnection objConn = new SqlConnection(xConnectionString))
                {
                    //Open DataBase
                    objConn.Open();

                    //ถ่ายทอด Command Class มาจาก Connection ที่เปิดไว้
                    SqlCommand objComm = objConn.CreateCommand();

                    objComm.CommandText = @"SELECT  dbo.AccountBalance.AccountID as AccountID,   dbo.Account.AccountNo as AccountNo , dbo.MLSValue.Value AS Name, dbo.FundTransaction.TransactionNo, dbo.TransactionType.Code
FROM         dbo.FundTransaction INNER JOIN
                      dbo.TransactionType ON dbo.FundTransaction.TransactionTypeID = dbo.TransactionType.TransactionTypeID INNER JOIN
                      dbo.MLSValue INNER JOIN
                      dbo.Account INNER JOIN
                      dbo.AccountBalance ON dbo.Account.AccountID = dbo.AccountBalance.AccountID ON dbo.MLSValue.MLSID = dbo.Account.NameMLSID ON 
                      dbo.FundTransaction.TransactionID = dbo.AccountBalance.PreviousTransactionID
WHERE     (dbo.TransactionType.TransactionTypeID IN (1, 2, 4, 5))";
                    
                    SqlDataReader objReader = objComm.ExecuteReader();

                    while (objReader.Read())
                    {
                        VOBankAccountBalance objRoleMenu = new VOBankAccountBalance();
                        objRoleMenu.AccountID = objReader["AccountID"] == DBNull.Value ? 0 : (int)objReader["AccountID"];
                        objRoleMenu.AccountNo = (string)objReader["AccountNo"];
                        objRoleMenu.AccountName = (string)objReader["Name"];
                        objRoleMenu.TransactionNo = (string)objReader["TransactionNo"];
                        objRoleMenu.TransactionType = (string)objReader["Code"];
                        list.Add(objRoleMenu);
                    }
                }
            }
            catch (Exception)
            {

            }
            return list;
        }

       
        #endregion
       
    }
}
