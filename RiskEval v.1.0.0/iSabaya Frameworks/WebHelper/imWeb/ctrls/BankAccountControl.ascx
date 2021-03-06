<%@ Control Language="C#" AutoEventWireup="True"
    Inherits="BankAccountControl" EnableViewState="true" Codebehind="BankAccountControl.ascx.cs" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="wc" %>
<%@ Register Src="~/UserControls/BankAccountComboControl.ascx" TagName="BankAccountComboControl"
    TagPrefix="uc5" %>
<%@ Register Src="~/UserControls/TimeIntervalControl.ascx" TagName="TimeIntervalControl"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/OrganizationControl.ascx" TagName="OrganizationControl"
    TagPrefix="uc4" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <dxe:ASPxRadioButtonList ID="rdoUseOldBank" runat="server" RepeatDirection="Horizontal">
                <Items>
                    <dxe:ListEditItem Text="ใช้บัญชีธนาคารเดิม" Value="0" Selected="true" />
                    <dxe:ListEditItem Text="สร้างบัญชีธนาคารใหม่" Value="1" />
                </Items>
            </dxe:ASPxRadioButtonList>
        </td>
    </tr>
</table>
<br />
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <dx:ASPxRoundPanel ID="rpnOldBankAccount" runat="server" HeaderText="ใช้บัญชีธนาคารเดิม">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table id="tableOldNewBankAccount">
                            <tr>
                                <td>
                                    <uc5:BankAccountComboControl ID="ctrlBankAccount" runat="server" IsRequiredField="True" />
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <dx:ASPxRoundPanel ID="rpnNewBankAccount" runat="server" HeaderText="สร้างบัญชีธนาคารใหม่">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table id="tableNewBankAccount">
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="ประเภทบัญชีธนาคาร">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <cc1:CategoryControl ID="ctrlBankAccountCategory" runat="server">
                                    </cc1:CategoryControl>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="ชื่อบัญชี">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dxcp:ASPxCallbackPanel ID="cbpAccountName" runat="server">
                                                    <PanelCollection>
                                                        <dxp:PanelContent ID="PanelContent1" runat="server">
                                                            <wc:MLSControl ID="ctrlMLSAccountName" runat="server" Width="300" />
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                            <td style="padding-left: 5px; vertical-align: middle">
                                                <dxe:ASPxImage ID="btnRefreshAccountName" runat="server">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="ธนาคาร-สาขา" CssClass="defaultFont">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <uc4:OrganizationControl ID="ctrlOrganization" runat="server" SelectAll="false" IsBank="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="LabelAccountNumber" runat="server" Text="เลขที่บัญชี" CssClass="defaultFont">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <cc1:BankAccountNumberControl ID="TextAccountNumber" runat="server">
                                    </cc1:BankAccountNumberControl>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="LabelOpenDate" runat="server" Text="วันที่เปิดบัญชี" CssClass="defaultFont">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <wc:DateTimeControl ID="DateOpenFrom" runat="server" />
                                </td>
                            </tr>
                            <tr id="trDirectDebit" runat="server">
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="อนุมัตหักบัญชีเงินฝาก">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <uc3:TimeIntervalControl ID="ctrlTimeInterval" runat="server" HideTime="true" IsRequiredField="false"
                                        ValidationGroup="ValidationGroup1" />
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
</table>
<br />