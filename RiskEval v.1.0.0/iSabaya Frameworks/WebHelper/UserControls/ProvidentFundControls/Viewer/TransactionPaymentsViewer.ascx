<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_Viewer_TransactionPaymentsViewer" Codebehind="TransactionPaymentsViewer.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<table width="100%">
    <tr>
        <td valign="top">
            <dx:ASPxGridView ID="gdvPayments" ClientInstanceName="gdvPayments" runat="server"
                KeyFieldName="ID" OnInit="gdvPayments_Init" OnHtmlRowPrepared="gdvPayments_HtmlRowPrepared" Width="100%" >
                <Columns>
                    <dx:GridViewDataColumn Name="TransactionNo" Caption="TransactionNo" FieldName="TransactionNo">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="Payer" Caption="Payer" FieldName="Payer">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="Payee" Caption="Payee" FieldName="Payee">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="Type" Caption="Type" FieldName="Type">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn Name="Amount" Caption="Amount" FieldName="Amount">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsPager PageSize="3" />
                <SettingsBehavior AllowFocusedRow="true" AutoExpandAllGroups="true" AllowGroup="true" />
                <Settings ShowGroupPanel="true" ShowFooter="true" ShowPreview="true" />
            </dx:ASPxGridView>
        </td>
    </tr>
</table>
