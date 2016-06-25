﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SummaryRiskAnalysisGoodGovernance.aspx.cs" Inherits="BBAnalysisWeb.Reports.SummaryRiskAnalysisGoodGovernance" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--<div style="/*width: 1000px;*/">--%>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"  Width="980px" Height="4900" ShowZoomControl="true" ZoomPercent="100"
                Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                ZoomMode="FullPage" ShowPrintButton="False" KeepSessionAlive="false">
            </rsweb:ReportViewer>
        <%--</div>--%>
    </form>
</body>
</html>
