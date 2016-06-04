<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="project_for_budgetor.aspx.cs" Inherits="project_for_budgetor" Culture="th-TH"
    UICulture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" runat="Server">
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" runat="Server">
    <asp:Literal ID="ltr_contentTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnl_filter" runat="server" GroupingText="ค้นหาโครงการ">
        <asp:Panel ID="pnl_filter_approve" runat="server">
            <asp:CheckBoxList ID="cbklist_approve" runat="server" RepeatDirection="Horizontal">
            </asp:CheckBoxList>
        </asp:Panel>
        ค้นหาโครงการแยกตามหน่วยงาน
        <asp:DropDownList ID="ddl_department" runat="server" DataSourceID="sds_department"
            DataTextField="d_desc" DataValueField="d_id">
            <asp:ListItem Value="%%">ทั้งหมด</asp:ListItem>
        </asp:DropDownList>
        <br />
        เลือกปีงบประมาณ
        <asp:DropDownList ID="ddlProjectYear" runat="server" DataTextField="pj_year" DataValueField="pj_year">
        </asp:DropDownList>
        <br />
        <asp:Button ID="btn_search" runat="server" Text="ค้นหา" OnClick="btn_search_Click" />
    </asp:Panel>
    <br />
    <asp:GridView ID="grd_project_list" runat="server" AutoGenerateColumns="False" DataSourceID="sds_project_list"
        EnableModelValidation="True" AllowPaging="True" OnRowDataBound="grd_project_list_RowDataBound"
        EmptyDataText="ไม่มีข้อมูล" ShowFooter="True" AllowSorting="True" CssClass="grid">
        <AlternatingRowStyle CssClass="gridAltRow" />
        <Columns>
            <asp:BoundField DataField="pj_id" HeaderText="pj_id" InsertVisible="False" ReadOnly="True"
                SortExpression="pj_id" Visible="false">
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="pj_code" HeaderText="pj_code" SortExpression="pj_code"
                Visible="false">
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="ชื่อโครงการ" SortExpression="pj_name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("pj_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbt_proj_name" runat="server" OnClick="lbt_proj_name_Click" Text='<%# Server.HtmlEncode(Eval("pj_name").ToString())  %>'
                        CommandArgument='<%# Eval("pj_id").ToString()  %>'></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="pj_category" HeaderText="ลักษณะโครงการ" SortExpression="pj_category">
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="d_id" HeaderText="รหัสหน่วยงาน" SortExpression="d_id" />
            <asp:BoundField DataField="pj_budget_money" HeaderText="วงเงินงบประมาณ" SortExpression="pj_budget_money"
                DataFormatString="{0:N}">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="pj_approval_status" HeaderText="สถานะโครงการ" SortExpression="pj_approval_status"
                Visible="False">
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="pj_approval_budget" HeaderText="วงเงินที่ได้รับการอนุมัติจากรัฐสภา"
                SortExpression="pj_approval_budget" Visible="False" DataFormatString="{0:N}">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="pj_lastupdate" HeaderText="วัน / เวลา" SortExpression="pj_lastupdate"
                DataFormatString="{0:dd/MM/yyyy hh:mm น.}">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="pj_type" HeaderText="pj_type" SortExpression="pj_type"
                Visible="false">
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="ผลการวิเคราะห์ ความเสี่ยง">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("sum_yes_percent") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal ID="lit_yes_percent" runat="server" Text='<%# Eval("sum_yes_percent") %>'></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle CssClass="gridFooter" />
        <HeaderStyle CssClass="gridHeader" />
        <PagerStyle CssClass="gridPager" HorizontalAlign="Center" />
        <RowStyle CssClass="gridRow" />
    </asp:GridView>
    <asp:SqlDataSource ID="sds_project_list" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
        SelectCommand="SELECT DISTINCT persons_detail.p_id, projects.pj_id, projects.pj_code, projects.pj_name, projects.pj_year, projects.pj_budget_money, projects.pj_type, projects.pj_category, projects.pj_lastupdate, projects.pj_approval_status, projects.pj_complete_status, projects.pj_status, tamma_total.sum_yes_percent, projects.d_id, department.d_name, projects.pj_approval_budget, projects.pj_filter_q1, projects.pj_filter_q2, projects.pj_filter_q3, projects.pj_filter_q4  FROM persons_detail INNER JOIN projects ON persons_detail.d_id = projects.d_id INNER JOIN department ON projects.d_id = department.d_id LEFT OUTER JOIN (SELECT pj_id, SUM(yes_percent) AS sum_yes_percent FROM report_tamma_total GROUP BY pj_id) AS tamma_total ON projects.pj_id = tamma_total.pj_id WHERE (persons_detail.p_id = @p_id) AND (persons_detail.pdt_is_delete &lt;&gt; 1) AND (ISNULL(projects.pj_approval_status, N'') LIKE @approval_status) AND (projects.pj_complete_status = N'ส่งผลแล้ว') AND (projects.pj_status = N'real') AND (projects.d_id LIKE @d_id) and ((@pj_year = N'') or (@pj_year != N'' and projects.pj_year = @pj_year))  ORDER BY projects.pj_lastupdate DESC, projects.pj_name">
        <SelectParameters>
            <asp:Parameter Name="p_id" />
            <asp:Parameter Name="approval_status" ConvertEmptyStringToNull="false" />
            <asp:Parameter Name="d_id" />
            <asp:Parameter Name="pj_year" ConvertEmptyStringToNull="false" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sds_department" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
        SelectCommand="SELECT '%%' AS d_id, N'ทั้งหมด' AS d_desc UNION ALL SELECT DISTINCT projects.d_id, projects.d_id + ':' + department.d_name AS d_desc FROM persons_detail INNER JOIN projects ON persons_detail.d_id = projects.d_id INNER JOIN department ON projects.d_id = department.d_id WHERE (persons_detail.p_id = @p_id) AND (persons_detail.pdt_is_delete &lt;&gt; 1) AND (ISNULL(projects.pj_approval_status, N'') LIKE @approval_status) AND (projects.pj_complete_status = N'ส่งผลแล้ว') ORDER BY d_id">
        <SelectParameters>
            <asp:Parameter Name="p_id" />
            <asp:Parameter Name="approval_status" ConvertEmptyStringToNull="false" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
