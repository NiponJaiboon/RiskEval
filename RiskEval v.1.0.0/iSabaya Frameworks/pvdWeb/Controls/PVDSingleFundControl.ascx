<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="ctrls_PVDSingleFundControl" Codebehind="PVDSingleFundControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<table cellpadding="0px" cellspacing="0px" border="0px">
	<tr>
		<td>
			<dxe:ASPxComboBox ID="cboFundCode" runat="server" IncrementalFilteringMode="StartsWith"
				EnableClientSideAPI="True" ValueType="System.Int32" Width="165px" 
				ClientInstanceName="cboFundCode">
			</dxe:ASPxComboBox>
		</td>
	</tr>
	<tr>
		<td>
			<dxe:ASPxLabel ID="lblFundName" ClientInstanceName="LabelFundName" ClientEnabled="true"
				ClientVisible="true" runat="server" >
			</dxe:ASPxLabel>
		</td>
	</tr>
</table>
<dxcb:ASPxCallback ID="cbLoadFund" runat="server" ClientInstanceName="cbLoadFund"
	OnCallback="cbLoadFund_Callback">
</dxcb:ASPxCallback>
