<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailsPage.aspx.cs" Inherits="DetailsPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>

    <!--
    <link rel="stylesheet" href="css/base/jquery.ui.all.css">
    <script src="js/jquery-1.7.1.js" type="text/javascript"></script>
	<script src="js/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.datepicker-th.js" type="text/javascript"></script>
    -->

    <link rel="stylesheet" href="css/base/jquery.ui.all.css" />
    <script src="js/ui/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="js/ui/jquery-ui-1.8.10.offset.datepicker.min.js" type="text/javascript"></script>

		<script type="text/javascript">
		    $(function () {
		        var d = new Date();
		        var toDay = d.getDate() + '/'
        + (d.getMonth() + 1) + '/'
        + (d.getFullYear() + 543);

		        // Datepicker
		        $("#datepicker").datepicker({ dateFormat: 'dd/mm/yy', isBuddhist: true, defaultDate: toDay, dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
		            dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
		            monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
		            monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
		            changeMonth: true,
		            changeYear: true
		        });

		    });


		    //	$(function() {
		    //	    $("#datepicker").datepicker();
		    ////		$("#format").change(function () {
		    ////			$( "#datepicker" ).datepicker( "option", "dateFormat", $( this ).val() );
		    // //		});
		    //	});

		</script>
		<style type="text/css">
			body 
			{
			    font-family:CordiaUPC,"DB ThaiText","Arials";
                font-size:18px;
			    margin: 50px;
			}
			.demoHeaders { margin-top: 2em; }
			#dialog_link {padding: .4em 1em .4em 20px; text-decoration: none; position: relative;}
			#dialog_link span.ui-icon {margin: 0 5px 0 0; position: absolute;left: .2em; top: 50%; margin-top: -8px;}
			ul#icons {margin: 0; padding: 0;}
			ul#icons li {margin: 2px; position: relative; padding: 4px 0; cursor: pointer; float: left;  list-style: none;}
			ul#icons span.ui-icon {float: left; margin: 0 4px;}
			ul.test {list-style:none; line-height:30px;}
		</style>	
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
            <table>
                <tr>
                    <td>เลขหนังสือนำส่ง</td>
                    <td>
                        <asp:TextBox ID="txtDocNo" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator 
                        ID="req1" 
                        runat="server"
                        ControlToValidate="txtDocNo"
                         ErrorMessage="*"
                          Text="*">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>วันที่นำส่ง</td>
                    <td><input type="text" id="datepicker" size="30" runat="server" style="width:200px"/>
                         <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" 
                        runat="server"
                        ControlToValidate="datepicker"
                         ErrorMessage="*"
                          Text="*">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            
            </table>

            <asp:Button ID="btnSubmit" runat="server"  Text="ยืนยันการส่งผล" 
                onclick="btnSubmit_Click"/>

    </div>
    </form>
</body>
</html>
