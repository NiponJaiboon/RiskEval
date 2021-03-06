<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ctrls_TimeHourMinuteControl" Codebehind="TimeHourMinuteControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="dateControl" %>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td>
            <dxe:ASPxSpinEdit ID="Hours" runat="server" Height="21px" Number="0" Width="50px"
                MaxValue="23" MinValue="0" DisplayFormatString="00">
            </dxe:ASPxSpinEdit>
        </td>
        <td>
            <dxe:ASPxSpinEdit ID="Minute" runat="server" Height="21px" Number="0" Width="50px"
                MaxValue="59" MinValue="0" DisplayFormatString="00">
            </dxe:ASPxSpinEdit>
        </td>
        <td>
            &nbsp;
            <dxe:ASPxLabel ID="detail" ClientInstanceName="detail" runat="server" Text="hh:mm">
            </dxe:ASPxLabel>
        </td>
    </tr>
</table>
<script type="text/javascript">
    function checkHours(hours) {
        if (hours > 23)
            return 23;
        else if (hours < 0)
            return 0;
        else
            return hours;
    }
    function checkMinute(minute) {
        if (minute > 59)
            return 59;
        else if (minute < 0)
            return 0;
        else
            return minute;
    }
    function checkNumber(val) {
        if (val == null) {
            return false;
        }
        if (val.length == 0) {
            return false;
        }
        for (var i = 0; i < val.length; i++) {
            if (ch < "0" || ch > "9")
                return false;
        }
        return true
    }
</script>
