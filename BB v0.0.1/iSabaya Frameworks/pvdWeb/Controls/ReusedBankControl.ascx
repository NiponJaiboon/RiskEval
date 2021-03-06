<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ReusedBankControl" Codebehind="ReusedBankControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<table cellpadding="0" cellspacing="0">
    <tr>
<%--        <td style="padding-right: 5">
            <dxe:ASPxLabel ID="ASPxLabel2" runat="server">
            </dxe:ASPxLabel>
        </td>--%>
        <td>
            <dxe:ASPxComboBox ID="cbmBankAccount" runat="server" EnableCallbackMode="true" CallbackPageSize="10"
                IncrementalFilteringMode="Contains" DropDownWidth="550" DropDownStyle="DropDownList"
                TextFormatString="{0} {1} {2}" Width="400">
                <Columns>
                    <dxe:ListBoxColumn FieldName="AccountNo" Caption="เลขที่บัญชีธนาคาร" />
                    <dxe:ListBoxColumn FieldName="AccountName" Caption="ชื่อบัญชีธนาคาร" />
                    <dxe:ListBoxColumn FieldName="Branch" Caption="สาขา" />
                </Columns>
            </dxe:ASPxComboBox>
        </td>
    </tr>
</table>