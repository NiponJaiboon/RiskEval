<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ForLTFControl" Codebehind="ForLTFControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<dx:ASPxRoundPanel ID="rdnPanel" runat="server">
    <PanelCollection>
        <dxp:PanelContent>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lblMassage"
                            runat="server" />
                    </td>
                </tr>
            </table>
            <table style="vertical-align: top">
                <tr>
                    <td>
                        <dxe:ASPxRadioButtonList ID="rdbtnTransferToDestinationFundUponLiquidation" RepeatDirection="Vertical"
                            RepeatLayout="Flow" runat="server" ValueType="System.Boolean">
                        </dxe:ASPxRadioButtonList>
                    </td>
                </tr>
            </table>
        </dxp:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>