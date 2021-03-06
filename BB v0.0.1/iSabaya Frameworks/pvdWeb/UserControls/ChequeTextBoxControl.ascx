<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ChequeTextBoxControl" Codebehind="ChequeTextBoxControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<table>
    <tr>
        <td>
            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="ธนาคาร">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxComboBox ID="comboBank" runat="server" ValueType="System.String" Width="100px">
            </dxe:ASPxComboBox>
        </td>
        <td>
            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="เลขที่เช็ค:">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxButtonEdit ID="txtChequeNo" runat="server" Height="16px" Width="150px">
                <Buttons>
                    <dxe:EditButton>
                    </dxe:EditButton>
                </Buttons>
            </dxe:ASPxButtonEdit>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <dxe:ASPxLabel ID="lblChequeName" runat="server" Text="">
            </dxe:ASPxLabel>
            <dxcb:ASPxCallback ID="cb1" runat="server" OnCallback="cb1_Callback">
            </dxcb:ASPxCallback>
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="จำนวนเงินหน้าเช็ค">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxSpinEdit runat="server" Number="0" Height="21px" DisplayFormatString="#,##0.00"
                ID="txtChequeAmount" Enabled="False">
            </dxe:ASPxSpinEdit>
        </td>
        <td>
            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="ใช้จำนวน:">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxSpinEdit runat="server" Number="0" Height="21px" DisplayFormatString="#,##0.00"
                ID="txtUseAmount">
            </dxe:ASPxSpinEdit>
        </td>
    </tr>
</table>
<dxcb:ASPxCallback ID="cbpTxtBtnBankAccount" runat="server" OnCallback="cbpTxtBtnBankAccount_Callback">
</dxcb:ASPxCallback>