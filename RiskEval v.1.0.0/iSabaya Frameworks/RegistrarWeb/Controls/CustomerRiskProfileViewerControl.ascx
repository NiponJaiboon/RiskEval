<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CustomerRiskProfileViewerControl" Codebehind="CustomerRiskProfileViewerControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="cc1" %>
<center>
    <table id="tbQuestionnaireDate" runat="server" class="tbFormbase" cellpadding="0"
        cellspacing="0">
        <tr>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel1" runat="server">
                </dxe:ASPxLabel>
            </td>
            <td style="padding-left: 5">
                <dxe:ASPxLabel ID="lblQuestionniareDate" runat="server">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
</center>
<br />
<cc1:QuestionnaireForm ID="ctrlQuestionniare" runat="server" ReadOnly="true">
</cc1:QuestionnaireForm>
<%--<table id="tbViewQuestionniare" runat="server">
</table>--%>