<%@ Control Language="C#" AutoEventWireup="true" Inherits="PaymentControl" Codebehind="PaymentControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="whwc" %>
<dxpc:ASPxPopupControl ID="popPaymentForm" runat="server" CloseAction="CloseButton"
    Modal="true" EnableAnimation="false" AllowDragging="True" AppearAfter="0" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" HeaderText="Payment">
    <ContentCollection>
        <dxpc:PopupControlContentControl>
            <table border="0" class="tbFormBase">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="LabelPaymethod" runat="server" Text="วิธีการชำระเงิน">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="ComboPayMethod" ClientInstanceName="ComboPayMethod" runat="server"
                            EnableClientSideAPI="True" SelectedIndex="0" ValueType="System.String">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
					var type = s.GetValue();

					labelCheck.SetVisible(true);
						txtAmount.SetVisible(true);
						lblAmountUse.SetVisible(true);
						txtAmountForThisTransaction.SetVisible(true);

					if(type=='BankAccountDeposit'){
    					panelDirectDebit.SetVisible(false);
						/*panelBankDeposit.SetVisible(true);*/
						cboPayType.SetVisible(true);
						cboPayType.SetSelectedIndex(1);
						//panelCheque.SetVisible(true);
						panelGrid.SetVisible(true);
						lblBy.SetVisible(true);
					}else if(type=='FundTransfer'){
						//panelCheque.SetVisible(false);
						/*panelBankDeposit.SetVisible(false);*/
    					panelDirectDebit.SetVisible(true);
						cboPayType.SetVisible(false);
						//panelGrid.SetVisible(false);
						lblBy.SetVisible(false);
					}else if(type=='Cash'){
						panelDirectDebit.SetVisible(false);
						cboPayType.SetVisible(false);
						/*cboPayType.SetSelectedIndex(1);*/
						//panelCheque.SetVisible(false);
						panelGrid.SetVisible(true);
						lblBy.SetVisible(false);
					}else if(type=='Cheque'){
						panelDirectDebit.SetVisible(false);
						cboPayType.SetVisible(false);
						/*cboPayType.SetSelectedIndex(1);*/
						//panelCheque.SetVisible(true);
						panelGrid.SetVisible(true);
						lblBy.SetVisible(false);
					}else if(type=='BillPayment'){
						//panelDirectDebit.SetVisible(false);
						//cboPayType.SetVisible(true);
						//panelCheque.SetVisible(false);
						//panelGrid.SetVisible(false);
						//lblBy.SetVisible(false);
						//labelCheck.SetVisible(false);
						//txtAmount.SetVisible(false);
						//lblAmountUse.SetVisible(false);
						//txtAmountForThisTransaction.SetVisible(false);

						panelDirectDebit.SetVisible(false);
						/*panelBankDeposit.SetVisible(true);*/
						cboPayType.SetVisible(true);
						cboPayType.SetSelectedIndex(1);
						//panelCheque.SetVisible(true);
						panelGrid.SetVisible(true);
						lblBy.SetVisible(true);
					}
				}" Init="function(s, e) {
					panelDirectDebit.SetVisible(false);
					/*panelCheque.SetVisible(true);
					cboPayType.SetVisible(true);*/
				}" />
                            <Items>
                                <dxe:ListEditItem Text="ฝากเข้าบัญชี" Value="BankAccountDeposit" />
                                <dxe:ListEditItem Text="ตัดเงินตรงจากบัญชี" Value="FundTransfer" />
                                <dxe:ListEditItem Text="Cash" Value="Cash" />
                                <dxe:ListEditItem Text="Cheque" Value="Cheque" />
                                <dxe:ListEditItem Text="BillPayment" Value="BillPayment" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel runat="server" Text="บัญชีที่จะนำเงินเข้า:" ID="ASPxLabel4">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <whuc:BankAccountTextBoxControl runat="server" ID="BankAccountTextBoxControl21" DefaultPanelName="Receive">
                        </whuc:BankAccountTextBoxControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" ClientInstanceName="lblBy" runat="server" Text="โดย">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" SelectedIndex="1" ValueType="System.String" ClientInstanceName="cboPayType"
                            ID="cboPayType">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
    if(s.GetValue()=='Cash')
    {
        tbChequeNo.SetVisible(false);
        ChequeDate.SetVisible(false);
        paymentDate.SetVisible(false);
        lblChequeNo.SetVisible(false);
        lblChequeDate.SetVisible(false);
        lblChequePaymentDate.SetVisible(false);
    }
    else
    {
        tbChequeNo.SetVisible(true);
        ChequeDate.SetVisible(true);
        paymentDate.SetVisible(true);
        lblChequeNo.SetVisible(true);
        lblChequeDate.SetVisible(true);
        lblChequePaymentDate.SetVisible(true);
    }
}"></ClientSideEvents>
                            <Items>
                                <dxe:ListEditItem Text="Cash" Value="Cash"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Cheque" Value="Cheque"></dxe:ListEditItem>
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" ClientInstanceName="labelCheck" runat="server" Text="จำนวนเงินเช็ค">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txtAmount" ClientInstanceName="txtAmount" runat="server" Height="21px"
                            Number="0" DisplayFormatString="#,##0.00">
                            <ClientSideEvents ValueChanged="function(s, e) {
					txtAmountForThisTransaction.SetValue(s.GetValue());
				}" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" ClientInstanceName="lblAmountUse" runat="server" Text="จำนวนเงินที่ใช้ :">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txtAmountForThisTransaction" runat="server" ClientInstanceName="txtAmountForThisTransaction"
                            Height="21px" Number="0" DisplayFormatString="#,##0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel6" ClientInstanceName="lblBankCode" runat="server" Text="ธนาคาร">
                        </dxe:ASPxLabel>
                    </td>
                    <td colspan="3">
                        <whuc:BankControl ID="BankControl1" ClientInstanceName="BankControl1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lblChequeNo" ClientInstanceName="lblChequeNo" runat="server" Text="เลขที่เช็ค">
                        </dxe:ASPxLabel>
                    </td>
                    <td colspan="3">
                        <dxe:ASPxTextBox ID="tbChequeNo" ClientInstanceName="tbChequeNo" runat="server" Width="170px">
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lblChequeDate" runat="server" ClientInstanceName="lblChequeDate"
                            Text="เช็คลงวันที่">
                        </dxe:ASPxLabel>
                    </td>
                    <td colspan="3">
                        <whwc:DateTimeControl ID="ChequeDate" ClientInstanceName="ChequeDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lblChequePaymentDate" ClientInstanceName="lblChequePaymentDate"
                            runat="server" Text="วันที่ชำระเงิน" />
                    </td>
                    <td colspan="3">
                        <whwc:DateTimeControl ID="paymentDate" ClientInstanceName="paymentDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <dxp:ASPxPanel ID="panelDirectDebit" ClientInstanceName="panelDirectDebit" runat="server"
                            Width="200px">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table>
                                        <tr>
                                            <td width="120">
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="บัญชีตัดเงิน" Width="150px">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <whuc:BankAccountTextBoxControl ID="BankAccountTextBoxControl1" runat="server" DefaultPanelName="TransferFrom"
                                                    BankAccountType="CustomerBankAccount" />
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>
                    </td>
                </tr>
            </table>
            <table style="margin-left:auto; margin-right:auto;">
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btnAdd" runat="server" AutoPostBack="False" Text="เพิ่ม" EnableClientSideAPI="True">
                            <ClientSideEvents Click="function(s, e) {
								callbackAddPayment.SendCallback();
							}" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnAddOldPayment" Visible="true" runat="server" Text="เช็คเดิม"
                            AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
	var win = popupOldPayment.GetWindow(0);
    popupOldPayment.ShowWindow(win);
}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<dxp:ASPxPanel ID="panelGrid" ClientInstanceName="panelGrid" runat="server">
    <PanelCollection>
        <dxp:PanelContent runat="server">
            <dxcb:ASPxCallback ID="callbackAddPayment" runat="server" ClientInstanceName="callbackAddPayment"
                OnCallback="AddPayment_Callback">
            </dxcb:ASPxCallback>
            <dxwgv:ASPxGridView ID="gdvPayment" runat="server" AutoGenerateColumns="False"
                ClientInstanceName="gdvPayment" KeyFieldName="LineNo" OnRowDeleting="gdvPayment_RowDeleting">
                <Columns>
                    <dxwgv:GridViewCommandColumn Name="Action" VisibleIndex="0">
                        <HeaderTemplate>
                            <dxe:ASPxButton ID="btnAddPayment" runat="server" Text="Add" AutoPostBack="false" OnInit="btnAddPayment_Init">
                            </dxe:ASPxButton>
                        </HeaderTemplate>
                        <DeleteButton Visible="True">
                        </DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="วิธีการชำระเงิน" FieldName="PaymentType" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="เลขที่เช็ค" FieldName="ChequeNo" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="จำนวนเงินเช็ค" FieldName="Amount" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="วันที่ชำระเงิน" FieldName="PaymentDate" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="จำนวนเงินที่ใช้" FieldName="AmountForThisTransaction"
                        VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsPager Visible="False">
                </SettingsPager>
            </dxwgv:ASPxGridView>
        </dxp:PanelContent>
    </PanelCollection>
</dxp:ASPxPanel>
<dxpc:ASPxPopupControl ID="popupOldPayment" AllowDragging="True" CloseAction="CloseButton"
    Modal="True" runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ClientInstanceName="popupOldPayment" Width="600px" HeaderText="เช็คเดิม">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <whuc:ChequeTextBoxControl ID="ChequeTextBoxControl1" runat="server" />
            <dxe:ASPxButton ID="btnSelectedOldPayment" Text="เพิ่ม" runat="server" ClientInstanceName="btnSelectedOldPayment"
                AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	cbAddOldPayment.SendCallback();
}" />
            </dxe:ASPxButton>
            <dxcb:ASPxCallback ID="cbAddOldPayment" runat="server" ClientInstanceName="cbAddOldPayment"
                OnCallback="cbAddOldPayment_Callback">
            </dxcb:ASPxCallback>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
