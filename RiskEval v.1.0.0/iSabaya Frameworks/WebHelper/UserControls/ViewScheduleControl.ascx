<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ViewScheduleControl" Codebehind="ViewScheduleControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td style="padding-right: 5">
            <dxe:ASPxLabel ID="lblScheduleTitle" runat="server">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxImage ID="btnViewDetail" runat="server">
            </dxe:ASPxImage>
        </td>
    </tr>
</table>
<dxpc:ASPxPopupControl ID="popSchedules" runat="server" AllowDragging="True" CloseAction="CloseButton"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    EnableAnimation="false" ShowPageScrollbarWhenModal="true">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server">
            <dxwgv:ASPxGridView ID="gridScheduleDetail" runat="server" AutoGenerateColumns="False"
                ClientInstanceName="gridScheduleDetail" KeyFieldName="ScheduleDetailID" Width="100%"
                EnableRowsCache="false" OnCustomCallback="gridScheduleDetail_CustomCallback"
                OnDataBinding="gridScheduleDetail_DataBinding">
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="คำอธิบาย" FieldName="Text" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsPager AlwaysShowPager="true" PageSize="10">
                </SettingsPager>
            </dxwgv:ASPxGridView>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>