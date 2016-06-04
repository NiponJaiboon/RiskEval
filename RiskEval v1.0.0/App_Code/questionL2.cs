using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ton.Data;

/// <summary>
/// Summary description for questionL2
/// </summary>
public class questionL2
{
    public string q2_id = "";
    public string q2_text = "";
    public string q2_order ="";
    public string stratrisk_id = "";
    public string q1_id ="";
    public string tm_id ="";
    public List<questionL3> qL3_list = new List<questionL3>();
    public questionL2()
    {
    }
    public questionL2(string q2_id,string q2_text,string q2_order,string stratrisk_id,string q1_id,string tm_id)
    {
        this.q2_id = q2_id;
        this.q2_text = q2_text;
        this.q2_order = q2_order;
        this.stratrisk_id = stratrisk_id;
        this.q1_id = q1_id;
        this.tm_id = tm_id;
    }
    public void loadQuestionL3()
    {
        string sql_str = @"
                SELECT q3_id, q3_text, q3_order, q3_pj_cate, q2_id, tm_id
                FROM  question3
                WHERE (q2_id = @q2_id)
                ORDER BY q3_order
        ";
        SqlCommand cmd = new SqlCommand(sql_str);
        cmd.Parameters.AddWithValue("@q2_id", q2_id);
        DataSet ds = DBHelper.getDataSet(cmd);

        qL3_list = new List<questionL3>();
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    questionL3 q3 = new questionL3();
                    q3.q3_id = ds.Tables[0].Rows[i]["q3_id"].ToString();
                    q3.q3_order = ds.Tables[0].Rows[i]["q3_order"].ToString();
                    q3.q2_id = ds.Tables[0].Rows[i]["q2_id"].ToString();
                    q3.q3_pj_cate = ds.Tables[0].Rows[i]["q3_pj_cate"].ToString();
                    q3.q3_text = ds.Tables[0].Rows[i]["q3_text"].ToString();
                    q3.tm_id = ds.Tables[0].Rows[i]["tm_id"].ToString();
                    
                    qL3_list.Insert(i, q3);
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}