<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_FundBankAccount" Codebehind="FundBankAccount.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<table class="tbFormbase" cellspacing="0" cellpadding="0">
    <tr>
        <td>
            <dx:ASPxComboBox ID="cbxBankAccount" runat="server" Width="320" DropDownWidth="600"
                DropDownStyle="DropDownList" ValueField="BankAccountID" ValueType="System.String"
                TextFormatString="{0} {1} {2} {3}" EnableCallbackMode="true" IncrementalFilteringMode="Contains"
                CallbackPageSize="30" OnDataBinding="cbxBankAccount_DataBinding" OnCallback="cbxBankAccount_Callback">
                <Columns>
                    <dx:ListBoxColumn FieldName="BankName" Name="BankName" Caption="ธนาคาร" Width="20" />
                    <dx:ListBoxColumn FieldName="BranchName" Name="BranchName" Caption="สาขา" Width="40" />
                    <dx:ListBoxColumn FieldName="AccountName" Name="AccountName" Caption="ชื่อบัญชี" />
                    <dx:ListBoxColumn FieldName="AccountNo" Name="AccountNo" Caption="เลขที่บัญชี" Width="50" />
                </Columns>
            </dx:ASPxComboBox>
        </td>
        <dx:ASPxCallback ID="cbBankAccount" runat="server" OnCallback="cbBankAccount_Callback">
        </dx:ASPxCallback>
    </tr>
</table>