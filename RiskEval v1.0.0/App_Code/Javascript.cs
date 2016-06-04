using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;

namespace ton
{
    /// <summary>
    /// Summary description for Javascript
    /// Thx K.Monchai 555
    /// </summary>
    public class JavaScript
    {
        /// <summary>
        /// Redirects.
        /// Created by Ton
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <example></example>
        public static void Redirect(string url)
        {
            Page page = (HttpContext.Current.Handler as Page);
            if (page != null)
                HttpContext.Current.Response.Write("<script>window.location='" + page.ResolveUrl(url) + "';</script>");
        }

        /// <summary>
        /// Redirects with Option.
        /// Created by Ton
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="option">Option</param>
        /// <example></example>
        public static void Redirect(string url,string option)
        {
            Page page = (HttpContext.Current.Handler as Page);
            if (page != null)
                HttpContext.Current.Response.Write("<script>window.open('" + page.ResolveUrl(url) + "', 'newpage', 'status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=yes,screenX=0,screenY=0,left=0,top=0,width=' + (screen.availWidth - 10) + ', height='+ (screen.availHeight - 122) +'', 'target="+option+"')</script>");
        }

        /// <summary>
        /// Redirects the new page.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <example>If use ASP.NET AJAX don't forget to register control in PostBackTrigger</example>
        public static void RedirectNewPage(string url)
        {
            Page page = (HttpContext.Current.Handler as Page);
            if (page != null)
                HttpContext.Current.Response.Write("<script>window.open('" + page.ResolveUrl(url) + "', 'newpage', 'status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=yes,screenX=0,screenY=0,left=0,top=0,width=' + (screen.availWidth - 10) + ', height='+ (screen.availHeight - 122) +'', 'target=_blank')</script>");
        }

        #region "Open Popup or Modal"

        /// <summary>
        /// Opens the modal dialog.
        /// </summary>
        /// <param name="ctl">The CTL.</param>
        /// <param name="url">The URL.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="isRefreshPage">if set to <c>true</c> [is refresh page].</param>
        /// <param name="clientIDList">The client ID list.</param>
        public static void OpenModalDialog(Control ctl, string url, int width, int height, bool isRefreshPage, params string[] clientIDList)
        {
            Page page = (HttpContext.Current.Handler as Page);
            StringBuilder sb = new StringBuilder();
            //window.opener.document
            sb.AppendLine("var left = (screen.width/2)-(" + width.ToString() + "/2);");
            sb.AppendLine("var top = (screen.height/2)-(" + height.ToString() + "/2);");
            if (page != null)
                sb.AppendLine("var MyArgs = window.showModalDialog('" + page.ResolveUrl(url) + "', 'ModalPopup', 'dialogWidth:" + width.ToString() + "px; dialogHeight:" + height.ToString() + "px; dialogLeft: '+left+'; dialogTop: '+top+';center:yes;resizable:no;status:no')");
            sb.AppendLine("if (MyArgs != null) {");
            sb.AppendLine("");
            if (clientIDList != null)
            {
                for (int i = 0; i < clientIDList.Length; i++)
                {
                    sb.AppendLine("if (MyArgs[" + i.ToString() + "] != null) {");
                    sb.AppendLine("document.getElementById('" + clientIDList[i] + "').value = MyArgs[" + i.ToString() + "];");
                    sb.AppendLine("}");
                }
            }
            sb.AppendLine("}");

            if (isRefreshPage == false)
            {
                sb.AppendLine("return false;");
            }

            if (ctl is Button)
            {
                Button btn = (Button)ctl;
                btn.OnClientClick = sb.ToString();
            }
            else if (ctl is ImageButton)
            {
                ImageButton imgBtn = (ImageButton)ctl;
                imgBtn.OnClientClick = sb.ToString();
            }
            else if (ctl is LinkButton)
            {
                LinkButton lnkBtn = (LinkButton)ctl;
                lnkBtn.OnClientClick = sb.ToString();
            }
            else if (ctl is HtmlControl)
            {
                HtmlControl wCtl = (HtmlControl)ctl;
                wCtl.Attributes.Add("onclick", sb.ToString());
            }
        }

