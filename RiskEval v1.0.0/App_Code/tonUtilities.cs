using System;
using System.Linq;
using System.Web;
using riskEval;
using ton.config;

/// <summary>
/// Summary description for tonUtilities
/// </summary>
///
namespace ton
{
    public class tonUtilities
    {
        public tonUtilities()
        {
        }

        public static string cleanQueryString(string parameter_value)
        {
            string result = "";
            if (!String.IsNullOrEmpty(parameter_value))
                result = HttpUtility.UrlEncode(parameter_value.Replace("'", ""));
            return result;
        }

        public static string getRiskLevel(double dbTotal)
        {
            string strReturn = "";
            if (dbTotal > 70)
            {
                strReturn = "<div class='risk_low'>ต่ำ</div>";
            }
            else if (dbTotal <= 70 && dbTotal > 30)
            {
                strReturn = "<div class='risk_middle'>ปานกลาง</div>";
            }
            else
            {
                strReturn = "<div class='risk_high'>สูง</div>";
            }
            return strReturn;
        }

        public static void pageaAuthorize(string allow, string warningtext)
        {
            var mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();
            if (null == ck)
                return;

            var list_allow = allow.Split(',').ToList<string>();

            try
            {
                bool valid = false;
                for (int i = 0; i < list_allow.Count; i++)
                {
                    if (ck.p_role_id == list_allow[i])
                    {
                        valid = true;
                        break;
                    }
                }

                if (!valid)
                {
                    throw new Exception(ck.p_id + "|" + ck.p_idno + " " + warningtext);
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //ton.JavaScript.MessageBox(warningtext, Global_config.RootURL);
                ton.JavaScript.Redirect(Global_config.RootURL);
            }
        }
    }
}