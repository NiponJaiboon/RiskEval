<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="PartyAttributeControl" Codebehind="PartyAttributeControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="ctrlWebHelper" %>
<%@ Register Src="MultilingualStringTextField.ascx" TagName="MultilingualStringTextField"
    TagPrefix="IMCtrl" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="IMCtrl" %>
<table style="padding-top: 5px; vertical-align: top">
    <tr>
        <td colspan="2">
            <dxe:ASPxButton ID="btnAdd" runat="server"
                AutoPostBack="false">
            </dxe:ASPxButton>
        </td>
        <td>
            <dxe:ASPxButton ID="btnExpire" runat="server"
                AutoPostBack="false">
            </dxe:ASPxButton>
        </td>
    </tr>
</table>
<table style="vertical-align: top">
    <tr>
        <td style="vertical-align: top">
            <asp:SqlDataSource ID="sdsPartyAttribute" runat="server" ConnectionString="<%$ ConnectionStrings:imSabayaConnectionString %>"
                SelectCommandType="Text">
                <SelectParameters>
                    <asp:Parameter Name="partyID" Type="Int32" />
                    <asp:Parameter Name="discriminator" Type="Int16" />
                    <asp:SessionParameter Name="langCode" SessionField="LanguageCode" />
                </SelectParameters>
            </asp:SqlDataSource>
            <dxwgv:ASPxGridView ID="gdvPartyAttribute" runat="server" KeyFieldName="PartyAttributeID"
                DataSourceID="sdsPartyAttribute" AutoGenerateColumns="False" Border-BorderWidth="1"
                OnCustomCallback="gdvPartyAttribute_CustomCallback" EnableRowsCache="false">
                <SettingsPager PageSize="15" Visible="true">
                </SettingsPager>
                <SettingsBehavior AllowFocusedRow="true" />
            </dxwgv:ASPxGridView>
            <dx:ASPxHiddenField ID="hddPartyAttribute" runat="server">
            </dx:ASPxHiddenField>
        </td>
        <td style="vertical-align: top">
            <dx:ASPxRoundPanel ID="rpanelPartyAttributeDetail" runat="server" Width="500px" Height="400px"
                ShowHeader="true">
                <Border BorderWidth="1" />
                <PanelCollection>
                    <dx:PanelContent>
                        <dxcp:ASPxCallbackPanel ID="cbpEdit" runat="server" OnCallback="cbpEdit_Callback"
                            OnPreRender="cbpEdit_PreRender">
                            <Paddings Padding="0px" />
                            <PanelCollection>
                                <dx:PanelContent ID="panelEditForm" runat="server">
                                    <table id="tbCtrl_PartyAttributes" runat="server" cellspacing="0" cellpadding="0"
                                        border="0" style="border-width: 1px; border-style: solid; border-collapse: separate;"
                                        width="100%">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" border="0" cellspacing="0" width="100%" runat="server" id="tbPartyAttributeForm"
                                                    style="border-collapse: collapse; empty-cells: show; visibility: hidden">
                                                    <tr id="trAttributeKey">
                                                        <td style="width: 30%">
                                                            <asp:Label ID="lblAttributeKey" runat="server" Text="AttributeKey"></asp:Label>
                                                        </td>
                                                        <td id="tdAttributeKey" runat="server">
                                                            <ctrlWebHelper:CategoryControl ID="ctrlAttributeKey" runat="server">
                                                            </ctrlWebHelper:CategoryControl>
                                                        </td>
                                                    </tr>
                                                    <tr id="trEffectiveFrom">
                                                        <td>
                                                            <asp:Label ID="lblEffectiveFrom" runat="server"></asp:Label>
                                                        </td>
                                                        <td id="tdEffectiveFrom" runat="server">
                                                            <IMCtrl:DateTimeControl ID="ctrlEffectiveFrom" runat="server" IsRequiredField="true"
                                                                ValidationGroup="attributeEdit"></IMCtrl:DateTimeControl>
                                                        </td>
                                                    </tr>
                                                    <tr id="trEffectiveTo">
                                                        <td>
                                                            <asp:Label ID="lblEffectiveTo" runat="server"></asp:Label>
                                                        </td>
                                                        <td id="tdEffectiveTo" runat="server">
                                                            <IMCtrl:DateTimeControl ID="ctrlEffectiveTo" runat="server" IsRequiredField="true"
                                                                ValidationGroup="attributeEdit"></IMCtrl:DateTimeControl>
                                                        </td>
                                                    </tr>
                                                    <tr id="trAttributeValues">
                                                        <td colspan="2" style="padding-top: 5px; text-align: center">
                                                            <asp:Label ID="lblAttributeValues" runat="server" Font-Bold="true" Text="คุณสมบัติ"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="trValueDate">
                                                        <td style="width: 30%">
                                                            <asp:Label ID="lblValueDate" runat="server" Text="Date"></asp:Label>
                                                        </td>
                                                        <td id="tdValueDate" runat="server">
                                                            <IMCtrl:DateTimeControl ID="ctrlValueDate" runat="server"></IMCtrl:DateTimeControl>
                                                        </td>
                                                    </tr>
                                                    <tr id="trValueImage">
                                                        <td>
                                                            <asp:Label ID="lblValueImage" runat="server" Text="Image"></asp:Label>
                                                        </td>
                                                        <td id="tdValueImage" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <dx:ASPxUploadControl ID="uplImage" runat="server" Size="35" OnFileUploadComplete="uplImage_FileUploadComplete"
                                                                            FileUploadMode="OnPageLoad">
                                                                            <ValidationSettings MaxFileSize="200000" AllowedContentTypes="image/jpeg,image/gif,image/pjpeg"
                                                                                AllowedFileExtensions=".jpg,.jpeg,.jpe,.gif">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxUploadControl>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="vertical-align: top">
                                                                        <dxe:ASPxButton ID="btnUploadImg" runat="server" AutoPostBack="false" Width="100px"
                                                                            Image-Url="~/Images/led_icon/page_white_get.png">
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td style="vertical-align: top" align="left">
                                                                        <dxcp:ASPxCallbackPanel ID="cbpValueImage" runat="server" OnCallback="cbpValueImage_Callback">
                                                                            <PanelCollection>
                                                                                <dx:PanelContent ID="panelValueImage" runat="server">
                                                                                    <dxe:ASPxBinaryImage ID="ctrlValueImage" runat="server">
                                                                                    </dxe:ASPxBinaryImage>
                                                                                    <asp:HiddenField ID="hddValueImage" runat="server" />
                                                                                </dx:PanelContent>
                                                                            </PanelCollection>
                                                                        </dxcp:ASPxCallbackPanel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr id="trValueMLS">
                                                        <td>
                                                            <asp:Label ID="lblValueMLS" runat="server" Text="Multilingual String"></asp:Label>
                                                        </td>
                                                        <td id="tdValueMLS" runat="server">
                                                            <IMCtrl:MultilingualStringTextField ID="ctrlValueMLS" runat="server">
                                                            </IMCtrl:MultilingualStringTextField>
                                                        </td>
                                                    </tr>
                                                    <tr id="trValueNode">
                                                        <td>
                                                            <asp:Label ID="lblValueNode" runat="server" Text="TreeListNode"></asp:Label>
                                                        </td>
                                                        <td id="tdValueNode" runat="server">
                                                        </td>
                                                    </tr>
                                                    <tr id="trValueNumber">
                                                        <td>
                                                            <asp:Label ID="lblNumber" runat="server" Text="Number"></asp:Label>
                                                        </td>
                                                        <td id="tdValueNumber" runat="server">
                                                            <dxe:ASPxSpinEdit ID="spnValueNumber" runat="server" NumberType="Float">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr id="trValueText">
                                                        <td>
                                                            <asp:Label ID="lblText" runat="server" Text="Text"></asp:Label>
                                                        </td>
                                                        <td id="tdValueText" runat="server">
                                                            <dxe:ASPxTextBox ID="txtValueText" runat="server" Width="170px">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table align="center" id="tbButtonSaveCancel" runat="server" cellpadding="3" cellspacing="0"
                                        visible="false">
                                        <tr>
                                            <td>
                                                <dxe:ASPxButton ID="btnSave" runat="server" AutoPostBack="false">
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btnCancel" runat="server" AutoPostBack="false">
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
</table>
<dx:ASPxPopupControl ID="popupActualSizeImage" ShowHeader="false" ShowFooter="false"
    CloseAction="OuterMouseClick" runat="server" PopupHorizontalAlign="Center" PopupVerticalAlign="Middle"
    ShowCloseButton="true">
    <ContentCollection>
        <dx:PopupControlContentControl>
            <dxe:ASPxBinaryImage ID="imgActualSize" runat="server">
            </dxe:ASPxBinaryImage>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>