<%@ Control Language="C#" AutoEventWireup="true" Inherits="global_Address" CodeBehind="AddressControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="ctrlWebHelper" %>
<%@ Register Src="TimeIntervalControl.ascx" TagName="ctrl" TagPrefix="ctrlTimeInterval" %>
<dx:ASPxHiddenField ID="hdfAddess" ClientInstanceName="hdfAddess" runat="server">
</dx:ASPxHiddenField>
<asp:Table ID="tableContent" runat="server" ClientIDMode="Static">
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" />
            <span style="color: Red">* </span>
        </asp:TableCell>
        <asp:TableCell>
            <ctrlWebHelper:CategoryControl ID="ctrlAddressNode" runat="server">
            </ctrlWebHelper:CategoryControl>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="LabelAccountNo" runat="server" />
            <span style="color: Red">* </span>
        </asp:TableCell>
        <asp:TableCell>
            <cc1:MLSControl ID="mlsAddressNo" runat="server">
            </cc1:MLSControl>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="LabelBuilding" runat="server" />
        </asp:TableCell>
        <asp:TableCell>
            <cc1:MLSControl ID="mlsBuilding" runat="server">
            </cc1:MLSControl>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" />
        </asp:TableCell>
        <asp:TableCell>
            <dxe:ASPxTextBox ID="txtFloor" runat="server">
            </dxe:ASPxTextBox>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="LabelRoomNo" runat="server" />
        </asp:TableCell>
        <asp:TableCell>
            <dxe:ASPxTextBox ID="txtRoomNo" runat="server">
            </dxe:ASPxTextBox>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="LabelStreet1" runat="server" />
        </asp:TableCell>
        <asp:TableCell>
            <cc1:MLSControl ID="mlsStreet1" runat="server">
            </cc1:MLSControl>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="LabelStreet2" runat="server" />
        </asp:TableCell>
        <asp:TableCell>
            <cc1:MLSControl ID="mlsStreet2" runat="server">
            </cc1:MLSControl>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="LabelPostalCode" runat="server" CssClass="defaultFont" />
            <span style="color: Red">* </span>
        </asp:TableCell>
        <asp:TableCell>
            <dxe:ASPxTextBox ID="txtPostalCode" runat="server" CssClass="defaultFont" Width="170px" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="LabelCountry" runat="server" CssClass="defaultFont" />
            <span style="color: Red">* </span>
        </asp:TableCell>
        <asp:TableCell>
            <dxe:ASPxComboBox ID="cboCountry" runat="server" IncrementalFilteringMode="StartsWith">
            </dxe:ASPxComboBox>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" />
            <span style="color: Red">* </span>
        </asp:TableCell>
        <asp:TableCell>
            <cc1:CategoryControl ID="ctrlRegionLevel1" runat="server" ClientInstanceName="ctrlRegionLevel1"
                IsLeaf="false">
            </cc1:CategoryControl>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" CssClass="defaultFont" />
            <span style="color: Red">* </span>
        </asp:TableCell>
        <asp:TableCell>
            <cc1:CategoryControl ID="ctrlRegionLevel2" runat="server" ClientInstanceName="ctrlRegionLevel2"
                IsLeaf="false">
            </cc1:CategoryControl>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="LabelPhone" runat="server" CssClass="defaultFont" />
        </asp:TableCell>
        <asp:TableCell>
            <dxe:ASPxTextBox ID="txtPhone" runat="server" CssClass="defaultFont" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="LabelPhone0" runat="server" CssClass="defaultFont" />
        </asp:TableCell>
        <asp:TableCell>
            <dxe:ASPxTextBox ID="txtFax" runat="server" CssClass="defaultFont" />
        </asp:TableCell>
    </asp:TableRow>
    <%--    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="ASPxLabel4" Text="วันที่มีผล" runat="server" CssClass="defaultFont" />
        </asp:TableCell>
        <asp:TableCell>
            <ctrlTimeInterval:ctrl ID="ctrlTimeInterVal" runat="server"/>
        </asp:TableCell>
        <asp:TableCell>
            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" CssClass="defaultFont" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <dxe:ASPxLabel ID="ASPxLabel5" Text="หมายเหตุ" runat="server" CssClass="defaultFont" />
        </asp:TableCell>
        <asp:TableCell>
            <dxe:ASPxTextBox ID="tbxRemark" runat="server" CssClass="defaultFont" />
        </asp:TableCell>
        <asp:TableCell>
            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" CssClass="defaultFont" />
        </asp:TableCell>
    </asp:TableRow>--%>
</asp:Table>
<asp:HiddenField ID="hddAddressID" runat="server" />