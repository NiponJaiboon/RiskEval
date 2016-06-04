using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ton;
using System.Data.SqlClient;
using riskEval;

public partial class project_details_edit : System.Web.UI.Page
{

    private List<SqlParameter> insertParameters = new List<SqlParameter>();

    protected void Page_Load(object sender, EventArgs e)
    {

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null)
        {

            string strDeptID = ck.d_code;

            //Redirect Login users to pending page
            //redirectToPendingPage(); 

            //string strDeptID = "01001";
            string strProjectStatus = ck.pj_status;

            //keep project code in cookie for usage in follwing pages
            mgCookie.UpdateCookies("pj_status", strProjectStatus);

            string strSQL1 = "select * from dbo.department d, dbo.ministry m where d.mi_id = m.mi_id and d.d_code = '" + strDeptID + "'";

            SqlDataSource4.SelectCommand = strSQL1;
            SqlDataSource4.DataBind();

            DataView dv1 = (DataView)SqlDataSource4.Select(DataSourceSelectArguments.Empty);

            foreach (DataRow dRow in dv1.Table.Rows)
            {

                lblDeptCode.Text = dRow["mi_code"].ToString();
                lblDeptName.Text = dRow["mi_name"].ToString();
                lblDivisionCode.Text = dRow["d_code"].ToString();
                lblDivisionName.Text = dRow["d_name"].ToString();

                mgCookie.UpdateCookies("mi_id", dRow["mi_id"].ToString());


            }


            if (!Page.IsPostBack)
            {


                string strSQL = "SELECT * from dbo.projects where pj_id = " + ck.pj_id;

                SqlDataSource2.SelectCommand = strSQL;
                SqlDataSource2.DataBind();

                DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

                lblProjectCode.Text = dv.Table.Rows[0]["pj_code"].ToString();
                txtProjectName.Text = dv.Table.Rows[0]["pj_name"].ToString();
                ddlYudtasad.SelectedValue = dv.Table.Rows[0]["pj_yut_id"].ToString();
                ddlYear.SelectedValue = dv.Table.Rows[0]["pj_year"].ToString();
                txtBudget.Text = dv.Table.Rows[0]["pj_budget"].ToString();
                
                DropDownList2.SelectedValue = dv.Table.Rows[0]["pj_budget_type"].ToString();
                //txtIntegrate.Text = dv.Table.Rows[0]["pj_integrateproject"].ToString();
                //txtRelate.Text = dv.Table.Rows[0]["pj_relateDept"].ToString();

            }
        }
    }








    protected void btnNext_Click(object sender, EventArgs e)
    {

        //save project code to cookie
        if (!Page.IsValid)
        {
            return;

        }
        else
        {
            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();

            // update to database
       
            SqlParameter p_id = new SqlParameter("@p_id", SqlDbType.Int);
            p_id.Direction = ParameterDirection.Input;
            p_id.Value = ck.p_id;

            SqlParameter d_id = new SqlParameter("@d_id", SqlDbType.NVarChar, 20);
            d_id.Direction = ParameterDirection.Input;
            d_id.Value = ck.d_code;

            SqlParameter mi_id = new SqlParameter("@mi_id", SqlDbType.NVarChar, 25);
            mi_id.Direction = ParameterDirection.Input;
            mi_id.Value = ck.mi_id;

            SqlParameter pj_name = new SqlParameter("@pj_name", SqlDbType.NVarChar, 500);
            pj_name.Direction = ParameterDirection.Input;
            pj_name.Value = txtProjectName.Text;

            SqlParameter pj_yut_id = new SqlParameter("@pj_yut_id", SqlDbType.Int);
            pj_yut_id.Direction = ParameterDirection.Input;
            pj_yut_id.Value = ddlYudtasad.SelectedValue;

            SqlParameter pj_year = new SqlParameter("@pj_year", SqlDbType.NVarChar, 4);
            pj_year.Direction = ParameterDirection.Input;
            pj_year.Value = ddlYear.SelectedValue;

            SqlParameter pj_budget = new SqlParameter("@pj_budget", SqlDbType.NVarChar, 50);
            pj_budget.Direction = ParameterDirection.Input;
            pj_budget.Value = txtBudget.Text;

            SqlParameter pj_budget_money = new SqlParameter("@pj_budget_money", SqlDbType.Float);
            pj_budget_money.Direction = ParameterDirection.Input;
            pj_budget_money.Value = txtBudget.Text.Replace(",", "");

            SqlParameter pj_budget_category = new SqlParameter("@pj_budget_category", SqlDbType.NVarChar, 200);
            pj_budget_category.Direction = ParameterDirection.Input;
            pj_budget_category.Value = DropDownList2.SelectedValue;

            SqlParameter pj_id = new SqlParameter("@pj_id", SqlDbType.Int);
            pj_id.Direction = ParameterDirection.Input;
            pj_id.Value = ck.pj_id;

            //SqlParameter pj_integrateProject = new SqlParameter("@pj_integrateProject", SqlDbType.NVarChar);
            //pj_integrateProject.Direction = ParameterDirection.Input;
            //pj_integrateProject.Value = txtIntegrate.Text;

            //SqlParameter pj_relateDept = new SqlParameter("@pj_relateDept", SqlDbType.NVarChar);
            //pj_relateDept.Direction = ParameterDirection.Input;
            //pj_relateDept.Value = txtRelate.Text;

            insertParameters.Add(p_id);
            insertParameters.Add(d_id);
            insertParameters.Add(mi_id);
            insertParameters.Add(pj_name);
            insertParameters.Add(pj_yut_id);
            insertParameters.Add(pj_year);
            insertParameters.Add(pj_budget);
            insertParameters.Add(pj_budget_money);
            insertParameters.Add(pj_id);
            insertParameters.Add(pj_budget_category);

            //insertParameters.Add(pj_integrateProject);
            //insertParameters.Add(pj_relateDept);

            try
            {
                SqlDataSource44.Update();
               //keep project code in cookie for usage in follwing pages  
                mgCookie.UpdateCookies("pj_code", lblProjectCode.Text);
            }
            catch(Exception ex)
            {
                //ELMA Log
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            //Response.Redirect("project_filter_edit.aspx");
            Response.Redirect("project_edit.aspx");
        }

    }


    protected void SqlDataSource3_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {

        e.Command.Parameters.Clear();

        foreach (SqlParameter p in insertParameters)
            e.Command.Parameters.Add(p);
    }

    protected void SqlDataSource44_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {

        e.Command.Parameters.Clear();

        foreach (SqlParameter p in insertParameters)
            e.Command.Parameters.Add(p);
    }

    protected void redirectToPendingPage()
    {

        string strSQL2 = "SELECT count(*) from dbo.projects where d_id = '" + lblDivisionCode.Text + "' and pj_code = '" + lblProjectCode.Text + "' and pj_status = '" + Request.QueryString["status"] + "'";

        SqlDataSource5.SelectCommand = strSQL2;
        SqlDataSource5.DataBind();

        DataView dv2 = (DataView)SqlDataSource5.Select(DataSourceSelectArguments.Empty);

        foreach (DataRow dRow in dv2.Table.Rows)
        {

            if (dRow["pj_name"] != null && dRow["pj_yut_id"] != null && dRow["pj_relateDept"] != null
                && dRow["pj_year"] != null && dRow["pj_budget"] != null && dRow["pj_integrateProject"] != null)
            {

                if (dRow["pj_filter_q1"] == null || dRow["pj_filter_q2"] == null)
                {
                    Response.Redirect("project_filter.aspx");
                }
                else if (dRow["pj_background"] == null || dRow["pj_urgency"] == null
                    || dRow["pj_risk_info"] == null || dRow["pj_risk_reduction1"] == null
                    || dRow["pj_risk_reduction2"] == null || dRow["pj_risk_eval1"] == null
                    || dRow["pj_risk_eval2"] == null || dRow["pj_risk_eval3"] == null)
                {
                    Response.Redirect("project_basicinfo.aspx");
                }
                else if (dRow["pj_category"] == null)
                {
                    Response.Redirect("project_category.aspx");
                }
                else if (dRow["pj_type"] == null)
                {
                    Response.Redirect("project_type.aspx");
                }
                else
                {
                    Response.Redirect("project_pickquestion.aspx");
                }

            }
            else
            {

                //populate data to each fields on this page
                if (dRow["pj_name"] != null)
                {
                    txtProjectName.Text = dRow["pj_name"].ToString();
                }
                if (dRow["pj_budget"] != null)
                {
                    txtBudget.Text = dRow["pj_budget"].ToString();
                }
                if (dRow["pj_integrateProject"] != null)
                {
                    txtIntegrate.Text = dRow["pj_integrateProject"].ToString();
                }
                if (dRow["pj_relateDept"] != null)
                {
                    txtRelate.Text = dRow["pj_relateDept"].ToString();
                }
                if (dRow["pj_year"] != null)
                {
                    ddlYear.SelectedValue = dRow["pj_year"].ToString();
                }
                if (dRow["pj_yut_id"] != null)
                {
                    ddlYudtasad.SelectedIndex = Convert.ToInt32(dRow["pj_yut_id"].ToString());
                }



            }



        }

    }
}