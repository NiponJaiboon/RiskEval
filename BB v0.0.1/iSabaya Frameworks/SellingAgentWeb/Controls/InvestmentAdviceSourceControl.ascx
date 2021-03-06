<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="InvestmentAdviceSourceControl" Codebehind="InvestmentAdviceSourceControl.ascx.cs" %>
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
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="ctrlWebHelper" %>
<dx:ASPxRoundPanel ID="rdnPanel" runat="server">
    <PanelCollection>
        <dxp:PanelContent>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lbl1" Text="ข้าพเจ้า" runat="server" />
                        <span style="color: Red">* </span>
                    </td>
                    <td>
                        <ctrlWebHelper:CategoryControl runat="server" ID="ctrlCategory">
                        </ctrlWebHelper:CategoryControl>
                    </td>
                </tr>
            </table>
        </dxp:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>