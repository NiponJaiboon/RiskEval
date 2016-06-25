<%@ Control Language="C#" AutoEventWireup="true" Inherits="ConfirmPopup" CodeBehind="ConfirmPopup.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<table width="600px" cellpadding="0" cellspacing="0" border="0" style="padding: 0 0 0 0;
    margin: 0 0 0 0">
    <tr>
        <td>
            <dx:ASPxPopupControl ID="imConfirm" ClientInstanceName="imConfirm" runat="server"
                ShowCloseButton="false" AllowDragging="true" CloseAction="None" PopupHorizontalAlign="WindowCenter"
                ShowFooter="true" Modal="true" PopupVerticalAlign="WindowCenter" EnableAnimation="false"
                AllowResize="true" Width="350px">
                <ContentCollection>
                    <dx:PopupControlContentControl>
                        <table>
                            <tr>
                                <td valign="top">
                                    <dxe:ASPxImage ID="imConfirm_Icon" ClientInstanceName="imConfirm_Icon" runat="server">
                                    </dxe:ASPxImage>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="imConfirm_Message" ClientInstanceName="imConfirm_Message" runat="server"
                                        EnableViewState="false">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <FooterTemplate>
                    <div align="center">
                        <table align="center">
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="imConfirm_btnOK" runat="server" Text="ยืนยัน" Width="80px" AutoPostBack="false"
                                        ClientInstanceName="imConfirm_btnOK">
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="imConfirm_btnCancel" runat="server" Text="ยกเลิก" Width="80px"
                                        AutoPostBack="false" ClientInstanceName="imConfirm_btnCancel">
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </FooterTemplate>
                <ModalBackgroundStyle Opacity="0" />
            </dx:ASPxPopupControl>
        </td>
    </tr>
</table>
