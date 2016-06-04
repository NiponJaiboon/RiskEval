using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ton.Data;

/// <summary>
/// Summary description for answer_factors_sub
/// </summary>
public class answer_factors_sub
{
    public string answer_factors_sub_id;
    public string af_opportunity;
    public string af_effect;
    public string af_impact;
    public string pj_id;
    public string fs_id;
    public string factors_sub_etc;
    public string description_more;
    // From Table Factors_Main_sub
    public string fm_id;
    public string fs_order;
    //----------------------------

	public answer_factors_sub()
	{
	}
    public int getTotalRow(string pj_id)
    {
        string sql_str = @"
                            SELECT	Count(*) 
                            FROM	answer_factors_sub
                            WHERE	pj_id = @pj_id
        ";
        SqlCommand cmd = new SqlCommand(sql_str);
        cmd.Parameters.AddWithValue("@pj_id", pj_id);
        string str_result = DBHelper.getScalarData(cmd);
        int result = Convert.ToInt32(str_result);
        return result;
    }
    public int getMax_fm_id(string pj_id)
    {
        string sql_str = @"
                            SELECT MAX(factors_sub.fm_id) AS [Max_fm_id]
                            FROM  answer_factors_sub INNER JOIN
                                           factors_sub ON answer_factors_sub.fs_id = factors_sub.fs_id
                            GROUP BY answer_factors_sub.pj_id
                            HAVING (answer_factors_sub.pj_id = @pj_id)
        ";
        SqlCommand cmd = new SqlCommand(sql_str);
        cmd.Parameters.AddWithValue("@pj_id", pj_id);
        string str_result = DBHelper.getScalarData(cmd);
        int result = Convert.ToInt32(str_result);
        return result;
    }
    public List<answer_factors_sub> load_answer_factors_sub_id(string pj_id)
    {
        string sql_str = @"
                SELECT  answer_factors_sub.answer_factors_sub_id, answer_factors_sub.af_opportunity, 
                        answer_factors_sub.af_effect, answer_factors_sub.af_impact,answer_factors_sub.pj_id, 
                        answer_factors_sub.fs_id,answer_factors_sub.factors_sub_etc,answer_factors_sub.description_more, factors_sub.fm_id, factors_sub.fs_order 
                FROM  answer_factors_sub INNER JOIN
                               factors_sub ON answer_factors_sub.fs_id = factors_sub.fs_id
                WHERE answer_factors_sub.pj_id = @pj_id 
                ORDER BY factors_sub.fs_order
            
        ";
        SqlCommand cmd = new SqlCommand(sql_str);
        cmd.Parameters.AddWithValue("@pj_id", pj_id);
        //cmd.Parameters.AddWithValue("@fs_id", fs_id);
        
        // ----> ||
        List<answer_factors_sub> ans_fact_sub_list = new List<answer_factors_sub>();

        DataSet ds = DBHelper.getDataSet(cmd);
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    answer_factors_sub ans_fact_sub = new answer_factors_sub();
                    ans_fact_sub.answer_factors_sub_id = ds.Tables[0].Rows[i]["answer_factors_sub_id"].ToString(); 
                    ans_fact_sub.af_opportunity = ds.Tables[0].Rows[i]["af_opportunity"].ToString();
                    ans_fact_sub.af_effect = ds.Tables[0].Rows[i]["af_effect"].ToString();
                    ans_fact_sub.af_impact = ds.Tables[0].Rows[i]["af_impact"].ToString(); 
                    ans_fact_sub.pj_id = ds.Tables[0].Rows[i]["pj_id"].ToString(); 
                    ans_fact_sub.fs_id = ds.Tables[0].Rows[i]["fs_id"].ToString(); 
                    ans_fact_sub.factors_sub_etc = ds.Tables[0].Rows[i]["factors_sub_etc"].ToString();
                    ans_fact_sub.description_more = ds.Tables[0].Rows[i]["description_more"].ToString();
                    ans_fact_sub.fm_id = ds.Tables[0].Rows[i]["fm_id"].ToString();
                    ans_fact_sub.fs_order = ds.Tables[0].Rows[i]["fs_order"].ToString(); 
                    ans_fact_sub_list.Insert(i, ans_fact_sub);
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return ans_fact_sub_list;
    }
    public void load_answer_factors_sub_id(string pj_id,string fs_id)
    {
        string sql_str = @"
                SELECT  answer_factors_sub.answer_factors_sub_id, answer_factors_sub.af_opportunity, 
                        answer_factors_sub.af_effect, answer_factors_sub.af_impact,answer_factors_sub.pj_id, 
                        answer_factors_sub.fs_id,answer_factors_sub.factors_sub_etc, answer_factors_sub.description_more
                FROM  answer_factors_sub 
                WHERE answer_factors_sub.pj_id = @pj_id and fs_id = @fs_id 
        ";
        SqlCommand cmd = new SqlCommand(sql_str);
        cmd.Parameters.AddWithValue("@pj_id", pj_id);
        cmd.Parameters.AddWithValue("@fs_id", fs_id);


        DataSet ds = DBHelper.getDataSet(cmd);
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    
                    answer_factors_sub_id = ds.Tables[0].Rows[i]["answer_factors_sub_id"].ToString();
                    af_opportunity = ds.Tables[0].Rows[i]["af_opportunity"].ToString();
                    af_effect = ds.Tables[0].Rows[i]["af_effect"].ToString();
                    af_impact = ds.Tables[0].Rows[i]["af_impact"].ToString();
                    pj_id = ds.Tables[0].Rows[i]["pj_id"].ToString();
                    fs_id = ds.Tables[0].Rows[i]["fs_id"].ToString();
                    factors_sub_etc = ds.Tables[0].Rows[i]["factors_sub_etc"].ToString();
                    description_more = ds.Tables[0].Rows[i]["description_more"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        
    }
    public int saveToTable()
    {
        string del_str_cmd = @"
                delete from answer_factors_sub
                where pj_id = @pj_id and fs_id = @fs_id
            ";
        SqlCommand cmd = new SqlCommand(del_str_cmd);
        cmd.Parameters.AddWithValue("@pj_id", pj_id);
        cmd.Parameters.AddWithValue("@fs_id", fs_id);
        //DBHelper.getAffectedData(cmd);

        string str_cmd = @" 
                    DELETE FROM answer_factors_sub
                    WHERE pj_id = @pj_id and fs_id = @fs_id

                    INSERT INTO [answer_factors_sub]
                    (   [af_opportunity]
                       ,[af_effect]
                       ,[af_impact]
                       ,[pj_id]
                       ,[fs_id]
                       ,[factors_sub_etc]
                       ,[description_more]
                    )
     VALUES
           (@oppo,@effect,@impact,@pj_id,@fs_id,@factors_sub_etc,@description_more)                            
        ";
        cmd = new SqlCommand(str_cmd);

        cmd.Parameters.AddWithValue("@oppo", af_opportunity);
        cmd.Parameters.AddWithValue("@effect", af_effect);
        cmd.Parameters.AddWithValue("@impact", af_impact);
        cmd.Parameters.AddWithValue("@pj_id", pj_id);
        cmd.Parameters.AddWithValue("@fs_id", fs_id);
        cmd.Parameters.AddWithValue("@factors_sub_etc", factors_sub_etc);
        cmd.Parameters.AddWithValue("@description_more", description_more);
        int result = DBHelper.getAffectedData(cmd);
        return result;
    }
}