<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_BillPaymentBrowseControl" Codebehind="BillPaymentBrowseControl.ascx.cs" %>
<%@ Register Src="MFAccountControl.ascx" TagName="MFAccountControl" TagPrefix="uc2" %>
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
<table>
    <%--<tr>
        <td>
            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="บัญชีหน่วย" />
        </td>
        <td>
            <uc2:MFAccountControl ID="MFAccountControl1" runat="server" />
        </td>
    </tr>--%>
</table>
<dxe:ASPxButton ID="btnShow" runat="server" Text="แสดง" AutoPostBack="False" >
    <ClientSideEvents Click="function(s, e) {
	cbSearchPayment.SendCallback();
}" />
</dxe:ASPxButton>
<dxcb:ASPxCallback ID="cbSearch" runat="server" ClientInstanceName="cbSearchPayment"
    OnCallback="cbSearch_Callback">
    <ClientSideEvents CallbackComplete="function(s, e) {
	gridMainPayment.PerformCallback();
}"></ClientSideEvents>
</dxcb:ASPxCallback>
<dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Payment" />
<dxwgv:ASPxGridView ID="gridMainPayment" ClientInstanceName="gridMainPayment" runat="server"
    AutoGenerateColumns="False" KeyFieldName="PaymentID" Width="500px">
    <Columns>
        <dxwgv:GridViewDataTextColumn FieldName="Payer" VisibleIndex="0">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="PayerAccountNo" VisibleIndex="0">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="Payee" VisibleIndex="1">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="PaymentDate" VisibleIndex="2">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="Amount" VisibleIndex="3">
        </dxwgv:GridViewDataTextColumn>
    </Columns>
    <SettingsBehavior AllowFocusedRow="True" />
    <ClientSideEvents FocusedRowChanged="function(s, e) {

	var rowIndex = gridMainPayment.GetFocusedRowIndex();
	cbgridMainPaymentChangeFocus.SendCallback(rowIndex);
}" />
    <SettingsPager Mode="ShowAllRecords">
    </SettingsPager>
    <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="150" />
</dxwgv:ASPxGridView>
<dxcb:ASPxCallback ID="cbSelectedPayment" runat="server" ClientInstanceName="cbSelectedPayment">
    <ClientSideEvents CallbackComplete="function(s, e) {
	gridPayments.PerformCallback();
}"></ClientSideEvents>
</dxcb:ASPxCallback>
<dxcb:ASPxCallback ID="cbgridMainPaymentChangeFocus" runat="server" ClientInstanceName="cbgridMainPaymentChangeFocus"
    OnCallback="cbgridMainPaymentChangeFocus_Callback">
    <ClientSideEvents CallbackComplete="function(s, e) {
	/*gridTransactions.PerformCallback();*/
}"></ClientSideEvents>
</dxcb:ASPxCallback>
<dxe:ASPxLabel ID="lbl1" runat="server" Text="ธุรกรรม" Visible="false" />
<dxwgv:ASPxGridView ID="gridTransactions" runat="server" Visible="false" ClientInstanceName="gridTransactions"
    KeyFieldName="TransactionID" Width="500px" AutoGenerateColumns="False">
    <Columns>
        <dxwgv:GridViewDataColumn FieldName="TransactionNo" VisibleIndex="0">
        </dxwgv:GridViewDataColumn>
        <dxwgv:GridViewDataColumn FieldName="Amount" VisibleIndex="1">
        </dxwgv:GridViewDataColumn>
        <dxwgv:GridViewDataColumn FieldName="Units" VisibleIndex="2">
        </dxwgv:GridViewDataColumn>
        <dxwgv:GridViewDataColumn FieldName="Account" VisibleIndex="3">
        </dxwgv:GridViewDataColumn>
        <dxwgv:GridViewDataColumn FieldName="TransactionTS" VisibleIndex="4">
        </dxwgv:GridViewDataColumn>
        <dxwgv:GridViewDataColumn FieldName="Type" VisibleIndex="5">
        </dxwgv:GridViewDataColumn>
    </Columns>
    <SettingsBehavior AllowFocusedRow="True" />
    <ClientSideEvents FocusedRowChanged="function(s, e) {

	/*var rowIndex = gridTransactions.GetFocusedRowIndex();
	cbgridTransactionChangeFocus.SendCallback(rowIndex);*/
}" />
    <SettingsPager Mode="ShowAllRecords">
    </SettingsPager>
    <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="150" />
</dxwgv:ASPxGridView>
<dxcb:ASPxCallback ID="cbgridTransactionChangeFocus" runat="server" ClientInstanceName="cbgridTransactionChangeFocus"
    OnCallback="cbgridTransactionChangeFocus_Callback">
</dxcb:ASPxCallback>
