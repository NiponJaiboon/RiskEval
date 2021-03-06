<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="MFAccountGridControl" Codebehind="MFAccountGridControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="ctrlWebHelper" %>

<dxcb:ASPxCallback ID="CallbackSearchByCard" runat="server" ClientInstanceName="Callback1"
    OnCallback="ASPxCallback1_Callback">
</dxcb:ASPxCallback>
<dxcb:ASPxCallback ID="CallbacklikeCustomerName" runat="server" ClientInstanceName="likeCustomerNameCallback"
    OnCallback="likeCustomerNameCallback_Callback">
</dxcb:ASPxCallback>
<dxcb:ASPxCallback ID="Callback1FromPopup" runat="server" ClientInstanceName="Callback1FromPopup"
    OnCallback="Callback1FromPopup_Callback">
</dxcb:ASPxCallback>
<dxcb:ASPxCallback ID="CallbackTest" runat="server" ClientInstanceName="CallbackTest"
    OnCallback="CallbackTest_Callback">
</dxcb:ASPxCallback>
<%--<dxrp:ASPxRoundPanel ID="Panel1" runat="server" BackColor="White" Width="200px">
    <BottomRightCorner Height="6px" Url="~/Images/ASPxRoundPanel/2127774470/BottomRightCorner.png"
        Width="6px" />
    <NoHeaderTopRightCorner Height="6px" Url="~/Images/ASPxRoundPanel/2127774470/NoHeaderTopRightCorner.png"
        Width="6px" />
    <HeaderRightEdge>
        <BackgroundImage HorizontalPosition="right" ImageUrl="~/Images/ASPxRoundPanel/2127774470/HeaderRightEdge.png"
            Repeat="NoRepeat" VerticalPosition="bottom" />
    </HeaderRightEdge>
    <Border BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
    <HeaderLeftEdge>
        <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/2127774470/HeaderLeftEdge.png"
            Repeat="NoRepeat" VerticalPosition="bottom" />
    </HeaderLeftEdge>
    <HeaderStyle BackColor="White">
        <BorderLeft BorderStyle="None" />
        <BorderRight BorderStyle="None" />
        <BorderBottom BorderStyle="None" />
    </HeaderStyle>
    <TopRightCorner Height="6px" Url="~/Images/ASPxRoundPanel/2127774470/TopRightCorner.png"
        Width="6px" />
    <HeaderContent>
        <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/2127774470/HeaderContent.png"
            Repeat="RepeatX" VerticalPosition="bottom" />
    </HeaderContent>
    <NoHeaderTopLeftCorner Height="6px" Url="~/Images/ASPxRoundPanel/2127774470/NoHeaderTopLeftCorner.png"
        Width="6px" />
    <PanelCollection>
        <dxp:PanelContent runat="server" _designerRegion="0">--%>
<table>
    <tr>
        <td>
            <dxe:ASPxLabel ID="lblType" runat="server" Text="ประเภท" />
        </td>
        <td>
            <dxe:ASPxComboBox ID="ComboIPartyType" runat="server" ValueType="System.String" SelectedIndex="0">
                <Items>
                    <dxe:ListEditItem Text="บุคคลธรรมดา" Value="P" />
                    <dxe:ListEditItem Text="นิติบุคคล" Value="O" />
                </Items>
                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                            //alert(s.GetValue());
                            var params = new Array(2);
                            params['source']='combo';
                            params['type']=s.GetValue();
                        	/*eventCallback(params);*/
                            }" />
            </dxe:ASPxComboBox>
        </td>
    </tr>
