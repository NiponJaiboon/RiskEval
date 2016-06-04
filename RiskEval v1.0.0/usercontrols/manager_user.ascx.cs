using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ton.config;
using System.Data.SqlClient;

public partial class usercontrols_manager_user : System.Web.UI.UserControl
{
    /// <summary>
    /// Exteme Admin show Is_Delete Option
    /// </summary>
    private bool extreme_admin;
    public string gvUser_Header = "";
    public string gvUser_Footer = "";
    public void show_is_delete_option(bool show)
    {
        int index = -1;
        // find index of "p_is_delete" column
        for (int i = 0; i < gv_uers.Columns.Count; i++)
        {
            if (gv_uers.Columns[i].SortExpression.Equals("p_is_delete"))
            {
                index = i;
                break;
            }
        }
        if (show)
        {
            pnl_is_delete.Visible = true;
            gv_uers.Columns[index].Visible = true;
        }
        else
        {
            pnl_is_delete.Visible = false;
            gv_uers.Columns[index].Visible = false;
        }
    }
    private void update_user_manager(string funct)
    {
        string result_msg = "";
        if (funct == "activate")
        {
            result_msg = update_user(true, false, "เปิดใช้งาน", "เปิดใช้งานผู้ที่ถูกเลือก");
        }
        else if (funct == "deactivate")
        {
            result_msg = update_user(false, false, "ปิดใช้งาน", "ปิดใช้งานผู้ที่ถูกเลือก");
        }
        else if (funct == "delete")
        {
            result_msg = update_user(false, true, "ลบ", "ลบผู้ที่ถูกเลือก");
        }
        else if (funct == "unlock")
        {
            result_msg = unlock_online_user("ปลดล๊อก", "ปลดล๊อกผู้ที่ถูกเลือก");
        }

        else
        {
            result_msg = "มีข้อผิดพลาด ไม่พบคำสั่ง";
        }
        ton.JavaScript.MessageBox(result_msg);
    }
    private string unlock_online_user(string log_head, string log_success)
    {
        int affected_count = 0;
        int row_count = 0;
        string log = log_head + " บัตรประชาชน\r\n";
        List<bool> li = new List<bool>();
        for (int i = 0; i < gv_uers.Rows.Count; i++)
        {
            GridViewRow vr = gv_uers.Rows[i];
            CheckBox cbk_seq = (CheckBox)vr.FindControl("cbk_seq");

            if (cbk_seq.Checked)
            {
                string strsql = @" Update [Persons]
                                        Set p_is_online = 'false'  
                                        Where p_id = @p_id 
                                    ";
                SqlCommand cmd = new SqlCommand(strsql);
                cmd.Parameters.AddWithValue("@p_id", gv_uers.DataKeys[i].Value.ToString());
                int result = ton.Data.DBHelper.getAffectedData(cmd);
                if (result <= 0)
                {
                    result = 0;
                }
                affected_count += result;

                row_count++;
                log += "    - " + gv_uers.DataKeys[i].Value.ToString() + "/" + gv_uers.DataKeys[i].Values["p_idno"] + " \r\n ";
            }
        }
        gv_uers.DataBind();
        log += log_success + "สำเร็จ " + affected_count.ToString() + " / " + row_count.ToString() + " แถว";
        return log;
    }
    private string update_user(bool is_active, bool is_delete, string log_head, string log_success)
    {
        int affected_count = 0;
        int row_count = 0;
        string log = log_head + " บัตรประชาชน\r\n";
        for (int i = 0; i < gv_uers.Rows.Count; i++)
        {
            GridViewRow vr = gv_uers.Rows[i];
            CheckBox cbk_seq = (CheckBox)vr.FindControl("cbk_seq");

            if (cbk_seq.Checked)
            {
                sds_users.UpdateParameters["p_is_active"].DefaultValue = is_active.ToString();
                sds_users.UpdateParameters["p_is_delete"].DefaultValue = is_delete.ToString();
                sds_users.UpdateParameters["p_id"].DefaultValue = gv_uers.DataKeys[i].Value.ToString();
                affected_count += sds_users.Update();
                row_count++;
                log += "    - " + gv_uers.DataKeys[i].Value.ToString() + "/" + gv_uers.DataKeys[i].Values["p_idno"] + " \r\n ";
            }
            //Response.Write(string.Format(" i={0} val={1} active={2} <br /> ", i.ToString(), cbk_seq.Checked.ToString(), gv_uers.DataKeys[i].Value.ToString()));
        }
        log += log_success + "สำเร็จ " + affected_count.ToString() + " / " + row_count.ToString() + " แถว";
        return log;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //Show Deleted User Fileter
            extreme_admin = true;
            show_is_delete_option(extreme_admin);
            //--------------------------------------
            ddl_is_active.SelectedItem.Selected = false;
            ddl_is_delete.Items.FindByValue("0").Selected = true;



            gvUser_Header = gv_uers.HeaderRow.FindControl("cbk_seq_selectAll").ClientID;
            gvUser_Footer = gv_uers.FooterRow.FindControl("cbk_seq_selectAll").ClientID;
        }

    }
    protected void imgbtn_activate_Click(object sender, ImageClickEventArgs e)
    {
        update_user_manager("activate");
    }

    protected void imgbtn_save_Click(object sender, ImageClickEventArgs e)
    {
        //extreme_admin = true;
        //show_is_delete_option(extreme_admin);
    }
    protected void imgbtn_deactivate_off_Click(object sender, ImageClickEventArgs e)
    {
        update_user_manager("deactivate");
    }
    protected void imgbtn_del_Click(object sender, ImageClickEventArgs e)
    {
        update_user_manager("delete");
    }
    protected void gv_uers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            #region GridView Effect
            e.Row.Attributes.Add("onmouseover", "this.className='gridHighlightRow'");
            // This will be the back ground color of the GridView Control
            if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.Attributes.Add("onmouseout", "this.className='gridAltRow'");
            else
                e.Row.Attributes.Add("onmouseout", "this.className='gridRow'");
            #endregion

            CheckBox chk_seq = (CheckBox)e.Row.FindControl("cbk_seq");
            #region p_is_active
            Image img_is_active = (Image)e.Row.FindControl("img_is_active");
            bool is_active = (bool)DataBinder.Eval(e.Row.DataItem, "p_is_active");
            if (is_active)
            {
                img_is_active.ImageUrl = Global_config.RootURL + "images/icon/icon_active_on.gif";
            }
            else
            {
                img_is_active.ImageUrl = Global_config.RootURL + "images/icon/icon_active_off.gif";
            }
            #endregion
            #region p_is_delete

            Image img_is_delete = (Image)e.Row.FindControl("img_is_delete");
            bool is_delete = (bool)DataBinder.Eval(e.Row.DataItem, "p_is_delete");
            if (is_delete)
            {
                img_is_delete.ImageUrl = Global_config.RootURL + "images/icon/icon_del_on.gif";
                chk_seq.Visible = false;
            }
            else
            {
                img_is_delete.ImageUrl = Global_config.RootURL + "images/icon/icon_del_off.gif";
                chk_seq.Visible = true;
            }
            #endregion
            #region p_is_online
            Image img_is_online = (Image)e.Row.FindControl("img_is_online");
            bool is_online = (bool)DataBinder.Eval(e.Row.DataItem, "p_is_online");
            if (is_online)
            {
                img_is_online.ImageUrl = Global_config.RootURL + "images/icon/icon_is_online_on.gif";
            }
            else
            {
                img_is_online.ImageUrl = Global_config.RootURL + "images/icon/icon_is_online_off.gif";
            }
            #endregion


            GridView gv_temp = e.Row.FindControl("grd_users_detail") as GridView;
            Image img_temp = e.Row.FindControl("img_exp_users_detail") as Image;
            string sqlstr_users_detail = "SELECT department.d_code, department.d_name FROM persons_detail INNER JOIN department ON persons_detail.d_id = department.d_id WHERE (persons_detail.p_id = @p_id) AND (persons_detail.pdt_is_delete <> 1) ORDER BY department.d_code  ";
            SqlCommand cmd = new SqlCommand(sqlstr_users_detail);
            cmd.Parameters.AddWithValue("@p_id", DataBinder.Eval(e.Row.DataItem, "p_id").ToString());
            gv_temp.DataSource = ton.Data.DBHelper.getDataSet(cmd);
            gv_temp.DataBind();
            gv_temp.Attributes.Add("style", "display:none");
            img_temp.Attributes.Add("OnClick", "toggleUserDetail('" + gv_temp.ClientID + "');");

        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        gv_uers.PageIndex = 0;
        gv_uers.DataBind();
        //Response.Write(sds_users.SelectCommand.ToString());
    }
    protected void sds_users_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {

    }

    protected void sds_users_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
    }
    protected void imgbtn_unlock_off_Click(object sender, ImageClickEventArgs e)
    {
        update_user_manager("unlock");
    }
    protected void btn_search0_Click(object sender, EventArgs e)
    {
        Response.Redirect("project_report_adm.aspx");
    }

    protected void ddl_ministry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_department.Items.Clear();
        ddl_department.Items.Insert(0, new ListItem("กรุณาเลือก", "-1"));
    }
}