using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using System.Data.SqlClient;
namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOAccountBalance
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

         public static List<VOAccountBalance> List(string xConnectionString)
        {
            //return value
            List<VOAccountBalance> list = new List<VOAccountBalance>();

            try
            {
                using (SqlConnection objConn = new SqlConnection(xConnectionString))
                {
                    //Open DataBase
                    objConn.Open();

                    //ถ่ายทอด Command Class มาจาก Connection ที่เปิดไว้
                    SqlCommand objComm = objConn.CreateCommand();

                    objComm.CommandText = @"SELECT     dbo.Account.AccountID, dbo.Account.AccountNo, dbo.MLSValue.Value AS name, dbo.FundTransaction.TransactionNo, dbo.TransactionType.Code
FROM         dbo.AccountBalance INNER JOIN
                      dbo.Account ON dbo.AccountBalance.AccountID = dbo.Account.AccountID INNER JOIN
                      dbo.MLSValue ON dbo.Account.NameMLSID = dbo.MLSValue.MLSID INNER JOIN
                      dbo.FundTransaction ON dbo.AccountBalance.PreviousTransactionID = dbo.FundTransaction.TransactionID INNER JOIN
                      dbo.TransactionType ON dbo.FundTransaction.TransactionTypeID = dbo.TransactionType.TransactionTypeID";// where dbo.TransactionType.TransactionTypeID in (1,2,4,5)";
                   
                    SqlDataReader objReader = objComm.ExecuteReader();

                    while (objReader.Read())
                    {
                        VOAccountBalance objRoleMenu = new VOAccountBalance();

                        objRoleMenu.AccountID = objReader["AccountID"] == DBNull.Value ? 0 : (int)objReader["AccountID"];
                        objRoleMenu.AccountName = (string)objReader["name"];
                        objRoleMenu.AccountNo = (string)objReader["AccountNo"];
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
      
    }
}
