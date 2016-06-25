<%@ Control Language="C#" AutoEventWireup="true" Inherits="AlertPopup" Codebehind="AlertPopup.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<table width="600px" cellpadding="0" cellspacing="0" border="0" style="padding:0 0 0 0; margin:0 0 0 0">
    <tr>
        <td>
            <dx:ASPxPopupControl ID="imAlert" ClientInstanceName="imAlert" runat="server" ShowCloseButton="false"
                AllowDragging="true" CloseAction="None" PopupHorizontalAlign="WindowCenter" ShowFooter="true"
                Modal="true" PopupVerticalAlign="WindowCenter" EnableAnimation="false" AllowResize="true" Width="350px">
                <ContentCollection>
                    <dx:PopupControlContentControl>
                        <table >
                            <tr>
                                <td valign="top">
                                    <dxe:ASPxImage ID="imAlert_Icon" ClientInstanceName="imAlert_Icon" runat="server">
                                    </dxe:ASPxImage>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="imAlert_Message" ClientInstanceName="imAlert_Message" runat="server"
                                        EnableViewState="false">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <FooterTemplate>
                    <table align="center">
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="imAlert_btnOK" runat="server" Text="OK" Width="80px" AutoPostBack="false"
                                    ClientInstanceName="imAlert_btnOK">
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </FooterTemplate>
                <ModalBackgroundStyle Opacity="0" />
            </dx:ASPxPopupControl>
        </td>
    </tr>
</table>
