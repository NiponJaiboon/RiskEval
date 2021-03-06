<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_EmployerTradeDateControl" Codebehind="EmployerTradeDateControl.ascx.cs" %>
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
<dxp:ASPxPanel ID="panelRootMain" runat="server">
    <PanelCollection>
        <dxp:PanelContent runat="server">
            <table align="left" style="padding: 0px 2px 0px 2px;">
                <tr>
                    <td>
                        <b>
                            <dxe:ASPxLabel ID="lblEmployer" runat="server">
                            </dxe:ASPxLabel>
                        </b>
                    </td>
                    <td>
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
                    <td>
                        <dxe:ASPxButton ID="btnBrowse" runat="server" AutoPostBack="False" Text="...">
                        </dxe:ASPxButton>
                    </td>
                    <td style="white-space: nowrap">
                        <dxe:ASPxLabel ID="lblEmployerOrgName" runat="server" Font-Size="Larger" Font-Bold="true">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="white-space: nowrap; padding-left: 10px">
                        <dxe:ASPxLabel ID="lblTradeDate" runat="server" Width="70px" Font-Size="Larger" Font-Bold="true">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="white-space: nowrap">
                        <dxe:ASPxLabel ID="lblTradeDateValue" runat="server" Font-Size="Larger" Font-Bold="true">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
            </table>
        </dxp:PanelContent>
    </PanelCollection>
</dxp:ASPxPanel>
<dxpc:ASPxPopupControl ID="popupAccount" runat="server" AllowDragging="True" CloseAction="CloseButton"
    HeaderText="บัญชีหน่วย" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dxcb:ASPxCallback ID="CallbacklikeCustomerName" runat="server" ClientInstanceName="likeCustomerNameCallback"
                OnCallback="likeCustomerNameCallback_Callback">
            </dxcb:ASPxCallback>
            <dxtc:ASPxPageControl Width="400px" ID="ASPxPageControl1" runat="server" ActiveTabIndex="1"
                EnableHierarchyRecreation="True">
                <TabPages>
                    <dxtc:TabPage Name="Search by name" Text="หาด้วยชื่อบัญชี">
                        <ContentCollection>
                            <dxw:ContentControl ID="Contentcontrol2" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="LabelName" runat="server" Text="ชื่อบัญชี" CssClass="defaultFont">
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
                    <dxwgv:GridViewDataColumn FieldName="OrganizationCode" ReadOnly="True" Caption="รหัสบริษัท"
                        ShowInCustomizationForm="False" VisibleIndex="0">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="EmployerNo" ReadOnly="True" Caption="รหัสนายจ้าง"
                        ShowInCustomizationForm="False" VisibleIndex="0">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="FullName" ReadOnly="True" Caption="ชื่อ" ShowInCustomizationForm="False"
                        VisibleIndex="1">
                        <EditFormSettings Visible="False"></EditFormSettings>
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
<dxcb:ASPxCallback ID="cbTest" runat="server" ClientInstanceName="cbTest" OnCallback="cbTest_Callback">
</dxcb:ASPxCallback>