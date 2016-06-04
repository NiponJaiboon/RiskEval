<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="ministry_dept_info_management.aspx.cs" Inherits="ministry_dept_info_management" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล จัดการข้อมูลกระทรวงและหน่วยงาน
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
จัดการข้อมูลกระทรวงและหน่วยงาน
</asp:Content>

<asp:Content ID="ContentLink" ContentPlaceHolderID="linkheader" runat="server">
<style type="text/css">
.caption_main {font-size:22px; color:Blue; padding-top:10px; padding-bottom:10px; font-weight:normal;}
.caption_sub1 {font-size:22px; color:Purple; padding-bottom:5px;}
.caption_sub2 {font-size:22px; color:Green; padding-bottom:5px;}
.caption_sub3 {font-size:22px; color:Red; padding-bottom:30px;}
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div>
       <asp:HyperLink ID="hypMin" runat="server" NavigateUrl="ministry_info_management.aspx" Text="จัดการข้อมูลกระทรวง"></asp:HyperLink><br />
       <asp:HyperLink ID="hypDept" runat="server" NavigateUrl="dept_info_management.aspx" Text="จัดการข้อมูลหน่วยงาน"></asp:HyperLink>
    </div>

</asp:Content>

