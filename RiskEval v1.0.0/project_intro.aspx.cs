using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class project_intro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        string strProjectStatus = Request.QueryString["status"];

        Response.Redirect("project_details.aspx?status=" + strProjectStatus);
    }
}