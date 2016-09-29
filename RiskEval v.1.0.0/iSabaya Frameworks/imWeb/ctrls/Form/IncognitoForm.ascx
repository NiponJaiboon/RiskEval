<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="IncognitoForm" Codebehind="IncognitoForm.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHeadline" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="whwc" %>
<dx:ASPxCallbackPanel ID="cbpSave" runat="server" OnCallback="cbpSave_Callback" ShowLoadingPanel="false">
    <PanelCollection>
        <dx:PanelContent ID="pnc" runat="server">
            <dx:ASPxHeadline ID="ASPxHeadline1" runat="server">
            </dx:ASPxHeadline>
            <dxe:ASPxLabel ID="lblTitle" runat="server" Font-Bold="true" Font-Size="Large">
            </dxe:ASPxLabel>
            <hr style="width: 100%; border-width: thin; border-style: solid" id="hrTitle" runat="server"
                visible="false" />
            <asp:Table class="tbFormBase" ID="headTable" runat="server" ClientIDMode="Static">
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHReference" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <dxe:ASPxTextBox ID="txtReference" runat="server">
                        </dxe:ASPxTextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHOrderedDate" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <whwc:DateTimeControl ID="ctrlOrderedDate" runat="server"></whwc:DateTimeControl>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHAlias" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <dxe:ASPxTextBox ID="txtAlias" runat="server">
                        </dxe:ASPxTextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHAgent" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <whuc:OrganizationControl ID="ctrlAgent" runat="server" OrganizationOnly="true">
                        </whuc:OrganizationControl>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHEmail" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <dxe:ASPxTextBox ID="txtEmail" runat="server">
                        </dxe:ASPxTextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHCitizenOf" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <whuc:CountryControl ID="ctrlCitizenOf" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHOccupation" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <whwc:CategoryControl ID="ctrlOccupation" runat="server">
                        </whwc:CategoryControl>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHNationality" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <whwc:CategoryControl ID="ctrlNationality" runat="server">
                        </whwc:CategoryControl>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHReligion" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <whwc:CategoryControl ID="ctrlReligion" runat="server">
                        </whwc:CategoryControl>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHFaxs" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <dxe:ASPxTextBox ID="txtFaxs" runat="server">
                        </dxe:ASPxTextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHPhone" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <dxe:ASPxTextBox ID="txtPhone" runat="server">
                        </dxe:ASPxTextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHMobile" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <dxe:ASPxTextBox ID="txtMobile" runat="server">
                        </dxe:ASPxTextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHRemark" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <dxe:ASPxMemo ID="txtRemark" runat="server" Width="170">
                        </dxe:ASPxMemo>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <dxe:ASPxLabel ID="lblHEffectivePeriod" runat="server">
                        </dxe:ASPxLabel>
                    </asp:TableCell>
                    <asp:TableCell>
                        <whuc:TimeIntervalControl ID="ctrlEffectivePeriod" HideLabelFrom="true" HideLabelTo="true"
                            runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <table id="tbSaveCommand" runat="server" width="100%">
                <tr align="center">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btnSave" runat="server" AutoPostBack="false">
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btnCancel" runat="server" AutoPostBack="false">
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hddVisibleEditForm" runat="server" />
            <asp:HiddenField ID="hddValueID" runat="server" />
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>