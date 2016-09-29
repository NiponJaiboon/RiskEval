<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="check_project_report.aspx.cs" Inherits="BBWeb.Reports.check_project_report" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>


<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <fieldset>
            <legend class="bold1">ค้นหาโครงการ</legend>
            <table>
                <tr>
                    <td>เลือกกระทรวง
                    </td>
                    <td>
                        <asp:DropDownList ID="Ministryddl" runat="server" OnSelectedIndexChanged="Ministryddl_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                </tr>
            </table>
        </fieldset>

        <%--<div style="/*width: 1000px;*/">--%>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Width="920px" Height="2150"
            Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            ZoomMode="FullPage" ShowPrintButton="False" KeepSessionAlive="false">
        </rsweb:ReportViewer>
        <%--</div>--%>
    </form>
</body>
</html>
