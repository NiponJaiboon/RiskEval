<%@ Control Language="C#" AutoEventWireup="True" Inherits="CountryControl" Codebehind="CountryControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<dx:ASPxHiddenField ID="hdnCountry" ClientInstanceName="hdnCountry" runat="server">
</dx:ASPxHiddenField>
 <dxe:ASPxComboBox ID="cboCountry" runat="Server" IncrementalFilteringMode="StartsWith" EnableAnimation="false">
 </dxe:ASPxComboBox>  
