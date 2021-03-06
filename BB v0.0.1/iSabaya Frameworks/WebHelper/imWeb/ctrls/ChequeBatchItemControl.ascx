<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_ChequeBatchItemControl" Codebehind="ChequeBatchItemControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="BankAccountComboControl.ascx" TagName="BankAccountComboControl"
    TagPrefix="uc1" %>
<dxcp:ASPxCallbackPanel ID="cbpTextChequeBatchItemEdit" runat="server" ClientInstanceName="cbpTextChequeBatchItemEdit"
    Width="200px" OnCallback="cbpTextChequeBatchItemEdit_Callback">
    <PanelCollection>
        <dxp:PanelContent runat="server">
            <dxe:ASPxButtonEdit runat="server" ClientInstanceName="textChequeBatchItemEdit" ID="textChequeBatchItemEdit">
                <ClientSideEvents ButtonClick="function(s, e) {
	var win = popupChequeBatchItem.GetWindow(0);
	popupChequeBatchItem.ShowWindow(win);
}"></ClientSideEvents>
                <Buttons>
                    <dxe:EditButton>
                    </dxe:EditButton>
                </Buttons>
            </dxe:ASPxButtonEdit>
            <dxe:ASPxLabel ID="labelChequeBatchItem" runat="server" ClientInstanceName="labelChequeBatchItem">
            </dxe:ASPxLabel>
        </dxp:PanelContent>
    </PanelCollection>
</dxcp:ASPxCallbackPanel>
<dxpc:ASPxPopupControl ID="popupChequeBatchItem" runat="server" HeaderText="เช็คที่ยังไม่ถูกใช้"
    AllowDragging="True" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" ClientInstanceName="popupChequeBatchItem">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server">
            &nbsp;<table>
                <tbody>
                    <tr>
                        <td>
                            <uc1:BankAccountComboControl ID="BankAccountTextBoxControl21" runat="server" />
                        </td>
                        <td>
                            <dxe:ASPxButton runat="server" ClientInstanceName="buttonSearch" Text="Search" ID="buttonSearch"
                                AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
	cbSearch.SendCallback();
}" />
                            </dxe:ASPxButton>
                            <dxcb:ASPxCallback ID="cbSearch" runat="server" ClientInstanceName="cbSearch" OnCallback="cbSearch_Callback">
                                <ClientSideEvents CallbackComplete="function(s, e) {
	gridChequeBatchItems.PerformCallback();
}"></ClientSideEvents>
                            </dxcb:ASPxCallback>
                        </td>
                    </tr>
                </tbody>
            </table>
            <dxwgv:ASPxGridView runat="server" KeyFieldName="ChequeBatchItemID" ClientInstanceName="gridChequeBatchItems"
                AutoGenerateColumns="False" ID="gridChequeBatchItems" Width="300">
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="ChequeNo" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="300" />
                <ClientSideEvents FocusedRowChanged="function(s, e) {
	var index = gridChequeBatchItems.GetFocusedRowIndex();

	cbSelectedChequeBatchItem.SendCallback(index);
}" />
                <SettingsBehavior AllowFocusedRow="True" />
            </dxwgv:ASPxGridView>
            <dxe:ASPxButton runat="server" ClientInstanceName="btnSelect" Text="Select" ID="btnSelect"
                AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	cbpTextChequeBatchItemEdit.PerformCallback();
	popupChequeBatchItem.Hide();

}" />
            </dxe:ASPxButton>
            <dxcb:ASPxCallback ID="cbSelectedChequeBatchItem" runat="server" ClientInstanceName="cbSelectedChequeBatchItem"
                OnCallback="cbSelectedChequeBatchItem_Callback">
            </dxcb:ASPxCallback>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>