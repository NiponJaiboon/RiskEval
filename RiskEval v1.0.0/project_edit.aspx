<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="project_edit.aspx.cs" Inherits="project_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="Server">
    เแก้ไขข้อมูลโครงการการวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="accountInfo">
        <fieldset class="login">
            <legend class="bold1">ข้อมูลโครงการ</legend>
            <table style="margin: 0px 0px 0px 0px; width: 800px;">
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
    <div>
        <p class="title">แก้ไขข้อมูลรายละเอียดโครงการ</p>
        <div style="margin-left: 25px">
            <asp:HyperLink ID="linkProjectDetails" runat="server" NavigateUrl="~/project_details_edit.aspx" Text="ข้อมูลรายละเอียดโครงการ"></asp:HyperLink><br />
            <asp:HyperLink ID="linkProjectBasicInfo" runat="server" NavigateUrl="~/project_basicinfo_edit.aspx" Text="แก้ไขข้อมูลพื้นฐานโครงการ"></asp:HyperLink><br />
            <asp:HyperLink ID="linkQuestionA" runat="server" NavigateUrl="~/question_set_A_edit.aspx" Text="แก้ไขคำถามชุด ก: ขั้นตอนการริเริ่มแผนงาน/โครงการและวิเคราะห์เบื้องต้น"></asp:HyperLink><br />
            <asp:HyperLink ID="linkQuestionB" runat="server" NavigateUrl="~/question_set_B_edit.aspx" Text="แก้ไขคำถามชุด ข: การวิเคราะห์และวางแผนรายละเอียดโครงการ"></asp:HyperLink><br />
            <asp:HyperLink ID="linkQuestionC" runat="server" NavigateUrl="~/question_set_C_edit.aspx" Text="แก้ไขคำถามชุด ค: การจัดลำดับและจัดสรรงบประมาณโครงการ  "></asp:HyperLink><br />
            <asp:HyperLink ID="linkQuestionD" runat="server" NavigateUrl="~/question_set_D_edit.aspx" Text="แก้ไขคำถามชุด ง: การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ"></asp:HyperLink><br />
            <asp:HyperLink ID="linkQuestionE" runat="server" NavigateUrl="~/question_set_E_edit.aspx" Text="แก้ไขคำถามชุด จ: การประเมินผลการดำเนินงานของโครงการต่อเนื่องและโครงการที่ทำเสร็จแล้วและต้องการขยายผลโครงการ"></asp:HyperLink><br />
            <div class="qGroup">
                <p class="qTitle">การวิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก</p>
                <asp:HyperLink ID="linkFactorRisk1" runat="server" NavigateUrl="~/factor_risk.aspx?mode=edit&p=1" Text="แก้ไขคำถามปัจจัยเสี่ยงข้อที่ 1"></asp:HyperLink><br />
                <asp:HyperLink ID="linkFactorRisk2" runat="server" NavigateUrl="~/factor_risk.aspx?mode=edit&p=2" Text="แก้ไขคำถามปัจจัยเสี่ยงข้อที่ 2"></asp:HyperLink><br />
                <asp:HyperLink ID="linkFactorRisk3" runat="server" NavigateUrl="~/factor_risk.aspx?mode=edit&p=3" Text="แก้ไขคำถามปัจจัยเสี่ยงข้อที่ 3"></asp:HyperLink><br />
                <asp:HyperLink ID="linkFactorRisk4" runat="server" NavigateUrl="~/factor_risk.aspx?mode=edit&p=4" Text="แก้ไขคำถามปัจจัยเสี่ยงข้อที่ 4"></asp:HyperLink><br />
                <asp:HyperLink ID="linkFactorRisk5" runat="server" NavigateUrl="~/factor_risk.aspx?mode=edit&p=5" Text="แก้ไขคำถามปัจจัยเสี่ยงข้อที่ 5"></asp:HyperLink><br />
                <asp:HyperLink ID="linkFactorRisk6" runat="server" NavigateUrl="~/factor_risk.aspx?mode=edit&p=6" Text="แก้ไขคำถามปัจจัยเสี่ยงข้อที่ 6"></asp:HyperLink><br />
            </div>
        </div>
    </div>
    <div class="question-button">
        <asp:Button ID="btnRedirect" runat="server" Text="บันทึกการแก้ไขข้อมูล" OnClick="btnRedirect_Click" />
    </div>
</asp:Content>
