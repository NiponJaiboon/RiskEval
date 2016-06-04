<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="project_submitted_list.aspx.cs" Inherits="project_submitted_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล โครงการที่เข้าข่ายการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="Server">
    โครงการที่เข้าข่ายการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="cph1">
        <div class="accountInfo">
            <fieldset class="login">
                <legend class="bold1">ค้นหาโครงการ</legend>
                <table style="margin: 0px 0px 0px 0px;">
                    <tr>
                        <td>
                            เลือกหน่วยงาน
                            <asp:DropDownList ID="ddlDept" runat="server" DataTextField="d_name" DataValueField="d_id"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr id="tr_ddlProjectYear" style="display: none;">
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
        <div style="margin-top: 10px">
            <asp:GridView DataKeyNames="pj_id" ID="GridView1" runat="server" AutoGenerateColumns="False"
                CssClass="grid" EmptyDataText="ไม่มีข้อมูล" EmptyDataRowStyle-CssClass="warning"
                ShowFooter="True" AllowPaging="true" AllowSorting="true" PageSize="10" OnRowDataBound="GridView1_OnRowDataBound"
                OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting">
                <Columns>
                    <asp:HyperLinkField DataTextField="pj_name" DataNavigateUrlFields="pj_id" DataNavigateUrlFormatString="project_submitted_view.aspx?pjid={0}"
                        HeaderText="ชื่อโครงการ" ItemStyle-Width="350px" />
                    <asp:BoundField DataField="pj_category" HeaderText="ลักษณะโครงการ" />
                    <asp:BoundField DataField="pj_budget" HeaderText="งบประมาณ (บาท)" />
                    <asp:BoundField DataField="pj_lastupdate" HeaderText="วันที่แก้ไขครั้งสุดท้าย" />
                    <asp:BoundField HeaderText="ผลการการวิเคราะห์ความเสี่ยง" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <FooterStyle CssClass="gridFooter" />
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridPager" HorizontalAlign="Center" />
                <RowStyle CssClass="gridRow" />
            </asp:GridView>
        </div>
        <div>
            <asp:Literal ID="lblResult" runat="server"></asp:Literal>
        </div>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
        </asp:SqlDataSource>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#cph1").find("#ctl00_ContentPlaceHolder1_ddlDept").val()) {
                $("#cph1").find("#tr_ddlProjectYear").show();
            }
        });
    </script>
</asp:Content>
