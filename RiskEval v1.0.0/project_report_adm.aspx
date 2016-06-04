<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="project_report_adm.aspx.cs" Inherits="project_report_adm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    รายงานผู้ใช้ระบบ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" runat="Server">
    รายงานผู้ใช้ระบบ
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 990px;">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Width="100%"
            Height="100%" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            ZoomMode="FullPage" ShowPrintButton="False" KeepSessionAlive="false" AsyncRendering="False" SizeToReportContent="False">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
