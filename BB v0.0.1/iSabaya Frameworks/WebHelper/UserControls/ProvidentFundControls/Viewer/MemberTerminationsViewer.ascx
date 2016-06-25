<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_Viewer_MemberTerminationsViewer" Codebehind="MemberTerminationsViewer.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Src="TerminationInfoViewer.ascx" TagName="TerminationInfoViewer"
    TagPrefix="IMCtrl" %>
<table>
    <tr>
        <td valign="top">
            <dx:ASPxGridView ID="gdvTerminationInfos" ClientInstanceName="gdvTerminationInfos"
                runat="server" KeyFieldName="ID">
                <Columns>
                    <%--<dx:GridViewDataColumn Name="AccountNo" Caption="Account No" FieldName="AccountNo">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="MemberNo" Caption="Member No" FieldName="MemberNo">
                    </dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn Name="Member" Caption="Member" FieldName="Member">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="TerminationType" Caption="Termination Type" FieldName="TerminationType">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dx:GridViewDataColumn>
                </Columns>
                <TotalSummary>
                <dx:ASPxSummaryItem FieldName="Member" ShowInColumn="Member" SummaryType="Count" />
                </TotalSummary>
                <SettingsPager PageSize="15" />
                <SettingsBehavior AllowFocusedRow="true" AutoExpandAllGroups="true" AllowGroup="true" />
                <Settings ShowGroupPanel="false" ShowFooter="true" ShowGroupedColumns="false" />
            </dx:ASPxGridView>
        </td>
        <td valign="top">
            <dx:ASPxCallbackPanel ID="cbpTerminationInfoViewer" runat="server" HideContentOnCallback="false"
                OnCallback="cbpTerminationInfoViewer_Callback">
                <PanelCollection>
                    <dx:PanelContent>
                        <IMCtrl:TerminationInfoViewer ID="ctrlTerminationInfoViewer" runat="server" />
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </td>
    </tr>
</table>
