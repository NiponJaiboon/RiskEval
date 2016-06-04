<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="check_project_report.aspx.cs" Inherits="check_project_report" %>

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
    <script type="text/javascript">
        //    window.onload = function () {
        //        var formatDropDown = document.getElementById('ctl00_ContentPlaceHolder1_ReportViewer1_ctl05_ctl04_ctl00_Menu');
        //        //var formats = formatDropDown.childNodes;
        //        if (formatDropDown != null) {
        //             
        //            formatDropDown.removeChild(formats[0]);
        //            formatDropDown.removeChild(formats[1]);
        //            formatDropDown.removeChild(formats[2]);
        //        }
        //    }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 100%" id="cph1">
        <fieldset>
            <legend class="bold1">ค้นหาโครงการ</legend>
            <table>
                <tr>
                    <td>
                        เลือกกระทรวง
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="tr_ddlProjectYear" style="display: none;">
                    <td>
                        เลือกปีงบประมาณ
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProjectYear" runat="server" DataTextField="pj_year" DataValueField="pj_year"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlProjectYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <div style="width: auto;">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ZoomMode="FullPage"
                ShowPrintButton="False" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="True">
            </rsweb:ReportViewer>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#cph1").find("#ctl00_ContentPlaceHolder1_DropDownList1").val() !== "-2") {
                $("#cph1").find("#tr_ddlProjectYear").show();
            }
        });
    </script>
</asp:Content>
