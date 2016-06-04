using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ton;
using riskEval;
using System.Data;
using ton.config;

public partial class project_approval_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ton.tonUtilities.pageaAuthorize(ton.config.Global_config.authtext_budgetor, ton.config.Global_config.warning_text);   

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null)
        {
            // option 1 Call from Cookies
            sds_project_summary.SelectParameters["pj_id"].DefaultValue = ck.pj_id;
            SqlDataSource1.SelectParameters["pj_id"].DefaultValue = ck.pj_id;
            SqlDataSource2.SelectParameters["pj_id"].DefaultValue = ck.pj_id;
            SqlDataSource3.SelectParameters["pj_id"].DefaultValue = ck.pj_id;
            SqlDataSource4.SelectParameters["pj_id"].DefaultValue = ck.pj_id;
            SqlDataSource5.SelectParameters["pj_id"].DefaultValue = ck.pj_id;
            sds_factor_tamma.SelectParameters["pj_id"].DefaultValue = ck.pj_id;
            sds_tamma_no_proceed.SelectParameters["pj_id"].DefaultValue = ck.pj_id;


            //// option 2 Call from Query String
            //// Option has problem with 128bit Encryption which contain '+' but URL.decode will replace '+' as ' '(space). 
            //// The decoding process error will occur
            //// Solution: replace with 64 bit Enryption
            //string pj_id = Encryption.Decrypt(Request.QueryString["id"], ton.Encryption.keyword);
            //pj_id = tonUtilities.cleanQueryString(pj_id);
            //sds_project_summary.SelectParameters["pj_id"].DefaultValue = pj_id;
            //// End Option2

            // Copy from ProjectSummary.aspx which created by Narut

            DataView dv1 = (DataView)sds_project_summary.Select(DataSourceSelectArguments.Empty);

            foreach (DataRow dRow in dv1.Table.Rows)
            {
                lblDeptCode.Text = dRow["mi_code"].ToString();
                lblDeptName.Text = dRow["mi_name"].ToString();
                lblDivisionCode.Text = dRow["d_code"].ToString();
                lblDivisionName.Text = dRow["d_name"].ToString();
                lblProjectName.Text = dRow["pj_name"].ToString();
                lblProjectCode.Text = dRow["pj_code"].ToString();
                lblYutasard.Text = dRow["yut_name"].ToString();
                lblIntegrateProject.Text = dRow["pj_integrateProject"].ToString();
                lblRelateDept.Text = dRow["pj_relateDept"].ToString();
                lblBudget.Text = dRow["pj_budget"].ToString();
                lblYear.Text = dRow["pj_year"].ToString();
                lit_approval.Text = dRow["pj_approval_status"].ToString();
                lbl_pj_doc_no.Text = dRow["pj_doc_number"].ToString();
                lbl_pj_date_doc_submitted.Text = dRow["pj_date_doc_submitted"].ToString().Substring(0, dRow["pj_date_doc_submitted"].ToString().IndexOf(" "));
            }

            // Show result in case its has value
            if ((lit_approval.Text == Global_config.pj_approval_status_value[1]) || string.IsNullOrEmpty(lit_approval.Text))
            {
                tbl_appr.Visible = false;
                btn_goto_step3.Enabled = true;
            }
            else
            {
                tbl_appr.Visible = true;
                btn_goto_step3.Enabled = false;
            }

            gUtilities gt = new gUtilities();
            litRisk1.Text = gt.getReportTammaTotal(ck.pj_id);
            litRisk2.Text = gt.getReportFactorRiskTotal(ck.pj_id);
            litRisk12.Text = gt.getReportTammaMainTotal(ck.pj_id);
            litRisk13.Text = gt.getReportTammaSubTotal(ck.pj_id);

        }

    }

    protected void btn_goto_step3_Click(object sender, EventArgs e)
    {
        Response.Redirect(Global_config.RootURL + "project_approval_3.aspx");
    }
    public double full100(double tt)
    {
        if(tt >= 99.0)
        {
            tt = 100;
        }
        if(tt > 100)
        {
            tt = 100;
        }
        return tt;
    }
    public int getColumnIndexFromSortExpression(GridView grd, String key)
    {
        for (int i = 0; i < grd.Columns.Count; i++)
        {
            if (grd.Columns[i].SortExpression == key)
            {
                return i;
            }
        }
        return -1;
    }

    public double t1 = 0.00;
    public int ty = 0;
    public int tn = 0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView grd_tmp = sender as GridView;
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            t1 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "yes_percent"));
            ty += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "answerY"));
            tn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "answerN"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "รวม";
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "yes_percent")].Text = full100(t1).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp,"answerY")].Text = full100(ty).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp,"answerN")].Text = full100(tn).ToString();            
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            ty = 0;
            tn = 0;
        }
    }
    public double t2 = 0.00;

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView grd_tmp = sender as GridView;
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            t2 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "yes_percent"));
            ty += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "answerY"));
            tn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "answerN"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "รวม";
            e.Row.Cells[3].Text = full100(t2).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "answerY")].Text = full100(ty).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "answerN")].Text = full100(tn).ToString();  
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            ty = 0;
            tn = 0;
        }
    }
    public double t3 = 0.00;
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView grd_tmp = sender as GridView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            t3 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "yes_percent"));
            ty += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "answerY"));
            tn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "answerN"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "รวม";
            e.Row.Cells[3].Text = full100(t3).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "answerY")].Text = full100(ty).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "answerN")].Text = full100(tn).ToString();  
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            ty = 0;
            tn = 0;
        }
    }
    public double t4 = 0.00;
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView grd_tmp = sender as GridView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            t4 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "yes_percent"));
            ty += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "answerY"));
            tn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "answerN"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "รวม";
            e.Row.Cells[3].Text = full100(t4).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "answerY")].Text = full100(ty).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "answerN")].Text = full100(tn).ToString();  
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            ty = 0;
            tn = 0;
        }

    }
    public double t5 = 0.00;
    public int ts = 0;
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView grd_tmp = sender as GridView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            t5 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "y_percent_total"));
            ty += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "y_total"));
            tn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "y_main"));
            ts += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "y_sub"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "รวม";
            e.Row.Cells[2].Text = full100(t5).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "y_total")].Text = full100(ty).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "y_main")].Text = full100(tn).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "y_sub")].Text = full100(ts).ToString();  
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            ty = 0;
            tn = 0;
            ts = 0;
        }
    }
    public double t6 = 0.00;
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView grd_tmp = sender as GridView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            t6 += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rf_percent"));
            ty += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "rf_proceed"));
            tn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "rf_not_proceed"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "รวม";
            e.Row.Cells[1].Text = full100(t6).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "rf_proceed")].Text = full100(ty).ToString();
            e.Row.Cells[getColumnIndexFromSortExpression(grd_tmp, "rf_not_proceed")].Text = full100(tn).ToString();  
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            ty = 0;
            tn = 0;
        }
    }
}