using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using ton.Data;

public partial class loveistheworld : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ton.tonUtilities.pageaAuthorize(ton.config.Global_config.authtext_admin, ton.config.Global_config.warning_text);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lbl_result.Text = "";
        DataSet ds = new DataSet();
        grd_temp.EmptyDataText = "ไม่มีข้อมูล";
        grd_temp.DataSource = null;
        grd_temp.DataBind();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = txt_com.Text.Trim();
        try
        {
            if (rad_cmd_type.SelectedValue == "0")
            {
         
                    ds = DBHelper.getDataSet(cmd);
                    grd_temp.DataSource = ds;
                    grd_temp.DataBind();
       

            }
            else if (rad_cmd_type.SelectedValue == "1")
            {
                int result = 0;
                result = DBHelper.getAffectedData(cmd);
                lbl_result.Text = result.ToString();
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
        }


    }
}