<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="projectlist.aspx.cs" Inherits="project_all_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
โครงการที่ยังไม่ส่งผลการวิเคราะห์ความเสี่ยง
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" Runat="Server">
    <link href="css/report.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" Runat="Server">
โครงการที่ยังไม่ส่งผลการวิเคราะห์ความเสี่ยง
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:GridView ID="gvProject" runat="server" Width="100%" CssClass="gvStyle" 
        DataKeyNames="pj_id"
        AutoGenerateColumns="False" 
        AllowPaging="True" 
        PageSize="10" HeaderStyle-CssClass="gv-header"
        RowStyle-CssClass="gv-rows"
        onpageindexchanging="gvProject_PageIndexChanging" onrowcommand="gvProject_RowCommand"  
        >
        <RowStyle CssClass="gv-rows"/>
        <Columns>
                <asp:TemplateField HeaderText="ชื่อโครงการ"  >
                <ItemTemplate>
                    <asp:Label ID="lblTitle" runat="server" Text='<%#(Eval("pj_name"))%>'></asp:Label>
                    <asp:LinkButton ID="lkProject" runat="server" Text='<%#(Eval("pj_name"))%>' 
                    CommandName="Select" 
                    CommandArgument='<%#(Eval("pj_id"))%>'></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle ForeColor="White" />
                <ItemStyle HorizontalAlign="Left" CssClass="gv-item" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ลักษณะโครงการ"  >
                <ItemTemplate>
                    <asp:Label ID="lblDesc" runat="server" Text='<%# (Eval("pj_category"))%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle ForeColor="White" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="งบประมาณ" >
                <ItemTemplate>
                    <asp:Label ID="lblbudget" runat="server" Text='<%# (Eval("pj_budget"))%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle ForeColor="White" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="วัน/เวลา" >
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server" Text='<%# String.Format("{0:dd-MMM-yyyy}",DateTime.Parse(Eval("pj_lastupdate").ToString()))%>' ></asp:Label>
                </ItemTemplate>
                <HeaderStyle ForeColor="White" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
                            
        </Columns>
        <PagerStyle CssClass="gv-pager"/>
        <HeaderStyle CssClass="gv-header-style"/>
        <AlternatingRowStyle CssClass="gv-alternate-row"/>
    </asp:GridView>
</asp:Content>

