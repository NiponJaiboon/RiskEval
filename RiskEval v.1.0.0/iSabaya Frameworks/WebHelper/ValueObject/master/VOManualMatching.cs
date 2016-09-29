using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOManualMatching
    {
        #region Member && Property
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string recordType;

        public string RecordType
        {
            get { return recordType; }
            set { recordType = value; }
        }
        private decimal sequenceNo;

        public decimal SequenceNo
        {
            get { return sequenceNo; }
            set { sequenceNo = value; }
        }
        private decimal bankCode;

        public decimal BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }
        private decimal companyAccount;

        public decimal CompanyAccount
        {
            get { return companyAccount; }
            set { companyAccount = value; }
        }
        //[ID] [int] IDENTITY(1,1) NOT NULL,
        //[RecordType] [nvarchar](1) NULL,
        //[SequenceNo] [decimal](6, 0) NULL,
        //[BankCode] [decimal](3, 0) NULL,
        //[CompanyAccount] [decimal](10, 0) NULL,
        //[PaymentDate] [datetime] NULL,
        private DateTime paymentDate;

        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }
        //[PaymentTime] [datetime] NULL,
        private DateTime paymentTime;

        public DateTime PaymentTime
        {
            get { return paymentTime; }
            set { paymentTime = value; }
        }
        //[CustomerName] [nvarchar](50) NULL,
        private string customerName;

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }
        //[CustomerNoRef1] [nvarchar](20) NULL,
        private string customeraNoRef1;

        public string CustomerNoRef1
        {
            get { return customeraNoRef1; }
            set { customeraNoRef1 = value; }
        }
        //[CustomerNoRef2] [nvarchar](20) NULL,
        private string customerNoRef2;

        public string CustomerNoRef2
        {
            get { return customerNoRef2; }
            set { customerNoRef2 = value; }
        }
        //[CustomerNoRef3] [nvarchar](20) NULL,
        private string customerNoRef3;

        public string CustomerNoRef3
        {
            get { return customerNoRef3; }
            set { customerNoRef3 = value; }
        }
        //[BranchNo] [decimal](4, 0) NULL,
        private decimal branchNo;

        public decimal BranchNo
        {
            get { return branchNo; }
            set { branchNo = value; }
        }
        //[TellerNo] [decimal](4, 0) NULL,
        private decimal tellerNo;

        public decimal TellerNo
        {
            get { return tellerNo; }
            set { tellerNo = value; }
        }
        //[KindOfTransaction] [nvarchar](1) NULL,
        private string kindOfTransaction;

        public string KindOfTransaction
        {
            get { return kindOfTransaction; }
            set { kindOfTransaction = value; }
        }
        //[TransactionCode] [nvarchar](3) NULL,
        private string transactiornCode;

        public string TransactionCode
        {
            get { return transactiornCode; }
            set { transactiornCode = value; }
        }
        //[ChequeNo] [decimal](7, 0) NULL,
        private decimal chequeNo;

        public decimal ChequeNo
        {
            get { return chequeNo; }
            set { chequeNo = value; }
        }
        //[Amount] [decimal](13, 0) NULL,
        private decimal amont;

        public decimal Amount
        {
            get { return amont; }
            set { amont = value; }
        }
        //[ChequeBankCode] [decimal](3, 0) NULL,
        private decimal chequeBankCode;

        public decimal ChequeBankCode
        {
            get { return chequeBankCode; }
            set { chequeBankCode = value; }
        }
        //[ChequeBranchCode] [nvarchar](4) NULL,
        private string chequeBranchCode;

        public string ChequeBranchCode
        {
            get { return chequeBranchCode; }
            set { chequeBranchCode = value; }
        }
        //[ReceiptNumber] [nvarchar](20) NULL,
        private string receiptNumber;

        public string ReceiptNumber
        {
            get { return receiptNumber; }
            set { receiptNumber = value; }
        }
        //[Space] [nvarchar](53) NULL,
        private string space;

        public string Space
        {
            get { return space; }
            set { space = value; }
        }
        #endregion

        //static Method
        public static List<VOManualMatching> List(string connectionString)
        {
            List<VOManualMatching> list = new List<VOManualMatching>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = @"SELECT ID, RecordType, SequenceNo, BankCode, CompanyAccount, 
PaymentDate, PaymentTime, CustomerName, CustomerNoRef1, CustomerNoRef2, CustomerNoRef3, 
BranchNo, TellerNo, KindOfTransaction, TransactionCode, ChequeNo, Amount, ChequeBankCode, 
ChequeBranchCode, ReceiptNumber, Space
FROM BP_KTB_Receivable_Detail
WHERE (ID NOT IN (SELECT ID FROM BP_KTB_Receivable_Detail AS BP_KTB_Receivable_Detail_1
                            WHERE (CustomerNoRef1 LIKE '99%'  and StatusFromMatching is null)))";

                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    VOManualMatching vo = new VOManualMatching();
                    vo.ID = reader["ID"] == null ? 0 : (int)reader["ID"];
                    vo.RecordType = reader["RecordType"] == null ? "" : (string)reader["RecordType"];
                    vo.SequenceNo = reader["SequenceNo"] == null ? 0 : (decimal)reader["SequenceNo"];
                    vo.BankCode = reader["BankCode"] == null ? 0 : (decimal)reader["BankCode"];
                    vo.CompanyAccount = reader["CompanyAccount"] == null ? 0 : (decimal)reader["CompanyAccount"];
                    if (reader["PaymentDate"] != null)
                    {
                        vo.PaymentDate = (DateTime)reader["PaymentDate"];
                    }
                    if (reader["PaymentTime"] != null)
                    {
                        vo.PaymentTime = (DateTime)reader["PaymentTime"];
                    }
                    vo.CustomerName = reader["CustomerName"] == null ? "" : (string)reader["CustomerName"];
                    vo.CustomerNoRef1 = reader["CustomerNoRef1"] == null ? "" : (string)reader["CustomerNoRef1"];
                    vo.CustomerNoRef2 = reader["CustomerNoRef2"] == null ? "" : (string)reader["CustomerNoRef2"];
                    vo.CustomerNoRef3 = reader["CustomerNoRef3"] == null ? "" : (string)reader["CustomerNoRef3"];
                    vo.BranchNo = reader["BranchNo"] == null ? 0 : (decimal)reader["BranchNo"];
                    vo.TellerNo = reader["TellerNo"] == null ? 0 : (decimal)reader["TellerNo"];
                    vo.KindOfTransaction = reader["KindOfTransaction"] == null ? "" : (string)reader["KindOfTransaction"];
                    vo.TransactionCode = reader["TransactionCode"] == null ? "" : (string)reader["TransactionCode"];
                    vo.ChequeNo = reader["ChequeNo"] == null ? 0 : (decimal)reader["ChequeNo"];
                    vo.Amount = reader["Amount"] == null ? 0 : (decimal)reader["Amount"];
                    vo.ChequeBankCode = reader["ChequeBankCode"] == null ? 0 : (decimal)reader["ChequeBankCode"];
                    vo.ChequeBranchCode = reader["ChequeBranchCode"] == null ? "" : (string)reader["ChequeBranchCode"];
                    vo.ReceiptNumber = reader["ReceiptNumber"] == null ? "" : (string)reader["ReceiptNumber"];
                    vo.Space = reader["Space"] == null ? "" : (string)reader["Space"];

                    list.Add(vo);
                }
            }

            return list;
        }
    }
}
