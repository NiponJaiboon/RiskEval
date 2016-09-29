<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_EmployeesControl" Codebehind="EmployeesControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Src="EmployerControl.ascx" TagName="EmployerControl" TagPrefix="uc1" %>
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <dx:ASPxButtonEdit ID="bteEmployees" runat="server">
                <Buttons>
                    <dx:EditButton>
                    </dx:EditButton>
                </Buttons>
            </dx:ASPxButtonEdit>
        </td>
        <td style="padding-left: 5" style="padding-right: 5">
            <dx:ASPxImage ID="imgDetail" runat="server" ImageUrl="<%$ Resources : Resource_ImgUrl, Detail %>">
            </dx:ASPxImage>
        </td>
        <td style="padding-left: 5" style="padding-right: 5">
            <dx:ASPxLabel ID="lblEmployee" runat="server">
            </dx:ASPxLabel>
        </td>
        <td>
            <asp:Image Style="visibility: hidden" ImageUrl="~/Images/loader1.gif" ID="imgLoader"
                runat="server" />
        </td>
    </tr>
</table>
<dxpc:ASPxPopupControl ID="popupEmployees" runat="server" AllowDragging="True" CloseAction="CloseButton"
    HeaderText="พนักงาน" Modal="false" PopupHorizontalAlign="OutsideRight" PopupVerticalAlign="Middle"
    EnableAnimation="false" Width="500px" Height="500px" PopupAction="None" AllowResize="false"
    ResizingMode="Postponed" ShowShadow="false">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="padding-right: 2px" width="100%">
                <tr>
                    <td nowrap="nowrap">
                        <dx:ASPxButton ID="btnSelectAll" ClientInstanceName="btnSelectAll" runat="server"
                            Text="Select All" Wrap="False" AutoPostBack="false">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="Clear" AutoPostBack="false">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnInverse" runat="server" Text="Inverse" AutoPostBack="false">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnReflesh" runat="server" Text="Reflesh" AutoPostBack="false">
                        </dx:ASPxButton>
                    </td>
                    <td align="right" style="width: 100%">
                        <dx:ASPxButton ID="btnSelect" runat="server" Text="Select" AutoPostBack="false">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="gridEmployees" runat="server" AutoGenerateColumns="False"
                KeyFieldName="AccountID" Width="100%" DataSourceID="sdsEmployees" OnCustomCallback="gridEmployees_CustomCallback"
                SettingsPager-Mode="ShowAllRecords" Settings-VerticalScrollableHeight="500" OnBeforePerformDataSelect="gridEmployees_BeforePerformDataSelect">
                <Columns>
                    <dxwgv:GridViewCommandColumn Name="SelectColumn" ShowSelectCheckbox="true" VisibleIndex="0">
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewCommandColumn Name="SelectColumn" VisibleIndex="1">
                        <CustomButtons>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnGridSelect" Text="Select">
                            </dxwgv:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DivisionCode" Caption="รหัสแผนก" VisibleIndex="2"
                        Settings-FilterMode="DisplayText">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="EmployeeNo" Caption="รหัสพนักงาน" VisibleIndex="3"
                        Settings-FilterMode="DisplayText">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="EmployeeName" Caption="ชื่อ-สกุล" VisibleIndex="4"
                        Settings-AutoFilterCondition="Contains" Settings-FilterMode="DisplayText">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem ShowInColumn="EmployeeName" FieldName="AccountID" SummaryType="Count" />
                </TotalSummary>
                <Settings ShowFooter="true" ShowVerticalScrollBar="True" ShowFilterRow="True" ShowFilterRowMenu="true" />
                <SettingsPager AlwaysShowPager="false">
                </SettingsPager>
                <SettingsBehavior AllowFocusedRow="false" />
            </dxwgv:ASPxGridView>
            <asp:SqlDataSource runat="server" ID="sdsEmployees" ConnectionString="<%$ ConnectionStrings:imSabayaConnectionString %>"
                SelectCommandType="StoredProcedure" SelectCommand="usp_PFEmployees">
                <SelectParameters>
                    <asp:Parameter Name="employerID" Type="Int32" />
                    <asp:Parameter Name="lang" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<dx:ASPxHiddenField ID="hddEmployeesSelected" runat="server">
</dx:ASPxHiddenField>
<dxpc:ASPxPopupControl ID="popupSelectedEmployees" runat="server" CloseAction="OuterMouseClick"
    Modal="false" EnableAnimation="false" Width="300px" ShowHeader="false" PopupHorizontalAlign="OutsideRight"
    PopupVerticalAlign="Middle" RenderIFrameForPopupElements="False" AllowDragging="True"
    AppearAfter="100" AllowResize="True" Height="50px">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server">
        </dxpc:PopupControlContentControl>
    </ContentCollection>
    <Border BorderWidth="1" />
</dxpc:ASPxPopupControl>