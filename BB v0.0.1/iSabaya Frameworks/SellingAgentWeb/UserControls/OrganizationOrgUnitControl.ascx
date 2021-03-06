<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="OrganizationOrgUnitControl" Codebehind="OrganizationOrgUnitControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<dxcp:ASPxCallbackPanel ID="cbpTextPartyEdit" runat="server" ClientInstanceName="cbpTextPartyEdit"
    OnCallback="cbpTextPartyEdit_Callback">
    <PanelCollection>
        <dxp:PanelContent ID="PanelContent1" runat="server">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td nowrap="nowrap">
                        <dxe:ASPxButtonEdit runat="server" ClientInstanceName="textPartyEdit" ID="textPartyEdit">
                            <Buttons>
                                <dxe:EditButton>
                                </dxe:EditButton>
                            </Buttons>
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td nowrap="nowrap" align="left" style="padding-left: 2px">
                        <dxe:ASPxLabel runat="server" ID="lblDescription" ClientInstanceName="lblDescription">
                        </dxe:ASPxLabel>
                    </td>
                    <td nowrap="nowrap" style="padding-left: 2px">
                        <dxe:ASPxLabel ID="labelParty" runat="server" ClientInstanceName="labelParty">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
            </table>
        </dxp:PanelContent>
    </PanelCollection>
</dxcp:ASPxCallbackPanel>
<dxpc:ASPxPopupControl ID="popupParty" runat="server" HeaderText="บริษัท" AllowDragging="True"
    EnableAnimation="false" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" ClientInstanceName="popupParty" ShowPageScrollbarWhenModal="True">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <div style="height: 300; overflow: scroll;">
                <dxwtl:ASPxTreeList ID="treeList" runat="server" AutoGenerateColumns="False" ClientInstanceName="treeList"
                    KeyFieldName="ID" ParentFieldName="OrgParentID" OnCustomJSProperties="treeList_CustomJSProperties">
                    <Columns>
                        <dxwtl:TreeListDataColumn FieldName="FullName" Caption="ชื่อ" VisibleIndex="1">
                        </dxwtl:TreeListDataColumn>
                        <dxwtl:TreeListDataColumn FieldName="Code" Caption="Code" VisibleIndex="2">
                        </dxwtl:TreeListDataColumn>
                    </Columns>
                    <%--<Settings SuppressOuterGridLines="True" />--%>
                    <SettingsBehavior AllowFocusedNode="True" />
                    <%--<SettingsSelection Enabled="True" />--%>
                    <Border BorderStyle="Solid" />
                </dxwtl:ASPxTreeList>
            </div>
            <%--<dxe:ASPxButton runat="server" ClientInstanceName="btnSelect" Text="Select" ID="btnSelect"
                AutoPostBack="False">
            </dxe:ASPxButton>--%>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<dx:ASPxHiddenField ID="hddOrganization" runat="server" ClientInstanceName="hddOrganization">
</dx:ASPxHiddenField>