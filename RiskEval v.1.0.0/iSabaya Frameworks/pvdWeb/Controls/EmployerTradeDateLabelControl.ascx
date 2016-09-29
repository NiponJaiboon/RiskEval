<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_EmployerTradeDateLabelControl" Codebehind="EmployerTradeDateLabelControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<table>
    <tr>
        <td align="left" nowrap="nowrap" style="padding-right:15px">
            <dx:ASPxLabel ID="labelEmployer" runat="server" Text="บริษัท">
            </dx:ASPxLabel>
        </td>
        <td align="left" nowrap="nowrap">
            <dx:ASPxLabel ID="labelTradeDate" runat="server" Text="TradeDate">
            </dx:ASPxLabel>
        </td>
    </tr>
</table>
