<%@ Control Language="C#" AutoEventWireup="true" Inherits="ctrls_time" Codebehind="time.ascx.cs" %>
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
    <tr width="170px">
        <td>
            <dateControl:DateTimeControl ID="dateCtrl" ClientInstanceName="dateCtrl" runat="server"
                WidthDTC="86">
            </dateControl:DateTimeControl>
            <%-- <dxe:ASPxDateEdit ID="dateCtrl" ClientInstanceName="dateCtrl" runat="server" Width="92px" >
            </dxe:ASPxDateEdit>--%>
        </td>
        <td>
            <dxe:ASPxSpinEdit ID="Hours" runat="server" Height="21px" Number="0" Width="42px"
                MaxValue="23" MinValue="0" DisplayFormatString="00">
            </dxe:ASPxSpinEdit>
        </td>
        <td>
            <dxe:ASPxSpinEdit ID="Minute" runat="server" Height="21px" Number="0" Width="42px"
                MaxValue="59" MinValue="0" DisplayFormatString="00">
            </dxe:ASPxSpinEdit>
        </td>
        <td>
               &nbsp;
               <dxe:ASPxLabel ID="detail" ClientInstanceName="detail" runat="server" Text="">
               </dxe:ASPxLabel>
        </td>
 
    </tr>
</table>
<%-- <tr>
        <td>
            <table cellpadding="0px" cellspacing="0">
                <tr>
                    <td>
                        <%--<dxe:ASPxTextBox ID="Hours" ClientInstanceName="Hours" runat="server" Width="23px"
                            Font-Size="Smaller">
                            <ClientSideEvents Init="function(s,e)
                            {
                                Hours.SetText('00');
                                Hours.SetValue(0);
                            }" />
                            
                           <ClientSideEvents KeyDown="function(s,e)
                            {
                                //alert('KeyDown ' + e.returnValue );
                                //alert( e.keyCode );
                                //alert('KeyDown ' + e.data );
                                //var a = e.returnValue;
                                //var b = e.keyCode;
                                //alert(a + ' ' + b);
                                //if(checkNumber(a))
                                //    Hours.SetValue(a);
                            }" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <table cellpadding="0px" cellspacing="0">
                            <tr>
                                <td>
                                    <%--<dxe:ASPxImage ID="addHours" ImageUrl="plus.gif" ClientInstanceName="addHours" runat="server">
                                        <ClientSideEvents Click="function(s,e)
                                    {
                                        var a = Hours.GetValue()
                                        var b = 1 + a*1 ;
                                        //Hours.SetValue( b );
                                        Hours.SetValue( checkHours( b ) );
                                    }" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--<dxe:ASPxImage ID="ASPxImage1" ImageUrl="minus.gif" ClientInstanceName="addHours"
                                        runat="server">
                                        <ClientSideEvents Click="function(s,e)
                                        {
                                            var a = Hours.GetValue()
                                            var b = (-1) + a*1 ;
                                            //Hours.SetValue( b );
                                            Hours.SetValue( checkHours( b ) );
                                        }" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <table cellpadding="0px" cellspacing="0">
                <tr>
                    <td>
                        <%-- <dxe:ASPxTextBox ID="Minute" ClientInstanceName="Minute" runat="server" Width="23px"
                            Font-Size="Smaller">
                            <%--<MaskSettings Mask="00" />
                            <ClientSideEvents Init="function(s,e)
                            {
                                Minute.SetText('00');
                                Minute.SetValue(0);
                            }" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <table cellpadding="0px" cellspacing="0">
                            <tr>
                                <td>
                                    <%--  <dxe:ASPxImage ID="ASPxImage2" ImageUrl="plus.gif" ClientInstanceName="addHours"
                                        runat="server">
                                        <ClientSideEvents Click="function(s,e)
                                        {
                                            var a = Minute.GetValue()
                                            var b = 1 + a*1 ;
                                            //Minute.SetValue( b );
                                            Minute.SetValue( checkMinute( b ) );
                                        }" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%-- <dxe:ASPxImage ID="ASPxImage3" ImageUrl="minus.gif" ClientInstanceName="addHours"
                                        runat="server">
                                        <ClientSideEvents Click="function(s,e)
                                        {
                                            var a = Minute.GetValue()
                                            var b = (-1) + a*1 ;
                                            //Minute.SetValue( b );
                                            Minute.SetValue( checkMinute( b ) );
                                        }" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>--%>
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
