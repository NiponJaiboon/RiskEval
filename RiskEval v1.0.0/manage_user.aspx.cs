using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;

public partial class manage_user : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ton.tonUtilities.pageaAuthorize(ton.config.Global_config.authtext_mangeuser, ton.config.Global_config.warning_text);
        var mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        user_role_id.Value = ck.p_role_id;
    }
}