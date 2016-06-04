<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="register_staff.aspx.cs" Inherits="register_staff" %>

<%@ Register Src="usercontrols/uc_register_staff.ascx" TagName="uc_register_staff"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    <asp:Literal ID="lit_title" runat="server" Text="ลงทะเบียนผู้ใช้งาน(สำนักงบประมาณ)"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="Server">
    <asp:Literal ID="lit_content_title" runat="server" Text="ลงทะเบียนผู้ใช้งาน(สำนักงบประมาณ)"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:uc_register_staff ID="uc_register_staff1" runat="server" />
</asp:Content>
