<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_PersonIPControl" Codebehind="PersonIPControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<dx:ASPxHiddenField ID="hdfUseAccountIP" runat="server">
</dx:ASPxHiddenField>
<table class="tbFormBase" cellpadding="0" cellspacing="0">
    <tr id="trAccountIP" style="display: none">
        <%--<td>
            <dxe:ASPxLabel ID="lblAccountIP" runat="server">
            </dxe:ASPxLabel>
        </td>--%>
        <td>
            <dxe:ASPxHyperLink ID="hplChangeIPUsage" runat="server">
            </dxe:ASPxHyperLink>
        </td>
    </tr>
</table>
<table class="tbFormBase" cellpadding="0" cellspacing="0">
    <tr id="trSelectedIP" style="display: ''">
        <td>
            <dxe:ASPxComboBox ID="comboIPLists" runat="server" Width="250" DropDownWidth="550"
                DropDownStyle="DropDownList" ValueField="InvestmentPlannerID" ValueType="System.String"
                TextFormatString="{0} {1}" EnableCallbackMode="true" IncrementalFilteringMode="Contains"
                CallbackPageSize="30" OnDataBinding="comboIPLists_DataBinding" OnCallback="comboIPLists_Callback">
                <Columns>
                    <dxe:ListBoxColumn FieldName="LicenseNo" Name="LicenseNo" Caption="รหัส" />
                    <dxe:ListBoxColumn FieldName="FullName" Name="FullName" Caption="ชื่อ-นามสกุล" />
                </Columns>
            </dxe:ASPxComboBox>
        </td>
        <%--<td>
            <dxe:ASPxLabel ID="lblSelectedIP" runat="server">
            </dxe:ASPxLabel>
        </td>--%>
        <td id="tdhplCancelChange" style="display: none; padding-left: 5">
            <dxe:ASPxHyperLink ID="hplCancelChange" runat="server">
            </dxe:ASPxHyperLink>
        </td>
    </tr>
</table>