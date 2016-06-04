using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;
using System.Data;
using System.Data.SqlClient;

public partial class tammapiban : System.Web.UI.Page
{
    string strStatus = string.Empty;
    int tm_id;

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
            retVal = gAdmin.ManageTammapiban((hdtm_id.Value == "" ? 0 : Convert.ToInt32(hdtm_id.Value)), desc, 1, hdSaveStatus.Value);
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
        hdtm_id.Value = "0";
    }

    private void bindData()
    {
        ClearText();
        DataSet ds = new DataSet();
        ds = gUtilities.GetData("select * from tammapiban where isActive=1;", "tammapiban");
        if (ds != null)
        {
            gvTammapiban.DataSource = ds;
            gvTammapiban.DataBind();

        }
    }

    protected void gvTammapiban_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AffectedGridView("Delete", e.RowIndex);
    }
    protected void gvTammapiban_RowEditing(object sender, GridViewEditEventArgs e)
    {
        AffectedGridView("Edit", e.NewEditIndex);
    }

    protected void gvDistributor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AffectedGridView("Delete", e.RowIndex);
    }

    protected void gvTammapiban_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTammapiban.PageIndex = e.NewPageIndex;
        bindData();
    }

    private void AffectedGridView(string CommandName, int iRow)
    {
        tm_id = Convert.ToInt32(this.gvTammapiban.DataKeys[iRow].Values[0].ToString());
        Label lblid = (Label)gvTammapiban.Rows[iRow].Cells[1].FindControl("lblid");
        Label lblTitle = (Label)gvTammapiban.Rows[iRow].Cells[2].FindControl("lblTitle");

        SqlConnection conn = gUtilities.DBConnection();

        if (CommandName.Trim() == "Edit")
        {
            txtDesc.Text = lblTitle.Text;
            hdtm_id.Value = tm_id.ToString();
            hdSaveStatus.Value = "update";
        }
        if (CommandName.Trim() == "Delete")
        {
            SqlCommand cmd = new SqlCommand("update tammapiban set isActive=0 where tm_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", tm_id.ToString());

            cmd.ExecuteNonQuery();
            bindData();

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearText();

    }
}