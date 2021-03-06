<%@ Control Language="C#" AutoEventWireup="true" Inherits="ctrls_MFAccountControlNew"
    CodeBehind="MFAccountControl.ascx.cs" %>
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
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <dxcp:ASPxCallbackPanel ID="cbpTxtMFAccountNo" runat="server" OnCallback="cbpTxtMFAccountNo_Callback">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <table class="tbFormBase" cellpadding="0" cellspacing="0">
                            <tr id="trComboAccount">
                                <td valign="top">
                                    <dxe:ASPxComboBox ID="cboAccountNo" runat="server" Width="250" DropDownWidth="550"
                                        DropDownStyle="DropDownList" ValueField="AccountID" ValueType="System.String"
                                        TextFormatString="{0} {1}" EnableCallbackMode="true" IncrementalFilteringMode="Contains"
                                        CallbackPageSize="30">
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="AccountNo" />
                                            <dxe:ListBoxColumn FieldName="AccountName" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr id="trCustomerName" style="display: none">
                                <td>
                                    <dxe:ASPxHyperLink ID="lblMFCustomerName" runat="server">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>
                        </table>
                        <dxpc:ASPxPopupControl ID="popupAccount" runat="server" AllowDragging="True" CloseAction="CloseButton"
                            HeaderText="บัญชีหน่วย" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                            EnableAnimation="false">
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
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gridCustomer" KeyFieldName="AccountID"
                                        AutoGenerateColumns="False" ID="GridCustomer" Width="100%" EnableRowsCache="false">
                                        <Columns>
                                            <dxwgv:GridViewDataColumn FieldName="AccountNo" ReadOnly="True" Caption="เลขที่"
                                                ShowInCustomizationForm="False" VisibleIndex="0">
                                                <EditFormSettings Visible="False"></EditFormSettings>
                                            </dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataColumn FieldName="Name" ReadOnly="True" Caption="ชื่อ" ShowInCustomizationForm="False"
                                                VisibleIndex="1">
                                                <EditFormSettings Visible="False"></EditFormSettings>
                                            </dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewCommandColumn Caption="รายละเอียด">
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
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </td>
        <dxcb:ASPxCallback ID="cbCheckOwner" runat="server" ClientInstanceName="cbCheckOwner"
            OnCallback="cbCheckOwner_Callback">
        </dxcb:ASPxCallback>
        <td style="padding-left: 5px;">
            <div id="divBiewSignature" runat="server" style="visibility: hidden">
                <dxe:ASPxButton ID="btnViewSignature" runat="server" EnableDefaultAppearance="false"
                    EnableTheming="false" AutoPostBack="False" ToolTip="Signature">
                    <Image AlternateText="Signature" Url="../Images/led_icon/text_signature.png">
                    </Image>
                </dxe:ASPxButton>
            </div>
        </td>
        <td id="tdIsEmployee" runat="server" style="padding-left: 5px; display: none;">
            <dxe:ASPxImage ID="imgIsEmployee" runat="server" ImageUrl="~/Images/led_icon/employee_account.png"
                ToolTip="The account is an employee of fund house/SA">
            </dxe:ASPxImage>
        </td>
        <%--<td style="padding-left: 5px; white-space: nowrap">
            <dxe:ASPxLabel ID="lblMFCustomerName" runat="server">
            </dxe:ASPxLabel>
        </td>--%>
        <td id="tdPopupViewSignature" runat="server">
            <div id="divPopupViewSignature" runat="server" style="position: absolute; vertical-align: top">
                <dxcp:ASPxCallbackPanel ID="cbpViewSignature" runat="server" OnCallback="cbpViewSignature_Callback"
                    ShowLoadingPanel="false">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <dxpc:ASPxPopupControl ID="popupViewSignature" runat="server" AllowDragging="false"
                                AllowResize="false" AppearAfter="0" CloseAction="OuterMouseClick" ShowHeader="false"
                                ShowCloseButton="false" PopupHorizontalAlign="LeftSides" PopupVerticalAlign="Below"
                                EnableAnimation="False" Font-Bold="False" HeaderText="Signature" DisappearAfter="0"
                                PopupAction="None">
                                <ContentStyle Paddings-Padding="0">
                                </ContentStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl>
                                        <whuc:SignatureControl ID="ctrlSignature" runat="server" ShowHeader="false" ShowEffective="false" />
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </div>
        </td>
    </tr>
</table>
<dxcb:ASPxCallback ID="cbTest" runat="server" ClientInstanceName="cbTest" OnCallback="cbTest_Callback">
</dxcb:ASPxCallback>