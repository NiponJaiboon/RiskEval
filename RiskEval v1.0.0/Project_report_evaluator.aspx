<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="Project_report_evaluator.aspx.cs" Inherits="Project_report_evaluator" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    รายงาน
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" runat="Server">
    รายงาน
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="cph1">
        <div class="accountInfo">
            <fieldset class="login">
                <legend class="bold1">ค้นหาโครงการ</legend>
                <table style="margin: 0px 0px 0px 0px;">
                    <tr>
                        <td>
                            เลือกปีงบประมาณ
                            <asp:DropDownList ID="ddlProjectYear" runat="server" DataTextField="pj_year" DataValueField="pj_year"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlProjectYear_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: auto;">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"
            ZoomMode="FullPage" ShowPrintButton="False" KeepSessionAlive="false"
            Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="True">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
