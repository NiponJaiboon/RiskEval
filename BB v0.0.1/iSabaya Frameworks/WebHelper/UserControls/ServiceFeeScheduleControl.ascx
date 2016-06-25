<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceFeeScheduleControl.ascx.cs"
    Inherits="ServiceFeeScheduleControl" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<dx:ASPxGridView ID="grid" runat="server" EnableRowsCache="false" ClientInstanceName="gridFundsTransferServices"
    Width="100%" OnCustomCallback="grid_CustomCallback" OnDataBinding="grid_DataBinding"
    KeyFieldName="ID" OnRowUpdating="grid_RowUpdating" OnCellEditorInitialize="grid_CellEditorInitialize"
    OnParseValue="grid_ParseValue" SettingsPager-PageSize="10" SettingsPager-PageSizeItemSettings-Visible="true"
    SettingsPager-Summary-Position="Right" SettingsPager-PageSizeItemSettings-Caption="จำนวนรายการต่อหน้า"
    SettingsPager-Summary-AllPagesText="{0} - {1} ({2} items)" SettingsPager-Summary-Text="หน้าที่ {0} จาก {1} หน้า (ทั้งหมด {2} รายการ)">
    <Columns>
        <dx:GridViewDataColumn Caption="Test" FieldName="ID">
        </dx:GridViewDataColumn>
    </Columns>
</dx:ASPxGridView>
