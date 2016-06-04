using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Web.Caching;

/// <summary>
/// Summary description for dbConnection
/// </summary>
/// 
namespace riskEval
{
    public class gUtilities
    {
        public static bool isAccesspage;
        public static string currentPage;
        public static string p_role_id;

        public static string CnnString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["GovBudgetingConnectionString"].ConnectionString.ToString();
                // return ConfigurationManager.ConnectionStrings["GovBudgeting01ConnectionString"];

            }
        }

        //public static bool isAccess
        //{
        //    get
        //    {
        //        return isAccesspage;
        //    }

        //    set
        //    {
        //        isAccesspage = "";
        //    }
        //}

        //public string getConnectionString()
        //{

        //    string strRetConnString = System.Web.Configuration.WebConfigurationManager.AppSettings["GovBudgetingConnectionString"];

        //    //string strEncryptionKey = System.Web.Configuration.WebConfigurationManager.AppSettings["encryptionKey"];
        //    return strRetConnString;
        //}

        public gUtilities()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static SqlConnection DBConnection()
        {
            var conn = new SqlConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn = new SqlConnection(CnnString);
                conn.Open();
            }
            return conn;
        }

        //gUtilities.GetProjectAnswer(sqlText, project_id);
        public static DataSet GetProjectAnswer(string strSql, string project_id)//The method to retrive dataset 
        {
            SqlConnection conn = DBConnection();
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@project_id", project_id);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            return ds;
        }

        public static DataSet GetData(string strSql, object TableName)//The method to retrive dataset 
        {
            var ds = new DataSet();
            using (var conn = DBConnection())
            {
                var da = new SqlDataAdapter(strSql, conn);
                da.Fill(ds, TableName.ToString());
            }
            return ds;
        }

        public static DataSet GetReportByUser(string strSql, string para, string paraVal)//The method to retrive dataset 
        {
            SqlConnection conn = DBConnection();
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@" + para, paraVal);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            return ds;
        }

        public static DataSet GetDataByProject(string strSql, string pj_id)//The method to retrive dataset 
        {
            SqlConnection conn = DBConnection();
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            return ds;
        }

        public static DataSet GetDataByAllReport(string strSql, string p_id, string paraName)//The method to retrive dataset 
        {
            SqlConnection conn = DBConnection();
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue(paraName, p_id);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            return ds;
        }


        public static DataTable ExecuteDetaTable(SqlCommand cmd)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
        public static DataSet ExecuteDetaset(SqlCommand cmd)
        {
            SqlConnection conn = DBConnection();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DBConnection().Close();
            return ds;
        }



        public static string Login(string IdCard, string strName, string UserStatus)
        {
            ManageCookie ck = new ManageCookie();
            int retIdentity = 0;//To get the identity from inserted value
            int retAllow = 0; //To validate user
            SqlConnection conn = DBConnection();
            DataSet ds = new DataSet();

            //Add one more criteria: p.p_is_deleted is 0
            string strsql = string.Format(@"select p.p_id, p.m_id, p.p_role_id 
                                        , p.p_idno, p.p_name_thai, p.p_sname_thai, p.p_name_eng, p.p_sname_eng
                                        , p.p_is_online, isnull(datediff(hour, p.last_login, getdate()),0) as last_login
                                        , m.mi_code, m.mi_name 
                                        ,d.d_code, d.d_name
                                        from persons p
                                        left join ministry m on p.m_id = m.mi_id
                                        left join persons_detail pd on p.p_id = pd.p_id 
                                        left join department d on pd.d_id = d.d_id
                            where p.p_idno = @idno and p.p_name_eng = @name and p.p_is_active=1 and p.p_is_delete=0 and p.p_role_id=@role_id; ");

            SqlCommand cmd = new SqlCommand(strsql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@idno", IdCard);
            cmd.Parameters.AddWithValue("@name", strName);
            cmd.Parameters.AddWithValue("@role_id", UserStatus);
            cmd.CommandTimeout = 0;
            //DataSet ds = ExecuteDetaset(cmd);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            retAllow = validateUser(ds);
            if (retAllow == 1)
            {
                //                Comment by Ton
                //                strsql = string.Format(@"insert into online_user(p_id,p_idno,p_name_thai,p_sname_thai) 
                //                values(@uid, @idno, @namethai, @sname);             
                //                select @@Identity;
                //                update persons set p_is_active=1, last_login=getdate(), p_is_online=1 where p_idno=@idno;
                //                ");

                //              Add one more criteria condition : also check UID
                strsql = string.Format(@"insert into online_user(p_id,p_idno,p_name_thai,p_sname_thai) 
                values(@uid, @idno, @namethai, @sname);             
                select @@Identity;
                update persons set p_is_active=1, last_login=getdate(), p_is_online=1 where (p_idno=@idno) AND (p_id = @uid) ;
                ");

                cmd.Parameters.Clear();
                cmd.CommandText = strsql;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@uid", ds.Tables[0].Rows[0]["p_id"]);
                cmd.Parameters.AddWithValue("@idno", IdCard);
                cmd.Parameters.AddWithValue("@namethai", ds.Tables[0].Rows[0]["p_name_thai"]);
                cmd.Parameters.AddWithValue("@sname", ds.Tables[0].Rows[0]["p_sname_thai"]);

                cmd.ExecuteNonQuery();


                retIdentity = Convert.ToInt32(cmd.ExecuteScalar());
                HttpContext.Current.Session["logId"] = Convert.ToString(retIdentity) + "/" + ds.Tables[0].Rows[0]["p_id"] + "/" + ds.Tables[0].Rows[0]["mi_name"];
                //ck.logId = retIdentity;
                ck.CreateCookies(ds.Tables[0], Convert.ToString(retIdentity));

            }
            else
            {
                ck.DeleteCookies();
                //Response.Cookies[myFunc.ckCKCode].Expires = DateTime.Now.AddDays(-1);
                //Response.Cookies[myFunc.ckUserType].Expires = DateTime.Now.AddDays(-1);
                //_Utility.MessageBox("Username Or Password are not correct.", txtUsername);
                //return;

            }
            DBConnection().Close();
            if (retAllow == 1 && retIdentity > 0)
            {
                return "";
            }
            else if (retAllow == 2)
            {
                return "ท่านไม่สามารถเข้าใช้งานได้ เนื่องจาขณะนี้มีผู้ใช้ที่ท่านระบุกำลังทำงานอยู่ในระบบ";
            }
            else if (retAllow == 0)
            {
                return "ข้อมูลไม่ถูกต้อง กรุณากรอกข้อมูลให้ถูกต้อง";
            }
            return "";
        }


        public static int Logout(users ck2)
        {
            if (null == ck2)
                return -1;

            try
            {
                var ck = new ManageCookie();
                using (SqlConnection conn = DBConnection())
                {
                    string strsql = string.Format(@"
                            update persons set p_is_online=0 where p_id=@p_id;
                            update online_user set logout_date=getdate() where id=@log_id;
                            ");
                    var cmd = new SqlCommand(strsql, conn) { CommandType = CommandType.Text };
                    cmd.Parameters.AddWithValue("@log_id", ck2.online_id);
                    cmd.Parameters.AddWithValue("@p_id", ck2.p_id);

                    cmd.ExecuteNonQuery();

                    ck.DeleteCookies();
                    //HttpContext.Current.Session["logId"] = null;
                    HttpContext.Current.Cache.Remove("menuData");
                    HttpContext.Current.Cache.Remove("menuText");
                }
            }
            catch { return -1; }
            return 0;
        }
        //public static void MessageLable(Label lbl, TextBox txt, string strMsg)
        //{
        //    lbl.Visible = true;
        //    lbl.Text = "<br />" + strMsg;
        //    txt.Focus();
        //    return;
        //}

        public static int validateUser(DataSet ds)
        {
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if ((ds.Tables[0].Rows[0]["p_is_online"].ToString() == "True") && (Convert.ToInt32(ds.Tables[0].Rows[0]["last_login"]) > 6))
                {
                    return 1;//Allow to login
                }
                else if ((ds.Tables[0].Rows[0]["p_is_online"].ToString() == "False"))
                {
                    return 1;//Allow to login
                }
                else
                {
                    return 2;//Not allow to login 
                }
            }
            return 0;//this user does not exist in the system
        }

        public static string ManageAnnoucement(int announce_id, string title, string desc, int status, string action)
        {

            string strsql;
            SqlConnection conn = DBConnection();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            switch (action)
            {
                case "delete":
                    strsql = string.Format(@"Delete from announce where annonce_id=@announce_id;");
                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@announce_id", announce_id);
                    break;
                case "insert":
                    strsql = string.Format(@"insert into announce(announce_title, announce_detail, announce_status, announce_date,user_created)
                            values(@announce_title,@announce_detail,@announce_status,null, @user_created)");
                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@announce_title", title);
                    cmd.Parameters.AddWithValue("@announce_detail", desc);
                    cmd.Parameters.AddWithValue("@announce_status", status);
                    cmd.Parameters.AddWithValue("@user_created", "admin");
                    break;
                case "update":
                    strsql = string.Format(@"Update announce set announce_title =@announce_title, announce_detail=@announce_detail
                             , announce_status=@announce_status, announce_date=null, user_modified=@user_modified where announce_id=@announce_id;");
                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@announce_id", announce_id);
                    cmd.Parameters.AddWithValue("@announce_title", title);
                    cmd.Parameters.AddWithValue("@announce_detail", desc);
                    cmd.Parameters.AddWithValue("@announce_status", status);
                    cmd.Parameters.AddWithValue("@user_modified", "admin");
                    break;

                default:
                    break;
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return "";
        }


        public static string retMenu(bool isMenu)
        {
            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();

            string user_status = ck.p_role_id;
            XmlDocument doc = new XmlDocument();

            if (HttpContext.Current.Cache["menuData"] != null)
            {
                doc = (XmlDocument)HttpContext.Current.Cache["menuData"];
            }
            else
            {
                doc.Load(HttpContext.Current.Server.MapPath(".") + "\\menu\\admin.xml");
                HttpContext.Current.Cache["menuData"] = doc;
            }

            if (HttpContext.Current.Cache["menuText"] != null && isMenu == true)
            {

                return (string)(HttpContext.Current.Cache["menuText"]);
            }

            StringBuilder sb = new StringBuilder();
            string Valpage = getCurrentPage();

            foreach (XmlNode item in doc.GetElementsByTagName("Parent"))
            {
                if (item.Attributes["id"].Value == user_status || item.Attributes["id"].Value == user_status)
                {
                    if (!isMenu)
                    {
                        if (item.Attributes["show"].Value == "y")
                        {
                            sb.Append("<li>");
                            sb.Append("<a href=\""
                            + item.Attributes["href"].Value
                            + "\">"
                            + item.Attributes["text"].Value + "</a>");
                            if (item.HasChildNodes)
                            {
                                sb.AppendLine("\n<ul>");
                                foreach (XmlNode c in item.ChildNodes)
                                {
                                    sb.AppendLine("<li><a href=\""
                            + c.Attributes["href"].Value
                            + "\">"
                            + c.Attributes["text"].Value
                            + "</a></li>");
                                }
                                sb.AppendLine("</ul>");
                            }
                            sb.AppendLine("</li>");
                        }
                    }
                    else
                    {
                        if (item.Attributes["show"].Value == "y")
                        {
                            sb.Append("<li id='tab" + item.Attributes["tab"].Value + "' class='mainnav'>");
                            sb.Append("<a href=\""
                            + item.Attributes["href"].Value
                            + "\"><span>"
                            + item.Attributes["text"].Value + "</span></a>");
                            if (item.HasChildNodes)
                            {
                                sb.AppendLine("\n<ul style='display: none;' class='dropdown'>");
                                foreach (XmlNode c in item.ChildNodes)
                                {
                                    if (c.Attributes["show"].Value == "y")
                                    {
                                        sb.AppendLine("<li><a href=\""
                                        + c.Attributes["href"].Value
                                        + "\"><span>"
                                        + c.Attributes["text"].Value
                                        + "</span></a></li>");
                                    }
                                }
                                sb.AppendLine("</ul>");
                            }
                            sb.AppendLine("</li>");
                        }
                    }
                }
            }

            if (isMenu)
            {
                HttpContext.Current.Cache["menuText"] = sb.ToString();
                return sb.ToString();
            }

            return sb.ToString();
        }

        public static string getCurrentPage()
        {
            string ValPath = HttpContext.Current.Request.Url.PathAndQuery.ToString(); //HttpContext.Current.Request.Url.AbsolutePath;

            string Valpage = ValPath.Substring(HttpContext.Current.Request.ApplicationPath.Length + 1, ValPath.Length - HttpContext.Current.Request.ApplicationPath.Length - 1);

            string[] words = ValPath.Split('/');

            if (words.Length > 0)
            {
                Valpage = words[words.Length - 1].ToString().ToLower();
            }

            return Valpage;
        }

        public static string checkMenu(string currPage, string role_id)
        {
            currentPage = currPage;
            p_role_id = role_id;
            string menuId = string.Empty;
            isAccesspage = false;

            if (HttpContext.Current.Cache["menuData"] != null)
            {
                XmlDocument doc = new XmlDocument();
                doc = (XmlDocument)(HttpContext.Current.Cache["menuData"]);

                foreach (XmlNode item in doc.GetElementsByTagName("Parent"))
                {
                    if (item.Attributes["href"].Value == currPage)
                    {
                        menuId = item.Attributes["tab"].Value;
                        if (menuId != "") { isAccesspage = true; break; }
                    }
                    else
                    {
                        if (item.Attributes["href"].Value.Contains(currentPage))
                        {
                            menuId = item.Attributes["tab"].Value;
                            if (menuId != "") { isAccesspage = true; break; }
                            //HttpContext.Current.Request.QueryString["status"]
                        }
                    }
                    if (role_id != "2")
                    {

                        if (item.HasChildNodes)
                        {
                            foreach (XmlNode c in item.ChildNodes)
                            {
                                menuId = item.Attributes["tab"].Value;
                                if (menuId == "1" && (currentPage.Contains("question_") || currentPage.Contains("project_") || currentPage.Contains("report_question")))
                                {
                                    menuId = item.Attributes["tab"].Value;
                                    isAccesspage = true;
                                    break;
                                }
                                else if (menuId == "2" && (currentPage.Contains("projects_")))
                                {
                                    menuId = item.Attributes["tab"].Value;
                                    isAccesspage = true;
                                    break;
                                }
                                if (c.Attributes["href"].Value.ToString().Contains(currentPage) == true)
                                {
                                    menuId = item.Attributes["tab"].Value;
                                    isAccesspage = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (isAccesspage) { break; }
                }
                checkPage(doc);
            }
            else
            {
                isAccesspage = true;
            }

            if (currPage == "contacts.aspx") { menuId = "10"; }
            return menuId;
        }

        public static void checkPage(XmlDocument data)
        {
            foreach (XmlNode item in data.GetElementsByTagName("Parent"))
            {
                if (item.Attributes["id"].Value == p_role_id)
                {
                    if (item.HasChildNodes)
                    {
                        checkPage(item, item.Attributes["id"].Value.Trim());
                        if (isAccesspage == true) { break; }
                        foreach (XmlNode c in item.ChildNodes)
                        {
                            checkPage(c, item.Attributes["id"].Value.Trim());
                            if (isAccesspage == true) { break; }
                        }
                        if (isAccesspage == true) { break; }
                    }
                    else
                    {
                        checkPage(item, item.Attributes["id"].Value.Trim());
                        if (isAccesspage == true) { break; }
                    }
                }
                else
                {
                    if (item.Attributes["id"].Value.Contains("0") && item.Attributes["href"].Value.Contains(currentPage) == true)
                    {
                        isAccesspage = true; break;
                    }
                }
            }

        }

        public static void checkPage(XmlNode item, string item_role_id)
        {
            if (item.Attributes["free"].Value == "1" && item.Attributes["href"].Value.Contains(currentPage) == true)
            {
                isAccesspage = true;
            }
            else if (p_role_id == item_role_id && item.Attributes["href"].Value.Contains(currentPage) == true)
            {
                isAccesspage = true;
            }
        }

        public string getPendingAnswerTotal(string pj_id, string qset_id)
        {

            SqlConnection conn = DBConnection();

            string strSql = "select count(*) from answer_q2 q2, answer_q3 q3 where q2.pj_id = @pj_id and q2.answer_q2_id = q3.answer_q2_id and q2.qset_id = @qset_id";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.Parameters.AddWithValue("@qset_id", qset_id);
            cmd.ExecuteNonQuery();
            int intQ1;
            int.TryParse(cmd.ExecuteScalar().ToString(), out intQ1);

            strSql = "select count(*) from answer_q2 q2 where q2.pj_id = @pj_id and q2.qset_id = @qset_id";
            cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.Parameters.AddWithValue("@qset_id", qset_id);
            cmd.ExecuteNonQuery();

            int intQ2;
            int.TryParse(cmd.ExecuteScalar().ToString(), out intQ2);

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            intQ1 += intQ2;

            return Convert.ToString(intQ1);

        }

        public string getAnswerTotal(string qset_id)
        {

            SqlConnection conn = DBConnection();

            string strSql = "select distinct (q3.q2_total + q3.q3_total) from question3_total q3 where q3.qset_id = @qset_id";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@qset_id", qset_id);
            cmd.ExecuteNonQuery();


            string intQ1 = Convert.ToString(cmd.ExecuteScalar());

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            return intQ1;
        }

        public double getReportQSETTotal(string pj_id)
        {

            SqlConnection conn = DBConnection();

            string strSql = "select sum(y_percent_total) from dbo.report_qset_total where pj_id = @pj_id";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.ExecuteNonQuery();

            double dbRet = 0.0;
            string result = cmd.ExecuteScalar().ToString();
            if (String.IsNullOrEmpty(result))
            {
                dbRet = -1.0;
            }
            else
            {
                double.TryParse(result, out dbRet);
            }
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            return dbRet;
        }




        public string getFactorRiskCount(string pj_id)
        {

            SqlConnection conn = DBConnection();

            string strSql = "select count(*) from dbo.answer_factors_sub where pj_id = @pj_id";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.ExecuteNonQuery();
            string intQ1 = Convert.ToString(cmd.ExecuteScalar());
            DBConnection().Close();
            return intQ1;
        }


        public string getFactorRiskCountByPerson(string p_id, string pj_id)
        {

            SqlConnection conn = DBConnection();

            string strSql = "select count(*) from answer_factors_sub a, projects p where a.pj_id = p.pj_id and p.p_id  = @p_id and p.pj_id = @pj_id";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.Parameters.AddWithValue("@p_id", p_id);
            cmd.ExecuteNonQuery();
            string intQ1 = Convert.ToString(cmd.ExecuteScalar());
            DBConnection().Close();
            return intQ1;
        }



        public string getReportTammaTotal(string pj_id)
        {

            SqlConnection conn = DBConnection();

            string strSql = "select sum(yes_percent) from dbo.report_tamma_total where pj_id = @pj_id";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.ExecuteNonQuery();
            //Add by Ton
            double dbTotal;
            Double.TryParse(cmd.ExecuteScalar().ToString(), out dbTotal);

            //Comment by Ton
            //double dbTotal = Convert.ToDouble(cmd.ExecuteScalar());
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            string strReturn = string.Empty;

            if (dbTotal > 70)
            {
                strReturn = "<div style='background-color:green; font-weight:bold; font-size:23px; width'>ต่ำ</div>";
            }
            else if (dbTotal <= 70 && dbTotal > 30)
            {
                strReturn = "<div style='background-color:yellow; font-weight:bold; font-size:23px;'>ปานกลาง</div>";
            }
            else
            {
                strReturn = "<div style='background-color:red; font-weight:bold; font-size:23px;'>สูง</div>";
            }


            return strReturn;

        }

        public string getReportTammaMainTotal(string pj_id)
        {

            SqlConnection conn = DBConnection();

            string strSql = "select sum(yes_percent) from dbo.report_tamma1 where pj_id = @pj_id";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.ExecuteNonQuery();
            //Add by Ton
            double dbTotal;
            Double.TryParse(cmd.ExecuteScalar().ToString(), out dbTotal);

            //Comment by Ton
            //double dbTotal = Convert.ToDouble(cmd.ExecuteScalar());
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            string strReturn = string.Empty;

            if (dbTotal > 70)
            {
                strReturn = "<div style='background-color:green; font-weight:bold; font-size:23px; width'>ต่ำ</div>";
            }
            else if (dbTotal <= 70 && dbTotal > 30)
            {
                strReturn = "<div style='background-color:yellow; font-weight:bold; font-size:23px;'>ปานกลาง</div>";
            }
            else
            {
                strReturn = "<div style='background-color:red; font-weight:bold; font-size:23px;'>สูง</div>";
            }

            return strReturn;

        }

        public string getReportTammaSubTotal(string pj_id)
        {

            SqlConnection conn = DBConnection();

            string strSql = "select sum(yes_percent) from dbo.report_tamma2 where pj_id = @pj_id";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.ExecuteNonQuery();

            //Add by Ton
            double dbTotal;
            Double.TryParse(cmd.ExecuteScalar().ToString(), out dbTotal);

            //Comment by Ton
            //double dbTotal = Convert.ToDouble(cmd.ExecuteScalar());
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            string strReturn = string.Empty;

            if (dbTotal > 70)
            {
                strReturn = "<div style='background-color:green; font-weight:bold; font-size:23px; width'>ต่ำ</div>";
            }
            else if (dbTotal <= 70 && dbTotal > 30)
            {
                strReturn = "<div style='background-color:yellow; font-weight:bold; font-size:23px;'>ปานกลาง</div>";
            }
            else
            {
                strReturn = "<div style='background-color:red; font-weight:bold; font-size:23px;'>สูง</div>";
            }

            return strReturn;

        }

        public string getReportFactorRiskTotal(string pj_id)
        {

            SqlConnection conn = DBConnection();

            string strSql = "select count(*) from dbo.answer_factors_sub where pj_id = @pj_id";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.ExecuteNonQuery();
            double dbTotal1 = Convert.ToDouble(cmd.ExecuteScalar());
            DBConnection().Close();


            strSql = "select count(*) from dbo.answer_factors_sub where pj_id = @pj_id and af_impact = 'จัดการได้' ";

            cmd = new SqlCommand(strSql, conn);
            cmd.Parameters.Clear();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@pj_id", pj_id);
            cmd.ExecuteNonQuery();
            double dbTotal2 = Convert.ToDouble(cmd.ExecuteScalar());
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            double dbResult = (dbTotal2 / dbTotal1) * 100;

            string strReturn = string.Empty;

            if (dbResult > 70)
            {
                strReturn = "<div style='background-color:green; font-weight:bold; font-size:23px; width'>ต่ำ</div>";
            }
            else if (dbResult <= 70 && dbResult > 30)
            {

                strReturn = "<div style='background-color:yellow; font-weight:bold; font-size:23px;'>ปานกลาง</div>";
            }
            else
            {
                strReturn = "<div style='background-color:red; font-weight:bold; font-size:23px;'>สูง</div>";
            }

            return strReturn;

        }

        public static string calRate(double val)
        {
            string strReturn = string.Empty;

            if (val > 70)
            {
                strReturn = "ต่ำ";
            }
            else if (val <= 70 && val > 30)
            {

                strReturn = "ปานกลาง";
            }
            else
            {
                strReturn = "สูง";
            }
            return strReturn;
        }


        public void setFactorImpact(string pj_id)
        {


            SqlConnection conn = DBConnection();

            string strSql = "exec report_factor_by_tammapiban [" + pj_id + "]";

            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            DBConnection().Close();


            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

        }



    }
}