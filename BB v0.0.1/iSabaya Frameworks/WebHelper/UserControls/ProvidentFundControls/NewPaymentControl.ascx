﻿<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WebHelper.pvdWeb.NewPaymentControl" Codebehind="NewPaymentControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Src="BankAccountControl.ascx" TagName="BAControl" TagPrefix="uc3" %>
<%@ Register  Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="whwc"%>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td valign="top">
            <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="600px" HeaderText="<%$ Resources:Resource_Transaction, Payment %>">
                <PanelCollection>
                    <dx:PanelContent>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="เลขที่บัญชีที่ใช้นำเงินเข้า">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <uc3:BAControl ID="ctrlBankAccount" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:Resource_TradeTransaction, lblPaidBy %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <dx:ASPxComboBox ID="cbxPayMethod" ClientInstanceName="cbxPayMethod" runat="server"
                                        EnableClientSideAPI="True" ValueType="System.String">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){
					                        var type = s.GetValue();
                                            formByPayMethod(type);
					                    }
		                                " />
                                        <Items>
                                            <dx:ListEditItem Text="ฝากเงินผ่านบัญชีธนาคาร" Value="BankAccountDeposit" />
                                            <dx:ListEditItem Text="โอนเงินผ่านบัญชีธนาคาร" Value="FundTransfer" />
                                            <dx:ListEditItem Text="เงินสด" Value="Cash" />
                                            <dx:ListEditItem Text="เช็ค, ดร๊าฟ" Value="Cheque" />
                                            <%--<dx:ListEditItem Text="BillPayment" Value="BillPayment" />--%>
                                        </Items>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr id="rowPayType">
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="ชำระด้วย">
                                    </dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <dx:ASPxComboBox ID="cbxPayType" ClientInstanceName="cbxPayType" runat="server" EnableClientSideAPI="True"
                                        ValueType="System.String">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e)
                                        {
					                        var type = s.GetValue();
                                            formByPayType(type);
					                    }
		                                " />
                                        <Items>
                                            <dx:ListEditItem Text="Cash" Value="Cash"></dx:ListEditItem>
                                            <dx:ListEditItem Text="Cheque" Value="Cheque"></dx:ListEditItem>
                                        </Items>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr id="rowAmount">
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="จำนวนเงิน">
                                    </dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <dx:ASPxSpinEdit ID="spnAmount" runat="server" ClientInstanceName="spnAmount" Height="21px"
                                        Number="0" DecimalPlaces="2" DisplayFormatString="#,#0.00" MinValue="0" MaxValue="9999999999">
                                    </dx:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr id="rowChequeNumber">
                                <td>
                                    <dx:ASPxLabel ID="lblChequeNo" ClientInstanceName="lblChequeNo" runat="server" Text="เลขที่เช็ค">
                                    </dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <dx:ASPxButtonEdit ID="bteChequeNo" ClientInstanceName="bteChequeNo" Width="170px"
                                                    ValidationSettings-Display="Dynamic" runat="server" Height="21">
                                                    <Buttons>
                                                        <dx:EditButton Image-Height="8" ToolTip="ตรวจสอบ Cheque ในระบบ">
                                                            <Image Url="../Images/led_icon/find.png">
                                                            </Image>
                                                        </dx:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s,e)
                                                    {
                                                        var ct = s.GetText();
                                                        if ( checkCheque(ct) )
                                                        {
                                                            cbChequeNo.SendCallback(ct);
                                                        }
                                                    }" />
                                                    <MaskSettings Mask="0000000" PromptChar="#" />
                                                </dx:ASPxButtonEdit>
                                                <dx:ASPxCallback ID="cbChequeNo" ClientInstanceName="cbChequeNo" runat="server" OnCallback="cbChequeNo_Callback">
                                                    <ClientSideEvents CallbackComplete="function(s,e)
                                                    {
                                                        var typeCheque = e.result;
                                                        manageChequePanel(typeCheque);
                                                    }" />
                                                </dx:ASPxCallback>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <dx:ASPxLabel ID="lblCheque" ClientInstanceName="lblCheque" runat="server" Text=""
                                                    ForeColor="Red">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="rowBank">
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel7" ClientInstanceName="lblBankCode" runat="server" Text="ธนาคาร">
                                    </dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <whuc:BankControl ID="BankControl1" clientinstancename="BankControl1" runat="server"
                                        CbxClientName="CbxClientName" />
                                </td>
                            </tr>
                            <tr id="rowChequeAmount">
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="จำนวนเงินใน Cheque">
                                    </dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <dx:ASPxSpinEdit ID="spnChequeAmount" runat="server" ClientInstanceName="spnChequeAmount"
                                                    Height="21px" Number="0" DisplayFormatString="#,#0.00" DecimalPlaces="2" MinValue="0"
                                                    MaxValue="9999999999">
                                                </dx:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <dx:ASPxLabel ID="lblLeftAmount" ClientInstanceName="lblLeftAmount" runat="server"
                                                    Text="">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="rowChequeUsedAmount">
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="จำนวนเงินที่ใช้จริง">
                                    </dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <dx:ASPxSpinEdit ID="spnChequeUsedAmount" runat="server" ClientInstanceName="spnChequeUsedAmount"
                                        Height="21px" Number="0" DisplayFormatString="#,#0.00" DecimalPlaces="2" MinValue="0"
                                        MaxValue="9999999999">
                                        <ClientSideEvents NumberChanged="function(s,e)
                                        {
                                            var a = s.GetValue();
                                            var b = spnChequeAmount.GetValue();
                                            if( a &gt; b )
                                            {
                                                alert('เงินที่ใช้มากกว่าเงินที่มี ');
                                                s.SetValue(b);
                                            }
                                        }" />
                                    </dx:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr id="rowChequeDate">
                                <td>
                                    <dx:ASPxLabel ID="lblChequeDate" runat="server" ClientInstanceName="lblChequeDate"
                                        Text="เช็คลงวันที่">
                                    </dx:ASPxLabel>
                                </td>
                                <td colspan="2">
                                    <whwc:DateTimeControl ID="ChequeDate" ClientInstanceName="ChequeDate" runat="server"
                                        ValidationGroup="group" IsRequiredField="true" />
                                </td>
                            </tr>
                            <tr id="rowPaymentDate">
                                <td>
                                    <dx:ASPxLabel ID="lblChequePaymentDate" ClientInstanceName="lblChequePaymentDate"
                                        runat="server" Text="วันที่ชำระเงิน" />
                                </td>
                                <td colspan="2">
                                    <whwc:DateTimeControl ID="paymentDate" ClientInstanceName="paymentDate" runat="server"
                                        ValidationGroup="group" IsRequiredField="true" />
                                </td>
                            </tr>
                            <tr id="rowDirectDebit">
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="บัญชีที่ใช้ตัดเงิน" />
                                </td>
                                <td colspan="2">
                                    <table cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td>
                                                <dx:ASPxButtonEdit ID="bteDirectdebit" ClientInstanceName="bteDirectdebit" runat="server"
                                                    Height="21">
                                                    <Buttons>
                                                        <dx:EditButton Image-Height="8">
                                                            <Image Url="~/Images/led_icon/find.png">
                                                            </Image>
                                                        </dx:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s,e)
                                                    {
                                                        cbAccountDirectDebit.SendCallback();
                                                    }" />
                                                </dx:ASPxButtonEdit>
                                                <dx:ASPxCallback ID="cbAccountDirectDebit" ClientInstanceName="cbAccountDirectDebit"
                                                    runat="server" OnCallback="cbAccountDirectDebit_Callback">
                                                    <ClientSideEvents CallbackComplete="function(s,e)
                                                    {
                                                        lblDiectDebit.SetText(e.result);
                                                    }" />
                                                </dx:ASPxCallback>
                                            </td>
                                            <td>
                                                &nbsp;
                                                <dx:ASPxLabel ID="lblDiectDebit" ClientInstanceName="lblDiectDebit" runat="server"
                                                    Text="">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table cellpadding="0px" cellspacing="0">
                            <tr>
                                <td style="padding-right: 5">
                                    <dx:ASPxButton ID="btnAdd" ClientInstanceName="btnAdd" runat="server" Text="เพิ่ม"
                                        Paddings-Padding="0px" AutoPostBack="false">
                                        <Image Url="~/Images/led_icon/add.png" Height="10">
                                        </Image>
                                        <ClientSideEvents Click="function(s,e){
                                            if(ASPxClientEdit.ValidateGroup('group'))
                                            {
                                                if(checkBeforeAdd())
                                                {
                                                    gridPaymetod.PerformCallback();
                                                }
                                            }
                                        }" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnClear" ClientInstanceName="btnClear" runat="server" Text="ล้าง"
                                        Paddings-Padding="0px" AutoPostBack="false">
                                        <Image Url="~/Images/led_icon/bin_closed.png" Height="10">
                                        </Image>
                                        <ClientSideEvents Click="function(s,e){
                                            reset();
                                        }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
                <ClientSideEvents Init="function(s,e){
                    initPanel();
                }" />
            </dx:ASPxRoundPanel>
        </td>
        <td valign="top" style="padding-left: 10">
            <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="รายการจ่ายเงิน">
                <PanelCollection>
                    <dx:PanelContent>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="gridPaymetod" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridPaymetod"
                                        OnCustomButtonCallback="delete_Callback" OnCustomCallback="add_Callback">
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" Width="30" Visible="false">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton Text="Delete">
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="วิธีการชำระเงิน" FieldName="PaymentType" VisibleIndex="1"
                                                CellStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Caption="วันที่ชำระเงิน" Name="PaymentDate" FieldName="PaymentDate"
                                                VisibleIndex="2" CellStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn Caption="จำนวนเงิน" FieldName="Amount" VisibleIndex="3"
                                                CellStyle-HorizontalAlign="Right" Width="130">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="จำนวนเงินที่ใช้" FieldName="AmountForThisTransaction"
                                                Width="130" VisibleIndex="4">
                                                <PropertiesTextEdit DisplayFormatString="#,#0.00">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowFooter="True" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="AmountForThisTransaction" SummaryType="Sum" DisplayFormat="#,#0.00" />
                                        </TotalSummary>
                                        <ClientSideEvents EndCallback="function(s, e){
                                            if(gridPaymetod.GetVisibleRowsOnPage() &gt; 0){
                                                btnAdd.SetEnabled(false);
                                            }else{
                                                btnAdd.SetEnabled(true);
                                            }
                                        }" />
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" ClientInstanceName="btnClear" runat="server" Text="ล้างรายการชำระเงินทั้งหมด"
                                        Paddings-Padding="0px" AutoPostBack="false">
                                        <Image Url="../Images/led_icon/bin_closed.png" Height="10">
                                        </Image>
                                        <ClientSideEvents Click="function(s,e){
                                            gridPaymetod.PerformCallback('clear');
                                        }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
