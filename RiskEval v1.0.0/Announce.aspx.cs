using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;
using System.Data;
using System.Data.SqlClient;

public partial class Announce : System.Web.UI.Page
{
    string strStatus = string.Empty;
    int announce_id;

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
            string title = txtTitle.Text.Trim();
            string desc = txtDesc.Text.Trim();
         //   int status = Convert.ToInt32(ddlStatus.SelectedValue);
            retVal = gUtilities.ManageAnnoucement((hdAnnounce_id.Value == "" ? 0 : Convert.ToInt32(hdAnnounce_id.Value)), title, desc, 1, hdSaveStatus.Value);
            bindData();
        }
    }

    private void ClearText()
    {
        txtDesc.Text = "";
        txtTitle.Text = "";
       // ddlStatus.SelectedIndex = 0;
        strStatus = "insert";
        btnSubmit.Text = "บันทึก";
        hdSaveStatus.Value = "insert";
        hdAnnounce_id.Value = "0";
    }

    private void bindData()
    {
        ClearText();
        DataSet ds = new DataSet();
        ds = gUtilities.GetData("select * from announce order by created_date desc;", "announce");
        if (ds != null)
        {
            gvAnnounce.DataSource = ds;
            gvAnnounce.DataBind();

        }
    }

    protected void gvAnnounce_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AffectedGridView("Delete", e.RowIndex);
    }
    protected void gvAnnounce_RowEditing(object sender, GridViewEditEventArgs e)
    {
        AffectedGridView("Edit", e.NewEditIndex);
    }

    protected void gvDistributor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AffectedGridView("Delete", e.RowIndex);
    }

    protected void gvAnnounce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAnnounce.PageIndex = e.NewPageIndex;
        bindData();
    }

    private void AffectedGridView(string CommandName, int iRow)
    {
        announce_id = Convert.ToInt32(this.gvAnnounce.DataKeys[iRow].Values[0].ToString());
        Label lblTitle = (Label)gvAnnounce.Rows[iRow].Cells[1].FindControl("lblTitle");
        Label lblDesc = (Label)gvAnnounce.Rows[iRow].Cells[2].FindControl("lblDesc");
       // HiddenField hd = (HiddenField)gvAnnounce.Rows[iRow].Cells[2].FindControl("hdStatus");

        SqlConnection conn = gUtilities.DBConnection();

        if (CommandName.Trim() == "Edit")
        {
            txtTitle.Text = lblDesc.Text;
            txtDesc.Text = lblDesc.Text;
            //ddlStatus.SelectedValue = hd.Value;
            hdAnnounce_id.Value = announce_id.ToString();
            hdSaveStatus.Value = "update";
        }
        if (CommandName.Trim() == "Delete")
        {

            SqlCommand cmd = new SqlCommand("delete from announce where announce_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", announce_id.ToString());

            cmd.ExecuteNonQuery();
            bindData();

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearText();

    }
}