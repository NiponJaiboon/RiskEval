using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;
using System.Xml;
using System.Text;

public partial class masterpage_Main : System.Web.UI.MasterPage
{
    public string DatabaseRecordKey;
    public string tabId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string chkpage = Request.Url.AbsoluteUri;
        if (!Page.IsPostBack)
        {

            string Valpage = gUtilities.getCurrentPage().ToLower();

            isLogin(Valpage);
        }
        else
        {
            tabId = (string)ViewState["tabId"];
        }


        DatabaseRecordKey = "1";
        
        this.Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "OnSubmitScript", "g_isPostBack = true;"); 

    }

    private void isLogin(string pageName)
    {
        bool isShow=false;
        //set default page for each role
        ltMenuContact.Text = string.Format(@"<li id='tab10' class='{0}'><a href='{1}'><span> ติดต่อเรา </span></a></li>", pageName == "contacts.aspx" ? "mainnav " : "mainnav", "contacts.aspx");
        ltMainmenu.Text = string.Format(@"<li id='tab0' class='{0}'><a href='{1}'><span> หน้าแรก </span></a></li>", pageName == "default.aspx" ? "mainnav" : "mainnav", "default.aspx");
   
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        if (ck != null) //manage_user.aspx
        {
            //show user details
            ltDept.Text += " " + ck.d_name;
            ltUser.Text += " " + ck.p_name_thai;
            if (ck.mi_name.Length > 20)
            { ltDept.Text = "หน่วยงาน: <a class='tooltip' href='#'>" + ck.mi_name.Substring(0, 20) + "<span class='classic'>" + ck.mi_name + "</span></a>"; }
            else { ltDept.Text = "หน่วยงาน: " + ck.mi_name; }
            isShow = true;
            ltTabmenu.Text = gUtilities.retMenu(true);

            if (ck.p_role_id == "1")
            {
                ltMainmenu.Text = string.Format(@"<li id='tab0' class='{0}'><a href='{1}'><span> หน้าแรก </span></a></li>", pageName == "project_home.aspx" ? "mainnav" : "mainnav", "project_home.aspx");
                if (pageName == "project_home.aspx")
                {
                    tabId = "tab0";
                }
            }
            else if (ck.p_role_id == "2")
            {
                ltMainmenu.Text = string.Format(@"<li id='tab0' class='{0}'><a href='{1}'><span> หน้าหลัก </span></a></li>", pageName == "project_for_budget_home.aspx" ? "mainnav" : "mainnav", "project_for_budget_home.aspx");
                if (pageName == "project_for_budget_home.aspx")
                {
                    tabId = "tab0";
                }
            }

        }

        if (tabId == null) 
        { 
        string pageAccess = gUtilities.checkMenu(pageName, ck == null ? "" : ck.p_role_id);
        tabId = pageAccess == "" ? "" : "tab" + pageAccess;
        }


        //if (gUtilities.isAccesspage == false) {Response.Redirect("default.aspx");}

        
        //if (ck != null && pageName != "register_staff.aspx") //manage_user.aspx
        //{
        //    ltDept.Text += " " + ck.d_name ;
        //    ltUser.Text += " " + ck.p_name_thai;
        //    if (ck.mi_name.Length > 30)
        //    { ltDept.Text = "หน่วยงาน: <a class='tooltip' href='#'>" + ck.mi_name.Substring(0, 30) + "<span class='classic'>"+ ck.mi_name +"</span></a>"; }
        //    else {ltDept.Text += " " + ck.mi_name; }
        //    ltTabmenu.Text = gUtilities.retMenu(true);
        //}
        //else if (ck != null && (pageName == "register.aspx" || pageName == "register_staff.aspx"))
        //{

        //}
        //else
        //{
        //    if (pageName != "default.aspx" && pageName != "contacts.aspx") { Response.Redirect("default.aspx"); }
        //}
        ltUser.Visible = isShow;
        ltDept.Visible = isShow;
        lkbLogout.Visible = isShow;
        ViewState["tabId"] = tabId;
   }
    protected void lkbLogout_Click(object sender, EventArgs e)
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        int retVal = gUtilities.Logout(ck);
        if (retVal == 0) { Response.Redirect("default.aspx"); }
       
    }

//    protected void showMenu(string role_id,string pageName)
//    {
//        string strMenu = string.Empty;
//       // loadMenuByRole();
//        ltTabmenu.Text = gUtilities.retMenu(true);
//        if (role_id=="4")
//        {
////         ltMenuAdmin.Text += string.Format(@"
////                        <li class='{0}'><a href='Announce.aspx'><span> ผู้ดูแลระบบ (Super Admin)</span></a>
////                                 <ul class='menu2'>
////                                    <li><a href='#'>กำหนดสิทธิ์การใช้งานในระบบ</a></li>
////                                    <li><a href='Announce.aspx'>แก้ไขข้อมูลประกาศสำนักประเมินผล</a></li>
////                                    <li><a href='" + ton.config.Global_config.RootURL + @"manage_user.aspx'>ยูสเสอร์เมนนู</a></li>
////                                   
////                                </ul>
////                            </li>
////                            ", pageName == "announce.aspx" ? "current" : "none");
//        }

//         //<li><a href='" + ton.config.Global_config.RootURL + @"register.aspx'>ลงทะเบียนenduser</a></li>
//         //                           <li><a href='" + ton.config.Global_config.RootURL + @"register_staff.aspx'>ลงทะเบียนstaff</a></li>
        
//    }

    //protected void loadMenuByRole()
    //{
    //    XmlDocument doc = new XmlDocument();
    //   // doc.Load("filename.xml");
    //    doc.Load(Server.MapPath(".") + "\\menu\\admin.xml");

    //    StringBuilder sb = new StringBuilder();

    //    sb.AppendLine("<ul>");
    //    foreach (XmlNode item in doc.GetElementsByTagName("Parent"))
    //    {
    //        sb.Append("<li>");
    //        sb.Append("<a href=\""
    //        + item.Attributes["href"].Value
    //        + "\">"
    //        + item.Attributes["text"].Value + "</a>");
    //        if (item.HasChildNodes)
    //        {
    //            sb.AppendLine("\n<ul>");
    //            foreach (XmlNode c in item.ChildNodes)
    //            {
    //                sb.AppendLine("<li><a href=\""
    //        + c.Attributes["href"].Value
    //        + "\">"
    //        + c.Attributes["text"].Value
    //        + "</a></li>");
    //            }
    //            sb.AppendLine("</ul>");
    //        }
    //        sb.AppendLine("</li>");
    //    }
    //    sb.AppendLine("</ul>");
    //    Literal1.Text = sb.ToString();

    //    //Response.Write(sb.ToString());
    //}
}
