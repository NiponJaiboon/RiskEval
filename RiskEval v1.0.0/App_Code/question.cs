using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ton.Data;

/// <summary>
/// Summary description for question
/// </summary>
public class question
{
    public string q1_id = "";
    public string q1_text = "";
    public string q1_order = "";
    public string q1_assumption = "";
    public string qset_id = "";
    public List<questionL2> qL2_list = new List<questionL2>();
    public question()
    {
    }
	public question(string q1_id,string q1_text,string q1_order,string q1_assumption,string qset_id )
	{
        this.q1_id = q1_id;
        this.q1_text = q1_text;
        this.q1_order = q1_order;
        this.q1_assumption = q1_assumption;
        this.qset_id = qset_id;
	}
    public void loadQuestionL2()
    {
        string sql_str = @"
                            SELECT q2_id, q2_text, q2_order, stratrisk_id, q1_id, tm_id 
                            FROM question2 
                            WHERE (q1_id = @q1_id) 
                            ORDER BY q2_order
        ";
        SqlCommand cmd = new SqlCommand(sql_str);
        cmd.Parameters.AddWithValue("@q1_id", q1_id);
        DataSet ds = DBHelper.getDataSet(cmd);

        qL2_list = new List<questionL2>();
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    questionL2 q2 = new questionL2();
                    q2.q2_id = ds.Tables[0].Rows[i]["q2_id"].ToString();
                    q2.q2_text = ds.Tables[0].Rows[i]["q2_text"].ToString();
                    q2.q2_order = ds.Tables[0].Rows[i]["q2_order"].ToString();
                    q2.stratrisk_id = ds.Tables[0].Rows[i]["stratrisk_id"].ToString();
                    q2.q1_id = ds.Tables[0].Rows[i]["q1_id"].ToString();
                    q2.tm_id = ds.Tables[0].Rows[i]["tm_id"].ToString();
                    qL2_list.Insert(i, q2);
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}