</table>
<dxtc:ASPxPageControl Width="400px" ID="ASPxPageControl1" runat="server" ActiveTabIndex="0"
    EnableHierarchyRecreation="True">
    <TabPages>
        <dxtc:TabPage Name="Search by id" Text="หาด้วยบัตร">
            <ContentCollection>
                <dxw:ContentControl runat="server">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="LabelCardType" runat="server" Text="บัตร" CssClass="defaultFont">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                            </td>
                            <td>
                                <ctrlWebHelper:CategoryControl ID="ComboTLNCardType" runat="server" ></ctrlWebHelper:CategoryControl>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="LabelCardCode" runat="server" Text="เลขที่บัตร" CssClass="defaultFont">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                            </td>
                            <td>
                                <dxe:ASPxTextBox ID="TextCardCode" runat="server" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </dxw:ContentControl>
            </ContentCollection>
        </dxtc:TabPage>
        <dxtc:TabPage Name="Search by name" Text="หาด้วยชื่อ-สกุล">
            <ContentCollection>
                <dxw:ContentControl runat="server">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="LabelName" runat="server" Text="ชื่อ/สกุล" CssClass="defaultFont">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                                <dxe:ASPxTextBox ID="TextSearchString" runat="server" ClientInstanceName="likeCustomerName" />
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ButtonSearch" Text="Search" runat="server" AutoPostBack="False"
                                    EnableClientSideAPI="True">
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dxw:ContentControl>
            </ContentCollection>
        </dxtc:TabPage>
    </TabPages>
</dxtc:ASPxPageControl>
<br />
<dxe:ASPxLabel ID="LabelCustomerName" runat="server" CssClass="defaultFont">
</dxe:ASPxLabel>
&nbsp;<br />
<dxcp:ASPxCallbackPanel ID="cbpGridAccountOutput" runat="server" OnCallback="cbpGridAccountOutput_Callback"
    Width="200px">
    <PanelCollection>
        <dxp:PanelContent runat="server">
            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gridAccountOutput" KeyFieldName="AccountID"
                AutoGenerateColumns="False" ID="gridAccountOutput" Width="300">
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="AccountNo" ReadOnly="True" Caption="AccountNo"
                        ShowInCustomizationForm="False" VisibleIndex="0">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Name" ReadOnly="True" Caption="AccountName"
                        ShowInCustomizationForm="False" VisibleIndex="1">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="300" />
            </dxwgv:ASPxGridView>
        </dxp:PanelContent>
    </PanelCollection>
</dxcp:ASPxCallbackPanel>
<dxcb:ASPxCallback ID="cbSelectedIndexChnage" runat="server" OnCallback="cbSelectedIndexChnage_Callback">
</dxcb:ASPxCallback>
<br />
<%-- </dxp:PanelContent>
    </PanelCollection>
    <TopLeftCorner Height="6px" Url="~/Images/ASPxRoundPanel/2127774470/TopLeftCorner.png"
        Width="6px" />
    <BottomLeftCorner Height="6px" Url="~/Images/ASPxRoundPanel/2127774470/BottomLeftCorner.png"
        Width="6px" />
</dxrp:ASPxRoundPanel>--%>
<dxpc:ASPxPopupControl ID="ASPxPopupControl1" AllowDragging="True" HeaderText="ผู้ถือหน่วย"
    CloseAction="CloseButton" Modal="True" runat="server" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" ClientInstanceName="popupCustomerSearch">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server">
            <dxwgv:ASPxGridView ID="GridCustomer" runat="server" AutoGenerateColumns="False"
                ClientInstanceName="gridCustomer" KeyFieldName="CustomerID" Width="300">
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="CustomerNo" Caption="Customer no" VisibleIndex="0"
                        ReadOnly="True" ShowInCustomizationForm="False">
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="FullName" Caption="Full Name" VisibleIndex="1"
                        ReadOnly="True" ShowInCustomizationForm="False">
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="300" />
            </dxwgv:ASPxGridView>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="buttonApplyAndClose" runat="server" Text="Apply &amp; Close"
                            UseSubmitBehavior="False" AutoPostBack="False" EnableClientSideAPI="True" />
                    </td>
                    <td>
                        <dxe:ASPxButton ID="buttonClose" runat="server" Text="Close" UseSubmitBehavior="False"
                            EnableClientSideAPI="True" AutoPostBack="False">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<asp:Panel ID="Panel1" runat="server" Height="50px" Visible="False" Width="125px">
</asp:Panel>
