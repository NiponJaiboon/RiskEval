<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScreeningProjectReport.aspx.cs" Inherits="BBWeb.Reports.ScreeningProjectReport" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>


<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--<div style="/*width: 1000px;*/">--%>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"  Width="920px" Height="1150" 
                Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                ZoomMode="FullPage" ShowPrintButton="False" KeepSessionAlive="false">
            </rsweb:ReportViewer>
        <%--</div>--%>
    </form>
</body>
</html>
