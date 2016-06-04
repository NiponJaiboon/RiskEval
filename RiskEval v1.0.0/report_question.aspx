<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Report.master" AutoEventWireup="true" CodeFile="report_question.aspx.cs" Inherits="report_question" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" Runat="Server">
รายงาน
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width:900px;">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Width="900" Height="900"
        Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
            ZoomMode="FullPage">     
    </rsweb:ReportViewer>
  </div>
</asp:Content>

