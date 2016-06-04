<%@ Page Language="C#" AutoEventWireup="true" CodeFile="enc.aspx.cs" Inherits="enc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Text:
        <asp:TextBox ID="txt_normal" runat="server" Width="372px"></asp:TextBox>
        <br />
        Encrypt:<asp:TextBox ID="txt_decrypt" runat="server" Height="112px" 
            TextMode="MultiLine" Width="360px"></asp:TextBox>
        <br />
        <asp:Button ID="btn_encrypt" runat="server" onclick="btn_encrypt_Click" 
            Text="Encrypt" />
        <asp:Button ID="btn_decrypt" runat="server" onclick="btn_decrypt_Click" 
            Text="Decrypt" />
    
    </div>
    </form>
</body>
</html>
