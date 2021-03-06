<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_EmployerControl" Codebehind="EmployerControl.ascx.cs" %>
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
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<table>
    <tr>
        <td valign="top">
            <dxcp:ASPxCallbackPanel ID="cbpTxtEmployerNo" runat="server" OnCallback="cbpTxtEmployerNo_Callback">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <dxe:ASPxComboBox ID="cboAccountNo" runat="server" IncrementalFilteringMode="StartsWith"
                            ClientInstanceName="cboAccountNo" EnableClientSideAPI="True" EnableAnimation="false">
                        </dxe:ASPxComboBox>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </td>
        <td valign="top">
            <dxe:ASPxButton ID="btnBrowse" runat="server" AutoPostBack="False" Text="..." Width="10px">
            </dxe:ASPxButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <dxe:ASPxLabel ID="labelTitle" runat="server" />
        </td>
    </tr>
</table>
<dxpc:ASPxPopupControl ID="popupAccount" runat="server" AllowDragging="True" CloseAction="CloseButton"
    HeaderText="บริษัท" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dxcb:ASPxCallback ID="CallbacklikeCustomerName" runat="server" ClientInstanceName="likeCustomerNameCallback"
                OnCallback="likeCustomerNameCallback_Callback">
            </dxcb:ASPxCallback>
            <dxtc:ASPxPageControl Width="400px" ID="ASPxPageControl1" runat="server" ActiveTabIndex="1"
                EnableHierarchyRecreation="True">
                <TabPages>
                    <dxtc:TabPage Name="Search by name" Text="หาด้วยชื่อบริษัท">
                        <ContentCollection>
                            <dxw:ContentControl ID="Contentcontrol2" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="LabelName" runat="server" Text="ชื่อบริษัท" CssClass="defaultFont">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtFirstName" runat="server" ClientInstanceName="likeCustomerName"
                                                Width="300px" />
                                        </td>
                                    </tr>
                                </table>
                                <dxe:ASPxButton ID="btnFindName" Text="ค้นหา" runat="server" AutoPostBack="False"
                                    EnableClientSideAPI="True">
                                </dxe:ASPxButton>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
            </dxtc:ASPxPageControl>
            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gridCustomer" KeyFieldName="EmployerID"
                AutoGenerateColumns="False" ID="GridCustomer" Width="100%">
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="EmployerNo" ReadOnly="True" Caption="เลขที่"
                        ShowInCustomizationForm="False" VisibleIndex="0">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Name" ReadOnly="True" Caption="ชื่อ" ShowInCustomizationForm="False"
                        VisibleIndex="1">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewCommandColumn Caption="Action">
                        <CustomButtons>
                            <dxwgv:GridViewCommandColumnCustomButton ID="buttonSelect" Text="เลือก">
                            </dxwgv:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="300" />
            </dxwgv:ASPxGridView>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
