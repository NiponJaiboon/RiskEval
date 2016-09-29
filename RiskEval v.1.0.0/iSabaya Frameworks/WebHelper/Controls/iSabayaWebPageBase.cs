using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using imSabaya;
using iSabaya;

namespace WebHelper
{
    public abstract class iSabayaWebPageBase : System.Web.UI.Page
    {
        public const string DefaultThemeName = "PlasticBlue";
        private string cssLink = "";

        protected string CSSLink
        {
            get { return cssLink; }
            set { cssLink = value; }
        }

        private imSabayaContext context;

        public virtual imSabayaContext iSabayaContext
        {
            get
            {
                if (null == context)
                {
                    context = new imSabayaContext(PersistenceLayer.WebSessionManager.PersistenceSession);
                    if (null == Session["UserID"])
                        context.UserID = 0;
                    else
                        context.UserID = (int)Session["UserID"];
                }
                return context;
            }
        }

        private User user = null;

        public virtual new User User
        {
            get
            {
                if (Session["UserID"] == null)
                    Response.Redirect("~/login.aspx");
                int userID = (int)Session["UserID"];
                if (null == this.user || user.UserID != userID)
                {
                    iSabayaContext.UserID = userID;
                    this.user = iSabayaContext.User;
                }
                return this.user;
            }
            set
            {
                if (null != value)
                {
                    this.user = value;
                    Session["UserID"] = value.UserID;
                    iSabayaContext.User = value;
                }
            }
        }

        public virtual long UserSessionID
        {
            get
            {
                if (Session["UserSessionID"] == null)
                    Response.Redirect("~/login.aspx");
                return (long)Session["UserSessionID"];
            }
            set
            {
                Session["UserSessionID"] = value;
            }
        }

        private Currency currency;

        public virtual Currency Currency
        {
            get
            {
                if (null != this.currency)
                    return this.currency;
                int id = Session["CurrencyID"] != null ? (int)Session["CurrencyID"] : 0;
                if (0 == id)
                    this.Currency = iSabayaContext.CurrentCurrency;
                else
                    this.Currency = iSabayaContext.PersistencySession.Get<Currency>(id);
                return this.currency;
            }
            set
            {
                if (null != value)
                {
                    this.currency = value;
                    Session["CurrencyID"] = value.ID;
                }
            }
        }

        private Language language;

        public virtual Language Language
        {
            get
            {
                if (null != this.language)
                    return this.language;
                String code = (String)Session["LanguageCode"];
                if (String.IsNullOrEmpty(code))
                    this.Language = iSabayaContext.CurrentLanguage;
                else
                    this.Language = iSabayaContext.PersistencySession.Get<Language>(code);
                return this.language;
            }
            set
            {
                if (null != value)
                {
                    this.language = value;
                    iSabayaContext.CurrentLanguage = value;
                    Session["LanguageCode"] = value.Code;
                }
            }
        }

        public virtual int MenuID
        {
            get
            {
                MySiteMapProvider provider = Session["MenuProvider"] as MySiteMapProvider;
                if (provider == null || provider.CurrentNode == null) return 0;
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

        public virtual String LanguageCode
        {
            get
            {
                String code = (String)Session["LanguageCode"];
                if (String.IsNullOrEmpty(code))
                    Session["LanguageCode"] = code = this.Language.Code;
                return code;
            }
            set
            {
                this.Language = iSabayaContext.PersistencySession.Get<Language>(value);
            }
        }

        private Country country;

        public virtual Country Country
        {
            get
            {
                if (null != this.country)
                    return this.country;
                int id = (int)Session["CountryID"];
                if (0 == id)
                    return null;
                this.country = iSabayaContext.PersistencySession.Get<Country>(id);
                return this.country;
            }
            set
            {
                if (null != value)
                {
                    this.country = value;
                    Session["CountryID"] = value.CountryID;
                }
            }
        }

        public virtual String DateOutputFormat
        {
            get { return iSabayaContext.imSabayaConfig.PF.DateOutputFormat; }
        }

        public virtual String DateTimeOutputFormat
        {
            get { return "dd MMM yyyy HH:mm:ss"; }
        }

        public virtual String DateInputFormat
        {
            get { return iSabayaContext.imSabayaConfig.PF.DateInputFormat; }
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

        /* Page PreInit */

        protected void Page_PreInit(object sender, EventArgs e)
        {
            string themeName = DefaultThemeName;
            if (Page.Request.Cookies[GetThemeCookieName()] != null)
            {
                themeName = HttpUtility.UrlDecode(Page.Request.Cookies[GetThemeCookieName()].Value);
            }

            string clientScriptBlock = "var DXCurrentThemeCookieName = \"" + GetThemeCookieName() + "\";";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DXCurrentThemeCookieName", clientScriptBlock, true);

            this.Theme = themeName;
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
            btn.ImageUrl = ResImageURL.Cancel;
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
                btn.Text = ResGeneral.ExportExcel;
            btn.ImageUrl = ResImageURL.Doc_Excel;
        }

        protected void SetButtonExportPDF(ASPxButton btn, String text = null)
        {
            if (!String.IsNullOrEmpty(text))
                btn.Text = text;
            else
                btn.Text = ResGeneral.ExportPDF;
            btn.ImageUrl = ResImageURL.Doc_PDF;
        }

        protected void SetTextbox(ASPxTextBox tbx, String validate = null)
        {
            tbx.Width = 170;
            if (!String.IsNullOrEmpty(validate))
                tbx.SetValidation(validate);
        }
    }
}