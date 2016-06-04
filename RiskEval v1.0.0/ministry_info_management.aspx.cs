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


public partial class ministry_info_management : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //check if the user role <> 3, then redirect to login page.
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null)
        {

            if (ck.p_role_id != "3")
            {

                //Response.Redirect("default.aspx");

            }

        }


        if (!Page.IsPostBack)
        {
            GridView1.DataSource = SqlDataSource2;
            GridView1.DataBind();
        }


    }
    protected void GridView1_RowDataBound(object sender,
                         GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataBinder.Eval(e.Row.DataItem, "mi_id");

        }
    }

    protected void GridView1_RowCommand(object sender,
                         GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            lblResult.Text = string.Empty;

            int mi_id = Convert.ToInt32(e.CommandArgument);

            hddmiid.Value = mi_id.ToString();

            SqlConnection conn = DBConnection();

            string strsql = string.Format(@"                 
                            select * from ministry
                            where mi_id = " + mi_id);

            SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtMName.Text = ds.Tables[0].Rows[0]["mi_name"].ToString();
                txtMCode.Text = ds.Tables[0].Rows[0]["mi_code"].ToString();
            }

            DBConnection().Close();

        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int pj_id = (int)GridView1.DataKeys[e.RowIndex].Value;
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

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

    protected void UpdateRecordByID(string mi_id, string mi_code, string mi_name)
    {



        SqlConnection conn = DBConnection();

        string strsql = string.Format(@"                 
                            select * from ministry
                            where mi_code = '" + mi_code + "' and mi_id <> " + mi_id);

        SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
        DataSet ds1 = new DataSet();
        da.Fill(ds1);
        DBConnection().Close();

        if (ds1.Tables[0].Rows.Count > 0)
        {
           //code ซ้ำ ไม่สามาถ update ได้
            lblResult.Text = "รหัสกระทรวงนี้ถูกใช้โดยกระทรวงอื่นแล้ว ไม่สามารถบันทึกได้";

        }
        else
        {

            try
            {

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;

                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("update ministry ");
                strSQL.Append("set mi_code = @mi_code, ");
                strSQL.Append("mi_name = @mi_name ");
                strSQL.Append("where mi_id = @mi_id ");

                cmd.CommandText = strSQL.ToString();
                cmd.Parameters.AddWithValue("@mi_id", mi_id);
                cmd.Parameters.AddWithValue("@mi_code", mi_code);
                cmd.Parameters.AddWithValue("@mi_name", mi_name);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch
            {
                lblResult.Text = "บันทึกผิดพลาด กรุณาลองใหม่";
            }
           
                lblResult.Text = "บันทึกสำเร็จ";

                GridView1.DataSource = SqlDataSource2;
                GridView1.DataBind();


        }

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

        if (txtMName.Text != string.Empty && txtMCode.Text != string.Empty)
        {
            lblResult.Text = string.Empty;
            UpdateRecordByID(hddmiid.Value, txtMCode.Text, txtMName.Text);
        }
        else
        {
            lblResult.Text = "กรุณากรอกรหัสกระทรวง และชื่อกระทรวง";

        }

       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hddmiid.Value = string.Empty;
        txtMCode.Text = string.Empty;
        txtMName.Text = string.Empty;
        lblResult.Text = string.Empty;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtMName.Text != string.Empty && txtMCode.Text != string.Empty)
        {
            lblResult.Text = string.Empty;
            AddNewMinistry(txtMCode.Text, txtMName.Text);
            
        }
        else
        {
            lblResult.Text = "กรุณากรอกรหัสกระทรวง และชื่อกระทรวง";

        }
    }

    protected void AddNewMinistry(string mi_code, string mi_name)
    {

        SqlConnection conn = DBConnection();

        string strsql = string.Format(@"                 
                            select * from ministry
                            where mi_code = '" + mi_code + "'");

        SqlDataAdapter da = new SqlDataAdapter(strsql, conn);
        DataSet ds1 = new DataSet();
        da.Fill(ds1);
        DBConnection().Close();

        if (ds1.Tables[0].Rows.Count > 0)
        {
            //code ซ้ำ ไม่สามาถ update ได้
            lblResult.Text = "รหัสกระทรวงนี้ถูกใช้โดยกระทรวงอื่นแล้ว ไม่สามารถบันทึกได้";

        }
        else
        {

            try
            {

                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;

                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("insert into ministry (mi_code, mi_name) values ");
                strSQL.Append(" (@mi_code, @mi_name) ");
                cmd.CommandText = strSQL.ToString();
                cmd.Parameters.AddWithValue("@mi_code", mi_code);
                cmd.Parameters.AddWithValue("@mi_name", mi_name);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch
            {
                lblResult.Text = "บันทึกผิดพลาด กรุณาลองใหม่";
            }

            lblResult.Text = "บันทึกสำเร็จ";

            GridView1.DataSource = SqlDataSource2;
            GridView1.DataBind();


        }

    }
}