using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using ton;
using System.Data.SqlClient;
using riskEval;


public partial class project_basicinfo : System.Web.UI.Page
{
    private List<SqlParameter> insertParameters = new List<SqlParameter>();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null)
        {

        String strPJid = ck.pj_id;

        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select d.d_code, d.d_name, m.mi_code, m.mi_name, p.pj_code, p.d_id, p.pj_name, y.yut_name, p.pj_year, p.pj_budget, pj_integrateProject, pj_relateDept, pj_background, pj_urgency, pj_risk_info, pj_risk_reduction1, pj_risk_reduction2, pj_risk_eval1, pj_risk_eval2, pj_risk_eval3");
        strSQL.Append(" from projects p, department d, ministry m, yutasad y");
        strSQL.Append(" where p.d_id = d.d_id and p.mi_id = m.mi_id and p.pj_yut_id = y.yut_id and p.pj_id = '" + strPJid+ "'");

        SqlDataSource4.SelectCommand = strSQL.ToString();
        SqlDataSource4.DataBind();

        DataView dv1 = (DataView)SqlDataSource4.Select(DataSourceSelectArguments.Empty);

        foreach (DataRow dRow in dv1.Table.Rows)
        {

            lblDeptCode.Text = dRow["mi_code"].ToString();
            lblDeptName.Text = dRow["mi_name"].ToString();
            lblDivisionCode.Text = dRow["d_code"].ToString();
            lblDivisionName.Text = dRow["d_name"].ToString();
            lblProjectName.Text = dRow["pj_name"].ToString();
            lblProjectCode.Text = dRow["pj_code"].ToString();
            lblYutasard.Text = dRow["yut_name"].ToString();
            //lblIntegrateProject.Text = dRow["pj_integrateProject"].ToString();
            //lblRelateDept.Text = dRow["pj_relateDept"].ToString();
            lblBudget.Text = dRow["pj_budget"].ToString();
            lblYear.Text = dRow["pj_year"].ToString();

            if (dRow["pj_background"].ToString() != string.Empty ||
                dRow["pj_urgency"].ToString() != string.Empty)
            {

                txtBackground.Text = dRow["pj_background"].ToString();
                txtUrgency.Text = dRow["pj_urgency"].ToString();

                //txtRiskInfo.Text = dRow["pj_risk_info"].ToString();
                //txtRiskReduction1.Text = dRow["pj_risk_reduction1"].ToString();
                //txtRiskReduction2.Text = dRow["pj_risk_reduction2"].ToString();
                //txtRiskEval1.Text = dRow["pj_risk_eval1"].ToString();
                //txtRiskEval2.Text = dRow["pj_risk_eval2"].ToString();
                //txtRiskEval3.Text = dRow["pj_risk_eval3"].ToString();

                txtBackground.Enabled = false;
                txtUrgency.Enabled = false;


                //txtRiskInfo.Enabled = false;
                //txtRiskReduction1.Enabled = false;
                //txtRiskReduction2.Enabled = false;
                //txtRiskEval1.Enabled = false;
                //txtRiskEval2.Enabled = false;
                //txtRiskEval3.Enabled = false;
            }
            else
            {
                txtBackground.Enabled = true;
                txtUrgency.Enabled = true;

                //txtRiskInfo.Enabled = true;
                //txtRiskReduction1.Enabled = true;
                //txtRiskReduction2.Enabled = true;
                //txtRiskEval1.Enabled = true;
                //txtRiskEval2.Enabled = true;
                //txtRiskEval3.Enabled = true;
            }

        }
        }
        else
        {
        //redirect to login page

        }

        //}
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {


        if (!Page.IsValid)
        {
            return;
        }
        else
        {

            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();

            SqlParameter pj_background = new SqlParameter("@pj_background", SqlDbType.NVarChar);
            pj_background.Direction = ParameterDirection.Input;
            pj_background.Value = txtBackground.Text;

            SqlParameter pj_urgency = new SqlParameter("@pj_urgency", SqlDbType.NVarChar);
            pj_urgency.Direction = ParameterDirection.Input;
            pj_urgency.Value = txtUrgency.Text;

            //SqlParameter pj_risk_info = new SqlParameter("@pj_risk_info", SqlDbType.NVarChar);
            //pj_risk_info.Direction = ParameterDirection.Input;
            //pj_risk_info.Value = txtRiskInfo.Text;

            //SqlParameter pj_risk_reduction1 = new SqlParameter("@pj_risk_reduction1", SqlDbType.NVarChar);
            //pj_risk_reduction1.Direction = ParameterDirection.Input;
            //pj_risk_reduction1.Value = txtRiskReduction1.Text;

            //SqlParameter pj_risk_reduction2 = new SqlParameter("@pj_risk_reduction2", SqlDbType.NVarChar);
            //pj_risk_reduction2.Direction = ParameterDirection.Input;
            //pj_risk_reduction2.Value = txtRiskReduction2.Text;

            //SqlParameter pj_eval1 = new SqlParameter("@pj_risk_eval1", SqlDbType.NVarChar);
            //pj_eval1.Direction = ParameterDirection.Input;
            //pj_eval1.Value = txtRiskEval1.Text;

            //SqlParameter pj_eval2 = new SqlParameter("@pj_risk_eval2", SqlDbType.NVarChar);
            //pj_eval2.Direction = ParameterDirection.Input;
            //pj_eval2.Value = txtRiskEval2.Text;

            //SqlParameter pj_eval3 = new SqlParameter("@pj_risk_eval3", SqlDbType.NVarChar);
            //pj_eval3.Direction = ParameterDirection.Input;
            //pj_eval3.Value = txtRiskEval3.Text;

            SqlParameter pj_id= new SqlParameter("@pj_id", SqlDbType.Int);
            pj_id.Direction = ParameterDirection.Input;
            pj_id.Value = ck.pj_id;

            insertParameters.Add(pj_background);
            insertParameters.Add(pj_urgency);

            //insertParameters.Add(pj_risk_info);
            //insertParameters.Add(pj_risk_reduction1);
            //insertParameters.Add(pj_risk_reduction2);
            //insertParameters.Add(pj_eval1);
            //insertParameters.Add(pj_eval2);
            //insertParameters.Add(pj_eval3);

            insertParameters.Add(pj_id);

            try
            {
                SqlDataSource1.Update();

            }
            catch
            {
                //ELMA Log

            }

            Response.Redirect("project_category.aspx");

        }

     
    }

    protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {

        e.Command.Parameters.Clear();

        foreach (SqlParameter p in insertParameters)
            e.Command.Parameters.Add(p);
    }

}