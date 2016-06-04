using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ton.Data;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for gAdmin
/// </summary>
namespace riskEval
{
    public class gAdmin  
    {


	    public gAdmin()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        public static string ManageYutasad(int announce_id, string title,int yut_id, string action)
        {

            string strsql;
            SqlConnection conn =  gUtilities.DBConnection();//DBConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            switch (action)
            {
                case "delete":
                    strsql = string.Format(@"Update from yutasad where yut_id=@yut_id;");
                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@yut_id", yut_id);
                    break;
                case "insert":
                    strsql = string.Format(@"insert into yutasad(yut_name, isActive)
                            values(@yut_name,1)");
                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@yut_name", title);
                    break;
                case "update":
                    strsql = string.Format(@"Update yutasad set yut_name =@yut_name where yut_id=@yut_id;");
                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@yut_name", title);
                    cmd.Parameters.AddWithValue("@yut_id", yut_id);
                    
                    break;

                default:
                    break;
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return "";
        }



        public static string ManageTammapiban(int announce_id, string title, int yut_id, string action)
        {

            string strsql;
            SqlConnection conn = gUtilities.DBConnection();//DBConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            switch (action)
            {
                case "delete":
                    strsql = string.Format(@"Update from tammapiban where tm_id=@tm_id;");
                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@tm_id", yut_id);
                    break;
                case "insert":
                    strsql = string.Format(@"insert into tammapiban(tm_name, isActive)
                            values(@tm_name,1)");
                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@tm_name", title);
                    break;
                case "update":
                    strsql = string.Format(@"Update tammapiban set tm_name =@tm_name where tm_id=@tm_id;");
                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@tm_name", title);
                    cmd.Parameters.AddWithValue("@tm_id", yut_id);
                    break;

                default:
                    break;
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return "";
        }


        public static DataSet getData()
        {
            string sql_str = @"
                SELECT  * From yutasad whree isActive =1;
        ";
            SqlCommand cmd = new SqlCommand(sql_str);

            DataSet ds = DBHelper.getDataSet(cmd);
            return ds;
        }


//        public void getRiskValue(string pj_id)
//        {
//            double dbTotal1 =0;
//            double dbTotal2=0;
//            double dbTamma1 = 0;
//            double dbTamma2 = 0;
//            double dbTammaTotal = 0;

//            SqlConnection conn = gUtilities.DBConnection();

//            string strSql = string.Format(@"select count(*) from dbo.answer_factors_sub where pj_id = @pj_id;
//                                        select count(*) from dbo.answer_factors_sub where pj_id = @pj_id and af_impact = 'จัดการได้' ;
//                                        
//                                        select sum(yes_percent) from report_tamma1 where pj_id = @pj_id;
//
//                                        select sum(yes_percent) from report_tamma2 where pj_id = @pj_id;
//
//                                        select sum(yes_percent) from report_tamma_total where pj_id = @pj_id;
//                                        ");

//            SqlCommand cmd = new SqlCommand(strSql, conn);
//            cmd.Parameters.Clear();
//            cmd.CommandText = strSql;
//            cmd.CommandType = CommandType.Text;
//            cmd.Parameters.AddWithValue("@pj_id", pj_id);
//            cmd.ExecuteNonQuery();

//            DataSet ds = new DataSet();

//            cmd.CommandTimeout = 0;
//            SqlDataAdapter da = new SqlDataAdapter(cmd);
//            da.Fill(ds);

//            gUtilities.DBConnection().Close();


//            if (ds != null)
//            {
//                if (ds.Tables.Count > 0)
//                {
//                    if (ds.Tables[0].Rows.Count > 0)
//                    {
//                        dbTotal1 = Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString());
//                    }
//                    if (ds.Tables[1].Rows.Count > 0)
//                    {
//                        dbTotal2 = Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString());
//                    }
//                    //tamma1
//                    if (ds.Tables[2].Rows.Count > 0)
//                    {
//                        dbTamma1 = Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString());
//                        strTamma1 = calRate(dbTamma1);
//                    }

//                    //tamma2
//                    if (ds.Tables[3].Rows.Count > 0)
//                    {
//                        dbTamma2 = Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString());
//                        strTamma2 = calRate(dbTamma2);
//                    }

//                    //tamma_total
//                    if (ds.Tables[4].Rows.Count > 0)
//                    {
//                        dbTammaTotal  = Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString());
//                        strTammaTotal = calRate(dbTammaTotal);
//                    }
                
//                }
//            }

//            double dbResult = (dbTotal2 / dbTotal1) * 100;
//            strRiskFactor = calRate(dbResult);
//        }


//        public static string calRate(double val)
//        {
//            string strReturn = string.Empty;

//            if (val > 70)
//            {
//                strReturn = "ต่ำ";
//            }
//            else if (val <= 70 && val > 30)
//            {

//                strReturn = "ปานกลาง";
//            }
//            else
//            {
//                strReturn = "สูง";
//            }
//            return strReturn;
//        }

    }




}