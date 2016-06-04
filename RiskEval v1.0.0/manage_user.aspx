<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="manage_user.aspx.cs" Inherits="manage_user" %>

<%@ Register Src="usercontrols/manager_user.ascx" TagName="manager_user" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="Server">
    <div id="manage_user_tile"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:manager_user ID="manager_user1" runat="server" />
    <input type="hidden" id="user_role_id" runat="server" />
    <script type="text/javascript">
        $("#manage_user_tile").html("จัดการข้อมูลผู้ใช้");

        if ($("#ctl00_ContentPlaceHolder1_user_role_id").val() === "3") {
            $("#manage_user_tile").html("ข้อมูลผู้ใช้");
            $("#mangeuser_action").hide();
            var gridrows = $("#ctl00_ContentPlaceHolder1_manager_user1_gv_uers tbody tr");
            if (gridrows) {
                for (var i = 0; i < gridrows.length; i++) {
                    if (!gridrows[i].cells[0]) {
                        continue;
                    }
                    //gridrows[i].cells[0].style.display = "none";
                    if ($(gridrows[i].cells[0]).find('input[type=checkbox]').length > 0) {
                        $(gridrows[i].cells[0]).find('input[type=checkbox]').attr("disabled", true);
                    }
                }
            }
        }
    </script>
</asp:Content>
