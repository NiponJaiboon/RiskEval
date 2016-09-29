<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_BankAccountSelectionControl" Codebehind="BankAccountSelectionControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
    <%@ Register Src="BankAccountControl.ascx" TagName="BankAccountControl" TagPrefix="uc1"  %>
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="padding-right: 5px">
            <dxe:ASPxRadioButton ID="rdoSelectBankAccount" runat="server">
            </dxe:ASPxRadioButton>
        </td>
        <td>
            <dxe:ASPxComboBox ID="cboBankAccount" runat="server" EnableAnimation="false" TextFormatString="{0}"
                ValueType="System.Int32" ValueField="BankAccountID" DropDownWidth="500px" IncrementalFilteringMode="StartsWith" >
                <Columns>
                    <dxe:ListBoxColumn Caption="เลขบัญชี" FieldName="AccountNo" />
                    <dxe:ListBoxColumn Caption="ชื่อบัญชี" FieldName="AccountName" />
                    <dxe:ListBoxColumn Caption="ธนาคาร" FieldName="BankName" />
                    <dxe:ListBoxColumn Caption="สาขา" FieldName="BranchName" />
                </Columns>
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxRadioButton ID="rdoOtherBankAccount" runat="server">
            </dxe:ASPxRadioButton>
        </td>
        <td>
            <dxe:ASPxHyperLink ID="hlkOtherBankAccout" runat="server" Font-Underline="false" Cursor="pointer">
            </dxe:ASPxHyperLink>
            <dx:ASPxCallback ID="cbOtherBankAccount" OnCallback="cbOtherBankAccount_Callback"
                runat="server">
            </dx:ASPxCallback>
            <asp:Image Style="visibility:hidden" ImageUrl="~/Images/loader1.gif" ID="imgLoader" runat="server" />
        </td>
    </tr>
</table>
<dx:ASPxHiddenField ID="hddBankAccountSelection" runat="server">
</dx:ASPxHiddenField>
<dxpc:ASPxPopupControl ID="popupOtherBankAccount" runat="server" AllowDragging="false"
    HeaderText="บัญชีธนาคาร" Modal="false" PopupHorizontalAlign="OutsideRight"
    PopupVerticalAlign="Below" EnableAnimation="false" Width="600px" AllowResize="false"
    PopupAction="None" PopupElementID="rdoOtherBankAccount" CloseAction="OuterMouseClick" >
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <uc1:BankAccountControl ID="ctrlOtherBankAccount" VisibleOpenDate="false" runat="server" IsUseExitName="false" />
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btnCreateOtherBankAccount" runat="server" Text="OK" AutoPostBack="false" >
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnCancelOtherBankAccount" runat="server" Text="Cancel" AutoPostBack="false" >
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
