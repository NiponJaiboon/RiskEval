<%@ Control Language="C#" AutoEventWireup="True"
    Inherits="WebHelper.UserControls.TimeIntervalControl" Codebehind="TimeIntervalControl.ascx.cs" %>
<%@ Register Src="time.ascx" TagName="time" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="uc2" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td id="tdLabelFrom" runat="server" style="white-space: nowrap">
            <dxe:ASPxLabel ID="lblFrom" Text="ตั้งแต่ : " runat="server" />
        </td>
        <td style="padding-right: 5px;">
            <uc2:DateTimeControl ID="DateFrom" runat="server" />
        </td>
        <td>
            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnMindate"
                Text="Min" ID="btnMindate" Visible="False">
                <ClientSideEvents Click="function(s, e){
                                cbpMinDate.PerformCallback();
                            }"></ClientSideEvents>
            </dxe:ASPxButton>
        </td>
        <td id="tdLabelTo" runat="server" style="white-space: nowrap">
            <dxe:ASPxLabel ID="lblTo" Text="ถึง : " runat="server" />
        </td>
        <td id="tdLabelSym" runat="server" style="white-space: nowrap">
            <dxe:ASPxLabel ID="lblToSym" Text="~" runat="server" />
        </td>
        <td style="padding-left: 5px;">
            <uc2:DateTimeControl ID="DateTo" runat="server" />
        </td>
        <td>
            <dxe:ASPxButton ID="btnMaxdate" runat="server" AutoPostBack="False" ClientInstanceName="btnMaxdate"
                Text="MAX" Visible="False">
            </dxe:ASPxButton>
        </td>
    </tr>
</table>