<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_PartyControl" Codebehind="PartyControl.ascx.cs" %>
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
<table cellpadding="0" cellspacing="0" class="tbFormbase">
    <tr>
        <td>
            <dxe:ASPxComboBox ID="comboParty" runat="server" Width="170" DropDownWidth="200"
                DropDownStyle="DropDownList" ValueField="PartyID" ValueType="System.String" TextFormatString="{0}"
                EnableCallbackMode="true" IncrementalFilteringMode="Contains" CallbackPageSize="30"
                OnDataBinding="comboParty_DataBinding">
                <Columns>
                    <dxe:ListBoxColumn FieldName="FullName" Name="FullName" Caption="ชื่อ" />
                </Columns>
            </dxe:ASPxComboBox>
        </td>
    </tr>
</table>
<%--<dxcp:ASPxCallbackPanel ID="cbpTextPartyEdit" runat="server" ClientInstanceName="cbpTextPartyEdit"
    OnCallback="cbpTextPartyEdit_Callback">
    <PanelCollection>
        <dxp:PanelContent runat="server">
            <dxe:ASPxButtonEdit runat="server" ClientInstanceName="textPartyEdit" ID="textPartyEdit">
                <ClientSideEvents ButtonClick="function(s, e) {
	                var win = popupParty.GetWindow(0);
	                popupParty.ShowWindow(win);
                }" />
                <Buttons>
                    <dxe:EditButton>
                    </dxe:EditButton>
                </Buttons>
            </dxe:ASPxButtonEdit>
            <dxe:ASPxLabel ID="labelParty" runat="server" ClientInstanceName="labelParty">
            </dxe:ASPxLabel>
        </dxp:PanelContent>
    </PanelCollection>
</dxcp:ASPxCallbackPanel>
<dxpc:ASPxPopupControl ID="popupParty" runat="server" AllowDragging="True" CloseAction="CloseButton"
    HeaderText="Party" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ClientInstanceName="popupParty">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server">
            <table>
                <tbody>
                    <tr>
                        <td>
                            <dxe:ASPxRadioButton ID="rdoIdentity" GroupName="A" Checked="true" Text="IdentityNo"
                                runat="server">
                            </dxe:ASPxRadioButton>
                        </td>
                        <td>
                            <dxe:ASPxTextBox runat="server" Width="170px" ClientInstanceName="textIdentityNo"
                                ID="textIdentityNo">
                            </dxe:ASPxTextBox>
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
	                                gridPartys.PerformCallback();
                                }" />
                            </dxcb:ASPxCallback>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxRadioButton ID="rdoFirstName" GroupName="A" Text="ชื่อ" runat="server">
                            </dxe:ASPxRadioButton>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txtFirstName" runat="server" Width="170px">
                            </dxe:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxRadioButton ID="rdoLastName" GroupName="A" Text="สกุล" runat="server">
                            </dxe:ASPxRadioButton>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txtLastName" runat="server" Width="170px">
                            </dxe:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                </tbody>
            </table>
            <dxwgv:ASPxGridView runat="server" KeyFieldName="PartyID" ClientInstanceName="gridPartys"
                AutoGenerateColumns="False" ID="gridPartys" Width="320px">
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="FullName" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <ClientSideEvents FocusedRowChanged="function(s, e) {
	                var index = gridPartys.GetFocusedRowIndex();
	                cbSelectedParty.SendCallback(index);
                }" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="300" />
                <SettingsBehavior AllowFocusedRow="True" />
            </dxwgv:ASPxGridView>
            <dxcb:ASPxCallback ID="cbSelectedParty" runat="server" ClientInstanceName="cbSelectedParty"
                OnCallback="cbSelectedParty_Callback">
            </dxcb:ASPxCallback>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>--%>