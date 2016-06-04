using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ton.Data;
using ton;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Web.Caching;

namespace riskEval
{


    /// <summary>
    /// Summary description for answer
    /// </summary>
    public class answer
    {
        public answer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string CnnString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["GovBudgetingConnectionString"].ConnectionString.ToString();
                // return ConfigurationManager.ConnectionStrings["GovBudgeting01ConnectionString"];

            }
        }

        public static SqlConnection DBConnection()
        {
            SqlConnection conn = new SqlConnection();
       

            if (conn.State == ConnectionState.Closed)
            {
                conn = new SqlConnection(CnnString);
                conn.Open();
            }
            
            return conn;
        }

        public int insertOrUpdateAnswerQ2(string answer_q2_text, string pj_id, string q2_id, string qset_id)
        {

            int retIdentity = 0;

            try
            {

                SqlConnection conn = DBConnection();

                string strsql = string.Format(@"
                            select * from answer_q2 where pj_id = " + pj_id + " and q2_id = " + q2_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DBConnection().Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //this project already have existing answer for question 2, update
                    strsql = string.Format(@"
                            update answer_q2 set answer_q2_text=@answer_q2_text, q2_id=@q2_id, updatedate=getDate(), qset_id = @qset_id where pj_id=@pj_id and q2_id = @q2_id;
                            select answer_q2_id from answer_q2 where pj_id = @pj_id and q2_id = @q2_id;
                            ");
                    SqlCommand cmd = new SqlCommand(strsql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@pj_id", pj_id);
                    cmd.Parameters.AddWithValue("@answer_q2_text", answer_q2_text);
                    cmd.Parameters.AddWithValue("@q2_id", q2_id);
                    cmd.Parameters.AddWithValue("@qset_id", qset_id);

                    retIdentity = Convert.ToInt32(cmd.ExecuteScalar());

                }
                else
                {
                    //this project does not have existing answer for question 2, insert
                    strsql = string.Format(@"
                            insert into answer_q2 (answer_q2_text, pj_id, q2_id, updatedate, qset_id) values (@answer_q2_text, @pj_id, @q2_id, getdate(), @qset_id); 
                            select @@Identity;
                            ");
                    SqlCommand cmd = new SqlCommand(strsql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@pj_id", pj_id);
                    cmd.Parameters.AddWithValue("@answer_q2_text", answer_q2_text);
                    cmd.Parameters.AddWithValue("@q2_id", q2_id);
                    cmd.Parameters.AddWithValue("@qset_id", qset_id);

                    retIdentity = Convert.ToInt32(cmd.ExecuteScalar());
                }

            }
            catch (Exception ex) {

               throw new Exception(ex.Message);
     
            }

            finally { DBConnection().Close(); }
            return retIdentity;

        }

        public int insertOrUpdateAnswerQ3(string answer_q3_text, string pj_id, string q3_id, string answer_q2_id)
        {

            try
            {

                SqlConnection conn = DBConnection();

                string strsql = string.Format(@"                 
                            select * from answer_q3 where answer_q2_id = " + answer_q2_id + " and q3_id = " + q3_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DBConnection().Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //this project already have existing answer for question 3, update
                    strsql = string.Format(@"                 
                            update answer_q3 set answer_q3_text=@answer_q3_text, 
                                    answer_q2_id=@answer_q2_id, 
                                    q3_id=@q3_id, 
                                    inputdate=getDate()
                                    where answer_q2_id = @answer_q2_id and
                                    q3_id = @q3_id
                            ");
                    SqlCommand cmd = new SqlCommand(strsql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@pj_id", pj_id);
                    cmd.Parameters.AddWithValue("@answer_q3_text", answer_q3_text);
                    cmd.Parameters.AddWithValue("@answer_q2_id", answer_q2_id);
                    cmd.Parameters.AddWithValue("@q3_id", q3_id);

                    cmd.ExecuteNonQuery();
                  
                   
                }
                else
                {
                    //this project does not have existing answer for question 3, insert

                    strsql = string.Format(@"                 
                            insert into answer_q3 (answer_q3_text, answer_q2_id, q3_id, inputdate) values (@answer_q3_text, @answer_q2_id, @q3_id, getdate())
                            ");
                    SqlCommand cmd = new SqlCommand(strsql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@pj_id", pj_id);
                    cmd.Parameters.AddWithValue("@answer_q3_text", answer_q3_text);
                    cmd.Parameters.AddWithValue("@answer_q2_id", answer_q2_id);
                    cmd.Parameters.AddWithValue("@q3_id", q3_id);

                    cmd.ExecuteNonQuery();
                    
                }


                return 1;

            }
            catch {

                return -1;

            }
            finally { DBConnection().Close(); }
      

        }

        public int insertAnswerStatus(int pj_id)
        {
            try
            {

                SqlConnection conn = DBConnection();
               
                string strsql = string.Format(@"
                            insert into answer_status (pj_id) values (@pj_id);
                            ");
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@pj_id", pj_id);
                cmd.ExecuteNonQuery();


            }
            catch { return -1; }
            finally { DBConnection().Close(); }
            return 1;

        }

        public string getLatestAnswerQ2(string pj_id, string qset_id)
        {

            string strRet = string.Empty;

            try
            {

                SqlConnection conn = DBConnection();

                string strsql = string.Format(@"
                            select max(q2_id) from answer_q2 where pj_id = @pj_id and qset_id = @qset_id;
                            ");
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@pj_id", pj_id);
                cmd.Parameters.AddWithValue("@qset_id", qset_id);

                object val = cmd.ExecuteScalar();

                if (val != null)
                {

                    strRet = Convert.ToString(cmd.ExecuteScalar());

                }
                else
                {
                    strRet = "0";
                }

                

            }
            catch
            {
                strRet = "0";
            }
            finally { DBConnection().Close(); }

            return strRet;

        }

        public string getLatestAnswerQSetID(string pj_id)
        {

            string strRet = string.Empty;

            try
            {

                SqlConnection conn = DBConnection();

                string strsql = string.Format(@"
                            (select max(qset_id) from answer_q2 where pj_id = @pj_id);
                            ");
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@pj_id", pj_id);
          
                object val = cmd.ExecuteScalar();

                if (val != null)
                {

                    strRet = Convert.ToString(cmd.ExecuteScalar());

                }
                else
                {
                    strRet = "0";
                }



            }
            catch
            {
                strRet = "0";
            }
            finally { DBConnection().Close(); }

            return strRet;

        }


    }

}