using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;

public partial class project_for_budget_home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ton.tonUtilities.pageaAuthorize(ton.config.Global_config.authtext_budgetor, ton.config.Global_config.warning_text);   
    }
}