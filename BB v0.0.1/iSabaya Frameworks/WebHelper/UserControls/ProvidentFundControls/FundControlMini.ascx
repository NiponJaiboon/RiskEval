<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="ctrls_FundControlMini" Codebehind="FundControlMini.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<dxe:aspxcombobox 
			id="ComboFundName" 
			runat="server" 
			clientinstancename="cmbFundCode"
			IncrementalFilteringMode="StartsWith" 
			enableclientsideapi="True" 
			valuetype="System.Int32"
			width="165px" 
			cssclass="defaultFont">
            </dxe:aspxcombobox>
