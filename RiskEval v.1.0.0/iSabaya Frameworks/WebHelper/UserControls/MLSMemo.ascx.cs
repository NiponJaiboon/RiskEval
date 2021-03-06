using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using iSabaya;
using WebHelper;

//using DevExpress.Web.ASPxPopupControl.PopupWindow;
namespace WebHelper.Controls.UserControls
{
    public partial class MLSMemo : iSabayaControl
    {
        #region Validation Section

        private bool isRequiredField = false;

        public bool IsRequiredField
        {
            get { return isRequiredField; }
            set { this.isRequiredField = value; }
        }

        private String validationGroup;

        /// <summary>
        /// Get or sets the group of controls for which the editor forces validation when it posts back to the server.
        /// </summary>
        public String ValidationGroup
        {
            get { return validationGroup; }
            set { this.validationGroup = value; }
        }

        #endregion Validation Section

        private String uId = "";

        public String UID
        {
            get
            {
                if (uId == null)
                    return this.ID;
                else
                {
                    if (uId.Length == 0)
                        return this.ID;
                    else
                        return uId;
                }
            }
            set
            {
                this.uId = value;
                ViewState["UID"] = this.uId;
            }
        }

        private Unit width = Unit.Pixel(170);

        public Unit Width
        {
            get { return width; }
            set { this.width = value; }
        }
        
        private Unit height = Unit.Pixel(60);

        public Unit Height
        {
            get { return height; }
            set { this.height = value; }
        }

        private bool enabled = true;

        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }

        private bool clientEnabled = true;

        public bool ClientEnabled
        {
            get { return this.clientEnabled; }
            set { this.clientEnabled = value; }
        }

        private readonly char[] EN_Chars = new char[]
    {
        ' ', '.',',','-','/','A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
        'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
        'T', 'U', 'V', 'W', 'X', 'Y', 'Z','0', '1', '2', '3',
        '4', '5', '6', '7', '8', '9'
    };
        private readonly char[] TH_Chars = new char[]
    {
        ' ', '.',',','-','/','ก','ข','ฃ','ค','ฅ','ฆ','ง',
        'จ','ฉ','ช','ซ','ฌ','ญ','ฎ','ฏ','ฐ','ฑ','ฒ',
        'ณ','ด','ต','ถ','ท','ธ','น','บ','ป','ผ','ฝ',
        'พ','ฟ','ภ','ม','ย','ร','ล','ว','ศ','ษ','ส','ห','ฬ','อ','ฮ',
        'โ','ใ','ไ','เ','แ','า','ำ','้','ุ','ู','ึ','่','ี','ะ','ฯ','ิ',
        '์','ั','็','๊','๋','ื',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    };

        private string CreateMask(int maxLength, String langCode)
        {
            StringBuilder maskExpression = new StringBuilder();
            maskExpression.Append(">");
            string charMaskExpression = CharMask(langCode);
            for (int i = 0; i < maxLength; i++)
                maskExpression.Append(charMaskExpression);
            return maskExpression.ToString();
        }

        private string CharMask(String codeLanguage)
        {
            StringBuilder maskExpression = new StringBuilder();
            maskExpression.Append('<');
            char[] arrayLanguage = null;
            if (codeLanguage == "en-US")
            {
                arrayLanguage = EN_Chars;
            }
            if (codeLanguage == "th-TH")
            {
                arrayLanguage = TH_Chars;
            }
            // if can't find language set default en-US
            else
            {
                arrayLanguage = EN_Chars;
            }
            for (int i = 0; i < arrayLanguage.Length; i++)
            {
                char allowedChar = arrayLanguage[i];
                maskExpression.Append(allowedChar);
                maskExpression.Append('|');
            }
            maskExpression.Length--;
            maskExpression.Append('>');
            return maskExpression.ToString();
        }

        private const string PREFIX_TEXTBOX = "txb_";

        public string PrefixTextBoxClientInstance
        {
            get { return string.Format("{0}{1}", this.ClientID, PREFIX_TEXTBOX); }
        }

