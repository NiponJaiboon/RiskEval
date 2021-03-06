<%@ Control Language="C#" AutoEventWireup="true" Inherits="ctrls_FundControl" Codebehind="FundControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<table cellpadding="0" cellspacing="0" border="0" class="tbFormbase">
    <tr>
        <td>
            <dxe:ASPxComboBox ID="cboFundCode" ClientInstanceName="cboFundCode" runat="server"
                IncrementalFilteringMode="Contains" EnableClientSideAPI="True" ValueType="System.Int32"
                Width="170px" OnCallback="cboFundCode_Callback">
            </dxe:ASPxComboBox>
        </td>
        <td style="padding: 10">
            <dxe:ASPxLabel ID="lblFundName" ClientInstanceName="LabelFundName" ClientEnabled="true"
                ClientVisible="true" runat="server">
            </dxe:ASPxLabel>
        </td>
    </tr>
</table>
<dxcb:ASPxCallback ID="cbSelectFund" runat="server" ClientInstanceName="cbSelectFund"
    OnCallback="cbSelectFund_Callback">
</dxcb:ASPxCallback>
<dxcb:ASPxCallback ID="cbLoadFund" runat="server" ClientInstanceName="cbLoadFund"
    OnCallback="cbLoadFund_Callback">
</dxcb:ASPxCallback>