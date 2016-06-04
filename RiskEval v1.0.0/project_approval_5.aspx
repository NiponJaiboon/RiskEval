<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="project_approval_5.aspx.cs" Inherits="project_approval_5" Culture="th-TH"
    UICulture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" runat="Server">
    รายงานการวิเคราะห์ความเสี่ยงฯ ที่ผ่านการพิจารณาจากรัฐสภา
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="accountInfo">
        <fieldset class="login">
            <legend class="bold1">ข้อมูลโครงการ</legend>
            <table style="margin: 0px 0px 0px 0px; width: 100%">
                <tr>
                    <td class="bold1">
                        รหัสกระทรวง:
                    </td>
                    <td>
                        <asp:Label ID="lblDeptCode" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        ชื่อกระทรวง:
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
                        ชื่อหน่วยงาน:
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
                    <td class="bold1">
                        ยุทธศาสตร์การจัดสรรงบประมาณ :
                    </td>
                    <td colspan="3">
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
                        งบประมาณ(บาท):
                    </td>
                    <td>
                        <asp:Label ID="lblBudget" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td colspan="2" class="bold1">
                        โครงการบูรณาการที่สำนักงบประมาณกำหนด:
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblIntegrateProject" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td colspan="2" class="bold1">
                        หน่วยงานที่เกี่ยงข้องกับการบูรณาการ:
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblRelateDept" runat="server"></asp:Label>
                        <asp:SqlDataSource ID="sds_project_summary" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
                            SelectCommand="SELECT d.d_code, d.d_name, m.mi_code, m.mi_name, p.pj_code, p.d_id, p.pj_name, y.yut_name, p.pj_year, p.pj_budget, p.pj_integrateProject, p.pj_relateDept, p.pj_background, p.pj_urgency, p.pj_type, p.pj_approval_status,pj_doc_number,pj_date_doc_submitted  FROM projects AS p INNER JOIN department AS d ON p.d_id = d.d_id INNER JOIN ministry AS m ON p.mi_id = m.mi_id INNER JOIN yutasad AS y ON p.pj_yut_id = y.yut_id WHERE (p.pj_id = @pj_id)">
                            <SelectParameters>
                                <asp:Parameter Name="pj_id" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="bold1">
                        เลขหนังสือนำส่ง
                    </td>
                    <td>
                        <asp:Label ID="lbl_pj_doc_no" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        วันที่นำส่ง
                    </td>
                    <td>
                        <asp:Label ID="lbl_pj_date_doc_submitted" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div class="submit-box">
        <p>
            <asp:HyperLink ID="linkreport1" runat="server" Text="รายงานที่ 1 รายงานการกลั่นกรองโครงการ"
                NavigateUrl="project_report.aspx?reportid=3"></asp:HyperLink></p>
        <p>
            <asp:HyperLink ID="linkreport2" runat="server" Text="รายงานที่ 2 รายงานการวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล"
                NavigateUrl="project_report.aspx?reportid=6"></asp:HyperLink></p>
        <p>
            <asp:HyperLink ID="linkreport3" runat="server" Text="รายงานที่ 3 รายงานการวิเคราะห์ความเสี่ยงด้านสภาพแวดล้อมภายในและภายนอก"
                NavigateUrl="project_report.aspx?reportid=2"></asp:HyperLink></p>
        <p>
            <asp:HyperLink ID="HyperLink1" runat="server" Text="รายงานที่ 4 รายงานสรุปผลการประเมินการวิเคราะห์ความเสี่ยงตามหลักธรรมภิบาล"
                NavigateUrl="project_report.aspx?reportid=7"></asp:HyperLink></p>
        <p>
            <asp:HyperLink ID="HyperLink2" runat="server" Text="รายงานที่ 5 รายงานความคิดเห็นเพิ่มเติมจากเจ้าหน้าที่สำนักงบประมาณ"
                NavigateUrl="project_report.aspx?reportid=8"></asp:HyperLink></p>
        <p>
            <asp:HyperLink ID="HyperLink3" runat="server" Text="รายงานที่ 6 รายงานการพิจารณาจากรัฐสภา"
                NavigateUrl="project_report.aspx?reportid=9"></asp:HyperLink></p>
    </div>
    <asp:SqlDataSource ID="sds_project_approve_final" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
        UpdateCommand="UPDATE projects SET pj_approval_status = @pj_approval_status, pj_approval_budget = @pj_approval_budget, pj_lastupdate = GETDATE() WHERE (pj_id = @pj_id)"
        SelectCommand="SELECT pj_id, pj_approval_status, pj_approval_comment, pj_approval_comment_1_1, pj_approval_budget FROM projects WHERE (pj_id = @pj_id)">
        <SelectParameters>
            <asp:Parameter Name="pj_id" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="pj_id" />
            <asp:Parameter Name="pj_approval_status" />
            <asp:Parameter Name="pj_approval_budget" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
