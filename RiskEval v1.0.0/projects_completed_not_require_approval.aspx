<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="projects_completed_not_require_approval.aspx.cs" Inherits="projects_completed_not_require_approval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ  โครงการกรอกสมบูรณ์ที่ไม่อยู่ในข่ายที่ต้องวิเคราะห์ความเสี่ยงฯ  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
   โครงการกรอกสมบูรณ์ที่ไม่อยู่ในข่ายที่ต้องวิเคราะห์ความเสี่ยงฯ 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:GridView ID="dgProject" runat="server" AutoGenerateColumns="false" OnRowDataBound="dgProject_OnRowDataBound">
        <Columns>
           
            <asp:HyperLinkField DataTextField ="pj_name" DataNavigateUrlFields ="pj_id" DataNavigateUrlFormatString ="redirectProjects.aspx?pj_id={0}&fm=notReqApproval" HeaderText="ชื่อโครงการ"  ItemStyle-Width="350px"/>
            <asp:BoundField DataField="pj_category" HeaderText="ลักษณะโครงการ" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="pj_budget" HeaderText="วงเงินงบประมาณ" ItemStyle-HorizontalAlign="Center" />    
            <asp:BoundField DataField="pj_lastupdate" HeaderText="วันที่แก้ไขครั้งสุดท้าย" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ผลการประเมินการวิเคราะห์ความเสี่ยง"  ItemStyle-HorizontalAlign="Center"  Visible="false"/>
            <asp:BoundField DataField="pj_filter_q1"  Visible="false" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="pj_filter_q2"  Visible="false" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="pj_filter_q3"  Visible="false" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="pj_filter_q4"  Visible="false" ItemStyle-HorizontalAlign="Center"  />
     
        </Columns>

          <EmptyDataTemplate>
            <asp:Label ID="lblErr" runat="server" CssClass="question-error" Text="ยังไม่มีโครงการของขั้นตอนนี้ในระบบ"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>