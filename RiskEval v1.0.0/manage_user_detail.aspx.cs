using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;
using ton;

public partial class manage_user_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tonUtilities.pageaAuthorize(ton.config.Global_config.authtext_mangeuser, ton.config.Global_config.warning_text);
    }
}