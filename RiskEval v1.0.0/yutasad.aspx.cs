using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;
using System.Data;
using System.Data.SqlClient;

public partial class yutasad : System.Web.UI.Page
{
    string strStatus = string.Empty;
    int yut_id;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClearText();
            bindData();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string retVal = string.Empty;
            string desc = txtDesc.Text.Trim();
            retVal = gAdmin.ManageYutasad((hdYut_id.Value == "" ? 0 : Convert.ToInt32(hdYut_id.Value)), desc, 1, hdSaveStatus.Value);
            bindData();
        }
    }

    private void ClearText()
    {
        txtDesc.Text = "";
        // ddlStatus.SelectedIndex = 0;
        strStatus = "insert";
        btnSubmit.Text = "บันทึก";
        hdSaveStatus.Value = "insert";
        hdYut_id.Value = "0";
    }

    private void bindData()
    {
        ClearText();
        DataSet ds = new DataSet();
        ds = gUtilities.GetData("select * from yutasad where isActive=1;", "yutasad");
        if (ds != null)
        {
            gvYutasad.DataSource = ds;
            gvYutasad.DataBind();

        }
    }

    protected void gvYutasad_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AffectedGridView("Delete", e.RowIndex);
    }
    protected void gvYutasad_RowEditing(object sender, GridViewEditEventArgs e)
    {
        AffectedGridView("Edit", e.NewEditIndex);
    }

    protected void gvDistributor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AffectedGridView("Delete", e.RowIndex);
    }

    protected void gvYutasad_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvYutasad.PageIndex = e.NewPageIndex;
        bindData();
    }

    private void AffectedGridView(string CommandName, int iRow)
    {
        yut_id = Convert.ToInt32(this.gvYutasad.DataKeys[iRow].Values[0].ToString());
        Label lblid = (Label)gvYutasad.Rows[iRow].Cells[1].FindControl("lblid");
        Label lblTitle = (Label)gvYutasad.Rows[iRow].Cells[2].FindControl("lblTitle");

        SqlConnection conn = gUtilities.DBConnection();

        if (CommandName.Trim() == "Edit")
        {
            txtDesc.Text = lblTitle.Text;
            hdYut_id.Value = yut_id.ToString();
            hdSaveStatus.Value = "update";
        }
        if (CommandName.Trim() == "Delete")
        {
            SqlCommand cmd = new SqlCommand("update yutasad set isActive=0 where yut_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", yut_id.ToString());

            cmd.ExecuteNonQuery();
            bindData();

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearText();

    }
}