</table>
<br />
<script type="text/javascript">
    function manageChequePanel(type) {
        var paymentType = cbxPayMethod.GetValue();
        if (paymentType == 'Cheque') {
            if (type == 'new') {
                lblCheque.SetText('Cheque ใหม่ในระบบ');
            }
            else {
                lblCheque.SetText('Cheque เดิมในระบบ');
                var b = type.split(';');
                var amount = b[0] * 1;
                var amountForTransaction = b[1] * 1;
                var strAmountForTransaction = b[2];
                var bankCode = b[3];
                var bankID = b[4];
                spnChequeAmount.SetValue(amount);
                lblLeftAmount.SetText(strAmountForTransaction);
                CbxClientName.SetValue(bankID);
                CbxClientName.SetText(bankCode);

                if (amountForTransaction == 0) {
                    alert('ไม่มีจำนวนเงินเหลือใน Cheque');
                    spnChequeUsedAmount.SetEnabled(0);
                }
                else {
                    spnChequeUsedAmount.SetMaxValue(amountForTransaction);
                    spnChequeUsedAmount.SetMinValue(0);
                }
                CbxClientName.SetEnabled(0);
                spnChequeAmount.SetEnabled(0);
            }
            showInfoCheque(type);
        }
        else {
            if (type == 'new') {

                lblCheque.SetText('Cheque ใหม่ในระบบ');
                showInfoCheque(type);
            }
            else {
                lblCheque.SetText('Cheque เดิมในระบบ');
            }
        }
    }
    function initPanel() {
        refreshControl();
        hideRow('rowPayType');
        hideRow('rowAmount');
        hideRow('rowChequeAmount');
        hideRow('rowChequeUsedAmount');
        hideRow('rowBank');
        hideRow('rowChequeNumber');
        hideRow('rowChequeDate');
        hideRow('rowPaymentDate');
        hideRow('rowDirectDebit');
    }
    function formByPayMethod(type) {
        initPanel();
        if (type == 'BankAccountDeposit') {
            showRow('rowPayType');
        }
        else if (type == 'FundTransfer') {
            //showRow('rowBank');
            //showRow('rowDirectDebit');
            showRow('rowAmount');
        }
        else if (type == 'Cash') {
            showRow('rowAmount');
        }
        else if (type == 'Cheque') {
            showRow('rowChequeNumber');
        }
        else if (type == 'BillPayment') {
            showRow('rowPayType');
        }
    }

    function formByPayType(type) {
        if (type == 'Cash') {
            lblCheque.SetText('');
            bteChequeNo.SetText('');
            spnChequeUsedAmount.SetValue(0);
            spnChequeUsedAmount.SetValue(0);
            showRow('rowAmount');
            showRow('rowPaymentDate');
            hideRow('rowChequeAmount');
            hideRow('rowChequeUsedAmount');
            hideRow('rowBank');
            hideRow('rowChequeNumber');
            hideRow('rowChequeDate');
        }
        else if (type == 'Cheque') {
            spnAmount.SetValue(0);
            showRow('rowChequeNumber');
            hideRow('rowAmount');
            hideRow('rowPaymentDate');
        }
    }

    function showInfoCheque(a) {
        if (a == 'new') {
            hideRow('rowAmount');
            showRow('rowChequeAmount');
            showRow('rowChequeUsedAmount');
            showRow('rowBank');
            showRow('rowChequeDate');
            showRow('rowPaymentDate');
        }
        else {
            hideRow('rowAmount');
            showRow('rowChequeAmount');
            showRow('rowChequeUsedAmount');
            showRow('rowBank');
            hideRow('rowChequeDate');
            hideRow('rowPaymentDate');
        }
    }

    function showRow(elementID) {
        document.getElementById(elementID).style.display = 'table-row';
    }

    function hideRow(elementID) {
        document.getElementById(elementID).style.display = 'none';
    }

    function refreshControl() {
        CbxClientName.SetEnabled(1);
        spnChequeAmount.SetEnabled(1);
        spnChequeUsedAmount.SetEnabled(1);
        lblCheque.SetText('');
        bteChequeNo.SetText('');
        cbxPayType.SetText('');
        bteChequeNo.SetText('');
        lblLeftAmount.SetText('');
        spnAmount.SetValue(0);
        spnChequeAmount.SetValue(0);
        spnChequeUsedAmount.SetValue(0);
        spnChequeUsedAmount.SetMaxValue(999999999);
        spnChequeUsedAmount.SetMinValue(0);
    }

    function reset() {
        initPanel();
        cbxPayMethod.SetText('');
    }

    function checkCheque(chequeID) {
        var a = chequeID;
        for (var i = 0; i < a.length; i++) {
            if (a.charAt(i) == '#') {
                return false;
            }
        }
        return true;
    }

    function checkBeforeAdd() {
        var type = cbxPayMethod.GetValue();
        var payType = '';
        if (type == 'BankAccountDeposit') {
            payType = cbxPayType.GetText()
            if (payType == '') {
                alert('เลือก Cash หรือ Cheque');
                return false;
            }
            if (payType == 'Cheque') {
                var c = bteChequeNo.GetText();
                if (!checkCheque(c)) {
                    alert('ระบุข้อมูลให้ถูกต้อง');
                    return false;
                }
            }
        }
        else if (type == 'FundTransfer') {
//            payType = bteDirectdebit.GetText();
//            if (payType == '') {
//                alert('ใส่บัญชี');
//                return false;
//            }
        }
        else if (type == 'Cash') {

        }
        else if (type == 'Cheque') {
            var c = bteChequeNo.GetText();
            if (!checkCheque(c)) {
                alert('ระบุเลขที่ Cheque ให้ถูกต้อง');
                return false;
            }
        }
        else if (type == 'BillPayment') {
            payType = cbxPayType.GetText()
            if (payType == '') {
                alert('เลือก Cash หรือ Cheque');
                return false;
            }
            if (payType == 'Cheque') {
                var c = bteChequeNo.GetText();
                if (!checkCheque(c)) {
                    return false;
                }
            }
        }
        return true;
    }
</script>