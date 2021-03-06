<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ScheduleControl" Codebehind="ScheduleControl.ascx.cs" %>
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
        <td style="padding: 2px 2px 2px 0px">
            <dxe:ASPxComboBox ID="cboSchedule" runat="server" IncrementalFilteringMode="StartsWith"
                EnableAnimation="false" AutoPostBack="false" OnDataBinding="cboSchedule_DataBinding">
            </dxe:ASPxComboBox>
        </td>
        <td>
            <dxe:ASPxLabel ID="lblScheduleTitle" runat="server">
            </dxe:ASPxLabel>
        </td>
    </tr>
</table>
<dxhf:ASPxHiddenField ID="hddSchedules" runat="server">
</dxhf:ASPxHiddenField>
<dxcb:ASPxCallback ID="cbSelectedSchedule" runat="server" OnCallback="cbSelectedSchedule_Callback">
</dxcb:ASPxCallback>
<dxpc:ASPxPopupControl ID="popSchedules" runat="server" AllowDragging="True" CloseAction="CloseButton"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    EnableAnimation="false" ShowPageScrollbarWhenModal="true">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server">
            <dxwgv:ASPxGridView ID="gdvSchedules" runat="server" KeyFieldName="ScheduleID" AutoGenerateColumns="False"
                EnableRowsCache="false" OnDataBinding="gdvSchedules_DataBinding" OnHtmlRowCreated="gdvSchedules_HtmlRowCreated"
                Width="500px">
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="Code" Settings-AutoFilterCondition="Contains"
                        Settings-FilterMode="DisplayText">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Title" Settings-AutoFilterCondition="Contains"
                        Settings-FilterMode="DisplayText">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Description" Settings-AutoFilterCondition="Contains"
                        Settings-FilterMode="DisplayText">
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewCommandColumn Caption="Action">
                        <CellStyle Wrap="False">
                        </CellStyle>
                        <CustomButtons>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnSelectGdvSchedules" Text="เลือก">
                            </dxwgv:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dxwgv:GridViewCommandColumn>--%>
                </Columns>
                <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                <SettingsBehavior ColumnResizeMode="Control" />
            </dxwgv:ASPxGridView>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>