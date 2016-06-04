using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ton.Data;

/// <summary>
/// Summary description for factors_main
/// </summary>
public class factors_main
{
    public string fm_id;
    public string fm_factors_text;
    public string fm_order;
    public List<factors_sub> fs_list;
	public factors_main()
	{
	}
    public void loadFactorMain()
    {
        string str_sql = @" select fm_id,fm_factors_text,fm_order 
                            from factors_main 
                            where fm_id = @fm_id
                            order by fm_order 
                            ";
        SqlCommand cmd = new SqlCommand(str_sql);
        cmd.Parameters.AddWithValue("@fm_id", fm_id);

        DataSet ds = DBHelper.getDataSet(cmd);

        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    fm_id = ds.Tables[0].Rows[i]["fm_id"].ToString();
                    fm_factors_text = ds.Tables[0].Rows[i]["fm_factors_text"].ToString();
                    fm_order = ds.Tables[0].Rows[i]["fm_order"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        
    }
    public void loadFactorSub()
    {
        string str_sql = @" select fs_id,fs_factors_text,fs_order,fm_id 
                            from factors_sub 
                            where fm_id = @fm_id  
                            order by fs_order
                            ";
        SqlCommand cmd = new SqlCommand(str_sql);
        cmd.Parameters.AddWithValue("@fm_id", fm_id);

        DataSet ds = DBHelper.getDataSet(cmd);

        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                fs_list = new List<factors_sub>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    factors_sub fs = new factors_sub();
                    fs.fm_id = ds.Tables[0].Rows[i]["fm_id"].ToString();
                    fs.fs_factors_text = ds.Tables[0].Rows[i]["fs_factors_text"].ToString();
                    fs.fs_id = ds.Tables[0].Rows[i]["fs_id"].ToString();
                    fs.fs_order = ds.Tables[0].Rows[i]["fs_order"].ToString();

                    fs_list.Insert(i, fs);
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

    }
}