using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ton;
using ton.config;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Web.Caching;

/// <summary>
/// Summary description for projects
/// </summary>
/// 

namespace riskEval
{

    public class projects
    {
        public projects()
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
            //conn = new SqlConnection();

            if (conn.State == ConnectionState.Closed)
            {
                conn = new SqlConnection(CnnString);
                conn.Open();
            }
            
            return conn;
			
		}

        public string getProjectInfo(string project_id, string columnname)
        {

            try
            {

                SqlConnection conn = DBConnection();

                string strsql = string.Format(@"                 
                            select " + columnname + " from projects where pj_id = " + project_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0 &&
                        (!ds.Tables[0].Rows[0].IsNull(columnname)))
                {

                    return ds.Tables[0].Rows[0][columnname].ToString();

                }
                else
                {

                    return string.Empty;

                }
               

            }
            catch
            {

                return string.Empty;

            }
            finally { DBConnection().Close(); }

        }

        public DataSet getProjectInfoAll_Real_Submitted(string p_id)
        {
            try
            {

                SqlConnection conn = DBConnection();

                string strsql = string.Format(@"   
                             select pj_id, pj_name, pj_category, pj_budget, CONVERT(VARCHAR(10), dateadd(year, 543,  pj_lastupdate), 103) as pj_lastupdate , 
                                pj_filter_q1, pj_filter_q2, pj_filter_q3, pj_filter_q4
                                from projects 
                                where pj_complete_status = 'ส่งผลแล้ว' 
                                and mi_id is not null
                                and pj_name is not null
                                and pj_yut_id is not null
                                and pj_year is not null
                                and pj_budget is not null
                                and pj_status = 'real' and p_id = " + p_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }
            catch
            {

                return null;

            }
            finally { DBConnection().Close(); }

        }

        public DataSet getProjectInfoAll_Sim_Submitted(string p_id)
        {

            try
            {

                SqlConnection conn = DBConnection();

                string strsql = string.Format(@"                 
                            select pj_id, pj_name, pj_category, pj_budget, 
                            CONVERT(VARCHAR(10), dateadd(year, 543,  pj_lastupdate), 103) as pj_lastupdate , 
                            pj_filter_q1, pj_filter_q2, pj_filter_q3, pj_filter_q4
                            from projects 
                            where pj_complete_status = 'ส่งผลแล้ว' and pj_status = 'sim' and p_id = " + p_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }
            catch
            {

                return null;

            }
            finally { DBConnection().Close(); }

        }

        public DataSet getProjectInfoAll_Real_notSubmitted(string p_id)
        {

            try
            {

                SqlConnection conn = DBConnection();

                //ยังกรอกไม่ครบ และยังไม่ส่งผล
                string strsql = string.Format(@"                 
                            select pj_id, pj_name, pj_category, pj_budget, CONVERT(VARCHAR(10), dateadd(year, 543,  pj_lastupdate), 103) as pj_lastupdate , 
                            pj_filter_q1, pj_filter_q2, pj_filter_q3, pj_filter_q4
                            from projects 
                           where (pj_complete_status is null or
						    (pj_complete_status not like N'%กรอกสมบูรณ์%'
							and pj_complete_status not like N'ส่งผลแล้ว%'))
                            and pj_status = 'real' 
                            and mi_id is not null
                            and pj_name is not null
                            and pj_yut_id is not null
                            and pj_year is not null
                            and pj_budget is not null and p_id = " + p_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }
            catch
            {

                return null;

            }
            finally { DBConnection().Close(); }

        }

        public DataSet getProjectInfoAll_Real_Approved(string p_id)
        {

            try
            {

                SqlConnection conn = DBConnection();

//                string strsql = string.Format(@"                 
//                            select * from projects 
//                                where mi_id is not null
//                                and pj_name is not null
//                                and pj_yut_id is not null
//                                and pj_year is not null
//                                and pj_budget is not null
//                                and pj_approval_status is not null
//                                and pj_status = 'real' and p_id = " + p_id);


                string strsql = string.Format(@" 
								select pj_id, pj_name, pj_category, pj_budget, CONVERT(VARCHAR(10), dateadd(year, 543,  pj_lastupdate), 103) as pj_lastupdate , 
                                pj_filter_q1, pj_filter_q2, pj_filter_q3, pj_filter_q4
                                from projects 
                                where (pj_complete_status is null 
                                or pj_complete_status not like N'ไม่อยู่ในเกณฑ์การประเมิน%')
                                and mi_id is not null
                                and pj_name is not null
                                and pj_yut_id is not null
                                and pj_year is not null
                                and pj_budget is not null
                        and pj_status = 'real' and p_id = " + p_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }
            catch
            {

                return null;

            }
            finally { DBConnection().Close(); }

        }

        public DataSet getProjectInfoAll_Sim_notSubmitted(string p_id)
        {

            try
            {

                SqlConnection conn = DBConnection();

//                string strsql = string.Format(@"                 
//                            select * from projects 
//                                 where (pj_complete_status != 'กรอกสมบูรณ์'
//                                or pj_complete_status not like '%ไม่อยู่ในเกณฑ์การประเมิน%')
//                                and mi_id is not null
//                                and pj_name is not null
//                                and pj_yut_id is not null
//                                and pj_year is not null
//                                and pj_budget is not null
//                                and pj_status = 'sim' and p_id = " + p_id);

                string strsql = string.Format(@" 
								select pj_id, pj_name, pj_category, pj_budget, CONVERT(VARCHAR(10), dateadd(year, 543,  pj_lastupdate), 103) as pj_lastupdate , 
                                   pj_filter_q1, pj_filter_q2, pj_filter_q3, pj_filter_q4
                                from projects 
                                where (pj_complete_status is null or
						            (pj_complete_status not like N'%กรอกสมบูรณ์%'
							    and pj_complete_status not like N'ส่งผลแล้ว%'))
                                and mi_id is not null
                                and pj_name is not null
                                and pj_yut_id is not null
                                and pj_year is not null
                                and pj_budget is not null
                        and pj_status = 'sim' and p_id = " + p_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }
            catch
            {

                return null;

            }
            finally { DBConnection().Close(); }

        }

        public void updateProjectCompleteStatus(string pj_id, string status)
        {
            string strsql;
            SqlConnection conn = DBConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;

            strsql = string.Format(@"Update projects set pj_complete_status = @status where pj_id = @pj_id");
            cmd.CommandText = strsql;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        }

        public void updateReport_Submitted(string pj_id, string p_id)
        {

            SqlConnection conn = DBConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Report_submitted", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.Parameters.AddWithValue("@p_id", p_id);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        }

        public DataSet getProjectInfoAll_Real_Completed_notSubmitted(string p_id)
        {

            try
            {

                SqlConnection conn = DBConnection();

                //ยังกรอกไม่ครบ และยังไม่ส่งผล
                string strsql = string.Format(@"                 
                            select * from projects 
                            where pj_complete_status = N'กรอกสมบูรณ์'   
                            and pj_complete_status not like N'%ไม่อยู่ในเกณฑ์การประเมิน%' 
                            and pj_status = 'real' 
                            and mi_id is not null
                            and pj_name is not null
                            and pj_yut_id is not null
                            and pj_year is not null
                            and pj_budget is not null and p_id = " + p_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }
            catch
            {

                return null;

            }
            finally { DBConnection().Close(); }

        }

        public DataSet getProjectInfoAll_Sim_Completed_notSubmitted(string p_id)
        {

            try
            {

                SqlConnection conn = DBConnection();

                //ยังกรอกไม่ครบ และยังไม่ส่งผล
                string strsql = string.Format(@"                 
                            select * from projects 
                            where pj_complete_status = 'กรอกสมบูรณ์'   
                            and pj_complete_status not like N'%ไม่อยู่ในเกณฑ์การประเมิน%'
                            and pj_status = 'sim' 
                            and mi_id is not null
                            and pj_name is not null
                            and pj_yut_id is not null
                            and pj_year is not null
                            and pj_budget is not null and p_id = " + p_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }
            catch
            {

                return null;

            }
            finally { DBConnection().Close(); }

        }

        public string redirectToPendingProjectDetails(string pj_id)
        {
            string strURL = string.Empty;
            string strPJStatus = string.Empty;

            try
            {

                SqlConnection conn = DBConnection();

                string strsql = string.Format(@"                 
                            select * from projects 
                            where pj_id = " + pj_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    strPJStatus = ds.Tables[0].Rows[0]["pj_status"].ToString();

                    if (ds.Tables[0].Rows[0]["pj_budget"] is DBNull)
                    {

                        strURL = "project_details.aspx?status=" + strPJStatus;
                   
                    }
                    else if (ds.Tables[0].Rows[0]["pj_filter_q1"] is DBNull)
                    {

                        strURL = "project_filter.aspx?status=" + strPJStatus;
                  
                    }
                    else if (ds.Tables[0].Rows[0]["pj_background"] is DBNull)
                    {

                        strURL = "project_basicinfo.aspx?status=" + strPJStatus;
    
                    }
                    else if (ds.Tables[0].Rows[0]["pj_category"] is DBNull)
                    {

                        strURL = "project_category.aspx?status=" + strPJStatus;
    
                    }
                    else if (ds.Tables[0].Rows[0]["pj_type"] is DBNull)
                    {
                        strURL = "project_type.aspx?status=" + strPJStatus;

                    }
                    else
                    {
                        strURL = "project_pickquestion.aspx";
                    }

                }
                else
                {

                    strURL = string.Empty;
                }
               

                }  
                catch
                {
                    strURL = string.Empty;
                }
                finally { DBConnection().Close(); }

                return strURL;

        }

        public DataSet getProject_Not_Require_Approval(string p_id)
        {

            try
            {

                SqlConnection conn = DBConnection();

                string strsql = string.Format(@"                 
                            select * from projects 
                                where pj_complete_status like N'%ไม่อยู่ในเกณฑ์การประเมิน%'
                                and pj_status = 'real' and p_id = " + p_id);

                SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;

            }
            catch
            {

                return null;

            }
            finally { DBConnection().Close(); }

        }

        public void updateDocNo_DocDate(string pj_id, string docno, string datedoc) {

            string strsql;
            SqlConnection conn = DBConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;

            strsql = string.Format(@"Update projects set pj_doc_number = @docno, pj_date_doc_submitted = @datedoc where pj_id = @pj_id");
            cmd.CommandText = strsql;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.Parameters.AddWithValue("@docno", docno);

            cmd.Parameters.AddWithValue("@datedoc", datedoc);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        }

        public int insert_ProjectBackUp(string pj_id)
        {
            string strsql = string.Format(@"
                INSERT INTO projects_backup ([pj_id],
	            [pj_code], 
	            [p_id], 
	            [d_id], 
	            [mi_id],
	            [pj_name],
	            [pj_yut_id],
	            [pj_year],
	            [pj_budget], 
	            [pj_budget_money], 
	            [pj_integrateProject] ,
	            [pj_relateDept],
	            [pj_filter_q1],
	            [pj_filter_q2], 
	            [pj_filter_q3], 
	            [pj_filter_q4], 
	            [pj_background],
	            [pj_urgency], 
	            [pj_risk_info], 
	            [pj_risk_reduction1],
	            [pj_risk_reduction2] ,
	            [pj_risk_eval1],
	            [pj_risk_eval2],
	            [pj_risk_eval3],
	            [pj_category],
	            [pj_type], 
	            [pj_status], 
	            [pj_lastupdate],
	            [pj_isinuse], 
	            [pj_complete_status],
	            [pj_approval_comment],
	            [pj_approval_comment_1_1],
	            [pj_approval_comment_1_2],
	            [pj_approval_comment_2] ,
	            [pj_approval_status] ,
	            [pj_doc_number],
	            [pj_date_doc_submitted],
	            [pj_approval_budget])
                    SELECT [pj_id],
	                    [pj_code], 
	                    [p_id], 
	                    [d_id], 
	                    [mi_id],
	                    [pj_name],
	                    [pj_yut_id],
	                    [pj_year],
	                    [pj_budget], 
	                    [pj_budget_money], 
	                    [pj_integrateProject] ,
	                    [pj_relateDept],
	                    [pj_filter_q1],
	                    [pj_filter_q2], 
	                    [pj_filter_q3], 
	                    [pj_filter_q4], 
	                    [pj_background],
	                    [pj_urgency], 
	                    [pj_risk_info], 
	                    [pj_risk_reduction1],
	                    [pj_risk_reduction2] ,
	                    [pj_risk_eval1],
	                    [pj_risk_eval2],
	                    [pj_risk_eval3],
	                    [pj_category],
	                    [pj_type], 
	                    [pj_status], 
	                    [pj_lastupdate],
	                    [pj_isinuse], 
	                    [pj_complete_status],
	                    [pj_approval_comment],
	                    [pj_approval_comment_1_1],
	                    [pj_approval_comment_1_2],
	                    [pj_approval_comment_2] ,
	                    [pj_approval_status] ,
	                    [pj_doc_number],
	                    [pj_date_doc_submitted],
	                    [pj_approval_budget]
                    FROM projects
                    WHERE pj_id = @pj_id");

            SqlConnection conn = DBConnection();
          
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;

            cmd.CommandText = strsql;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
       
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            return ret;


        }


        public int delete_project(string pj_id)
        {
            string strsql = string.Format(@"
                    Delete 
                    FROM projects
                    WHERE pj_id = @pj_id");

            SqlConnection conn = DBConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;

            cmd.CommandText = strsql;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);

            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            return ret;


        }
    }




}

