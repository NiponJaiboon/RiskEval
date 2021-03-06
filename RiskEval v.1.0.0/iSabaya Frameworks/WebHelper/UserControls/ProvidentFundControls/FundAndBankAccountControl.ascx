<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_FundAndBankAccountControl" Codebehind="FundAndBankAccountControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<table runat="server" id="tableContent" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <dxe:ASPxLabel ID="lblEmployer" runat="server" Text="บริษัท" Width="99">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxComboBox ID="cboEmployer" runat="server" IncrementalFilteringMode="StartsWith"
                EnableClientSideAPI="True" ValueType="System.Int32" Width="170" ClientInstanceName="cboEmployer">
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr id="trFund" runat="server">
        <td>
            <dxe:ASPxLabel ID="lblFund" runat="server" Text="กองทุน" Width="99">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxComboBox ID="cboFundCode" runat="server" IncrementalFilteringMode="StartsWith"
                EnableClientSideAPI="True" ValueType="System.Int32" Width="170" ClientInstanceName="cboFundCode">
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr id="trBank" runat="server">
        <td>
            <dxe:ASPxLabel ID="lblBank" runat="server" Text="ธนาคาร" Width="99">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxComboBox ID="comboBank" runat="server" Width="170" EnableClientSideAPI="True"
                OnCallback="comboBank_Callback">
            </dxe:ASPxComboBox>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0">
    <br />
    <tr>
        <td>
            <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowHeader="false" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="กองทุน: ">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="lblFundName" ClientInstanceName="lblFundName" Text="-" runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="ธนาคาร: ">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="lblBankName" ClientInstanceName="lblBankName" Text="-" runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="นายจ้าง: ">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="lblEmployerName" ClientInstanceName="lblEmployerName" Text="-"
                                        runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
</table>
<dxcb:ASPxCallback ID="cbEmployerName" runat="server" OnCallback="cbEmployerName_Callback">
</dxcb:ASPxCallback>
<dxcb:ASPxCallback ID="cbFundAndBankName" runat="server" OnCallback="cbFundAndBankName_Callback">
</dxcb:ASPxCallback>