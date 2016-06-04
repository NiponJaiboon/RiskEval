using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ton.Data
{
    public class DBHelper
    {
        private const int sql_timeout = 300;
        public static string ConnString()
        {
            string strConn = ton.config.Global_config.DBConnectionString;
            return strConn; 
        }
        public static string getScalarData(SqlCommand SQL)
        {
            string result = "-1";
            SqlConnection conn = new SqlConnection(ConnString());

            SQL.Connection = conn;
            SQL.CommandTimeout = sql_timeout;
            try
            {
                conn.Open();
                result = SQL.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                result = "-1";
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            return (result);
        }
        public static DataSet getDataSet(SqlCommand SQL)
        {

            DataSet ds = new DataSet();

            SqlConnection conn = new SqlConnection(ConnString());
            SQL.Connection = conn;
            SQL.CommandTimeout = sql_timeout;
            SqlDataAdapter da = new SqlDataAdapter(SQL);
            try
            {
                conn.Open();
                da.Fill(ds);
                //ErrMsg = "";


            }
            catch (Exception ex)
            {
                ////String ErrMsg = ex.Message;
                ////DataTable DT = new DataTable();

                ////DT.Columns.Add("Error");

                ////DataRow myNewRow;

                ////myNewRow = DT.NewRow();

                ////myNewRow["Error"] = ex.Message.ToString();

                ////DT.Rows.Add(myNewRow);

                ////ds.Tables.Add(DT);

                ////ds = null;
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            return (ds);

        }
        public static int getAffectedData(SqlCommand SQL)
        {
            return getAffectedData(SQL, "");
        }
        public static int getAffectedData(SqlCommand SQL, string Comment)
        {
            int result = -1;
            SqlConnection conn = new SqlConnection(ConnString());

            if (SQL.CommandText == string.Empty)
            {
                return -1;
            }

            SQL.Connection = conn;
            SQL.CommandTimeout = sql_timeout;
            try
            {
                conn.Open();
                result = SQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ////result = ex.Message;
                
                result = -1;
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;

            }

            return (result);
        }
    }
}