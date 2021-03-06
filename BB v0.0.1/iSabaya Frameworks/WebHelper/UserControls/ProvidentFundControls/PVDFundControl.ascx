<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="ctrls_PVDFundControl" Codebehind="PVDFundControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<table cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td nowrap="nowrap">
            <dxe:ASPxComboBox ID="cboFundCode" runat="server" IncrementalFilteringMode="StartsWith" EnableAnimation="false"
                EnableClientSideAPI="True" ValueType="System.Int32" Width="170px" ClientInstanceName="cboFundCode">
            </dxe:ASPxComboBox>
        </td>
        <td nowrap="nowrap">
        &nbsp;
            <dxe:ASPxLabel ID="lblFundName" ClientInstanceName="LabelFundName" ClientEnabled="true"
                ClientVisible="true" runat="server">
            </dxe:ASPxLabel>
        </td>
    </tr>
</table>
<dxcb:ASPxCallback ID="cbLoadFund" runat="server" ClientInstanceName="cbLoadFund"
    OnCallback="cbLoadFund_Callback">
</dxcb:ASPxCallback>
