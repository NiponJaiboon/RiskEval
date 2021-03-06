<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ScheduleBrowseControl" Codebehind="ScheduleBrowseControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>
<dxcp:ASPxCallbackPanel ID="cbpTextScheduleEdit" runat="server" ClientInstanceName="cbpTextScheduleEdit"
    Width="200px" OnCallback="cbpTextScheduleEdit_Callback">
    <PanelCollection>
        <dxp:PanelContent runat="server">
            <dxe:ASPxTextBox ID="txtHiddenAccountId" ClientInstanceName="txtHiddenAccountId"
                runat="server" Width="170px" Visible="true">
                 <ClientSideEvents Init="function(s, e) {
                 txtHiddenAccountId.SetVisible(false);
                 }" 
                 
                 />
            </dxe:ASPxTextBox>
            <dxe:ASPxButtonEdit runat="server" ClientInstanceName="textScheduleEdit" ID="textScheduleEdit">
                <ClientSideEvents ButtonClick="function(s, e) {
	var win = popupSchedule.GetWindow(0);
	popupSchedule.ShowWindow(win);
}"></ClientSideEvents>
                <Buttons>
                    <dxe:EditButton>
                    </dxe:EditButton>
                </Buttons>
            </dxe:ASPxButtonEdit>
            <dxe:ASPxLabel ID="labelSchedule" runat="server" ClientInstanceName="labelSchedule">
            </dxe:ASPxLabel>
        </dxp:PanelContent>
    </PanelCollection>
</dxcp:ASPxCallbackPanel>
<dxpc:ASPxPopupControl ID="popupSchedule" runat="server" AllowDragging="True" CloseAction="CloseButton"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    HeaderText="ปฏิทิน" ClientInstanceName="popupSchedule">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server">
            &nbsp;<table>
                <tbody>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <dxe:ASPxTextBox runat="server" Width="170px" ClientInstanceName="textKeyWord" ID="textKeyWord"
                                Visible="False">
                            </dxe:ASPxTextBox>
                        </td>
                        <td>
                            <dxe:ASPxButton runat="server" ClientInstanceName="buttonSearch" Text="Search" ID="buttonSearch"
                                AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
	cbSearch.SendCallback();
}" />
                            </dxe:ASPxButton>
                            <dxcb:ASPxCallback ID="cbSearch" runat="server" ClientInstanceName="cbSearch" OnCallback="cbSearch_Callback">
                                <ClientSideEvents CallbackComplete="function(s, e) {
	gridSchedules.PerformCallback();
}"></ClientSideEvents>
                            </dxcb:ASPxCallback>
                        </td>
                    </tr>
                </tbody>
            </table>
            <dxwgv:ASPxGridView runat="server" KeyFieldName="ScheduleID" ClientInstanceName="gridSchedules"
                AutoGenerateColumns="False" ID="gridSchedules" Width="400px">
                <ClientSideEvents CustomButtonClick="function(s, e) {
	            var buttonID = e.buttonID;               
                var visibleIndex = e.visibleIndex;
                  if(buttonID = 'buttonSelect')
                  {
                         
                        
                         	cbpTextScheduleEdit.PerformCallback(visibleIndex);
                         	popupSchedule.Hide();
                         
                  }
               }" />
                <Columns>
                    <dxwgv:GridViewCommandColumn Caption="Action">
                        <CustomButtons>
                            <dxwgv:GridViewCommandColumnCustomButton ID="buttonSelect" Text="เลือก">
                            </dxwgv:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Title" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="EffectivePeriod">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
               
               
                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="300" />
            </dxwgv:ASPxGridView>
         
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
