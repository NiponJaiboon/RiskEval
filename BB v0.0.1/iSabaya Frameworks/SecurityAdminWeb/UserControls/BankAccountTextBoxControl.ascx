<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="BankAccountTextBoxControl" Codebehind="BankAccountTextBoxControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Src="OrganizationControl.ascx" TagName="OrganizationControlMini"
    TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<dxcp:ASPxCallbackPanel ID="cbpTxtBtnBankAccount" runat="server" Width="100px" OnCallback="cbpTxtBtnBankAccount_Callback">
    <PanelCollection>
        <dxp:PanelContent runat="server">
            <table style="width: 100%;">
                <tr>
                    <%--   <td align="right">
                        <dxe:ASPxComboBox ID="cbBankCode" runat="server"
                         ValueType="System.String"
                           IncrementalFilteringMode="StartsWith" Width="100px"
                         >
                          <ClientSideEvents SelectedIndexChanged="function(s, e) {
	//alert(s.GetSelectedItem().value);
	cbBAccountNo.PerformCallback(s.GetSelectedItem().value);
}" />

                        </dxe:ASPxComboBox>
                    </td>
                    <td align="left">
                        <dxe:ASPxComboBox ID="cbBAccountNo"
                        ClientInstanceName="cbBAccountNo"
                          IncrementalFilteringMode="StartsWith"
                          EnableClientSideAPI="true"
                        runat="server" ValueType="System.String" OnCallback="cbBAccountNo_Callback"
                            Width="100px">
                        </dxe:ASPxComboBox>
                    </td>--%>
                    <td align="left">
                        <dxe:ASPxButtonEdit ID="txtBtnBankAccount" runat="server" Width="300px">
                            <Buttons>
                                <dxe:EditButton>
                                </dxe:EditButton>
                            </Buttons>
                        </dxe:ASPxButtonEdit>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxcb:ASPxCallback ID="cbLostFocusText" ClientInstanceName="cbLostFocusText" runat="server"
                            OnCallback="cbLostFocusText_Callback">
                        </dxcb:ASPxCallback>
                        <dxe:ASPxLabel ID="lblAccountName" runat="server" Text="">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
            </table>
        </dxp:PanelContent>
    </PanelCollection>
</dxcp:ASPxCallbackPanel>
<dxpc:ASPxPopupControl ID="popupCustom" runat="server" HeaderText="บัญชีธนาคาร" AllowDragging="True"
    CloseAction="CloseButton" Width="500px" Modal="True" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server">
            <dxe:ASPxRadioButtonList ID="rdoType" ClientInstanceName="rdoType" runat="server"
                SelectedIndex="0" Width="500px" RepeatDirection="Horizontal" Visible="false">
                <Items>
                    <dxe:ListEditItem Text="แสดงทั้งหมด" Value="3" />
                    <dxe:ListEditItem Text="หาในระบบ" Value="0" />
                    <dxe:ListEditItem Text="ค่าเริ่มต้น" Value="2" />
                </Items>
            </dxe:ASPxRadioButtonList>
            <table width="100px">
                <tr>
                    <td valign="top" rowspan="3">
                        <dxp:ASPxPanel ID="panelSearch" runat="server" ClientInstanceName="panelSearch" Visible="False">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table id="TABLE1">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="ธนาคาร">
                                                </dxe:ASPxLabel>
                                                <uc1:OrganizationControlMini ID="ctrlOrganizationControlMini1" runat="server" IsBank="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="เลขที่บัญชี" Width="70px">
                                                </dxe:ASPxLabel>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtAccountNo" runat="server" Width="100px">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxcp:ASPxCallbackPanel ID="callbackAcc" runat="server" OnCallback="callbackAcc_Callback">
                                                                <PanelCollection>
                                                                    <dxp:PanelContent runat="server">
                                                                        <dxe:ASPxLabel ID="lblSourceAccount" runat="server">
                                                                        </dxe:ASPxLabel>
                                                                    </dxp:PanelContent>
                                                                </PanelCollection>
                                                            </dxcp:ASPxCallbackPanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxcp:ASPxCallbackPanel ID="cbpOtherBank" runat="server" ClientInstanceName="cbpOtherBank"
                                                    OnCallback="cbpOtherBank_Callback">
                                                    <PanelCollection>
                                                        <dxp:PanelContent runat="server">
                                                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="ชื่อบัญชี" Width="50px">
                                                            </dxe:ASPxLabel>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txtAccountName" runat="server" Width="170px">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                    <Paddings Padding="0px" />
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>
                        <dxp:ASPxPanel ID="panelList" runat="server" ClientInstanceName="panelList" Width="500px">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxwgv:ASPxGridView ID="gridList" ClientInstanceName="gridList" KeyFieldName="BankAccountID"
                                        Width="500px" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn FieldName="BankName" VisibleIndex="0" Caption="ธนาคาร">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Branch" VisibleIndex="1" Caption="สาขา">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="AccountNo" VisibleIndex="2" Caption="เลขที่">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="AccountName" VisibleIndex="3" Caption="ชื่อ">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="IsDirectDebit" VisibleIndex="4" Caption="ใช้ไดเร็คเดบิต">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewCommandColumn Caption="Action">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="buttonSelect" Text="เลือก">
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dxwgv:GridViewCommandColumn>
                                        </Columns>
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="250" />
                                        <Settings ShowFilterRow="True" />
                                    </dxwgv:ASPxGridView>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>
                    </td>
                </tr>
            </table>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<dxcb:ASPxCallback ID="cbUpdateCode" runat="server" OnCallback="cbUpdateCode_Callback">
</dxcb:ASPxCallback>