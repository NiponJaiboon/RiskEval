using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ton.Data;

/// <summary>
/// Summary description for question_set
/// </summary>
public class question_set
{
    public string qset_id = "";
    public string qset_text = "";
    public List<question> qL1_list;
	public question_set()
	{
	}
    public void loadQuestionSet()
    {
        string sql_str = @" SELECT [qset_id], [qset_text] 
                            FROM [question_set] 
                            WHERE ([qset_id] = @qset_id) ";
        SqlCommand cmd = new SqlCommand(sql_str);
        cmd.Parameters.AddWithValue("@qset_id", qset_id);
        DataSet ds = DBHelper.getDataSet(cmd);
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    qset_text = ds.Tables[0].Rows[i]["qset_text"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
    public void loadQuestionL1()
    {
        string sql_str = @"
                            SELECT q1_id, q1_text, q1_order, q1_assumption, qset_id 
                            FROM question1 
                            WHERE (qset_id = @qset_id) ORDER BY q1_order
        ";
        SqlCommand cmd = new SqlCommand(sql_str);
        cmd.Parameters.AddWithValue("@qset_id",qset_id);
        DataSet ds = DBHelper.getDataSet(cmd);

        qL1_list = new List<question>();
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    question q1 = new question();
                    q1.q1_id = ds.Tables[0].Rows[i]["q1_id"].ToString();
                    q1.q1_text = ds.Tables[0].Rows[i]["q1_text"].ToString();
                    q1.q1_order = ds.Tables[0].Rows[i]["q1_order"].ToString();
                    q1.q1_assumption = ds.Tables[0].Rows[i]["q1_assumption"].ToString();
                    q1.qset_id = ds.Tables[0].Rows[i]["qset_id"].ToString();
                    qL1_list.Insert(i, q1);
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}