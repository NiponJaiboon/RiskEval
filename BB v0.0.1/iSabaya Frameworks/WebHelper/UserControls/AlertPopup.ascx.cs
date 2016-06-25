using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

public partial class AlertPopup : System.Web.UI.UserControl
{
    private ASPxButton ButtonOK
    {
        get { return (ASPxButton)imAlert.FindControl("imAlert_btnOK"); }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!IsPostBack)
        {
            ButtonOK.ClientSideEvents.Click = @"function(s,e)
            {
                if(urlRec != ''){ 
                    var url = urlRec;
                    urlRec = '';
                    window.location.replace(url); 
                }
                if(alertOKActionFunc != null){
                    alertOKActionFunc();
                    alertOKActionFunc = null;
                }
                imAlert_Message.SetText('');
                imAlert.Hide();
            }";
            imAlert.ClientSideEvents.PopUp = @"function(s,e)
            {
                imAlert_btnOK.Focus();
            }";

            Literal txtScript = new Literal();
            string jsUtil = "\n<script type='text/javascript'>";
            jsUtil += @"
            alertState = '';
            var urlRec = '';
            var alertOKActionFunc = null;
            window.alert = function (txt, status, headerTxt) 
            {
                isHolding = true;
                imAlert_Message.SetText(txt);
                switch (status) {
                    case 'e':
                        imAlert_Icon.SetImageUrl('" + Page.ResolveUrl("~/images/error.png") + @"');     
                        if (headerTxt == null)
                            headerTxt = 'Error';       
                        break;
                    case 's':
                        imAlert_Icon.SetImageUrl('" + Page.ResolveUrl("~/images/check.png") + @"');
                        if (headerTxt == null)
                            headerTxt = 'Success'; 
                        break;
                    case 'i':
                        imAlert_Icon.SetImageUrl('" + Page.ResolveUrl("~/images/check.png") + @"');
                        if (headerTxt == null)
                            headerTxt = 'Information'; 
                        break;
                    default:
                        imAlert_Icon.SetImageUrl('" + Page.ResolveUrl("~/images/warning.png") + @"');
                        if (headerTxt == null)
                            headerTxt = 'Warning'; 
                        break;
                }
                imAlert.SetHeaderText(headerTxt);
                imAlert.Show();
            }";
            jsUtil += "</script>";
            txtScript.Text = jsUtil;
            Page.Header.Controls.Add(txtScript);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}