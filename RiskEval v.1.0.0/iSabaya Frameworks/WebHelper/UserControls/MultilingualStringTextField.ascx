<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_MultilingualStringTextField" Codebehind="MultilingualStringTextField.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<dxpc:ASPxPopupControl SkinID="None" Width="200px" ID="pcHint" runat="server" EncodeHtml="false"
    EnableViewState="False" PopupHorizontalAlign="OutsideRight" PopupVerticalAlign="TopSides"
    ShowHeader="False" EnableHotTrack="False" PopupHorizontalOffset="5" PopupAction="None"
    CloseAction="CloseButton" PopupVerticalOffset="-10" EnableHierarchyRecreation="True">
    <Windows>
        <dxpc:PopupWindow Name="Lang1" Text="<b>ไทย</b>" PopupElementID="txtLang1">
        </dxpc:PopupWindow>
        <dxpc:PopupWindow Name="Lang2" Text="<b>English</b>" PopupElementID="txtLang1">
        </dxpc:PopupWindow>
    </Windows>
    <ContentStyle BackColor="#FFFBBA" ForeColor="#916E04">
        <Paddings PaddingBottom="12px" PaddingLeft="14px" PaddingRight="14px" PaddingTop="12px" />
        <Border BorderWidth="0px" />
    </ContentStyle>
    <Border BorderColor="#FFC800" BorderStyle="Solid" BorderWidth="1px" />
</dxpc:ASPxPopupControl>
<%--<asp:Table ID="Table1" runat="server">
</asp:Table>--%>
<table cellpadding="0" cellspacing="0">
    <tr>
        <%--<td>
            <dxe:ASPxLabel ID="lblLang1" runat="server" Text="lblLang1" />
        </td>--%>
        <td>
            <dxe:ASPxTextBox ID="txtLang1" runat="server" Width="400px">
                <%-- <ClientSideEvents LostFocus="function(s, e){SetHintVisible('Lang1', false);}"
                    GotFocus="function(s, e){SetHintVisible('Lang1', true);}" />--%>
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <%--<td>
            <dxe:ASPxLabel ID="lblLangggdehdehtetgvdtmfhfmhf,kgmjf2" runat="server" Text="lblLang2" />
        </td>--%>
        <td style="padding-top: 2px">
            <dxe:ASPxTextBox ID="txtLang2" runat="server" Width="400px" />
        </td>
    </tr>
</table>