        /// <summary>
        /// Opens the popup windows.
        /// </summary>
        /// <param name="ctl">The CTL.</param>
        /// <param name="url">The URL.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void OpenPopupWindows(Control ctl, string url, int width, int height)
        {
            Page page = (HttpContext.Current.Handler as Page);
            string clientID = ctl.ClientID;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var left = (screen.width/2)-(" + width.ToString() + "/2);");
            sb.AppendLine("var top = (screen.height/2)-(" + height.ToString() + "/2);");
            if (page != null)
                sb.AppendLine("window.open('" + page.ResolveUrl(url) + "', 'Popup" + clientID + "', 'width=" + width.ToString() + ", height=" + height.ToString() + ", top='+top+', left='+left+', status=no, resizable=no, scrollbars=yes, toolbar=no, location=no, menubar=no').focus();");
            sb.AppendLine("return false;");

            if (ctl is Button)
            {
                Button btn = (Button)ctl;
                btn.OnClientClick = sb.ToString();
            }
            else if (ctl is ImageButton)
            {
                ImageButton imgBtn = (ImageButton)ctl;
                imgBtn.OnClientClick = sb.ToString();
            }
            else if (ctl is LinkButton)
            {
                LinkButton lnkBtn = (LinkButton)ctl;
                lnkBtn.OnClientClick = sb.ToString();
            }
            else if (ctl is HtmlControl)
            {
                HtmlControl wCtl = (HtmlControl)ctl;
                wCtl.Attributes.Add("onclick", sb.ToString());
            }
        }

        /// <summary>
        /// Opens the popup windows.
        /// </summary>
        /// <param name="ctl">The CTL.</param>
        /// <param name="url">The URL.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="fixWindowName">if set to <c>true</c> [fix window name].</param>
        public static void OpenPopupWindows(Control ctl, string url, int width, int height, bool fixWindowName)
        {
            Page page = (HttpContext.Current.Handler as Page);
            string clientID = ctl.ClientID;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var left = (screen.width/2)-(" + width.ToString() + "/2);");
            sb.AppendLine("var top = (screen.height/2)-(" + height.ToString() + "/2);");
            if (fixWindowName)
            {
                if (page != null)
                    sb.AppendLine("window.open('" + page.ResolveUrl(url) + "', 'Popup', 'width=" + width.ToString() + ", height=" + height.ToString() + ", top='+top+', left='+left+', status=no, resizable=no, scrollbars=yes, toolbar=no, location=no, menubar=no').focus();");
            }
            else
            {
                if (page != null)
                    sb.AppendLine("window.open('" + page.ResolveUrl(url) + "', 'Popup" + clientID + "', 'width=" + width.ToString() + ", height=" + height.ToString() + ", top='+top+', left='+left+', status=no, resizable=no, scrollbars=yes, toolbar=no, location=no, menubar=no').focus();");
            }
            sb.AppendLine("return false;");

            if (ctl is Button)
            {
                Button btn = (Button)ctl;
                btn.OnClientClick = sb.ToString();
            }
            else if (ctl is ImageButton)
            {
                ImageButton imgBtn = (ImageButton)ctl;
                imgBtn.OnClientClick = sb.ToString();
            }
            else if (ctl is LinkButton)
            {
                LinkButton lnkBtn = (LinkButton)ctl;
                lnkBtn.OnClientClick = sb.ToString();
            }
            else if (ctl is HtmlControl)
            {
                HtmlControl wCtl = (HtmlControl)ctl;
                wCtl.Attributes.Add("onclick", sb.ToString());
            }
        }

        /// <summary>
        /// Sets the value to control in parent window.
        /// </summary>
        /// <param name="clientID">The client ID.</param>
        /// <param name="value">The value.</param>
        /// <param name="isClosePopup">if set to <c>true</c> [is close popup].</param>
        public static void SetValueToControlInParentWindow(string clientID, string value, bool isClosePopup)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("window.opener.document.getElementById('" + clientID + "').value = '" + HttpUtility.HtmlDecode(value) + "';");
            if (isClosePopup)
            {
                sb.AppendLine("window.close();");
            }

