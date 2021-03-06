<%@ Control Language="C#" AutoEventWireup="true" Inherits="RiskProfileDescriptionControl"
    CodeBehind="RiskProfileDescriptionControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
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
<table id="tbShowRiskLevel" runat="server" cellpadding="0" cellspacing="0" class="tbFormbase">
    <tr>
        <td id="tdRiskDetail1" runat="server" align="center">
            <dxe:ASPxLabel ID="ASPxLabel43" runat="server">
            </dxe:ASPxLabel>
        </td>
        <td align="center" style="width: 150">
            <dxe:ASPxLabel ID="lblRiskScore" runat="server">
            </dxe:ASPxLabel>
        </td>
        <td id="tdRiskDetail2" runat="server">
            <dxe:ASPxLabel ID="ASPxLabel44" runat="server">
            </dxe:ASPxLabel>
        </td>
        <td align="center" style="width: 200">
            <table cellpadding="0" cellspacing="0" class="tbFormbase">
                <tr>
                    <td align="center">
                        <dxe:ASPxLabel ID="lblRiskLevel" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td align="center" style="padding-left: 5px">
                        <dxe:ASPxImage ID="imgViewDescription" Cursor="pointer" runat="server">
                        </dxe:ASPxImage>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<dx:ASPxPopupControl ID="popViewDescription" runat="server" AllowDragging="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="800"
    CloseAction="CloseButton">
    <ContentCollection>
        <dx:PopupControlContentControl>
            <table id="tbDescription" runat="server" cellpadding="0" cellspacing="0">
                <tr id="trLevelHead" runat="server">
                    <td style="width: 250" align="center">
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="width: 200" align="center">
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="width: 50;" align="center">
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="width: 100" align="center">
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="width: 300" align="center">
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <%--<td>
            <dxe:ASPxLabel ID="ASPxLabel6" runat="server">
            </dxe:ASPxLabel>
        </td>--%>
                </tr>
                <tr id="trLevel1" runat="server" style="display: none">
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td align="center">
                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="1">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <%--<td>
            <dxe:ASPxLabel ID="ASPxLabel12" runat="server">
            </dxe:ASPxLabel>
        </td>--%>
                </tr>
                <tr id="trLevel2" runat="server" style="display: none">
                    <td rowspan="3">
                        <dxe:ASPxLabel ID="ASPxLabel13" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td rowspan="3">
                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td align="center">
                        <dxe:ASPxLabel ID="ASPxLabel15" runat="server" Text="2">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel16" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel17" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr id="trLevel2_1" runat="server" style="display: none">
                    <td align="center">
                        <dxe:ASPxLabel ID="ASPxLabel18" runat="server" Text="3">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel19" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr id="trLevel2_2" runat="server" style="display: none">
                    <td align="center">
                        <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="4">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel22" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel23" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr id="trLevel3" runat="server" style="display: none">
                    <td rowspan="2">
                        <dxe:ASPxLabel ID="ASPxLabel24" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td rowspan="2">
                        <dxe:ASPxLabel ID="ASPxLabel25" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td align="center">
                        <dxe:ASPxLabel ID="ASPxLabel26" runat="server" Text="5">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel27" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel28" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr id="trLevel3_1" runat="server" style="display: none">
                    <td align="center">
                        <dxe:ASPxLabel ID="ASPxLabel29" runat="server" Text="6">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel30" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel31" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr id="trLevel4" runat="server" style="display: none">
                    <td rowspan="2">
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td rowspan="2">
                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td align="center">
                        <dxe:ASPxLabel ID="ASPxLabel32" runat="server" Text="7">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel33" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel34" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr id="trLevel4_1" runat="server" style="display: none">
                    <td align="center">
                        <dxe:ASPxLabel ID="ASPxLabel35" runat="server" Text="8">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel36" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel37" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr id="trLevel5" runat="server" style="display: none">
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel38" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel39" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td align="center">
                        <dxe:ASPxLabel ID="ASPxLabel40" runat="server" Text="9">
                        </dxe:ASPxLabel>
                        <span style="color: Red">* </span>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel41" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel42" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <%--<td>
            <dxe:ASPxLabel ID="ASPxLabel43" runat="server">
            </dxe:ASPxLabel>
        </td>--%>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>