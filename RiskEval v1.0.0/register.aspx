<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="register.aspx.cs" Inherits="register" %>

<%@ Register Src="usercontrols/uc_register_users.ascx" TagName="uc_register_users"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    ลงทะเบียนผู้ใช้งาน
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="Server">
    ลงทะเบียนผู้ใช้งาน
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:uc_register_users ID="uc_register_users1" runat="server" />
</asp:Content>
