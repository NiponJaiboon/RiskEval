<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loveistheworld.aspx.cs" Inherits="loveistheworld" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="js/jquery-1.7.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mojo" runat="server">
    
        <asp:TextBox ID="txt_com" runat="server" Height="279px" 
             TextMode="MultiLine" Width="779px"></asp:TextBox>
    
        <br />
        <asp:RadioButtonList ID="rad_cmd_type" runat="server" 
            RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Value="0">GetData</asp:ListItem>
            <asp:ListItem Value="1">Non-Query</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Exec" 
            Width="137px" />
&nbsp;&nbsp;&nbsp;
        <br />
        OutPut:<asp:Label ID="lbl_result" runat="server" BorderColor="#FF3300" 
            BorderStyle="Solid" ForeColor="#003300" Text="Label"></asp:Label>
        <br />
        <br />
        GRIDVIEW:<br />
        <asp:GridView ID="grd_temp" runat="server">
        </asp:GridView>
        <br />
        Error From:<br />
        <asp:Label ID="lbl_error" runat="server" BorderColor="#FF3300" 
            BorderStyle="Solid" Text="Label"></asp:Label>
    
    </div>
    </form>
</body>
</html>
