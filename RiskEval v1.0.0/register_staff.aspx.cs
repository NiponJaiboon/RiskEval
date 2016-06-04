using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;
using ton.config;

public partial class register_staff : System.Web.UI.Page
{
    protected bool allowEditbyRole()
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        if (ck != null)
        {
            if (!string.IsNullOrEmpty(ck.p_role_id))
            {
                if (ck.p_role_id != "1")
                {
                    return true;
                }
                else
                {
                    Response.Redirect(Global_config.RootURL);
                }
            }
        }
        return false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (allowEditbyRole())
        {
            lit_title.Text = "แก้ไขทะเบียนผู้ใช้งาน(สำนักงบประมาณ)";
            lit_content_title.Text = lit_title.Text;
        }
    }
}