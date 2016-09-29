<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_Viewer_TransactionViewer" Codebehind="TransactionViewer.ascx.cs" %>
<table id="tbControl" runat="server" width="500px" align="center">
    <tr>
        <td>
            <asp:Table ID="tbData" runat="server" Width="100%">
            </asp:Table>
        </td>
    </tr>
    <tr>
        <td>
            <br />
            <asp:Table ID="tbQuantity" runat="server" Width="100%">
            </asp:Table>
        </td>
    </tr>
</table>
