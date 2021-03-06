<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_NodePopupControl" Codebehind="NodePopupControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>
<%--<asp:HiddenField ID="HiddenField_SelectedCode" runat="server" />--%>
<dxhf:ASPxHiddenField ID="HiddenField_SelectedCode" runat="server">
</dxhf:ASPxHiddenField>
<dxcb:ASPxCallback ID="cbSelect" runat="server" OnCallback="cbSelect_Callback" ClientInstanceName="cbSelect">
</dxcb:ASPxCallback>
<dxcb:ASPxCallback ID="cbOpenPopupClick" runat="server" OnCallback="cbOpenPopupClick_Callback">
</dxcb:ASPxCallback>
<dxe:ASPxButtonEdit ID="btnEditTreeListnode" runat="server" ReadOnly="true" ReadOnlyStyle-BackColor="WhiteSmoke">
    <Buttons>
        <dxe:EditButton>
        </dxe:EditButton>
    </Buttons>
</dxe:ASPxButtonEdit>
<dxpc:ASPxPopupControl ID="ASPxPopupControl1" AllowDragging="True" CloseAction="CloseButton"
    AllowResize="true" Modal="True" runat="server" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" HeaderText="ข้อมูลพื้นฐาน" ShowPageScrollbarWhenModal="true">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="คำอธิบาย" Width="50px">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txtFilter" runat="server" Width="100px" ClientInstanceName="txtFilter">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnFilter" runat="server" AutoPostBack="False" ClientInstanceName="btnFilter"
                            Text="กรอง">
                            <ClientSideEvents Click="function(s, e) {
								cbpTree.PerformCallback(txtFilter.GetValue());
							}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxcp:ASPxCallbackPanel ID="cbpTree" ClientInstanceName="cbpTree" runat="server"
                OnCallback="cbpTree_Callback">
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <div style="height: 200; overflow: auto; top: auto; vertical-align: super">
                            <dxwtl:ASPxTreeList ID="treeList" runat="server" AutoGenerateColumns="False" ClientInstanceName="treeList"
                                KeyFieldName="NodeID" ParentFieldName="ParentNodeID" Width="100%">
                                <Columns>
                                    <dxwtl:TreeListCommandColumn ButtonType="Link" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwtl:TreeListCommandColumnCustomButton ID="btnSelect" Text="เลือก">
                                            </dxwtl:TreeListCommandColumnCustomButton>
                                        </CustomButtons>
                                    </dxwtl:TreeListCommandColumn>
                                    <dxwtl:TreeListDataColumn Caption="รหัส" FieldName="Code" VisibleIndex="1">
                                    </dxwtl:TreeListDataColumn>
                                    <dxwtl:TreeListDataColumn Caption="คำอธิบาย" FieldName="Desc" VisibleIndex="2">
                                    </dxwtl:TreeListDataColumn>
                                </Columns>
                                <Settings SuppressOuterGridLines="True" />
                                <SettingsBehavior AllowFocusedNode="true" ExpandCollapseAction="NodeDblClick" />
                                <%-- <SettingsSelection Enabled="True" />--%>
                                <Border BorderStyle="Solid" />
                            </dxwtl:ASPxTreeList>
                        </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>