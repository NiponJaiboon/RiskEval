using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ton;

public partial class enc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ton.tonUtilities.pageaAuthorize(ton.config.Global_config.authtext_admin, ton.config.Global_config.warning_text);
    }
    protected void btn_encrypt_Click(object sender, EventArgs e)
    {
        try
        {
            txt_decrypt.Text = Encryption.Encrypt(txt_normal.Text, Encryption.keyword);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
    protected void btn_decrypt_Click(object sender, EventArgs e)
    {
        try
        {
            txt_normal.Text = Encryption.Decrypt(txt_decrypt.Text, Encryption.keyword);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}