using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.IO;
using System.Net.Json;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BizPortal;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxLoadingPanel;
using DevExpress.Web.ASPxSiteMapControl;
using iSabaya;
using Resources;

namespace WebHelper
{
    public abstract class iSabayaWebPageBase : System.Web.UI.Page
    {
        public static iSystem ThisSystem { get; protected set; }
        protected ASPxLoadingPanel loading;

        static iSabayaWebPageBase()
        {
            //CommonConstants.Systems.TryGetValue(ApplicationID, out ThisSystem);
            //ThisSystem = (iSystem)CreateApplication();
        }

        [WebMethod]
        public static void AbandonSession()
        {
            HttpContext.Current.Session.Abandon();
        }

        public const string DefaultThemeName = "";
        private string cssLink = "";

        protected string CSSLink
        {
            get { return cssLink; }
            set { cssLink = value; }
        }

        //public static int ApplicationID
        //{
        //    get
        //    {
        //        int applicationID = 0;
        //        int.TryParse(System.Configuration.ConfigurationManager.AppSettings["SystemID"], out applicationID);
        //        return applicationID;
        //    }
        //}

        public static void SetThisSystem(SystemEnum applicationID)
        {
            ThisSystem = new iSystem(applicationID);
            ThisSystem.Title = System.Configuration.ConfigurationManager.AppSettings["ApplicationTitle"].ToString();
        }

        //public static void SetConfiguration(Context context)
        //{
        //    BizPortalConfiguration config = ThisSystem.SetConfiguration(context);
        //    BizPortalConfiguration.CurrentConfiguration = config;
        //    if (config != null)
        //    {
        //        BizPortalConfiguration.CurrentConfiguration.Security.WebSessionTimeoutValueInMinutes = config.Security.WebSessionTimeoutValueInMinutes;
        //        BizPortalConfiguration.CurrentConfiguration.Security.MinPasswordLength = config.Security.MinPasswordLength;
        //        BizPortalConfiguration.CurrentConfiguration.Security.MaxPasswordLength = config.Security.MaxPasswordLength;
        //        BizPortalConfiguration.CurrentConfiguration.Security.MaxConsecutiveFailedLogonAttempts = config.Security.MaxConsecutiveFailedLogonAttempts;
        //        BizPortalConfiguration.CurrentConfiguration.Security.MaxDaysOfInactivity = config.Security.MaxDaysOfInactivity;
        //    }

        //ThisSystem.Description = new MultilingualString
        //{
        //    Values = new List<MLSValue>
        //    {
        //        CreateMultilingualStringValue(System.Configuration.ConfigurationManager.AppSettings["AppDescription1"].ToString()),
        //        CreateMultilingualStringValue(System.Configuration.ConfigurationManager.AppSettings["AppDescription2"].ToString())
        //    }
        //};

        private static MLSValue CreateMultilingualStringValue(string text)
        {
            string[] parts = text.Split(new char[] { ',' });
            return new MLSValue(parts[0], parts[1]);
        }

        protected abstract Context sessionContext { get; set; }

        public virtual Context SessionContext
        {
            get { return this.sessionContext; }
            set { this.sessionContext = value; }
        }

        //private Context sessionContext;
        //public virtual Context SessionContext
        //{
        //    //get { return this.sessionContext; }
        //    get
        //    {
        //        if (null == sessionContext)
        //        {
        //            sessionContext = new Context(PersistenceLayer.WebSessionManager.PersistenceSession);
        //            sessionContext.MySystem = ThisSystem;
        //            if (null == Session["UserID"])
        //                sessionContext.UserID = 0;
        //            else
        //                sessionContext.UserID = (int)Session["UserID"];
        //        }
        //        return sessionContext;
        //    }
        //}

        private PrincipalContext principalContext;

        public virtual PrincipalContext activeDirectoryContext
        {
            get
            {
                if (null == principalContext)
                {
                    principalContext = new PrincipalContext(ContextType.Domain, "isabaya100", "DC=isabaya,DC=net", "watchara", "P@ssw0rd");
                }
                return principalContext;
            }
        }

        public virtual new User User { get { return SessionContext.User; } }

        //private User user = null;
        //public virtual User User
        //{
        //    get
        //    {
        //        if (user == null)
        //            user = SessionContext.User;
        //        return user;
        //    }
        //    set
        //    {
        //        if (null != value)
        //        {
        //            this.user = value;
        //            Session["UserID"] = value.ID;
        //        }
        //    }
        //}

