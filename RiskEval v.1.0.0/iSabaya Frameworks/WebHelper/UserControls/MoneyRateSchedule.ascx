<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_MoneyRateSchedule" Codebehind="MoneyRateSchedule.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<dx:ASPxButtonEdit ID="btnCommission" ClientInstanceName="btnCommission" runat="server">
    <Buttons>
        <dx:EditButton>
        </dx:EditButton>
    </Buttons>
</dx:ASPxButtonEdit>
<dx:ASPxPopupControl ID="popup" runat="server" CloseAction="CloseButton" HeaderText="Money Rate Schedule"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" EnableHierarchyRecreation="True"
    AllowDragging="True">
    <ContentCollection>
        <dx:PopupControlContentControl>
            <dx:ASPxTreeList ID="tltMoneyRateSchedule" runat="server" Width="170px" DataSourceID="sdsMoneyRateSchedule"
                KeyFieldName="MoneyRateScheduleID" AutoGenerateColumns="false" AutoGenerateServiceColumns="false">
                <Columns>
                    <dx:TreeListDataColumn FieldName="Code" Caption="รหัส">
                    </dx:TreeListDataColumn>
                    <dx:TreeListDataColumn FieldName="EffectiveFrom" Caption="เริ่มต้นระยะเวลาที่มีผล">
                    </dx:TreeListDataColumn>
                    <dx:TreeListDataColumn FieldName="EffectiveTo" Caption="สิ้นสุดระยะเวลาที่มีผล">
                    </dx:TreeListDataColumn>
                </Columns>
                <SettingsBehavior AllowFocusedNode="True" />
            </dx:ASPxTreeList>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<asp:SqlDataSource ID="sdsMoneyRateSchedule" runat="server" DataSourceMode="DataReader"
    ConnectionString="<%$ ConnectionStrings:imSabayaConnectionString%>" SelectCommand="SELECT MoneyRateScheduleID, Code ,EffectiveFrom ,EffectiveTo
                FROM MoneyRateSchedule"></asp:SqlDataSource>