<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="BankAccountControlManual" Codebehind="BankAccountControlManual.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Src="BankAccountControl.ascx" TagName="BankAccountControl" TagPrefix="uc2" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<table>
    <tr>
        <td>
            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="ธนาคาร : ">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxComboBox ID="comboBank" IncrementalFilteringMode="StartsWith" runat="server"
                Width="100px">
            </dxe:ASPxComboBox>
            <%-- ValueType="System.String"--%>
        </td>
        <td>
            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="เลขที่บัญชี : ">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxButtonEdit ID="txtAccountNo" runat="server" Height="16px" Width="150px">
                <Buttons>
                    <dxe:EditButton>
                    </dxe:EditButton>
                </Buttons>
            </dxe:ASPxButtonEdit>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <dxe:ASPxLabel ID="lblAccountName" ClientInstanceName="lblAccountName" runat="server"
                Text="">
            </dxe:ASPxLabel>
            <dxcb:ASPxCallback ID="cb1" ClientInstanceName="cb1" runat="server" OnCallback="cb1_Callback">
            </dxcb:ASPxCallback>
        </td>
    </tr>
</table>