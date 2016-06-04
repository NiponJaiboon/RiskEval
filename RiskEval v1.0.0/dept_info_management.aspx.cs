using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using riskEval;
using System.Data;
using System.Data.SqlClient;
using ton;
using System.Configuration;
using System.Xml;

public partial class dept_info_management : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ddlMin.DataSource = SqlDataSource2;
            ddlMin.DataTextField = "mi_name";
            ddlMin.DataValueField = "mi_id";
            ddlMin.DataBind();

            ddlMin.Items.Insert(0, new ListItem("[กรุณาเลือก]", "[กรุณาเลือก]"));
            ddlMin.SelectedIndex = 0;



        }
        else
        {

            if (ddlMin.SelectedIndex != 0)
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.Append("select m.mi_code, m.mi_name, d.d_name, d_code, d.d_id as d_id, d.id, m.mi_id  from department d, ministry m ");
                strSQL.Append("where d.mi_id = m.mi_id and d.mi_id = " + ddlMin.SelectedValue);

                SqlDataSource4.SelectCommand = strSQL.ToString();
                SqlDataSource4.DataBind();

                GridView1.DataSource = SqlDataSource4;
                GridView1.DataBind();

            }
            else
            {

                StringBuilder strSQL = new StringBuilder();

                strSQL.Append("select m.mi_code, m.mi_name, d.d_name, d_code, d.d_id as d_id, d.id, m.mi_id  from department d, ministry m ");
                strSQL.Append("where d.mi_id = m.mi_id and d.mi_id = 0");

                SqlDataSource4.SelectCommand = strSQL.ToString();
                SqlDataSource4.DataBind();

                GridView1.DataSource = SqlDataSource4;
                GridView1.DataBind();

            }
        }

    }

    protected void ddlMin_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMin.SelectedIndex != 0)
        {
            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select m.mi_code, m.mi_name, d.d_name, d_code, d.d_id as d_id, d.id, m.mi_id  from department d, ministry m ");
            strSQL.Append("where d.mi_id = m.mi_id and d.mi_id = " + ddlMin.SelectedValue);

            SqlDataSource4.SelectCommand = strSQL.ToString();
            SqlDataSource4.DataBind();

            GridView1.DataSource = SqlDataSource4;
            GridView1.DataBind();

        }
        else
        {

            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select m.mi_code, m.mi_name, d.d_name, d_code, d.d_id as d_id, d.id, m.mi_id  from department d, ministry m ");
            strSQL.Append("where d.mi_id = m.mi_id and d.mi_id = 0");

            SqlDataSource4.SelectCommand = strSQL.ToString();
            SqlDataSource4.DataBind();

            GridView1.DataSource = SqlDataSource4;
            GridView1.DataBind();

        }
    }

    protected void GridView1_RowDataBound(object sender,
                        GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
           

        //}
    }

    protected void GridView1_RowCommand(object sender,
                         GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            lblResult.Text = string.Empty;

            int id = Convert.ToInt32(e.CommandArgument);
            hdddeptid.Value = id.ToString();

            SqlConnection conn = DBConnection();

            string strsql = string.Format(@"                 
                            select * from department
                            where id = " + id);

            SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDName.Text = ds.Tables[0].Rows[0]["d_name"].ToString();
                txtDCode.Text = ds.Tables[0].Rows[0]["d_code"].ToString();
            }

            DBConnection().Close();

        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int pj_id = (int)GridView1.DataKeys[e.RowIndex].Value;
    }

    protected void UpdateRecordByID(string d_id)
    {

        int retIdentity = 0;
        try
        {

            SqlConnection conn = DBConnection();
            string strsql1 = string.Format(@"                 
                            select * from department where (d_code = " + txtDCode.Text + " or d_name = '" + txtDName.Text + "') and id != " + d_id);

            SqlDataAdapter da = new SqlDataAdapter(strsql1, conn);
            DataSet ds1 = new DataSet();
            da.Fill(ds1);


//            string strsql2 = string.Format(@"                 
//                            select * from department where (d_code = " + txtDCode.Text + " and d_name = '" + txtDName.Text + "') and id != " + d_id);
//            SqlDataAdapter da1 = new SqlDataAdapter(strsql2, conn);
//            DataSet ds2 = new DataSet();
//            da1.Fill(ds2);

        
            //if (ds2.Tables[0].Rows.Count > 0)
            //{
            //    lblResult.Text = "บันทึกการแก้ไขสำเร็จ";
            //}
            //else 
                
                if (ds1.Tables[0].Rows.Count > 0)
            {
                //รหัสหน่วยงานซ้ำ หรือ  ชื่อหน่วยงานซ้ำ  ไม่สามาถ update ได้
                lblResult.Text = "รหัสหน่วยงาน หรือ ชื่อหน่วยงาน นี้ถูกใช้โดยหน่วยงานอื่นแล้ว ไม่สามารถบันทึกได้";
            }
            else
            {
                string strsql = string.Format(@"
                            update department set d_code = @d_code, d_name = @d_name
                            where id = @id");
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", hdddeptid.Value);
                cmd.Parameters.AddWithValue("@d_code", txtDCode.Text);
                cmd.Parameters.AddWithValue("@d_name", txtDName.Text);
                retIdentity = cmd.ExecuteNonQuery();

            }

          
        }
        catch (Exception ex)
        {
            lblResult.Text = "ไม่สามารถบันทึกได้ กรุณาลองใหม่อีกครั้ง";
            //lblResult.Text = ex.Message;
        }
        finally
        {
            DBConnection().Close();
        }


        if (retIdentity > 0)
        {

            lblResult.Text = "บันทึกการแก้ไขสำเร็จ";

            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select m.mi_code, m.mi_name, d.d_name, d_code, d.d_id as d_id, d.id, m.mi_id  from department d, ministry m ");
            strSQL.Append("where d.mi_id = m.mi_id and d.mi_id = " + ddlMin.SelectedValue);

            SqlDataSource4.SelectCommand = strSQL.ToString();
            SqlDataSource4.DataBind();

            GridView1.DataSource = SqlDataSource4;
            GridView1.DataBind();

        }
    }

    protected void AddRecord()
    {

        int retIdentity = 0;
        try
        {

            SqlConnection conn = DBConnection();
            string strsql1 =
                string.Format(
                    @"                 
                            select * from department where d_code = " +
                    txtDCode.Text + " or d_name = '" + txtDName.Text + "'");

            SqlDataAdapter da = new SqlDataAdapter(strsql1, conn);
            DataSet ds1 = new DataSet();
            da.Fill(ds1);
            //DBConnection().Close();

            if (ds1.Tables[0].Rows.Count > 0)
            {
                //code ซ้ำ ไม่สามาถ update ได้
                lblResult.Text = "รหัสหน่วยงาน หรือ ชื่อหน่วยงาน นี้ถูกใช้โดยหน่วยงานอื่นแล้ว ไม่สามารถบันทึกได้";

            }
            else
            {

                conn = DBConnection();
                string strsql =
                    string.Format(
                        @"
                            insert into department (d_id, d_name, d_code, mi_id) values 
                            (@d_id, @d_name, @d_code, @mi_id)");

                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@d_id", hdddeptid.Value);
                cmd.Parameters.AddWithValue("@mi_id", ddlMin.SelectedValue);
                cmd.Parameters.AddWithValue("@d_code", txtDCode.Text);
                cmd.Parameters.AddWithValue("@d_name", txtDName.Text);
                retIdentity = cmd.ExecuteNonQuery();

            }

        }
        catch (Exception ex)
        {
            lblResult.Text = "ไม่สามารถเพิ่มหน่วยงานใหม่ได้ กรุณาลองใหม่อีกครั้ง";
            //lblResult.Text = ex.Message;
        }
        finally
        {
            DBConnection().Close();
        }


        if (retIdentity > 0)
        {

            lblResult.Text = "เพิ่มหน่วยงานใหม่สำเร็จ";

            StringBuilder strSQL = new StringBuilder();

            strSQL.Append("select m.mi_code, m.mi_name, d.d_name, d_code, d.d_id as d_id, d.id, m.mi_id  from department d, ministry m ");
            strSQL.Append("where d.mi_id = m.mi_id and d.mi_id = " + ddlMin.SelectedValue);

            SqlDataSource4.SelectCommand = strSQL.ToString();
            SqlDataSource4.DataBind();

            GridView1.DataSource = SqlDataSource4;
            GridView1.DataBind();

        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = GridView1.DataSource as DataTable;

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

            GridView1.DataSource = dataView;
            GridView1.DataBind();
        }
    }

    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        UpdateRecordByID(hdddeptid.Value);

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddRecord();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblResult.Text = string.Empty;
        txtDCode.Text = string.Empty;
        txtDName.Text = string.Empty;

        ddlMin.SelectedIndex = 0;

        StringBuilder strSQL = new StringBuilder();

        strSQL.Append("select m.mi_code, m.mi_name, d.d_name, d_code, d.d_id as d_id, d.id, m.mi_id  from department d, ministry m ");
        strSQL.Append("where d.mi_id = m.mi_id and d.mi_id = 0");

        SqlDataSource4.SelectCommand = strSQL.ToString();
        SqlDataSource4.DataBind();

        GridView1.DataSource = SqlDataSource4;
        GridView1.DataBind();

    }

    public static string CnnString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["GovBudgetingConnectionString"].ConnectionString.ToString();
            // return ConfigurationManager.ConnectionStrings["GovBudgeting01ConnectionString"];

        }
    }

    public static SqlConnection DBConnection()
    {
        SqlConnection conn = new SqlConnection();
        //conn = new SqlConnection();

        if (conn.State == ConnectionState.Closed)
        {
            conn = new SqlConnection(CnnString);
            conn.Open();
        }

        return conn;

    }
}