using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;
using ton;
using ton.config;

public partial class project_for_budgetor : System.Web.UI.Page
{
    public string get_pj_approval_text(string pj_approval_status)
    {
        int index;
        Int32.TryParse(pj_approval_status, out index);

        if (index >= Global_config.pj_approval_status_text.Count())
        {
            index = 0;
        }

        string result = Global_config.pj_approval_status_text[index];
        return result;
    }

    public string get_pj_approval_value(string pj_approval_status)
    {
        int index;
        Int32.TryParse(pj_approval_status, out index);

        if (index >= Global_config.pj_approval_status_value.Count())
        {
            index = 0;
        }

        string result = Global_config.pj_approval_status_value[index];
        return result;
    }

    public string getTitlebyApproveStatus(string pj_approval_status)
    {
        string result = "";
        if (pj_approval_status == Global_config.pj_approval_status_text[0])
        {
            result = "แสดงความคิดเห็นเพิ่มเติมจากเจ้าหน้าที่จัดทำงบประมาณ";
            pnl_filter_approve.Visible = false;
            show_selectedDepartment_project();
        }
        else if (pj_approval_status == Global_config.pj_approval_status_text[1])
        {
            result = "บันทึกผลการพิจารณาจากรัฐสภา";
            pnl_filter_approve.Visible = false;
            show_selectedDepartment_project();
        }
        else if ((pj_approval_status == Global_config.pj_approval_status_text[2]) || (pj_approval_status == Global_config.pj_approval_status_text[3]) || (pj_approval_status == Global_config.pj_approval_status_text[4]))
        {
            result = "โครงการที่ผ่านการพิจารณาจากรัฐสภา";
            pnl_filter_approve.Visible = true;
            show_approved_project();
            for (int i = 0; i < grd_project_list.Columns.Count; i++)
            {
                if ((grd_project_list.Columns[i].SortExpression == "pj_approval_status") || (grd_project_list.Columns[i].SortExpression == "pj_approval_budget"))
                {
                    grd_project_list.Columns[i].Visible = true;
                }
            }
        }
        else if ((pj_approval_status == Global_config.pj_approval_status_text[5]))
        {
            result = "ทั้งหมด";
            pnl_filter_approve.Visible = false;
            show_selectedDepartment_project();
        }
        else
        {
            result = "แสดงความคิดเห็นเพิ่มเติมจากเจ้าหน้าที่จัดทำงบประมาณ";
            pnl_filter_approve.Visible = false;
            show_selectedDepartment_project();
        }
        return result;
    }

    public void show_approved_project()
    {
                string appr_criteria = "";
        string appr_3criteria = string.Format("N'{0}',N'{1}',N'{2}'", Global_config.pj_approval_status_value[2], Global_config.pj_approval_status_value[3], Global_config.pj_approval_status_value[4]);
        List<string> app_crt = new List<string>();
        for (int i = 0; i < cbklist_approve.Items.Count; i++)
        {
            ListItem li_temp = cbklist_approve.Items[i];
            if (li_temp.Selected)
            {
                app_crt.Add("N'" + li_temp.Value + "'");
            }
        }
        appr_criteria = string.Join(",", app_crt.ToArray());

        if (!string.IsNullOrEmpty(appr_criteria))
        {
            sds_project_list.SelectParameters["approval_status"].DefaultValue = "%%";
            sds_project_list.SelectCommand = sds_project_list.SelectCommand.Insert(sds_project_list.SelectCommand.LastIndexOf("ORDER"), " AND (projects.pj_approval_status IN (" + appr_criteria + ") ) ");

            if (!Page.IsPostBack)
            {
                // list all department_id in projects which approved_status = 2,3,4
                sds_department.SelectParameters["approval_status"].DefaultValue = "%%";
                sds_department.SelectCommand = sds_department.SelectCommand.Insert(sds_department.SelectCommand.LastIndexOf("ORDER"), " AND (projects.pj_approval_status IN (" + appr_3criteria + ") ) ");
            }
        }
        else
        {
            // In case select nothing. let put dummy value which show Empty Data
            sds_project_list.SelectParameters["approval_status"].DefaultValue = "NULL";
        }

        sds_project_list.SelectParameters["d_id"].DefaultValue = ddl_department.SelectedValue;
        sds_project_list.SelectParameters["pj_year"].DefaultValue = ddlProjectYear.SelectedValue;

        sds_project_list.DataBind();
        grd_project_list.DataBind();
    }

    public void show_selectedDepartment_project()
    {
        sds_project_list.SelectParameters["d_id"].DefaultValue = ddl_department.SelectedValue;
        sds_project_list.SelectParameters["pj_year"].DefaultValue = ddlProjectYear.SelectedValue;

        sds_project_list.DataBind();
        grd_project_list.DataBind();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        string approval_status = tonUtilities.cleanQueryString(Request.QueryString["status"]);
        string approval_text = get_pj_approval_text(approval_status);
        string apporval_value = get_pj_approval_value(approval_status);

        getTitlebyApproveStatus(approval_text);
    }

    protected void ddl_department_DataBound(object sender, EventArgs e)
    {
    }

    protected void grd_project_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='gridHighlightRow'");
            // This will be the back ground color of the GridView Control
            if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.Attributes.Add("onmouseout", "this.className='gridAltRow'");
            else
                e.Row.Attributes.Add("onmouseout", "this.className='gridRow'");