        //public virtual long UserSessionID
        //{
        //    get
        //    {
        //        if (Session["UserSessionID"] == null)
        //            Response.Redirect("~/login.aspx");
        //        return (long)Session["UserSessionID"];
        //    }
        //    set
        //    {
        //        Session["UserSessionID"] = value;
        //    }
        //}

        public virtual Currency Currency { get; set; }

        //private Currency currency;

        //public virtual Currency Currency
        //{
        //    get
        //    {
        //        if (null != this.currency)
        //            return this.currency;
        //        int id = Session["CurrencyID"] != null ? (int)Session["CurrencyID"] : 0;
        //        if (0 == id)
        //            this.Currency = SessionContext.Configuration.DefaultCurrency;
        //        else
        //            this.Currency = SessionContext.PersistenceSession.Get<Currency>(id);
        //        return this.currency;
        //    }
        //    set
        //    {
        //        if (null != value)
        //        {
        //            this.currency = value;
        //            Session["CurrencyID"] = value.Code;
        //        }
        //    }
        //}

        public virtual Language Language { get; set; }

        //private Language language;
        //public virtual Language Language
        //{
        //    get
        //    {
        //        if (null != this.language)
        //            return this.language;
        //        String code = (String)Session["LanguageCode"];
        //        if (String.IsNullOrEmpty(code))
        //            this.Language = SessionContext.Configuration.DefaultLanguage;
        //        else
        //            this.Language = SessionContext.PersistenceSession.Get<Language>(code);
        //        return this.language;
        //    }
        //    set
        //    {
        //        if (null != value)
        //        {
        //            this.language = value;
        //            SessionContext.CurrentLanguage = value;
        //            Session["LanguageCode"] = value.Code;
        //        }
        //    }
        //}

