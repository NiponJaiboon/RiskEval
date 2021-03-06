<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WebHelper.UserControls.OrganizationControl" Codebehind="OrganizationControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<table clase="tbFormBase" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <dxe:ASPxComboBox ID="comboOrganizationList" runat="server" Width="170" DropDownWidth="550"
                DropDownStyle="DropDownList" ValueField="PartyID" ValueType="System.Int32" TextFormatString="{0}-{1}"
                EnableCallbackMode="true" IncrementalFilteringMode="Contains" CallbackPageSize="30"
                OnDataBinding="comboOrganizationList_DataBinding" OnCallback="comboOrganizationList_Callback"
                EnableSynchronization="false">
                <Columns>
                    <dxe:ListBoxColumn FieldName="Code" Name="Code" Caption="รหัสบริษัท" />
                    <dxe:ListBoxColumn FieldName="FullName" Name="FullName" Caption="บริษัท" />
                </Columns>
            </dxe:ASPxComboBox>
        </td>
    </tr>
</table>