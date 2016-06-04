using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ton.Data;

/// <summary>
/// Summary description for factors
/// </summary>
public class factors
{
    public List<factors_main> factor_main_list;
	public factors()
	{
	}
    public void loadFactorMain()
    {
        factor_main_list = new List<factors_main>();
        string str_sql = @" select fm_id,fm_factors_text,fm_order 
                            from factors_main 
                            order by fm_order 
                            ";
        SqlCommand cmd = new SqlCommand(str_sql);

        DataSet ds = DBHelper.getDataSet(cmd);

        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    factors_main fm = new factors_main();
                    fm.fm_id = ds.Tables[0].Rows[i]["fm_id"].ToString();
                    fm.fm_factors_text = ds.Tables[0].Rows[i]["fm_factors_text"].ToString();
                    fm.fm_order = ds.Tables[0].Rows[i]["fm_order"].ToString();
                    factor_main_list.Insert(i, fm);
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        
    }
}