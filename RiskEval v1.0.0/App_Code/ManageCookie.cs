using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ton;

/// <summary>
/// Summary description for manageCookie
/// </summary>
/// 
namespace riskEval
{
    public class ManageCookie
    {
        //public int logId;
        public void CreateCookies(DataTable dt, string online_id)
        {
            DateTime cookieExpiry = DateTime.Now.AddHours(8);
            // First Approach for storing multiple values in a single Cookie
            // 'Cookie_Key' has two sub keys (values)

            // if we delete cookie 'Cookie_Key', It'll expire both sub cookie values        
            // HttpContext.Current.Response.Cookies["Cookie_Key"].Expires = cookieExpiry;

            // Second Approach for storing multiple values in a single Cookie
            HttpCookie cookie = new HttpCookie("Cookie_Key");
            cookie.Values["p_id"] = Encryption.Encrypt(dt.Rows[0]["p_id"].ToString(), Encryption.keyword);
            cookie.Values["p_idno"] = Encryption.Encrypt(dt.Rows[0]["p_idno"].ToString(), Encryption.keyword);
            cookie.Values["p_name_thai"] = Encryption.Encrypt(dt.Rows[0]["p_name_thai"].ToString(), Encryption.keyword);
            cookie.Values["p_role_id"] = Encryption.Encrypt(dt.Rows[0]["p_role_id"].ToString(), Encryption.keyword);
            cookie.Values["m_id"] = Encryption.Encrypt(dt.Rows[0]["m_id"].ToString(), Encryption.keyword);
            cookie.Values["mi_code"] = Encryption.Encrypt(dt.Rows[0]["mi_code"].ToString(), Encryption.keyword);
            cookie.Values["mi_name"] = Encryption.Encrypt(dt.Rows[0]["mi_name"].ToString(), Encryption.keyword);
            cookie.Values["d_code"] = Encryption.Encrypt(dt.Rows[0]["d_code"].ToString(), Encryption.keyword);
            cookie.Values["d_name"] = Encryption.Encrypt(dt.Rows[0]["d_name"].ToString(), Encryption.keyword);
            cookie.Values["online_id"] = Encryption.Encrypt(online_id, Encryption.keyword);
            cookie.Values["pj_id"] = null;
            cookie.Values["pj_code"] = null;
            cookie.Values["pj_status"] = null;
            cookie.Values["q1_id"] = null;
            cookie.Values["q2_id"] = null;
            cookie.Values["q2_status"] = null;
            cookie.Values["qset_id"] = null;
            cookie.Values["answer_q2_id"] = null;

            cookie.Values["pj_type"] = null;
            cookie.Expires = cookieExpiry;

            // Finally Add this cookie to the Cookies collection
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public users ReadCookies()
        {
            users us = null;
            try
            {
                var cookies = HttpContext.Current.Request.Cookies["Cookie_Key"];
                if (cookies != null
                    && cookies.Values.Count > 1)
                {
                    us = new users
                         {
                             p_id =
                                 Encryption.Decrypt(
                                     cookies.Values["p_id"],
                                     Encryption.keyword),
                             p_idno =
                                 Encryption.Decrypt(
                                     cookies.Values["p_idno"],
                                     Encryption.keyword),
                             p_name_thai =
                                 Encryption.Decrypt(
                                     cookies.Values["p_name_thai"],
                                     Encryption.keyword),
                             p_sname_thai =
                                 Encryption.Decrypt(
                                     cookies.Values["p_sname_thai"],
                                     Encryption.keyword),
                             p_name_eng =
                                 Encryption.Decrypt(
                                     cookies.Values["p_name_eng"],
                                     Encryption.keyword),
                             p_sname_eng =
                                 Encryption.Decrypt(
                                     cookies.Values["p_sname_eng"],
                                     Encryption.keyword),
                             p_address =
                                 Encryption.Decrypt(
                                     cookies.Values["p_address"],
                                     Encryption.keyword),
                             p_phone =
                                 Encryption.Decrypt(
                                     cookies.Values["p_phone"],
                                     Encryption.keyword),
                             p_phone_ext =
                                 Encryption.Decrypt(
                                     cookies.Values["p_phone_ext"],
                                     Encryption.keyword),
                             p_mobile =
                                 Encryption.Decrypt(
                                     cookies.Values["p_mobile"],
                                     Encryption.keyword),
                             m_id =
                                 Encryption.Decrypt(
                                     cookies.Values["m_id"],
                                     Encryption.keyword),
                             d_id =
                                 Encryption.Decrypt(
                                     cookies.Values["d_id"],
                                     Encryption.keyword),
                             p_role_id =
                                 Encryption.Decrypt(
                                     cookies.Values["p_role_id"],
                                     Encryption.keyword),
                             mi_code =
                                 Encryption.Decrypt(
                                     cookies.Values["mi_code"],
                                     Encryption.keyword),
                             mi_name =
                                 Encryption.Decrypt(
                                     cookies.Values["mi_name"],
                                     Encryption.keyword),
                             d_code =
                                 Encryption.Decrypt(
                                     cookies.Values["d_code"],
                                     Encryption.keyword),
                             d_name =
                                 Encryption.Decrypt(
                                     cookies.Values["d_name"],
                                     Encryption.keyword),
                             online_id =
                                 Encryption.Decrypt(
                                     cookies.Values["online_id"],
                                     Encryption.keyword),
                             pj_status =
                                 Encryption.Decrypt(
                                     cookies.Values["pj_status"],
                                     Encryption.keyword),
                             pj_id =
                                 Encryption.Decrypt(
                                     cookies.Values["pj_id"],
                                     Encryption.keyword),
                             pj_code =
                                 Encryption.Decrypt(
                                     cookies.Values["pj_code"],
                                     Encryption.keyword),
                             q1_id =
                                 Encryption.Decrypt(
                                     cookies.Values["q1_id"],
                                     Encryption.keyword),
                             q2_id =
                                 Encryption.Decrypt(
                                     cookies.Values["q2_id"],
                                     Encryption.keyword),
                             q3_id =
                                 Encryption.Decrypt(
                                     cookies.Values["q3_id"],
                                     Encryption.keyword),
                             qset_id =
                                 Encryption.Decrypt(
                                     cookies.Values["qset_id"],
                                     Encryption.keyword),
                             answer_q2_id =
                                 Encryption.Decrypt(
                                     cookies.Values["answer_q2_id"],
                                     Encryption.keyword),
                             mi_id =
                                 Encryption.Decrypt(
                                     cookies.Values["mi_id"],
                                     Encryption.keyword)
                         };
                    /**/
                }
            }
            catch (Exception ex)
            {
                //  ton.JavaScript.MessageBox(ex.Message.ToString());
                Console.Write(ex.Message.ToString());
            }
            return us;

        }
        //public String[] ReadCookies()
        //{
        //    // Check if the cookie, we already sent to client, is comming with the 
        //    // request back up to the server
        //    //Read a cookie value directly, encoding it for safety.
        //    String[] result = null;
        //    try
        //    {
        //        result = new String[3];
        //        if (cookies.Values.Count > 1)
        //        {
        //            // First approach to Get/Read Cookies
        //            cookies.Values.CopyTo(result, 0);

        //            for (int i = 0; i < result.Count(); i++)
        //            {
        //                result[i] = Encryption.Decrypt(result[i], Encryption.keyword);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ton.JavaScript.MessageBox(ex.Message.ToString());
        //    }


        //    return result;
        //}

        public void DeleteCookies()
        {
            if (HttpContext.Current.Response.Cookies["Cookie_Key"] != null)
            {
                // The Cookie Expiry is made one day earlier than the current date time
                DateTime cookieExpiry = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies["Cookie_Key"].Expires = cookieExpiry;
                // OR
                HttpCookie deleteCookie = HttpContext.Current.Response.Cookies["Cookie_Key"];
                deleteCookie.Expires = cookieExpiry;
                HttpContext.Current.Response.Cookies.Add(deleteCookie);
            }
        }

        public void UpdateCookies(string cookieName, string cookieValue)
        {
            var cookie = HttpContext.Current.Request.Cookies["Cookie_Key"];

            if (cookie != null)
            {
                cookie.Values[cookieName] = Encryption.Encrypt(cookieValue, Encryption.keyword);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

    }
}