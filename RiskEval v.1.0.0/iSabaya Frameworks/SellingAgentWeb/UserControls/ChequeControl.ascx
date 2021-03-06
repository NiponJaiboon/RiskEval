<%@ Control Language="C#" AutoEventWireup="True"
    Inherits="ctrls_ChequeControl" Codebehind="ChequeControl.ascx.cs" %>
<%@ Register Src="BankAccountTextBoxControl.ascx" TagName="BankAccountTextBoxControl"
    TagPrefix="uc3" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Src="BankControl.ascx" TagName="BankControl" TagPrefix="uc5" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="dateControl" %>
<%--<asp:Panel ID="Panel1" runat="server" GroupingText="Cheque" CssClass="defaultFont">--%>
<table>
    <tr>
        <td>
            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="ธนาคาร" CssClass="defaultFont">
            </dxe:ASPxLabel>
        </td>
        <td>
            <uc5:BankControl ID="BankControl1" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxLabel ID="LabelChequeNumber" runat="server" Text="เลขที่เช็ค" CssClass="defaultFont">
            </dxe:ASPxLabel>
        </td>
        <td>
            <dxe:ASPxTextBox ID="TextChequeNumber" runat="server" Width="170px" CssClass="defaultFont">
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxLabel ID="LabelDateCheque" runat="server" Text="เช็คลงวันที่" CssClass="defaultFont">
            </dxe:ASPxLabel>
        </td>
        <td>
            <%-- <dxe:ASPxDateEdit ID="DateCheque" runat="server" CssClass="defaultFont">
                </dxe:ASPxDateEdit>--%>
            <dateControl:DateTimeControl ID="DateCheque" runat="server" />
        </td>
    </tr>
</table>
<%--</asp:Panel>--%>