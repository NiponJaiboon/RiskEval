<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="project_filter.aspx.cs" Inherits="project_filter" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ กลั่นกรองโครงการ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="Server">
    กลั่นกรองโครงการ
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
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
                    <!--<tr>
                        <td colspan="2" class="bold1">โครงการบูรณาการที่สำนักงบประมาณกำหนด:</td>
                        <td colspan="2"><asp:Label ID="lblIntegrateProject" runat="server"></asp:Label></td>
                    </tr>
                        <tr>
                            <td colspan="2" class="bold1">หน่วยงานที่เกี่ยงข้องกับการบูรณาการ:</td>
                            <td colspan="2"><asp:Label ID="lblRelateDept" runat="server"></asp:Label></td>
                    </tr>-->
                </table>
            </fieldset>
        </div>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
        </asp:SqlDataSource>
        <div class="title">
            ระบุข้อมูลรายละเอียดโครงการ</div>
        <div style="margin: 10px 10px 10px 10px">
            <table style="border: 1px solid black;">
                <tr>
                    <td style="border: 1px solid black; width: 5%">
                        1.
                    </td>
                    <td style="border: 1px solid black;">
                        เป็นแผนงาน/โครงการใหม่ ที่เพิ่งเริ่มต้นในปีงบประมาณนั้นๆ <span style="font-weight: bold;
                            text-decoration: underline;">หรือ</span> เป็นแผนงาน/โครงการที่ ใหม่ที่ต้องการขยายผลต่อจากโครงการที่ทำเสร็จไปแล้ว
                    </td>
                    <td style="border: 1px solid black; width: 20%">
                        <asp:RequiredFieldValidator ID="reqQ1" runat="server" Text="กรุณาเลือกใช่ หรือ ไม่ใช่"
                            ErrorMessage="กรุณาเลือกใช่ หรือ ไม่ใช่" ControlToValidate="radQ1" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                        <asp:RadioButtonList ID="radQ1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="ใช่">ใช่</asp:ListItem>
                            <asp:ListItem Value="ไม่ใช่">ไม่ใช่</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid black; width: 5%">
                        2.
                    </td>
                    <td style="border: 1px solid black;">
                        เป็นแผนงาน/โครงการที่มีลักษณะจัดเป็น<span style="font-weight: bold; text-decoration: underline;">รายจ่ายลงทุน</span>
                    </td>
                    <td style="border: 1px solid black; width: 20%">
                        <asp:RequiredFieldValidator ID="reqQ2" runat="server" Text="กรุณาเลือกใช่ หรือ ไม่ใช่"
                            ErrorMessage="กรุณาเลือกใช่ หรือ ไม่ใช่" ControlToValidate="radQ2" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                        <asp:RadioButtonList ID="radQ2" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="ใช่">ใช่</asp:ListItem>
                            <asp:ListItem Value="ไม่ใช่">ไม่ใช่</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid black; width: 5%">
                        3.
                    </td>
                    <td style="border: 1px solid black;">
                        เป็นแผนงาน/โครงการที่มติคณะรัฐมนตรีล่าสุดเมื่อวันที่ 29 ธันวาคม 2552 ได้กำหนดให้เป็นแผนงาน/โครงการที่มีความสำคัญทางนโยบายของรัฐบาล
                        ดังนี้<br />
                        <p style="margin-left: 15px">
                            ก. เป็นแผนงาน/โครงการ ที่มีผลกระทบต่อความสำเร็จในการบรรลุผลตามเป้าหมายกระทรวง ส่วนราชการ/
                            รัฐวิสาหกิจภายใต้กระทรวง กลุ่มจังหวัด หรือจังหวัด <span style="font-weight: bold;
                                text-decoration: underline;">และ</span><br />
                            ข. เป็นแผนงาน/โครงการ ที่มีผลกระทบต่อชีวิตความเป็นอยู่ของประชาชน สิ่งแวดล้อมหรือการให้บริการขั้นพื้นฐานของประชาชน
                            <span style="font-weight: bold; text-decoration: underline;">และ</span><br />
                            ค. เป็นแผนงาน/โครงการ ที่ใช้งบประมาณสูงของส่วนราชการ /รัฐวิสาหกิจ หน่วยงานอื่นของรัฐ
                            กลุ่มจังหวัด จังหวัด
                        </p>
                    </td>
                    <td style="border: 1px solid black; width: 20%">
                        <asp:RequiredFieldValidator ID="reqQ3" runat="server" Text="กรุณาเลือกใช่ หรือ ไม่ใช่"
                            ErrorMessage="กรุณาเลือกใช่ หรือ ไม่ใช่" ControlToValidate="radQ3" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                        <asp:RadioButtonList ID="radQ3" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="ใช่">ใช่</asp:ListItem>
                            <asp:ListItem Value="ไม่ใช่">ไม่ใช่</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid black; width: 5%">
                        4.
                    </td>
                    <td style="border: 1px solid black;">
                        เป็นแผนงาน/โครงการอื่นที่ส่วนราชการ/รัฐวิสาหกิจหรือผู้ตรวจราชการสำนักนายกรัฐมนตรีหรือผู้ตรวจราชการกระทรวง
                        หรือสำนักงบประมาณเห็นควรให้มีการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
                    </td>
                    <td style="border: 1px solid black; width: 20%">
                        <asp:RequiredFieldValidator ID="reqQ4" runat="server" Text="กรุณาเลือกใช่ หรือ ไม่ใช่"
                            ErrorMessage="กรุณาเลือกใช่ หรือ ไม่ใช่" ControlToValidate="radQ4" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                        <asp:RadioButtonList ID="radQ4" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="ใช่">ใช่</asp:ListItem>
                            <asp:ListItem Value="ไม่ใช่">ไม่ใช่</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
        <div style="color: White">
            <asp:Literal ID="lblResult" runat="server"></asp:Literal>
        </div>
        <div>
            <asp:Label ID="lblPrintWarning" runat="server" Text="กรณีที่ไม่ผ่านการประเมิน โปรดพิมพ์รายงานนี้ก่อนดำเนินการต่อ" Visible="false"></asp:Label>
            <asp:Button ID="btnPrint" runat="server" Text="Print เพื่อเป็นหลักฐาน" Visible="false" OnClientClick="return window.print();" />
        </div>
        <div>
            <asp:Button ID="btnNext" Visible="true" runat="server" Text="บันทึกข้อมูลและดำเนินการต่อไป" OnClick="btnNext_Click" OnClientClick="return confirm('ต้องการบันทึกและดำเนินการต่อไป');" />
            <asp:Button ID="btnNextPage" runat="server" Text="โปรดดำเนินงานต่อ" Visible="false" OnClick="btnNextPage_Click" />
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnUpdating="SqlDataSource1_Updating"
        ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" UpdateCommand="update [projects] set pj_filter_q1 = @pj_filter_q1, pj_filter_q2 = @pj_filter_q2, pj_filter_q3 = @pj_filter_q3, pj_filter_q4 = @pj_filter_q4 where [pj_id] = @pj_id">
    </asp:SqlDataSource>
    <div>
    </div>
</asp:Content>
