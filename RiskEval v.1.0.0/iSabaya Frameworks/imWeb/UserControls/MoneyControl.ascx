<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_MoneyControl" Codebehind="MoneyControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td style="padding-right:5px">
            <dxe:ASPxSpinEdit ID="txtAmount" runat="server" Height="21px" Number="0" DisplayFormatString="#,##0.0000" DecimalPlaces="2">
            </dxe:ASPxSpinEdit>
        </td>
        <td>
            <dxe:ASPxComboBox ID="ComboCurrency" runat="Server" Width="60px">
            </dxe:ASPxComboBox>
        </td>
    </tr>
</table>
