<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="OwnerMFAccountForm" Codebehind="OwnerMFAccountForm.ascx.cs" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxNavBar" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="whwc" %>
<style type="text/css">
    .SubTitle
    {
        background-color: #eeeeee;
    }
    .SubTitle td
    {
        padding: 4px 0px 6px 0px;
        border-bottom: thin solid Gray;
    }
</style>
<table width="100%" class="tbFormBase">
    <tr class="SubTitle">
        <td colspan="2">
            <dx:ASPxLabel ID="lblTitleOwnerName" runat="server">
            </dx:ASPxLabel>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="padding-top: 2px">
        </td>
    </tr>
    <tr>
        <td>
            <dx:ASPxLabel ID="lblHParty" runat="server">
            </dx:ASPxLabel>
            <asp:HiddenField ID="hddPartyID" runat="server" />
            <asp:HiddenField ID="hddAgentID" runat="server" />
            <asp:HiddenField ID="hddCustID" runat="server" />
        </td>
        <td>
            <dx:ASPxLabel ID="lblPartyName" runat="server">
            </dx:ASPxLabel>
        </td>
    </tr>
    <tr id="trBirthDate" runat="server">
        <td>
            <dx:ASPxLabel ID="lblHBirthDate" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <whwc:DateTimeControl ID="ctrlBirthDate" runat="server">
            </whwc:DateTimeControl>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <dx:ASPxLabel ID="lblHNationality" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <cc1:TreeListNodeComboBox ID="ctrlNationality" runat="server">
            </cc1:TreeListNodeComboBox>
        </td>
    </tr>
    <tr>
        <td valign="top" style="width: 10%;">
            <dx:ASPxLabel ID="lblHEmail" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxTextBox ID="txtEmail" runat="server">
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr id="trTitleSecOwners" runat="server" class="SubTitle">
        <td valign="top" colspan="2">
            <dx:ASPxLabel ID="lblTitleSecondaryOwner" runat="server">
            </dx:ASPxLabel>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="padding-top: 2px">
        </td>
    </tr>
    <tr id="trConnective" runat="server">
        <td>
            <dx:ASPxLabel ID="lblHConnective" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxComboBox ID="ComboAccountCriteria" runat="server" ValueType="System.String">
                <Items>
                    <dx:ListEditItem Text="และ" Value="And" />
                    <dx:ListEditItem Text="เพื่อ" Value="For" />
                    <dx:ListEditItem Text="โดย" Value="By" />
                </Items>
            </dx:ASPxComboBox>
        </td>
    </tr>
    <tr id="trSecOwnerNamePrefix" runat="server">
        <td valign="top">
            <dx:ASPxLabel ID="lblHSecOwnerNamePrefix" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <cc1:NameAffixControl ID="ctrlSecOwnerNamePrefix" runat="server">
            </cc1:NameAffixControl>
        </td>
    </tr>
    <tr id="trSecOwnerName" runat="server">
        <td valign="top">
            <dx:ASPxLabel ID="lblHSecOwnerName" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <table cellpadding="0px" cellspacing="0px" style="padding: 0px 2px 0px 0px">
                <tr>
                    <td>
                        <table id="tbCtrlTitleSecOwner" runat="server" width="100%" cellpadding="0" cellspacing="0"
                            style="padding-bottom: 2px;">
                            <tr>
                                <td align="left">
                                    <table id="tbTitleSecOwner" runat="server" width="100%" align="left" cellpadding="0"
                                        cellspacing="0">
                                        <tr>
                                            <td>
                                                <table align="left" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="padding: 0px 4px 2px 0px">
                                                                        <cc1:MLSControl ID="mlsSecOwnerFirstName" runat="server" >
                                                                        </cc1:MLSControl>
                                                                    </td>
                                                                    <td style="padding: 0px 4px 2px 0px">
                                                                        <cc1:MLSControl ID="mlsSecOwnerLastName" runat="server">
                                                                        </cc1:MLSControl>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <dx:ASPxButton ID="btnAddSecondaryOwner" runat="server" Text="เพิ่มผู้เปิดบัญชีร่วม"
                                                                AutoPostBack="False">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <dxwgv:ASPxGridView ID="gdvSecondaryOwners" runat="server" AutoGenerateColumns="False"
                            EnableRowsCache="false" ClientInstanceName="gdvSecondaryOwners" OnDataBinding="gdvSecondaryOwners_DataBinding"
                            OnCustomCallback="gdvSecondaryOwners_CustomCallback" Width="500px" OnCustomButtonCallback="gdvSecondaryOwners_CustomButtonCallback">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="CustomerNo" Caption="รหัสลูกค้า">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="FullName" Caption="ชื่อ">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn ButtonType="Image">
                                    <CustomButtons>
                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnDelete">
                                        </dxwgv:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dxwgv:GridViewCommandColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="SubTitle">
        <td colspan="2">
            <dx:ASPxLabel ID="lblTitleIdentity" runat="server">
            </dx:ASPxLabel>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="padding-top: 2px">
        </td>
    </tr>
    <tr id="trTaxPayer" runat="server">
        <td style="vertical-align: top">
            <dx:ASPxLabel ID="lblHTaxPayerNO" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxTextBox ID="txtTaxPayerNO" runat="server" MaxLength="10">
                <MaskSettings Mask="0000000000" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">
            <dx:ASPxLabel ID="lblHIdentityType" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <cc1:TreeListNodeComboBox ID="ctrlIdentityType" runat="server" VisibleOtherItem="false">
            </cc1:TreeListNodeComboBox>
        </td>
    </tr>
    <tr>
        <td>
            <dx:ASPxLabel ID="lblHIdentityNO" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxTextBox ID="txtIdentityNO" runat="server">
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td>
            <dx:ASPxLabel ID="lblHIssuedBy" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <dx:ASPxTextBox ID="txtIssuedBy" runat="server">
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td>
            <dx:ASPxLabel ID="lblHEffectiveFrom" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <whwc:DateTimeControl ID="ctrlDateFrom" runat="server">
            </whwc:DateTimeControl>
        </td>
    </tr>
    <tr>
        <td>
            <dx:ASPxLabel ID="lblHEffectiveTo" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <whwc:DateTimeControl ID="ctrlDateTo" runat="server">
            </whwc:DateTimeControl>
        </td>
    </tr>
</table>