        public virtual int MenuID
        {
            get
            {
                MySiteMapProvider provider = Session["MenuProvider"] as MySiteMapProvider;
                if (provider == null || provider.CurrentNode == null)
                    return 0;
                try
                {
                    return int.Parse(provider.CurrentNode.Key);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public virtual String LanguageCode { get; set; }

        //public virtual String LanguageCode
        //{
        //    get
        //    {
        //        String code = (String)Session["LanguageCode"];
        //        if (String.IsNullOrEmpty(code))
        //            Session["LanguageCode"] = code = this.Language.Code;
        //        return code;
        //    }
        //    set
        //    {
        //        this.Language = SessionContext.PersistenceSession.Get<Language>(value);
        //    }
        //}

        public virtual Country Country { get; set; }

        //private Country country;
        //public virtual Country Country
        //{
        //    get
        //    {
        //        if (null != this.country)
        //            return this.country;
        //        int id = (int)Session["CountryID"];
        //        if (0 == id)
        //            return null;
        //        this.country = SessionContext.PersistenceSession.Get<Country>(id);
        //        return this.country;
        //    }
        //    set
        //    {
        //        if (null != value)
        //        {
        //            this.country = value;
        //            Session["CountryID"] = value.CountryID;
        //        }
        //    }
        //}

        public virtual String DateOutputFormat
        {
            get { return "dd MMM yyy"; }
        }

        public virtual String DateTimeOutputFormat
        {
            get { return "dd MMM yyyy HH:mm:ss"; }
        }

        public virtual String DateInputFormat
        {
            get { return "ddMMyyyy"; }
        }

        public virtual String CurrencyFormat
        {
            get { return "#,#0.00"; }
        }

        public virtual String UnitsFormat
        {
            get { return "#,#0.0000"; }
        }

        public virtual Unit EditorWidth
        {
            get { return Unit.Pixel(170); }
        }

        public virtual string EmailExpression
        {
            get { return "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"; }
        }

        public virtual int UserPrivilegeLevel
        {
            get { return (int)Session["UserPrivilegeLevel"]; }
        }

        public virtual RoleMenu MenuPermission
        {
            get { return this.User.GetEffectiveMenuPermission(ThisSystem, this.MenuID); }
        }

        protected override void InitializeCulture()
        {
            if (!Page.IsCallback)
            {
                if (Request["__EVENTARGUMENT"] != null)
                {
                    String lanqCode = Request["__EVENTARGUMENT"];
                    string[] words = lanqCode.Split(',');
                    if (words[0] == "language")
                    {
                        lanqCode = words[1];
                        this.ChangeLanguage(lanqCode);
                    }
                }
            }
            base.InitializeCulture();
        }

        private static string[] aspNetFormElements = new string[]
        {
            "__EVENTTARGET",
            "__EVENTARGUMENT",
            "__VIEWSTATE",
            "__EVENTVALIDATION",
            "__VIEWSTATEENCRYPTED",
        };

        protected override void Render(HtmlTextWriter writer)
        {
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
            base.Render(htmlWriter);
            string html = stringWriter.ToString();
            int formStart = html.IndexOf("<form");
            int endForm = -1;
            if (formStart >= 0)
                endForm = html.IndexOf(">", formStart);

            if (endForm >= 0)
            {
                StringBuilder viewStateBuilder = new StringBuilder();
                foreach (string element in aspNetFormElements)
                {
                    int startPoint = html.IndexOf("<input type=\"hidden\" name=\"" + element + "\"");
                    if (startPoint >= 0 && startPoint > endForm)
                    {
                        int endPoint = html.IndexOf("/>", startPoint);
                        if (endPoint >= 0)
                        {
                            endPoint += 2;
                            string viewStateInput = html.Substring(startPoint, endPoint - startPoint);
                            html = html.Remove(startPoint, endPoint - startPoint);
                            viewStateBuilder.Append(viewStateInput).Append("\r\n");
                        }
                    }
                }

                if (viewStateBuilder.Length > 0)
                {
                    viewStateBuilder.Insert(0, "\r\n");
                    html = html.Insert(endForm + 1, viewStateBuilder.ToString());
                }
            }

            writer.Write(html);
        }

        /* Page PreInit */

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //string themeName = DefaultThemeName;
            if (!Page.IsCallback)
            {
                if (TimeOut())
                    return;
            }
            //if (Page.Request.Cookies[GetThemeCookieName()] != null)
            //{
            //    themeName = HttpUtility.UrlDecode(Page.Request.Cookies[GetThemeCookieName()].Value);
            //}

            //string clientScriptBlock = "var DXCurrentThemeCookieName = \"" + GetThemeCookieName() + "\";";
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DXCurrentThemeCookieName", clientScriptBlock, true);

            //this.Theme = themeName;
            loading = new ASPxLoadingPanel()
            {
                ID = "loading",
            };
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                int menuID = this.MenuID;
                //SessionContext.Log(functionID, pageID, this.MenuID, action, message);
                //SessionContext.Log(0, 0, this.MenuID, "enter", "");
            }
            if (!IsCallback)
            {
                Response.AppendHeader("Pragma", "no-cache");
                Response.AppendHeader("Cache-Control", "no-cache");

                Response.CacheControl = "no-cache";
                Response.Expires = -1;

                Response.ExpiresAbsolute = new DateTime(1900, 1, 1);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                ASPxWebControl.RegisterBaseScript(this);
                RegisterScript("Utilities", "~/Scripts/Utilities.js");
                RegisterScript("DateUtil", "~/Scripts/DateUtil.js");
                // CSS
                RegisterCSSLink("~/CSS/styles.css");
                RegisterCSSLink("~/CSS/imStyles.css");
                if (!string.IsNullOrEmpty(this.CSSLink))
                    RegisterCSSLink(this.CSSLink);

                if (null != loading)
                {
                    loading.ClientInstanceName = "loading";
                    loading.Modal = true;
                    loading.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
                    loading.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                    loading.Text = "Processing...";
                    loading.ShowImage = true;
                    Page.Controls.Add(loading);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Title = ThisSystem.Title;
            //+ "  " + ThisSystem.Description.ToString(this.LanguageCode);
        }

        protected void RegisterScript(string key, string url)
        {
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(key))
                Page.ClientScript.RegisterClientScriptInclude(key, Page.ResolveUrl(url));
        }

        protected void RegisterCSSLink(string url)
        {
            HtmlLink link = new HtmlLink();
            Page.Header.Controls.Add(link);
            link.EnableViewState = false;
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            link.Href = url;
        }

        protected string GetThemeCookieName()
        {
            string cookieName = "DemoCurrentTheme";
            string path = Page.Request.ApplicationPath;

            int startPos = path.IndexOf("ASPx");
            if (startPos != -1)
            {
                int endPos = path.IndexOf("/", startPos);
                if (endPos != -1)
                    cookieName = path.Substring(startPos, endPos - startPos);
            }
            return cookieName;
        }

        public string ConnectionString
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["imSabayaConnectionString"].ToString();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        protected void SetButton(ASPxButton btn, String btnType, String text = null)
        {
            if ("add" == btnType)
            {
                if (!String.IsNullOrEmpty(text))
                    btn.Text = text;
                else
                    btn.Text = ResGeneral.Add;
                btn.ImageUrl = ResImageURL.Add;
            }
            if ("Questionaire" == btnType)
            {
                if (!String.IsNullOrEmpty(text))
                    btn.Text = text;
                else
                    btn.Text = ResGeneral.Questionaire;
                btn.ImageUrl = ResImageURL.QuestTionaire;
            }
        }

        protected void SetButtonAdd(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Add;
            btn.ImageUrl = ResImageURL.Add;
        }

        protected void SetButtonRefresh(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Update;
            btn.ImageUrl = ResImageURL.Refresh;
        }

        protected void SetButtonUpload(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Upload;
            btn.ImageUrl = ResImageURL.Upload;
        }

        protected void SetButtonEdit(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Edit;
            btn.ImageUrl = ResImageURL.Edit;
        }

        protected void SetButtonSave(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Save;
            btn.ImageUrl = ResImageURL.Save;
        }

        protected void SetButtonCancel(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Cancel;
            btn.ImageUrl = ResImageURL.Cross;
        }

        protected void SetButtonConfirm(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Approve;
            btn.ImageUrl = ResImageURL.Approve;
        }

        protected void SetButtonExpire(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Expire;
            btn.ImageUrl = ResImageURL.Expire;
        }

        protected void SetButtonPrevious(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Previous;
            btn.ImageUrl = ResImageURL.Previous;
        }

        protected void SetButtonClear(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Clear;
            btn.ImageUrl = ResImageURL.Clear;
        }

        protected void SetButtonBack(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Back;
            btn.ImageUrl = ResImageURL.Back;
        }

        protected void SetButtonShow(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Show;
            btn.ImageUrl = ResImageURL.Show;
        }

        protected void SetButtonNext(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Next;
            btn.ImageUrl = ResImageURL.Next;
            btn.ImagePosition = DevExpress.Web.ASPxClasses.ImagePosition.Right;
        }

        protected void SetButtonHome(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Home;
            btn.ImageUrl = ResImageURL.House;
        }

        protected void SetButtonFind(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Find;
            btn.ImageUrl = ResImageURL.Find;
        }

        protected void SetButtonHelp(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Home;
            btn.ImageUrl = ResImageURL.House;
        }

        protected void SetButtonHelp(ASPxImage img, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                img.ToolTip = text;
            else
                img.ToolTip = ResGeneral.Help;
            img.ImageUrl = ResImageURL.Help;
        }

        protected void SetButtonClose(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Close;
            btn.ImageUrl = ResImageURL.Cross;
        }

        protected void SetButtonImport(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Upload;
            btn.ImageUrl = ResImageURL.Upload;
        }

        protected void SetButtonPrint(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResImageURL.Export;
        }

        protected void SetButtonExport(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Export;
            btn.ImageUrl = ResImageURL.Export;
        }

        protected void SetButtonExportExcel(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = string.Empty;
            btn.ImageUrl = ResImageURL.Doc_Excel;
        }

        protected void SetButtonExportPDF(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = string.Empty;
            btn.ImageUrl = ResImageURL.Doc_PDF;
        }

        protected void SetButtonExportCSV(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = string.Empty;
            btn.ImageUrl = ResImageURL.CSV;
        }

        protected void SetButtonExportText(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = string.Empty;
            btn.ImageUrl = ResImageURL.Text;
        }

        protected void SetButtonApprove(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Approve;
            btn.ImageUrl = ResImageURL.Approve;
        }

        protected void SetButtonReject(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Reject;
            btn.ImageUrl = ResImageURL.Reject;
        }

        protected void SetButtonReturn(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Return;
            btn.ImageUrl = ResImageURL.Return;
        }

        protected void SetButtonValidate(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Validate;
            btn.ImageUrl = ResImageURL.Validate;
        }

        protected void SetButtonSearch(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.Search;
            btn.ImageUrl = ResImageURL.Find;
        }

        protected void SetTextbox(ASPxTextBox tbx, String validate = null)
        {
            tbx.Width = 170;
            if (!String.IsNullOrEmpty(validate))
                tbx.SetValidation(validate);
        }

        protected JsonStringValue JsonMessage(string message, string variable = null)
        {
            if (string.IsNullOrEmpty(variable))
                return new JsonStringValue("message", message);
            return new JsonStringValue(variable, message);
        }

        protected JsonBooleanValue JsonResult(bool result, string variable = null)
        {
            if (string.IsNullOrEmpty(variable))
                return new JsonBooleanValue("result", result);
            return new JsonBooleanValue(variable, result);
        }

        protected Dictionary<string, string> JSPropertiesMessage(string message, string variable = null)
        {
            Dictionary<string, string> mes = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(variable))
                mes.Add("message", message);
            else
                mes.Add(variable, message);
            return mes;
        }

        protected Dictionary<string, bool> JSPropertiesResult(bool result, string variable = null)
        {
            Dictionary<string, bool> re = new Dictionary<string, bool>();
            if (string.IsNullOrEmpty(variable))
                re.Add("result", result);
            else
                re.Add(variable, result);
            return re;
        }

        public int ParseSpinEditValueToInt(ASPxSpinEdit spinEdit)
        {
            if (spinEdit == null)
                return 0;
            if (spinEdit.Value == null)
                return 0;
            return int.Parse(spinEdit.Value.ToString());
        }

        protected virtual bool TimeOut()
        {
            if (Session["UserID"] == null || Session["MenuProvider"] == null)
            {
                Response.Redirect("~/login.aspx");
                //string path = ConvertRelativeUrlToAbsoluteUrl("~/login.aspx");
                //string scriptToRedirectUrl = "<script language='javascript'> "
                //    //+ "alert('" + path + "');"
                //    //+ "alert(window.parent.location);"
                //                        + "window.top.location.href = '" + path + "';"
                //                        + "</scr" + "ipt>";
                ////Response.Write(scriptToRedirectURL);
                //ClientScript.RegisterStartupScript(GetType(), "Load", scriptToRedirectUrl);
                return true;
            }
            return false;
        }

        public string ConvertRelativeUrlToAbsoluteUrl(string relativeUrl)
        {
            return string.Format("http{0}://{1}{2}",
                //(Request.Url.Port != null) ? string.Format(":{0}", Request.Url.Port.ToString()) : string.Empty,
                (Request.IsSecureConnection) ? "s" : string.Empty,
                string.Format("{0}:{1}", Request.Url.Host, Request.Url.Port),
                Page.ResolveUrl(relativeUrl)
            );
        }

        protected void ChangeLanguage(string languageCode)
        {
            this.Language = iSabaya.Language.FindByCode(SessionContext, languageCode);
            LoginManager.SetCurrentLanguage(SessionContext, Language);
            Session["MenuProvider"] = MenuManager.BuildMenu(SessionContext, ThisSystem.GetRootMenus(SessionContext));
            ASPxSiteMapDataSource ASPxSiteMapDataSource1 = (ASPxSiteMapDataSource)Session["ASPxSiteMapDataSource1"];
            ASPxSiteMapDataSource1.Provider = (MySiteMapProvider)Session["MenuProvider"];
        }

        protected void DisplayNoCaption(ASPxGridViewColumnDisplayTextEventArgs e, string caption)
        {
            if (e.Column.Caption == caption)
                e.DisplayText = Convert.ToString(e.VisibleRowIndex + 1);
        }

        protected void DisplayNoCaption(ASPxGridViewColumnDisplayTextEventArgs e, string caption, string text)
        {
            if (e.Column.Caption == caption)
                if (e.Value != null)
                    e.DisplayText = text;
        }
        protected static string CreateTraceInfo(StackTrace st, string message, string messageCode = "")
        {
            var stackIndent = new StringBuilder();
            for (int i = 0; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);
                //stackIndent.AppendLine(string.Format(" No : {0}", i.ToString(CultureInfo.InvariantCulture)));
                stackIndent.AppendLine(string.Format(" Method : {0} -> ", sf.GetMethod().Name));
                //stackIndent.AppendLine(string.Format(" Message : {0}:{1}", messageCode, message));
                //stackIndent.AppendLine(string.Format(" File : {0}", sf.GetFileName()));
                //stackIndent.AppendLine(string.Format(" Line Number : {0}", sf.GetFileLineNumber()));
            }
            stackIndent.AppendLine(string.Format(" Message : {0}:{1}", messageCode, message));
            return ((stackIndent.ToString().Length <= 1950) ? stackIndent.ToString() : stackIndent.ToString().Substring(0, 1950) + "...");
        }

        protected static string CreateTraceInfo(Exception exception, string messageCode = "")
        {
            var st = new StackTrace(exception);
            var stackIndent = new StringBuilder();
            for (int i = 0; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);
                //stackIndent.AppendLine(string.Format(" No : {0}", i.ToString(CultureInfo.InvariantCulture)));
                stackIndent.AppendLine(string.Format(" Method : {0} -> ", sf.GetMethod().Name));
                //stackIndent.AppendLine(string.Format(" Message : {0}:{1}", messageCode, message));
                //stackIndent.AppendLine(string.Format(" File : {0}", sf.GetFileName()));
                //stackIndent.AppendLine(string.Format(" Line Number : {0}", sf.GetFileLineNumber()));
            }
            stackIndent.AppendLine(string.Format(" Message : {0}:{1}", messageCode, exception.Message));
            return ((stackIndent.ToString().Length <= 1950) ? stackIndent.ToString() : stackIndent.ToString().Substring(0, 1950) + "...");
        }
    }
}