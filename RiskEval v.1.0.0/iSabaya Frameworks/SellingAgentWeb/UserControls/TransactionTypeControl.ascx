<%@ Control Language="C#" AutoEventWireup="true" Inherits="ctrls_TransactionTypeControl"
    CodeBehind="TransactionTypeControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%--<dxe:ASPxComboBox ID="ComboTransactionType" runat="Server">
</dxe:ASPxComboBox>--%>
<script type="text/javascript">
    var textSeparator = ";";
    function OnListBoxSelectionChanged(lbTransactionTypes, args) {
        if (args.index == 0)
            args.isSelected ? lbTransactionTypes.SelectAll() : lbTransactionTypes.UnselectAll();
        UpdateSelectAllItemState();
        UpdateText();
    }
    function UpdateSelectAllItemState() {
        IsAllSelected() ? lbTransactionTypes.SelectIndices([0]) : lbTransactionTypes.UnselectIndices([0]);
    }
    function IsAllSelected() {
        for (var i = 1; i < lbTransactionTypes.GetItemCount(); i++)
            if (!lbTransactionTypes.GetItem(i).selected)
                return false;
        return true;
    }
    function UpdateText() {
        var selectedItems = lbTransactionTypes.GetSelectedItems();
        ComboTransactionType.SetText(GetSelectedItemsText(selectedItems));
    }
    function SynchronizeListBoxValues(dropDown, args) {
        lbTransactionTypes.UnselectAll();
        var texts = dropDown.GetText().split(textSeparator);
        var values = GetValuesByTexts(texts);
        lbTransactionTypes.SelectValues(values);
        UpdateSelectAllItemState();
        UpdateText(); // for remove non-existing texts
    }
    function GetSelectedItemsText(items) {
        var texts = [];
        for (var i = 0; i < items.length; i++)
        //if (items[i].index != 0)
            if (items[i].text != "ทุกประเภท") {
                texts.push(items[i].text);
            }
        return texts.join(textSeparator);
    }
    function GetValuesByTexts(texts) {
        var actualValues = [];
        var item;
        for (var i = 0; i < texts.length; i++) {
            item = lbTransactionTypes.FindItemByText(texts[i]);
            if (item != null)
                actualValues.push(item.value);
        }
        return actualValues;
    }
</script>
<dxe:ASPxDropDownEdit ID="ComboTransactionType" ClientInstanceName="ComboTransactionType"
    Width="300" runat="server" EnableAnimation="False" OnDataBinding="ComboTransactionType_DataBinding">
    <DropDownWindowStyle BackColor="#EDEDED" />
    <DropDownWindowTemplate>
        <dxe:ASPxListBox ID="lbTransactionTypes" ClientInstanceName="lbTransactionTypes"
            runat="server" Width="300" Height="210" ValueType="System.Int32" OnCallback="lbTransactionTypes_Callback"
            OnDataBinding="lbTransactionTypes_DataBinding">
            <Columns>
                <dxe:ListBoxColumn FieldName="title" Caption="ชื่อ" Width="100%" />
            </Columns>
            <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
        </dxe:ASPxListBox>
        <table style="width: 100%" cellspacing="0" cellpadding="4">
            <tr>
                <td align="right">
                    <dxe:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close">
                        <ClientSideEvents Click="function(s, e){ ComboTransactionType.HideDropDown(); }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </DropDownWindowTemplate>
    <ClientSideEvents TextChanged="SynchronizeListBoxValues" DropDown="SynchronizeListBoxValues"
        Init="SynchronizeListBoxValues" />
</dxe:ASPxDropDownEdit>