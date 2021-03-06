<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_CustomerControlNew" Codebehind="MFCustomerControlEdit.ascx.cs" %>
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
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="ctrlWebHelper" %>
<table border="0px" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <dxe:ASPxButtonEdit ID="txtMFCustomerNo" runat="server">
                <Buttons>
                    <dxe:EditButton>
                    </dxe:EditButton>
                </Buttons>
            </dxe:ASPxButtonEdit>
        </td>
    </tr>
    <tr>
        <td>
            <dxcp:ASPxCallbackPanel ID="cbpTxtMFAccountNo" runat="server" OnCallback="cbpTxtMFAccountNo_Callback"
                Width="200px">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <dxe:ASPxLabel ID="lblMFCustomerName" runat="server">
                        </dxe:ASPxLabel>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </td>
    </tr>
</table>
<dxpc:ASPxPopupControl ID="popupAccount" runat="server" AllowDragging="True" CloseAction="CloseButton"
    HeaderText="ลูกค้า" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    Height="581px" EnableAnimation="false">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dxcb:ASPxCallback ID="CallbackSearchByCard" runat="server" ClientInstanceName="Callback1"
                OnCallback="ASPxCallback1_Callback">
            </dxcb:ASPxCallback>
            <dxcb:ASPxCallback ID="CallbacklikeCustomerName" runat="server" ClientInstanceName="likeCustomerNameCallback"
                OnCallback="likeCustomerNameCallback_Callback">
            </dxcb:ASPxCallback>
            <%--<dxcb:ASPxCallback ID="cbSelectRow" runat="server" OnCallback="cbSelectRow_Callback">
            </dxcb:ASPxCallback>--%>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lblType" runat="server" Text="ประเภท" />
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="ComboIPartyType" runat="server" ValueType="System.String" SelectedIndex="0">
                            <Items>
                                <dxe:ListEditItem Text="ทั้งหมด" Value="A" />
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
            <dxtc:ASPxPageControl Width="400px" ID="ASPxPageControl1" runat="server" ActiveTabIndex="1"
                EnableHierarchyRecreation="True">
                <TabPages>
                    <dxtc:TabPage Name="Search by id" Text="หาด้วยบัตร">
                        <ContentCollection>
                            <dxw:ContentControl ID="Contentcontrol1" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="LabelCardType" runat="server" Text="ประเภทบัตร" CssClass="defaultFont">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <ctrlWebHelper:CategoryControl ID="ComboTLNCardType" runat="server">
                                            </ctrlWebHelper:CategoryControl>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="LabelCardCode" runat="server" Text="รหัสบัตร" CssClass="defaultFont">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <dxe:ASPxTextBox ID="TextCardCode" runat="server" Width="200px" />
                                        </td>
                                    </tr>
                                </table>
                                <dxe:ASPxButton ID="btnFindCard" runat="server" Text="ค้นหา" AutoPostBack="False">
                                </dxe:ASPxButton>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Name="Search by name" Text="หาด้วยชื่อ-สกุล">
                        <ContentCollection>
                            <dxw:ContentControl ID="Contentcontrol2" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="LabelName" runat="server" Text="ชื่อ" CssClass="defaultFont">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxRadioButton ID="rdoFname" runat="server" Checked="True" GroupName="A">
                                            </dxe:ASPxRadioButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtFirstName" runat="server" ClientInstanceName="likeCustomerName"
                                                Width="170px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="สกุล" CssClass="defaultFont">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxRadioButton ID="rdoLName" runat="server" GroupName="A">
                                            </dxe:ASPxRadioButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtLastName" runat="server" ClientInstanceName="likeCustomerName"
                                                Width="170px" />
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
            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gridCustomer" KeyFieldName="CustomerID"
                AutoGenerateColumns="False" ID="GridCustomer" Width="100%">
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="CustomerNo" ReadOnly="True" Caption="เลขที่"
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
                <%--<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>--%>
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="300" />
            </dxwgv:ASPxGridView>
            &nbsp;&nbsp;
            <%-- <dxe:ASPxButton ID="btnSelectCustomer" runat="server" AutoPostBack="False" Height="26px"
                Text="เลือก">
            </dxe:ASPxButton>--%>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<dxcb:ASPxCallback ID="cbSelect" runat="server" OnCallback="cbSelect_Callback">
</dxcb:ASPxCallback>
<dxcb:ASPxCallback ID="cbLostFocus" runat="server" OnCallback="cbLostFocus_Callback">
</dxcb:ASPxCallback>
