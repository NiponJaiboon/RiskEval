<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_FIFOReleasedCreditControl" Codebehind="FIFOReleasedCreditControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<dx:ASPxGridView ID="gdvReleasedCredit" ClientInstanceName="gdvReleasedCredit" runat="server"
    OnBeforePerformDataSelect="gdvReleasedCredit_BeforePerformDataSelect">
    <Columns>
        <dx:GridViewDataDateColumn Name="InitialDate">
            <CellStyle Wrap="False">
            </CellStyle>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataDateColumn Name="TradeDate">
            <CellStyle Wrap="False">
            </CellStyle>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Name="RemainingUnits" >
            <CellStyle Wrap="False">
            </CellStyle>
        </dx:GridViewDataTextColumn>
    </Columns>
    <Settings ShowFooter="true" />
    <SettingsBehavior AllowSort="false" />
</dx:ASPxGridView>
<asp:HiddenField ID="hddInvestmentID" runat="server" />
