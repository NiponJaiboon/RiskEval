<%@ Control Language="C#" AutoEventWireup="true" Inherits="ctrls_TimeControl" Codebehind="TimeControl.ascx.cs" %>
<%@ Register Src="time.ascx" TagName="time" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<table>
    <tr>
        <td>
            <dxe:ASPxLabel ID="LabelFrom" Text="ตั้งแต่" runat="server" />
        </td>
        <td>
            <dxcp:ASPxCallbackPanel ID="cbpMinDate" runat="server" Width="200px" ClientInstanceName="cbpMinDate">
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2">
                                    <dxe:ASPxDateEdit ID="DateFrom" runat="server" ClientInstanceName="DateFrom">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            <dxcp:ASPxCallbackPanel ID="cbpMaxdate" runat="server" ClientInstanceName="cbpMaxdate"
                Width="200px">
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </td>
    </tr>
</table>