            #region สูง กลาง ต่ำ

            bool show = false;
            string fl1 = DataBinder.Eval(e.Row.DataItem, "pj_filter_q1").ToString();
            string fl2 = DataBinder.Eval(e.Row.DataItem, "pj_filter_q2").ToString();
            string fl3 = DataBinder.Eval(e.Row.DataItem, "pj_filter_q3").ToString();
            string fl4 = DataBinder.Eval(e.Row.DataItem, "pj_filter_q4").ToString();

            if (fl4 == "ใช่")
            {
                show = true;
            }
            else if (fl4 == "ไม่ใช่")
            {
                if ((fl1 == "ใช่") && (fl2 == "ใช่") && (fl3 == "ใช่"))
                {
                    show = true;
                }
                else
                {
                    show = false;
                }
            }

            if (show)
            {
                Literal lit_temp = e.Row.FindControl("lit_yes_percent") as Literal;
                Double dbTotal;
                Double.TryParse(lit_temp.Text, out dbTotal);
                lit_temp.Text = ton.tonUtilities.getRiskLevel(dbTotal);
            }

            #endregion สูง กลาง ต่ำ
        }
    }

    protected void lbt_proj_name_Click(object sender, EventArgs e)
    {
        LinkButton lbt_temp = sender as LinkButton;

        //option 1 pass with cookies
        ManageCookie mgCookie = new ManageCookie();
        mgCookie.UpdateCookies("pj_id", lbt_temp.CommandArgument);

        // option 2 pass with QueryString. There are problem with '+' in url
        //Server.Transfer(Global_config.RootURL + "project_approval_1.aspx" + "?id=" + ton.Encryption.Encrypt(lbt_temp.CommandArgument, ton.Encryption.keyword));
        string approval_status = tonUtilities.cleanQueryString(Request.QueryString["status"]);
        string prefix = "1";
        if (approval_status == "1")
        {
            prefix = "4";
        }
        else if ((approval_status == "2") || (approval_status == "3") || (approval_status == "4"))
        {
            prefix = "5";
        }
        else
        {
            prefix = "1";
        }
        Response.Redirect(Global_config.RootURL + "project_approval_" + prefix + ".aspx" + "?id=" + ton.Encryption.Encrypt(lbt_temp.CommandArgument, ton.Encryption.keyword));

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        {
            tonUtilities.pageaAuthorize(ton.config.Global_config.authtext_23, Global_config.warning_text);

            string approval_status = tonUtilities.cleanQueryString(Request.QueryString["status"]);
            string approval_text = get_pj_approval_text(approval_status);
            string apporval_value = get_pj_approval_value(approval_status);
            var mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();

            if (!Page.IsPostBack)
            {
                #region Generated checkboxlist approve

                for (int i = 2; i < Global_config.pj_approval_status_text.Count() - 1; i++)
                {
                    //2 ผ่านจากสภา , 3 ไม่ผ่านจากสภา , 4 ไม่ผ่านจากสำนักงบ, 5 ทั้งหมด
                    cbklist_approve.Items.Add(new ListItem(Global_config.pj_approval_status_text[i], Global_config.pj_approval_status_value[i]));
                }
                cbklist_approve.DataBind();
                for (int i = 0; i < cbklist_approve.Items.Count; i++)
                {
                    cbklist_approve.Items[i].Selected = true;
                }

                #endregion Generated checkboxlist approve
            }

            if (ck != null)
            {
                if (ck.p_role_id != "2")
                {
                    //ton.JavaScript.MessageBox("หน้านี้อนุญาติเฉพาะเจ้าหน้าที่สำนักงบประมาณเท่านั้น", Global_config.RootURL);
                    //Response.Redirect(Global_config.RootURL);
                }

                sds_project_list.SelectParameters["p_id"].DefaultValue = ck.p_id;
                sds_project_list.SelectParameters["approval_status"].DefaultValue = apporval_value;
                sds_project_list.SelectParameters["d_id"].DefaultValue = ddl_department.SelectedValue;

                sds_department.SelectParameters["p_id"].DefaultValue = sds_project_list.SelectParameters["p_id"].DefaultValue;
                sds_department.SelectParameters["approval_status"].DefaultValue = sds_project_list.SelectParameters["approval_status"].DefaultValue;
                ltr_contentTitle.Text = getTitlebyApproveStatus(approval_text);

                //DataView ds  = sds_department.Select(DataSourceSelectArguments.Empty) as DataView;
                if (string.IsNullOrEmpty(ddlProjectYear.SelectedValue))
                {
                    var dtView = (DataView)sds_project_list.Select(new DataSourceSelectArguments());

                    if (null == dtView)
                        return;

                    ddlProjectYear.DataSource = (from t in dtView.Table.AsEnumerable()
                                                 group t by t.Field<string>("pj_year") into g
                                                 orderby g.Key
                                                 select new
                                                 {
                                                     pj_year = g.Key,
                                                 }); ;
                    ddlProjectYear.DataBind();

                    ddlProjectYear.Items.Insert(0, new ListItem("[ทุกปีงบประมาณ]", ""));
                    ddlProjectYear.SelectedIndex = 0;
                }
            }
        }
    }
}