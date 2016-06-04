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

public partial class project_category : System.Web.UI.Page
{

    private List<SqlParameter> insertParameters = new List<SqlParameter>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null)
        {

            String strPJid = ck.pj_id;
        //String strPJCode = "01001-100";

        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select d.d_code, d.d_name, m.mi_code, m.mi_name, p.pj_code, p.d_id, p.pj_name, y.yut_name, p.pj_year, p.pj_budget, pj_integrateProject, pj_relateDept, pj_background, pj_urgency, pj_category");
        strSQL.Append(" from projects p, department d, ministry m, yutasad y");
        strSQL.Append(" where p.d_id = d.d_id and p.mi_id = m.mi_id and p.pj_yut_id = y.yut_id and p.pj_id = " + strPJid);

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

            if (dRow["pj_category"].ToString() != string.Empty)
            {

                if (dRow["pj_category"].ToString() == "บริหารทั่วไป")
                {
                    radProjectCate.SelectedIndex = 0;
                }
                else if (dRow["pj_category"].ToString() == "บริหารสังคม")
                {
                    radProjectCate.SelectedIndex = 1;
                }
                 else if (dRow["pj_category"].ToString() == "เศรษฐกิจ")
                {
                    radProjectCate.SelectedIndex = 2;
                }
                
                radProjectCate.Enabled = false;
            }

        }
        }
         else
        {

        //redirect to login page

        }

        }
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

            SqlParameter pj_cate = new SqlParameter("@pj_category", SqlDbType.NVarChar, 100);
            pj_cate.Direction = ParameterDirection.Input;
            pj_cate.Value = radProjectCate.SelectedValue.ToString();

            SqlParameter pj_id = new SqlParameter("@pj_id", SqlDbType.Int);
            pj_id.Direction = ParameterDirection.Input;
            pj_id.Value = ck.pj_id;

            insertParameters.Add(pj_cate);
            insertParameters.Add(pj_id);

            try
            {
                SqlDataSource1.Update();

                Response.Redirect("project_type.aspx");

            }
            catch
            {
                //ELMA Log

            }


        }


    }



    protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {

        e.Command.Parameters.Clear();

        foreach (SqlParameter p in insertParameters)
            e.Command.Parameters.Add(p);
    }
}