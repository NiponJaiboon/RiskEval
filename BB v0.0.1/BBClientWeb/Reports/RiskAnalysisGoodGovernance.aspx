﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RiskAnalysisGoodGovernance.aspx.cs" Inherits="BBClientWeb.Reports.RiskAnalysisGoodGovernance" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:reportviewer id="ReportViewer1" runat="server" width="100%" Height="1400px"
                    showprintbutton="False" keepsessionalive="false">
            </rsweb:reportviewer>
            <%--<rsweb:reportviewer id="ReportViewer1" runat="server" font-names="Verdana" width="980px" height="1080"
                    font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt"
                    zoommode="FullPage" showprintbutton="False" keepsessionalive="false">
            </rsweb:reportviewer>--%>
        </div>
    </form>
</body>
</html>
