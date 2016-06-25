using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VONAV_TEMP
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        //[FundCode] [nvarchar] NOT NULL,
        private string fundCode;
        public string FundCode
        {
            get { return fundCode; }
            set { fundCode = value; }
        }
        //[Date] [datetime] NULL,
        private string date;
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        //[UnitNav] [money] NULL,
        private decimal unitNav;
        public decimal UnitNav
        {
            get { return unitNav; }
            set { unitNav = value; }
        }
        //[Units] [float] NOT NULL,
        private float units;
        public float Units
        {
            get { return units; }
            set { units = value; }
        }
        //[Amount] [money] NULL,
        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        //[isPublic] [bit] NULL,
        private bool isPublic;
        public bool IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }
        //[Accounts] [int] NULL,
        private int accounts;
        public int Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }
        //[PurchasePrice] [money] NULL,
        private decimal purchasePrice;
        public decimal PurchasePrice
        {
            get { return purchasePrice; }
            set { purchasePrice = value; }
        }
        //[RedeemPrice] [money] NULL,
        private decimal redeemPrice;
        public decimal RedeemPrice
        {
            get { return redeemPrice; }
            set { redeemPrice = value; }
        }
        //[SwitchInPrice] [money] NULL,
        private decimal switchInPrice;
        public decimal SwitchInPrice
        {
            get { return switchInPrice; }
            set { switchInPrice = value; }
        }
        //[SwitchOutPrice] [money] NULL,
        private decimal switchOutPrice;
        public decimal SwitchOutPrice
        {
            get { return switchOutPrice; }
            set { switchOutPrice = value; }
        }
        //[ExternalSwitchOutPrice] [money] NULL,
        private decimal externalSwitchOutPrice;
        public decimal ExternalSwitchOutPrice
        {
            get { return externalSwitchOutPrice; }
            set { externalSwitchOutPrice = value; }
        }
        //[ExternalSwitchInPrice] [money] NULL,
        private decimal externalSwitchInPrice;
        public decimal ExternalSwitchInPrice
        {
            get { return externalSwitchInPrice; }
            set { externalSwitchInPrice = value; }
        }
        //[PurchasePriceIncludingFee] [money] NULL,
        private decimal purchasePriceIncludingFee;
        public decimal PurchasePriceIncludingFee
        {
            get { return purchasePriceIncludingFee; }
            set { purchasePriceIncludingFee = value; }
        }
        //[RedeemPriceIncludingFee] [money] NULL,
        private decimal redeemPriceIncludingFee;
        public decimal RedeemPriceIncludingFee
        {
            get { return redeemPriceIncludingFee; }
            set { redeemPriceIncludingFee = value; }
        }
        //[SwitchInPriceIncludingFee] [money] NULL,
        private decimal switchInPriceIncludingFee;
        public decimal SwitchInPriceIncludingFee
        {
            get { return switchInPriceIncludingFee; }
            set { switchInPriceIncludingFee = value; }
        }
        //[SwitchOutPriceIncludingFee] [money] NULL,
        private decimal switchOutPriceIncludingFee;
        public decimal SwitchOutPriceIncludingFee
        {
            get { return switchOutPriceIncludingFee; }
            set { switchOutPriceIncludingFee = value; }
        }
        //[ExternalSwitchInPriceIncludingFee] [money] NULL,
        private decimal externalSwitchInPriceIncludingFee;
        public decimal ExternalSwitchInPriceIncludingFee
        {
            get { return externalSwitchInPriceIncludingFee; }
            set { externalSwitchInPriceIncludingFee = value; }
        }
        //[ExternalSwitchOutPriceIncludingFee] [money] NULL,
        private decimal externalSwitchOutPriceIncludingFee;
        public decimal ExternalSwitchOutPriceIncludingFee
        {
            get { return externalSwitchOutPriceIncludingFee; }
            set { externalSwitchOutPriceIncludingFee = value; }
        }

        //static method
        public static List<VONAV_TEMP> List(string xConnectionString)
        {
            //return value
            List<VONAV_TEMP> list = new List<VONAV_TEMP>();

            try
            {
                using (SqlConnection objConn = new SqlConnection(xConnectionString))
                {
                    //Open DataBase
                    objConn.Open();

                    //ถ่ายทอด Command Class มาจาก Connection ที่เปิดไว้
                    SqlCommand objComm = objConn.CreateCommand();
                    objComm.CommandText = @"select * from NAV_TEMP";
                    SqlDataReader objReader = objComm.ExecuteReader();

                    while (objReader.Read())
                    {
                        VONAV_TEMP obj = new VONAV_TEMP();
                        obj.ID = (int)objReader["ID"];
                        //[FundCode] [nvarchar] NOT NULL,
                        obj.FundCode = objReader["FundCode"] == DBNull.Value ? "" : (string)objReader["FundCode"];
                        //[Date] [datetime] NULL,
                        obj.Date = objReader["Date"] == DBNull.Value ? "" : (string)objReader["Date"];
                        //[UnitNav] [money] NULL,
                        obj.UnitNav = objReader["UnitNav"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["UnitNav"]);
                        //[Units] [float] NOT NULL,
                        obj.Units = objReader["Units"] == DBNull.Value ? 0 : float.Parse((string)objReader["Units"]);
                        //[Amount] [money] NULL,
                        obj.Amount = objReader["Amount"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["Amount"]);
                        //[isPublic] [bit] NULL,
                        obj.IsPublic = Convert.ToBoolean(int.Parse((string)objReader["isPublic"]));
                        //[Accounts] [int] NULL,
                        obj.Accounts = objReader["Accounts"] == DBNull.Value ? 0 : int.Parse((string)objReader["Accounts"]);
                        //[PurchasePrice] [money] NULL,
                        obj.PurchasePrice = objReader["PurchasePrice"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["PurchasePrice"]);
                        //[RedeemPrice] [money] NULL,
                        obj.RedeemPrice = objReader["RedeemPrice"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["RedeemPrice"]);
                        //[SwitchInPrice] [money] NULL,
                        obj.SwitchInPrice = objReader["SwitchInPrice"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["SwitchInPrice"]);
                        //[SwitchOutPrice] [money] NULL,
                        obj.SwitchOutPrice = objReader["SwitchOutPrice"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["SwitchOutPrice"]);
                        //[ExternalSwitchOutPrice] [money] NULL,
                        obj.ExternalSwitchOutPrice = objReader["ExternalSwitchOutPrice"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["ExternalSwitchOutPrice"]);
                        //[ExternalSwitchInPrice] [money] NULL,
                        obj.ExternalSwitchInPrice = objReader["ExternalSwitchInPrice"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["ExternalSwitchInPrice"]);
                        //[PurchasePriceIncludingFee] [money] NULL,
                        obj.PurchasePriceIncludingFee = objReader["PurchasePriceIncludingFee"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["PurchasePriceIncludingFee"]);
                        //[RedeemPriceIncludingFee] [money] NULL,
                        obj.RedeemPriceIncludingFee = objReader["RedeemPriceIncludingFee"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["RedeemPriceIncludingFee"]);
                        //[SwitchInPriceIncludingFee] [money] NULL,
                        obj.SwitchInPriceIncludingFee = objReader["SwitchInPriceIncludingFee"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["SwitchInPriceIncludingFee"]);
                        //[SwitchOutPriceIncludingFee] [money] NULL,
                        obj.SwitchOutPriceIncludingFee = objReader["SwitchOutPriceIncludingFee"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["SwitchOutPriceIncludingFee"]);
                        //[ExternalSwitchInPriceIncludingFee] [money] NULL,
                        obj.ExternalSwitchInPriceIncludingFee = objReader["ExternalSwitchInPriceIncludingFee"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["ExternalSwitchInPriceIncludingFee"]);
                        //[ExternalSwitchOutPriceIncludingFee] [money] NULL,
                        obj.ExternalSwitchOutPriceIncludingFee = objReader["ExternalSwitchOutPriceIncludingFee"] == DBNull.Value ? 0 : decimal.Parse((string)objReader["ExternalSwitchOutPriceIncludingFee"]);

                        list.Add(obj);
                    }
                }
            }
            catch (Exception)
            {

            }
            return list;
        }

        public int Delete(string xConnectionString, int id)
        {
            try
            {
                using (SqlConnection objConn = new SqlConnection(xConnectionString))
                {
                    objConn.Open();

                    SqlCommand objComm = objConn.CreateCommand();
                    objComm.CommandText = @"    DELETE FROM NAV_TEMP 
                                                WHERE  (ID = @ID)";
                    objComm.Parameters.Add("@ID", SqlDbType.Int).Value = this.ID;
                    objComm.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return 0;   //not remove
            }
            return 1;       //success remove
        }
    }
}
