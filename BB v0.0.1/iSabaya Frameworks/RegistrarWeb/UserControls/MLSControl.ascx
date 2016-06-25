<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebHelper.Controls.UserControls.MLSControl" EnableViewState="true" Codebehind="MLSControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<dxpc:ASPxPopupControl ID="pppHint" runat="server" ShowHeader="false" Width="70px" EncodeHtml="false"
 EnableViewState="False" PopupHorizontalAlign="OutsideRight" PopupVerticalAlign="TopSides"
 EnableHotTrack="False" PopupHorizontalOffset="5"  >
    <ContentStyle BackColor="#FFFBBA" ForeColor="#916E04">
        <Paddings PaddingBottom="12px" PaddingLeft="14px" PaddingRight="14px" PaddingTop="12px" />
        <Border BorderWidth="0px" />
    </ContentStyle>
    <Border BorderColor="#FFC800" BorderStyle="Solid" BorderWidth="1px" />
</dxpc:ASPxPopupControl>

<table id="tab" runat="server" cellpadding="0" cellspacing="0" style="">
</table>
