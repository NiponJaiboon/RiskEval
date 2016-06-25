using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

public partial class ConfirmPopup : System.Web.UI.UserControl
{
    private ASPxButton ButtonOK
    {
        get { return (ASPxButton)imConfirm.FindControl("imConfirm_btnOK"); }
    }

    private ASPxButton ButtonCancel
    {
        get { return (ASPxButton)imConfirm.FindControl("imConfirm_btnCancel"); }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!IsPostBack)
        {
            ButtonOK.ClientSideEvents.Click = @"function(s,e)
            {
                if(runProp != null){
                    runProp();
                    runProp = null;
                }
                imConfirm_Message.SetText('');
                imConfirm.Hide();
            }";

            ButtonCancel.ClientSideEvents.Click = @"function(s,e)
            {
                if(calcelCallBack != null){
                    calcelCallBack();
                    calcelCallBack = null;
                }
                imConfirm_Message.SetText('');
                imConfirm.Hide();
            }";

            imConfirm.ClientSideEvents.PopUp = @"function(s,e)
            {
                imConfirm_btnOK.Focus();
            }";

            Literal txtScript = new Literal();
            string jsUtil = "\n<script type='text/javascript'>";
            jsUtil += @"
            var runProp = null;
            var calcelCallBack = null;

            function RunConfirm() { }
            RunConfirm.prototype.Do = function () { }

            window.confirm = function (txt, headerTxt) 
            {
                imConfirm_Message.SetText(txt);
                imConfirm_Icon.SetImageUrl('" + Page.ResolveUrl("~/images/notification.png") + @"');
                if (headerTxt == null){ headerTxt = 'ยืนยันรายการ'; }
                imConfirm.SetHeaderText(headerTxt);
                imConfirm.Show();
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