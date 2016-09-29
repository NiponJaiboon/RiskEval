<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_MonthlyControl" Codebehind="MonthlyControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%--<table border="1" cellpadding="0" cellspacing="0" >
    <tr>
        <td align="right">
            <dx:ASPxTextBox ID="txtMonth" runat="server">
            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" />
            </dx:ASPxTextBox>
        </td>
        <td align="left">
            <dx:ASPxTextBox ID="txtYear" runat="server">
            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" />
            </dx:ASPxTextBox>
        </td >
    </tr>
</table>--%>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td align="right">
            <dx:ASPxSpinEdit ID="spnMonth" runat="server" Width="42px">
            </dx:ASPxSpinEdit>
        </td>
        <td align="left">
            <dx:ASPxSpinEdit ID="spnYear" runat="server" Width="65px">
            </dx:ASPxSpinEdit>
        </td>
    </tr>
</table>