            Page page = (HttpContext.Current.Handler as Page);
            if (page != null)
                ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "ReturnValue" + clientID, sb.ToString(), true);
        }

        /// <summary>
        /// Gets the value from parent to popup.
        /// </summary>
        /// <param name="parentClientID">The parent client ID.</param>
        /// <param name="popupClientID">The popup client ID.</param>
        public static void GetValueFromParentToPopup(string parentClientID, string popupClientID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var ctl = document.getElementById('" + popupClientID + "');");
            sb.AppendLine("ctl.value = window.opener.document.getElementById('" + parentClientID + "').value;");

            Page page = (HttpContext.Current.Handler as Page);
            if (page != null)
                ScriptManager.RegisterStartupScript(page, page.GetType(), "GetValueFromParent" + popupClientID, sb.ToString(), true);
        }

        /// <summary>
        /// Sets the modal popup return value.
        /// </summary>
        /// <param name="valueList">The value list.</param>
        public static void SetModalPopupReturnValue(params string[] valueList)
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("window.opener.document.getElementById('" + clientID + "').value = '" + value + "';");
            for (int i = 0; i < valueList.Length; i++)
            {
                sb.AppendLine("var val" + i.ToString() + " = '" + HttpUtility.HtmlDecode(valueList[i]) + "';");
                sb.AppendLine("val" + i.ToString() + " = val" + i.ToString() + ";");
            }

            sb.Append("var MyArgs = new Array(");
            for (int i = 0; i < valueList.Length; i++)
            {
                sb.Append("val" + i);
                if (i < valueList.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.AppendLine(");");
            sb.AppendLine("window.returnValue = MyArgs;");
            sb.AppendLine("window.close();");

            Page page = (HttpContext.Current.Handler as Page);
            if (page != null)
                ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "ModalPopupReturnValue", sb.ToString(), true);
        }

        /// <summary>
        /// Refreshes the parent.
        /// </summary>
        /// <param name="isPostBack">if set to <c>true</c> [is post back].</param>
        public static void RefreshParent(bool isPostBack)
        {
            Page page = (HttpContext.Current.Handler as Page);

            if (!isPostBack)
            {
                if (page != null)
                    ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "RefreshParent", "if (window.opener && !window.opener.closed) { window.opener.location.reload(); }", true);
            }
            else
            {
                if (page != null)
                    ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "RefreshParent", "if (window.opener && !window.opener.closed) { window.opener.document.forms(0).submit(); }", true);
            }
        }

        /// <summary>
        /// Closes the popup windows.
        /// </summary>
        public static void ClosePopupWindows()
        {
            Page page = (HttpContext.Current.Handler as Page);
            if (page != null)
                ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "ClosePopup", "window.close();", true);
        }

        /// <summary>
        /// Closes the popup windows and refresh parent.
        /// </summary>
        /// <param name="isPostBack">if set to <c>true</c> [is post back].</param>
        public static void ClosePopupWindowsAndRefreshParent(bool isPostBack)
        {
            Page page = (HttpContext.Current.Handler as Page);

            if (!isPostBack)
            {
                if (page != null)
                    ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "ClosePopup", "if (window.opener && !window.opener.closed) { window.opener.location.reload(); } window.close();", true);
            }
            else
            {
                if (page != null)
                    ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "ClosePopup", "if (window.opener && !window.opener.closed) { window.opener.document.forms(0).submit(); } window.close();", true);
            }
        }

        /// <summary>
        /// Closes the popup windows.
        /// </summary>
        /// <param name="ctl">The CTL.</param>
        public static void ClosePopupWindows(Control ctl)
        {
            if (ctl is Button)
            {
                Button btn = ctl as Button;
                btn.OnClientClick = "window.close(); return false;";
            }
        }

        #endregion

        #region "Alert Message"

        /// <summary>
        /// Messages the box.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void MessageBox(string message)
        {
            Page page = (HttpContext.Current.Handler as Page);
            MessageBox(page, message);
        }

        /// <summary>
        /// Messages the box.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="message">The message.</param>
        public static void MessageBox(Page page, string message)
        {
            int msgCount = page.Request.RequestType.Length - 3;
            page.Request.RequestType += " ";
            string msgKey = "AlertMessage" + msgCount;

            //Change special characters to aviod error : Unterminated string constant
            message = message.Replace("\n", "\\n");
            message = message.Replace("'", "\\'");
            message = message.Replace("\r\\n", "\\n");

            ScriptManager.RegisterStartupScript(page, page.GetType(), msgKey, "alert('" + message + "');", true);
        }

        /// <summary>
        /// Messages the box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="url">The URL.</param>
        public static void MessageBox(string message, string url)
        {
            Page page = (HttpContext.Current.Handler as Page);

            if (page != null)
            {
                int msgCount = page.Request.RequestType.Length - 3;
                page.Request.RequestType += " ";
                string msgKey = "AlertMessage" + msgCount;

                //Change special characters to aviod error : Unterminated string constant
                message = message.Replace("\n", "\\n");
                message = message.Replace("'", "\\'");
                message = message.Replace("\r\\n", "\\n");

                ScriptManager.RegisterStartupScript(page, page.GetType(), msgKey, "alert('" + message + "'); window.location = \"" + page.ResolveUrl(url) + "\" ;", true);
            }
        }

        #endregion

        #region "Confirm Message"

        /// <summary>
        /// Confirms the box.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        /// <param name="message">The message.</param>
        public static void ConfirmBox(Button btn, string message)
        {
            //Change special characters to aviod error : Unterminated string constant
            message = message.Replace("\n", "\\n");
            message = message.Replace("'", "\\'");
            message = message.Replace("\r\\n", "\\n");

            btn.Attributes.Add("onclick", "return confirm('" + message + "');");
        }

        /// <summary>
        /// Confirms the box.
        /// </summary>
        /// <param name="imbBtn">The imb BTN.</param>
        /// <param name="message">The message.</param>
        public static void ConfirmBox(ImageButton imbBtn, string message)
        {
            //Change special characters to aviod error : Unterminated string constant
            message = message.Replace("\n", "\\n");
            message = message.Replace("'", "\\'");
            message = message.Replace("\r\\n", "\\n");

            imbBtn.Attributes.Add("onclick", "return confirm('" + message + "');");
        }

        #endregion
    }
}