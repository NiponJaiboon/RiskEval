<%@ Control Language="C#" AutoEventWireup="true" Inherits="BankAccountComboControl"
    CodeBehind="BankAccountComboControl.ascx.cs" %>
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
<table id="checkDisplay" runat="server" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="ชนิดบัญชีธนาคาร">
            </dxe:ASPxLabel>
            <%--<span style="color: Red">*</span>--%>
        </td>
        <td>
            <dxe:ASPxComboBox ID="combotype" ClientInstanceName="combotype" runat="server" SelectedIndex="0"
                ValueType="System.String">
                <ClientSideEvents SelectedIndexChanged="function(s,e){

                        cbpBankAccount.PerformCallback();
                }" />
                <Items>
                    <dxe:ListEditItem Text="บัญชีลอย" Value="3" />
                    <dxe:ListEditItem Selected="True" Text="บัญชีกองทุน" Value="1" />
                    <dxe:ListEditItem Text="บัญชีลูกค้ารับเงิน" Value="2" />
                </Items>
            </dxe:ASPxComboBox>
        </td>
        <td>
            <dxcp:ASPxCallbackPanel ID="cbpBankAccount" runat="server" ClientInstanceName="cbpBankAccount"
                OnCallback="cbpBankAccount_Callback">
                <PanelCollection>
                    <dxp:PanelContent>
                        <dxe:ASPxComboBox ID="cboBankAccount" ClientInstanceName="cboBankAccount" runat="server"
                            Width="320" DropDownWidth="600" DropDownStyle="DropDownList" ValueField="BankAccountID"
                            ValueType="System.String" TextFormatString="{0} {1} {2} {3}" EnableCallbackMode="true"
                            IncrementalFilteringMode="Contains" CallbackPageSize="30" OnDataBinding="cboBankAccount_DataBinding"
                            OnCallback="cboBankAccount_Callback">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Bank" Name="BankName" Caption="ธนาคาร" Width="20" />
                                <dxe:ListBoxColumn FieldName="Branch" Name="BranchName" Caption="สาขา" Width="40" />
                                <dxe:ListBoxColumn FieldName="AccountName" Name="AccountName" Caption="ชื่อบัญชี" />
                                <dxe:ListBoxColumn FieldName="AccountNo" Name="AccountNo" Caption="เลขที่บัญชี" Width="50" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </td>
    </tr>
</table>