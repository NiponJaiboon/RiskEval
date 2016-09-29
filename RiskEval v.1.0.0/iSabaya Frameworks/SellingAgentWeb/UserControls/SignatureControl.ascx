<%@ Control Language="C#" AutoEventWireup="true" Inherits="ctrls_SignatureControl"
    CodeBehind="SignatureControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>

<table id="tbCtrl_Signature" runat="server" cellspacing="0" cellpadding="0" border="0"
    style="border-collapse: separate;">
    <tr>
        <td>
            <table cellpadding="0" border="0" cellspacing="0" width="100%" runat="server" id="tbSignature"
                style="border-collapse: collapse; empty-cells: show;">
            </table>
            <asp:HiddenField ID="hddMfAccountID" runat="server" />
        </td>
    </tr>
</table>
<dx:ASPxCallbackPanel ID="cbpEditPerson" ClientInstanceName="cbpEditPerson" runat="server"
    OnCallback="cbpEditPerson_Callback">
    <PanelCollection>
        <dx:PanelContent>
            <dx:ASPxPopupControl ID="popupEditPerson" ClientInstanceName="popupEditPerson" runat="server"
                Modal="true" Width="1000" Height="650" ShowPageScrollbarWhenModal="true" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" ShowCloseButton="true" EnableHierarchyRecreation="True"
                EnableHotTrack="False">
                <contentstyle>
                        <Paddings Padding="0px"></Paddings>
                    </contentstyle>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>