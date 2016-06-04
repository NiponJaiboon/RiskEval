<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="project_summary.aspx.cs" Inherits="project_summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="linkheader" runat="Server">
    <script src="js/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.datepicker-th.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.button.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.position.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.resizable.js" type="text/javascript"></script>
    <script src="js/ui/jquery.ui.dialog.js" type="text/javascript"></script>
    <script src="js/ui/jquery.effects.core.js" type="text/javascript"></script>
    <script src="js/ui/jquery-ui-1.8.10.offset.datepicker.min.js" type="text/javascript"></script>
    <link href="css/layout.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="css/productiondropdown.css" rel="stylesheet" type="text/css" />
    <link href="css/tooltips.css" rel="stylesheet" type="text/css" />
    <link href="css/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function getAbsolutePath() {
            var loc = window.location;
            var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
            return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
        }

        function checkLength(o, n, min, max) {
            if (o.val().length > max || o.val().length < min) {
                o.addClass("ui-state-error");
                return "กรุณากรอก" + n + " ";
            }
            else {
                o.removeClass("ui-state-error");
                return "";
            }
        }

        $(function () {
            $("#dialog:ui-dialog").dialog("destroy");
            $("#createuser")
			.button()
		    .click(function () {
		        $("#dialog-form").css("visibility", "visible");
		        $("#dialog-form").dialog("open");
		        $("#datepicker").datepicker({ isBuddhist: true });
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

		        $("#dialog-form").dialog({
		            resizable: true,
		            height: 200,
		            width: 500,
		            modal: true,
		            buttons: {
		                "ยืนยันการส่งผล": function () {
		                    var bValid = checkLength($("#txtDocNo"), "เลขหนังสือนำส่ง", 1, 15);
		                    bValid += checkLength($("#datepicker"), "วันที่นำส่ง", 1, 10);
		                    if (bValid.length > 0) {
		                        alert(bValid);
		                    }
		                    else {
		                        var datedoctext = $("#datepicker").val();
		                        var dateyear = datedoctext.substr(datedoctext.length - 4, 4);
		                        var dateday = datedoctext.substr(0, 2);
		                        var datemonth = datedoctext.substr(3, 2);
		                        //datedoctext = datedoctext.replace(dateyear, dateyear - 543);
		                        dateyear = dateyear - 543;
		                        datedoctext = dateyear + '-' + datemonth + '-' + dateday;

		                        $.get("detailsPage.aspx", { docno: $("#txtDocNo").val(), datedoc: datedoctext },
                                   function (data) {
                                       //alert("Data Loaded: " + data);
                                       $(this).dialog("close");
                                       window.location.replace(getAbsolutePath() + "project_summary_submitted.aspx");
                                   })
                                   .error(function (x, e) {
                                       alert(x.status);
                                   }, "json")
		                    }
		                },
		                "ยกเลิก": function () {
		                    $(this).dialog("close");

		                }
		            }
		        });

		        return false;
		    });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="Server">
    รายงานวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล (กรณีที่ยังไม่ส่งผล)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="accountInfo">
        <fieldset class="login">
            <legend class="bold1">ข้อมูลโครงการ</legend>
            <table style="margin: 0px 0px 0px 0px;">
                <tr>
                    <td class="bold1">
                        รหัสกระทรวง:
                    </td>
                    <td>
                        <asp:Label ID="lblDeptCode" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        กระทรวง:
                    </td>
                    <td>
                        <asp:Label ID="lblDeptName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bold1">
                        รหัสหน่วยงาน:
                    </td>
                    <td>
                        <asp:Label ID="lblDivisionCode" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        หน่วยงาน:
                    </td>
                    <td>
                        <asp:Label ID="lblDivisionName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bold1">
                        รหัสโครงการ:
                    </td>
                    <td>
                        <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        ชื่อโครงการ:
                    </td>
                    <td>
                        <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="bold1">
                        ยุทธศาสตร์การจัดสรรงบประมาณ:
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblYutasard" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bold1">
                        ปีงบประมาณ:
                    </td>
                    <td>
                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        วงเงินงบประมาณทั้งสิ้น (บาท):
                    </td>
                    <td>
                        <asp:Label ID="lblBudget" runat="server"></asp:Label>
                    </td>
                </tr>
                <!-- <tr>
                    <td colspan="2" class="bold1">โครงการบูรณาการที่สำนักงบประมาณกำหนด:</td>
                    <td colspan="2"><asp:Label ID="lblIntegrateProject" runat="server"></asp:Label></td>
                </tr>
                    <tr>
                        <td colspan="2" class="bold1">หน่วยงานที่เกี่ยงข้องกับการบูรณาการ:</td>
                        <td colspan="2"><asp:Label ID="lblRelateDept" runat="server"></asp:Label></td>
                </tr> -->
            </table>
        </fieldset>
    </div>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
    </asp:SqlDataSource>
    <div style="margin-top: 15px">
        <p>
            <asp:HyperLink ID="linkreport1" runat="server" Text="รายงานที่ 1 รายงานการกลั่นกรองโครงการ"
                NavigateUrl="project_report.aspx?reportid=3"></asp:HyperLink></p>
        <p>
            <asp:HyperLink ID="linkreport2" runat="server" Text="รายงานที่ 2 รายงานการวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล"
                NavigateUrl="project_report.aspx?reportid=5"></asp:HyperLink></p>
        <p>
            <asp:HyperLink ID="linkreport3" runat="server" Text="รายงานที่ 3 รายงานการวิเคราะห์ความเสี่ยงด้านสภาพแวดล้อมภายในและภายนอก"
                NavigateUrl="project_report.aspx?reportid=2"></asp:HyperLink></p>
        <div style="margin-top: 15px">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnEditProject" runat="server" OnClick="btnEditProject_Click" Text="แก้ไขข้อมูลวิเคราะห์ความเสี่ยง"
                            CssClass="ui-button" />
                    </td>
                    <td>
                        <asp:Panel ID="pnlsubmit" runat="server">
                            <div id="hSubmit">
                                <button id="createuser" class="ui-button ui-button-text">
                                    ส่งผลโครงการวิเคราะห์ความเสี่ยง</button>
                            </div>
                        </asp:Panel>
                        <script language="javascript">
                            var pj_type = '<%=pj_type %>';
                            if (pj_type == "sim") {
                                $('#hSubmit').hide();
                            } else {
                                $('#hSubmit').show();
                            }
                        </script>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSubmitProject" runat="server" OnClick="btnSubmitProject_Click"
                Text="ส่งผลโครงการการวิเคราะห์ความเสี่ยง" Visible="false" />
        </div>
    </div>
    <div id="dialog-form" title="กรุณากรอกข้อมูล" style="visibility: hidden;" class="ui-dialog">
        <table>
            <tr>
                <td style="text-align: left">
                    <label for="name">เลขหนังสือนำส่ง</label>
                </td>
                <td style="text-align: left">
                    <input type="text" name="txtDocNo" id="txtDocNo" maxlength="15" size="30" style="font-size: 18px; font-family: CordiaUPC,DB ThaiText,Arials" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <label for="name">วันที่นำส่ง</label>
                </td>
                <td style="text-align: left">
                    <input type="text" id="datepicker" name="datepicker" size="20" style="font-size: 18px; font-family: CordiaUPC,DB ThaiText,Arials" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