        private List<ASPxMemo> ab;
        private List<DevExpress.Web.ASPxPopupControl.PopupWindow> pw;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControls();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IList<Language> ll = (IList<Language>)Language.Languages;
                int i = 0;
                foreach (ASPxMemo a in ab)
                {
                    //a.MaskSettings.Mask = CreateMask(100, ll[i].Code);
                    a.ErrorText = "ใช้ภาษา" + ll[i].Code;
                    a.ValidationSettings.Display = Display.Dynamic;
                    i++;
                    a.Enabled = Enabled;
                    a.ClientEnabled = ClientEnabled;
                }
            }
        }

        private string creatMask(String language, int maxLenght)
        {
            return "";
        }

        private void InitializeControls()
        {
            ab = new List<ASPxMemo>();
            pw = new List<DevExpress.Web.ASPxPopupControl.PopupWindow>();
            pppHint.ClientInstanceName = this.ClientID + pppHint.ID;
            //IList<Language> listLanguage = Language.List(iSabayaContext);
            String idName = "", idPopupName = "";
            foreach (Language l in Language.Languages)
            {
                idName = PREFIX_TEXTBOX + l.Code.ToString();
                idPopupName = "ppp_" + l.Code.ToString();
                ;
                //////////////////////////////////////////////////
                ////////////////// Text Box //////////////////////
                HtmlTableRow hr = new HtmlTableRow();
                HtmlTableCell hc = new HtmlTableCell();
                hc.Attributes.Add("style", "padding-bottom:2px; padding-top:2px;padding-right:2px;");
                ASPxMemo atb = new ASPxMemo() { };
                if (IsRequiredField)
                    atb.SetValidation(ValidationGroup);
                atb.Width = Width;
                atb.ID = idName;
                atb.ClientInstanceName = this.ClientID + idName;
                hc.Controls.Add(atb);
                hr.Cells.Add(hc);
                tab.Rows.Add(hr);
                ab.Add(atb);
                //////////////////////////////////////////////////
                ////////////////// Hint //////////////////////////
                DevExpress.Web.ASPxPopupControl.PopupWindow pww = new DevExpress.Web.ASPxPopupControl.PopupWindow();
                pww.Name = idPopupName;
                pww.Text = l.Code.ToString();
                pww.PopupElementID = idName;
                pppHint.Windows.Add(pww);
                //pw.Add(pww);
                //////////////////////////////////////////////////
                ////////////////// event /////////////////////////
                atb.ClientSideEvents.GotFocus = @"function(s,e)
            {
			    var hintWindow = " + pppHint.ClientInstanceName + @".GetWindowByName('" + pww.Name + @"');
			    " + pppHint.ClientInstanceName + @".ShowWindow(hintWindow);
            }";
                atb.ClientSideEvents.LostFocus = @"function(s,e)
            {
    			var hintWindow = " + pppHint.ClientInstanceName + @".GetWindowByName('" + pww.Name + @"');
    			" + pppHint.ClientInstanceName + @".HideWindow(hintWindow);
            }";
            }
        }

        public new MultilingualString Value
        {
            get
            {
                MultilingualString mls = new MultilingualString();
                DateTime updatedTS = DateTime.Now;
                //IList<Language> listLanguage = Language.List(iSabayaContext);
                int i = 0;
                foreach (Language lang in Language.Languages)
                {
                    MLSValue value = new MLSValue(mls, lang.Code, ab[i].Text);
                    value.UpdatedTS = updatedTS;
                    mls.Values.Add(value);
                    i++;
                }
                return mls;
            }
            set
            {
                if (value != null)
                {
                    IList<Language> languages = iSabayaContext.Languages;
                    for (int i = 0; i < languages.Count; i++)
                    {
                        ASPxTextBox txt = tab.FindControl(PREFIX_TEXTBOX + languages[i].Code) as ASPxTextBox;
                        if (txt != null)
                            txt.Text = value.GetValue(languages[i].Code);
                    }
                }
                else
                {
                    Clear();
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < ab.Count; i++)
                ab[i].Text = string.Empty;
        }
    }
}