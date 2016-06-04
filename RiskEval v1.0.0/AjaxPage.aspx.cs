using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;

public partial class AjaxPage : System.Web.UI.Page
{
    protected string UpdateDatabase(string dbKey)
    {
        string returnValue = "OK";

        // Update the database here setting the returnValue variable to "Failed" if not successful...

        return returnValue;
    }

    private void Page_Load(object sender, System.EventArgs e)
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        int retVal = gUtilities.Logout(ck);
      //  if (retVal == 0) { Response.Redirect("default.aspx"); }

        //string callRequest = (this.Request["CallRequest"] == null) ? string.Empty : this.Request["CallRequest"];
        string returnValue = string.Empty;
        returnValue = retVal==0?"OK":"NO OK";

        //if (callRequest == "UpdateDatabase")
        //{
        //    string dbKey = (this.Request["DbKey"] == null) ? string.Empty : this.Request["DbKey"];
        //    returnValue = UpdateDatabase(dbKey);
        //}

        this.Response.ClearHeaders();
        this.Response.Clear();
        this.Response.Write(returnValue);
        this.Response.End();
    }

 
}